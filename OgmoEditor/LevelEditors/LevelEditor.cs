﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using OgmoEditor.LevelData;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelEditors.Actions;
using OgmoEditor.LevelEditors.Tools;

namespace OgmoEditor.LevelEditors
{
    public class LevelEditor : Control
    {
        static private readonly Brush NoFocusBrush = new SolidBrush(Color.FromArgb(80, 220, 220, 220));
        static private readonly Brush ShadowBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0));
        static private readonly Pen GridBorderPen = new Pen(Color.Black, 2);
        private const int LAYER_ABOVE_ALPHA = 130;

        private enum MouseMode { Normal, Pan, Camera };

        private MouseMode mouseMode = MouseMode.Normal;
        private bool mousePanMode;
        private Point lastMousePoint;

        public Level Level { get; private set; }
        public LevelView LevelView { get; private set; }
        public Graphics Graphics { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public new Point MousePosition { get; private set; }        

        public LinkedList<OgmoAction> UndoStack { get; private set; }
        public LinkedList<OgmoAction> RedoStack { get; private set; }

        private EventHandler Repaint;
        private ActionBatch batch;
        private Brush levelBGBrush;
        private Pen gridPen;

        public LevelEditor(Level level)
            : base()
        {
            Level = level;
            Graphics = CreateGraphics();
            Dock = System.Windows.Forms.DockStyle.Fill;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            LevelView = new LevelView(this);

            //Create the undo/redo stacks
            UndoStack = new LinkedList<OgmoAction>();
            RedoStack = new LinkedList<OgmoAction>();

            //Create the layer editors
            LayerEditors = new List<LayerEditor>();
            foreach (var l in level.Layers)
                LayerEditors.Add(l.GetEditor(this));

            //Init the level BG brush
            BackgroundImage = Ogmo.NewEditorDraw.ImgBG;
            levelBGBrush = new SolidBrush(Ogmo.Project.BackgroundColor);
            gridPen = new Pen(Ogmo.Project.GridColor);

            //Events
            Repaint = delegate { Invalidate(); };
            Application.Idle += Repaint;
            this.Paint += Draw;
            this.Resize += onResize;
            this.MouseClick += onMouseClick;
            this.MouseDown += onMouseDown;
            this.MouseUp += onMouseUp;
            this.MouseMove += onMouseMove;
            this.MouseWheel += onMouseWheel;
            this.KeyDown += onKeyDown;
            this.KeyUp += onKeyUp;
        }

        public void OnRemove()
        {
            //Remove external events
            Application.Idle -= Repaint;
        }

        private void Draw(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Draw the background logo
            e.Graphics.Transform = LevelView.Identity;
            e.Graphics.DrawImage(Ogmo.NewEditorDraw.ImgLogo, new Rectangle(Width/2 - Ogmo.NewEditorDraw.ImgLogo.Width/4, Height/2 - Ogmo.NewEditorDraw.ImgLogo.Height/4, Ogmo.NewEditorDraw.ImgLogo.Width/2, Ogmo.NewEditorDraw.ImgLogo.Height/2));
            if (!Focused)
                e.Graphics.FillRectangle(NoFocusBrush, new Rectangle(0, 0, Width, Height));

            //Draw the level bg
            e.Graphics.Transform = LevelView.Matrix;
            e.Graphics.FillRectangle(ShadowBrush, new Rectangle(10, 10, Level.Size.Width, Level.Size.Height));
            e.Graphics.FillRectangle(levelBGBrush, new Rectangle(0, 0, Level.Size.Width, Level.Size.Height));

            //Layers below the current one
            int i;
            for (i = 0; i < Ogmo.LayersWindow.CurrentLayerIndex; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].NewDraw(e.Graphics, false, 255);
            }

            //Current layer
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].NewDraw(e.Graphics, true, 255);

            //Layers above the current one
            for (; i < LayerEditors.Count; i++)
            {
                if (i < Ogmo.Project.LayerDefinitions.Count && Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].NewDraw(e.Graphics, false, LAYER_ABOVE_ALPHA);
            }

            //Draw the grid
            if (Ogmo.MainWindow.EditingGridVisible)
            {
                e.Graphics.Transform = LevelView.Identity;

                PointF inc = new PointF(Ogmo.LayersWindow.CurrentLayer.Definition.Grid.Width * LevelView.Zoom, Ogmo.LayersWindow.CurrentLayer.Definition.Grid.Height * LevelView.Zoom);
                while (inc.X <= 4)
                    inc.X *= 2;
                while (inc.Y <= 4)
                    inc.Y *= 2;

                float width = Ogmo.CurrentLevel.Size.Width * LevelView.Zoom;
                float height = Ogmo.CurrentLevel.Size.Height * LevelView.Zoom;

                PointF offset = LevelView.EditorToScreen(LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].DrawOffset);

                for (float xx = inc.X; xx < width; xx += inc.X)
                    e.Graphics.DrawLine(gridPen, offset.X + xx, offset.Y, offset.X + xx, offset.Y + height);

                for (float yy = inc.Y; yy < height; yy += inc.Y)
                    e.Graphics.DrawLine(gridPen, offset.X, offset.Y + yy, offset.X + width, offset.Y + yy);

                e.Graphics.DrawRectangle(GridBorderPen, new Rectangle((int)offset.X, (int)offset.Y, (int)width + 1, (int)height));
            }

            /*
            EditorDraw content = Ogmo.EditorDraw;
            
            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
            content.SpriteBatch.Draw(content.TexBG, DrawBounds, new Rectangle(0, 0, DrawBounds.Width, DrawBounds.Height), this.Focused ? Color.White : NoFocus);
            content.SpriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, this.Focused ? Color.White : NoFocus, 0, new Vector2(content.TexLogo.Width / 2, content.TexLogo.Height / 2), .5f, SpriteEffects.None, 0);
            content.SpriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, LevelView.Matrix);
            content.DrawRectangle(10, 10, Level.Size.Width, Level.Size.Height, new Color(0, 0, 0, .5f));
            content.DrawRectangle(0, 0, Level.Size.Width, Level.Size.Height, Ogmo.Project.BackgroundColor.ToXNA());
            content.SpriteBatch.End();

            //Layers below the current one
            int i;
            for (i = 0; i < Ogmo.LayersWindow.CurrentLayerIndex; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].Draw(false, 1);
            }

            //Current layer
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].Draw(true, 1);

            //Layers above the current one
            for (; i < LayerEditors.Count; i++)
            {
                if (i < Ogmo.Project.LayerDefinitions.Count && Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].Draw(false, LAYER_ABOVE_ALPHA);
            }

            //Draw the grid
            if (Ogmo.MainWindow.EditingGridVisible)
            {
                content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null);

                Vector2 inc = new Vector2(Ogmo.LayersWindow.CurrentLayer.Definition.Grid.Width * LevelView.Zoom, Ogmo.LayersWindow.CurrentLayer.Definition.Grid.Height * LevelView.Zoom);
                while (inc.X <= 4)
                    inc.X *= 2;
                while (inc.Y <= 4)
                    inc.Y *= 2;

                float width = Ogmo.CurrentLevel.Size.Width * LevelView.Zoom;
                float height = Ogmo.CurrentLevel.Size.Height * LevelView.Zoom;

                Vector2 offset = ((-LevelView.Position + LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].DrawOffset) * LevelView.Zoom) + LevelView.Origin;

                for (float xx = inc.X; xx < width; xx += inc.X)
                    content.DrawLine(offset.X + xx, offset.Y, offset.X + xx, offset.Y + height, Ogmo.Project.GridColor.ToXNA() * .5f);

                for (float yy = inc.Y; yy < height; yy += inc.Y)
                    content.DrawLine(offset.X, offset.Y + yy, offset.X + width, offset.Y + yy, Ogmo.Project.GridColor.ToXNA() * .5f);

                content.DrawHollowRect((int)offset.X, (int)offset.Y, (int)width + 1, (int)height, Color.Black);
                content.DrawHollowRect((int)offset.X - 1, (int)offset.Y - 1, (int)width + 3, (int)height + 2, Color.Black);

                content.SpriteBatch.End();
            }

            //Draw the camera
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, LevelView.Matrix);
            if (Ogmo.Project.CameraEnabled)
            {
                int w = Ogmo.Project.CameraSize.Width / 8;
                int h = Ogmo.Project.CameraSize.Height / 8;

                int x2 = Level.CameraPosition.X + Ogmo.Project.CameraSize.Width;
                int y2 = Level.CameraPosition.Y + Ogmo.Project.CameraSize.Height;

                content.DrawLine(Level.CameraPosition.X - 1, Level.CameraPosition.Y - 1, Level.CameraPosition.X + w, Level.CameraPosition.Y - 1, Color.Red);
                content.DrawLine(Level.CameraPosition.X, Level.CameraPosition.Y, Level.CameraPosition.X + w, Level.CameraPosition.Y, Color.Red);
                content.DrawLine(Level.CameraPosition.X - 1, Level.CameraPosition.Y - 1, Level.CameraPosition.X - 1, Level.CameraPosition.Y + h, Color.Red);
                content.DrawLine(Level.CameraPosition.X, Level.CameraPosition.Y, Level.CameraPosition.X, Level.CameraPosition.Y + h, Color.Red);

                content.DrawLine(x2 - w, Level.CameraPosition.Y - 1, x2 + 1, Level.CameraPosition.Y - 1, Color.Red);
                content.DrawLine(x2 - w, Level.CameraPosition.Y, x2, Level.CameraPosition.Y, Color.Red);
                content.DrawLine(x2 + 1, Level.CameraPosition.Y, x2 + 1, Level.CameraPosition.Y + h, Color.Red);
                content.DrawLine(x2, Level.CameraPosition.Y, x2, Level.CameraPosition.Y + h, Color.Red);

                content.DrawLine(Level.CameraPosition.X - 1, y2 + 1, Level.CameraPosition.X + w, y2 + 1, Color.Red);
                content.DrawLine(Level.CameraPosition.X - 1, y2, Level.CameraPosition.X + w, y2, Color.Red);
                content.DrawLine(Level.CameraPosition.X - 1, y2, Level.CameraPosition.X - 1, y2 - h, Color.Red);
                content.DrawLine(Level.CameraPosition.X, y2, Level.CameraPosition.X, y2 - h, Color.Red);

                content.DrawLine(x2 - w, y2 + 1, x2 + 1, y2 + 1, Color.Red);
                content.DrawLine(x2 - w, y2, x2, y2, Color.Red);
                content.DrawLine(x2 + 1, y2 - h, x2 + 1, y2 + 1, Color.Red);
                content.DrawLine(x2, y2 - h, x2, y2, Color.Red);
            }

            content.SpriteBatch.End();
            */
        }

        public void SaveAsImage()
        {
            //Get the path!
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Level as Image...";
            dialog.Filter = "PNG Image File|*.png";

            string file = Level.SaveName;
            int num = file.LastIndexOf(".oel");
            if (num != -1)
                file = file.Remove(num);
            file = file + ".png";
            dialog.FileName = file;
            dialog.InitialDirectory = Ogmo.Project.SavedDirectory;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            //Draw the level!
            /*
            float scale = Math.Min(Math.Min(4096.0f / Level.Size.Width, 1), Math.Min(4096.0f / Level.Size.Height, 1));
            int width = (int)(scale * Level.Size.Width);
            int height = (int)(scale * Level.Size.Height);
            Matrix scaleMatrix = Matrix.CreateScale(scale);

            RenderTarget2D texture = new RenderTarget2D(Ogmo.EditorDraw.GraphicsDevice, width, height);
            Ogmo.EditorDraw.GraphicsDevice.SetRenderTarget(texture);
            Ogmo.EditorDraw.GraphicsDevice.Clear(Ogmo.Project.BackgroundColor.ToXNA());

            for (int i = 0; i < LayerEditors.Count; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                {
                    Ogmo.EditorDraw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, LayerEditors[i].DrawMatrix * scaleMatrix);
                    LayerEditors[i].DrawLocal(false, 1);
                    Ogmo.EditorDraw.SpriteBatch.End();
                }
            }
            Ogmo.EditorDraw.GraphicsDevice.SetRenderTarget(null);

            //Save it then dispose it
            Stream stream = dialog.OpenFile();
            texture.SaveAsPng(stream, width, height);
            stream.Close();
            texture.Dispose();
             */
        }

        public void SaveCameraAsImage()
        {
            //Get the path!
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Level as Image...";
            dialog.Filter = "PNG Image File|*.png";
            dialog.InitialDirectory = Ogmo.Project.SavedDirectory;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            /*
            //Draw the level!
            float scale = Math.Min(Math.Min(4096.0f / Ogmo.Project.CameraSize.Width, 1), Math.Min(4096.0f / Ogmo.Project.CameraSize.Height, 1));
            int width = (int)(scale * Ogmo.Project.CameraSize.Width);
            int height = (int)(scale * Ogmo.Project.CameraSize.Height);
            Matrix cameraMatrix = Matrix.CreateScale(scale) * Matrix.CreateTranslation(-Level.CameraPosition.X, -Level.CameraPosition.Y, 0);

            RenderTarget2D texture = new RenderTarget2D(Ogmo.EditorDraw.GraphicsDevice, width, height);
            Ogmo.EditorDraw.GraphicsDevice.SetRenderTarget(texture);
            Ogmo.EditorDraw.GraphicsDevice.Clear(Ogmo.Project.BackgroundColor.ToXNA());

            for (int i = 0; i < LayerEditors.Count; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                {
                    Ogmo.EditorDraw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, LayerEditors[i].DrawMatrix * cameraMatrix);
                    LayerEditors[i].DrawLocal(false, 1);
                    Ogmo.EditorDraw.SpriteBatch.End();
                }
            }
            Ogmo.EditorDraw.GraphicsDevice.SetRenderTarget(null);

            //Save it then dispose it
            Stream stream = dialog.OpenFile();
            texture.SaveAsPng(stream, width, height);
            stream.Close();
            texture.Dispose();
             */
        }

        private void DrawLayer(LayerEditor layer, bool current, float alpha)
        {
            
            layer.DrawLocal(current, alpha);
            Ogmo.EditorDraw.SpriteBatch.End();
        }

        public void SwitchTo()
        {
            Focus();
            Ogmo.MainWindow.ZoomLabel.Text = LevelView.ZoomString;
        }

        /*
         *  ACTIONS API
         */
        public void Perform(OgmoAction action)
        {
            if (action == null)
                return;

            //If a batch is in progress, stop it!
            EndBatch();

            //If you're over the undo limit, chop off an action
            while (UndoStack.Count >= Properties.Settings.Default.UndoLimit)
                UndoStack.RemoveFirst();

            //If the level is so-far unchanged, change it and store that fact
            if (!Level.Changed)
            {
                action.LevelWasChanged = false;
                Level.Changed = true;
            }

            //Add the action to the undo stack and then do it!
            UndoStack.AddLast(action);  
            action.Do();

            //Clear the redo stack
            RedoStack.Clear();
        }

        /*
         *  Action batching so undo/redo affect a group of actions. Call StartBatch, BatchPerform..., and end with EndBatch
         */
        public void StartBatch()
        {
            batch = new ActionBatch();

            if (!Level.Changed)
            {
                batch.LevelWasChanged = false;
                Level.Changed = true;
            }

            while (UndoStack.Count >= Properties.Settings.Default.UndoLimit)
                UndoStack.RemoveFirst();
            UndoStack.AddLast(batch);
            RedoStack.Clear();
        }

        public void BatchPerform(OgmoAction action)
        {
            if (action == null)
                return;

            batch.Add(action);
            action.Do();
        }

        public void EndBatch()
        {
            batch = null;
        }

        public void Undo()
        {
            if (UndoStack.Count > 0)
            {
                //Remove it
                OgmoAction action = UndoStack.Last.Value;
                UndoStack.RemoveLast();

                //Undo it
                action.Undo();

                //Roll back level changed flag
                Level.Changed = action.LevelWasChanged;

                //Add it to the redo stack
                RedoStack.AddLast(action);
            }
        }

        public void Redo()
        {
            if (RedoStack.Count > 0)
            {
                //Remove it
                OgmoAction action = RedoStack.Last.Value;
                RedoStack.RemoveLast();

                //Redo it
                action.Do();

                //Mark level as changed
                Level.Changed = true;

                //Add it to the undo stack
                UndoStack.AddLast(action);
            }
        }

        public bool CanUndo
        {
            get { return UndoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get { return RedoStack.Count > 0; }
        }

        /*
         *  EVENTS
         */
        private void onResize(object sender, EventArgs e)
        {
            LevelView.OnParentResized();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (mouseMode == MouseMode.Normal && e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                mouseMode = MouseMode.Pan;
            }
            else if (mouseMode == MouseMode.Normal && e.KeyCode == System.Windows.Forms.Keys.C)
            {
                mouseMode = MouseMode.Camera;
            }
            else
            {
                //Call the layer event
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnKeyDown(e.KeyCode);
            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (mouseMode == MouseMode.Pan && e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                mouseMode = MouseMode.Normal;
                mousePanMode = false;
            }
            else if (mouseMode == MouseMode.Camera && e.KeyCode == System.Windows.Forms.Keys.C)
            {
                mouseMode = MouseMode.Normal;
                mousePanMode = false;
                if (!Util.Ctrl)
                    SnapCamera();
            }
            else
            {
                //Call the layer event
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnKeyUp(e.KeyCode);
            }
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            if (mouseMode != MouseMode.Normal)
                return;

            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftClick(LevelView.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightClick(LevelView.ScreenToEditor(e.Location));
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            if (mouseMode != MouseMode.Normal)
            {
                //Enter mouse move mode
                mousePanMode = true;
                lastMousePoint = e.Location;

                if (mouseMode == MouseMode.Camera)
                {
                    //Update the camera position
                    Level.CameraPosition = LevelView.ScreenToEditor(e.Location);
                    foreach (var ed in LayerEditors)
                        ed.UpdateDrawOffset(Level.CameraPosition);
                }
            }
            else
            {
                //Call the layer event
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftDown(LevelView.ScreenToEditor(e.Location));
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightDown(LevelView.ScreenToEditor(e.Location));
                else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    //Enter mouse move mode
                    mousePanMode = true;
                    lastMousePoint = e.Location;
                }
            }
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            if (mouseMode != MouseMode.Normal)
            {
                if (mouseMode == MouseMode.Camera && !Util.Ctrl)
                    SnapCamera();

                //Exit mouse move mode
                mousePanMode = false;
            }
            else
            {
                //Call the layer event
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftUp(LevelView.ScreenToEditor(e.Location));
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightUp(LevelView.ScreenToEditor(e.Location));
                else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    //Exit mouse move mode
                    mousePanMode = false;
                }
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            //Pan the camera if in move mode
            if (mousePanMode)
            {
                if (mouseMode == MouseMode.Camera)
                {
                    //Update the camera position
                    Level.CameraPosition = LevelView.ScreenToEditor(e.Location);
                    foreach (var ed in LayerEditors)
                        ed.UpdateDrawOffset(Level.CameraPosition);
                }
                else
                {
                    LevelView.Pan(new Point(e.Location.X - lastMousePoint.X, e.Location.Y - lastMousePoint.Y));
                    lastMousePoint = e.Location;
                }
            }

            //Update the mouse coord display
            MousePosition = LevelView.ScreenToEditor(e.Location);
            Point mouseDraw = Ogmo.Project.LayerDefinitions[Ogmo.LayersWindow.CurrentLayerIndex].SnapToGrid(MousePosition);
            Point gridPos = Ogmo.Project.LayerDefinitions[Ogmo.LayersWindow.CurrentLayerIndex].ConvertToGrid(MousePosition);
            Ogmo.MainWindow.MouseCoordinatesLabel.Text = "Mouse: ( " + mouseDraw.X.ToString() + ", " + mouseDraw.Y.ToString() + " )";
            Ogmo.MainWindow.GridCoordinatesLabel.Text = "Grid: ( " + gridPos.X.ToString() + ", " + gridPos.Y.ToString() + " )";

            //Call the layer event
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseMove(MousePosition);
        }

        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                LevelView.ZoomIn(e.Location);
            else
                LevelView.ZoomOut(e.Location);
            Ogmo.MainWindow.ZoomLabel.Text = LevelView.ZoomString;
        }

        private void SnapCamera()
        {
            Level.CameraPosition = Ogmo.LayersWindow.CurrentLayer.Definition.SnapToGrid(Level.CameraPosition);
            foreach (var ed in LayerEditors)
                ed.UpdateDrawOffset(Level.CameraPosition);
        }
    }
}
