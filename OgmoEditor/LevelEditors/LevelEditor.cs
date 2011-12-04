using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OgmoEditor.LevelEditors;
using OgmoEditor.XNA;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Windows.Forms;
using OgmoEditor.LevelData;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelEditors.Actions;
using OgmoEditor.LevelEditors.Tools;

namespace OgmoEditor.LevelEditors
{
    using Point = System.Drawing.Point;

    public class LevelEditor : GraphicsDeviceControl
    {
        static private readonly Color NoFocus = new Color(.95f, .95f, .95f);
        private const float LAYER_ABOVE_ALPHA = .5f;

        private enum MouseMode { Normal, Pan, Camera };

        private MouseMode mouseMode = MouseMode.Normal;
        private bool mousePanMode;
        private Point lastMousePoint;

        public Level Level { get; private set; }
        public LevelView LevelView { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public Rectangle DrawBounds { get; private set; }
        public new Point MousePosition { get; private set; }
        public Point CameraPosition { get; private set; }

        public LinkedList<OgmoAction> UndoStack { get; private set; }
        public LinkedList<OgmoAction> RedoStack { get; private set; }

        private EventHandler Repaint;
        private ActionBatch batch;

        public LevelEditor(Level level)
            : base()
        {
            Level = level;
            this.Size = level.Size;
            Dock = System.Windows.Forms.DockStyle.Fill;

            //Create the undo/redo stacks
            UndoStack = new LinkedList<OgmoAction>();
            RedoStack = new LinkedList<OgmoAction>();

            //Create the layer editors
            LayerEditors = new List<LayerEditor>();
            foreach (var l in level.Layers)
                LayerEditors.Add(l.GetEditor(this));
        }

        protected override void Initialize()
        {
            //Init the screen bounds and camera
            LevelView = new LevelView();
            LevelView.Origin = new Vector2(Width / 2, Height / 2);
            centerCamera();
            DrawBounds = new Rectangle(0, 0, Width, Height);

            //Events
            Repaint = delegate { Invalidate(); };
            Application.Idle += Repaint;
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

        private void centerCamera()
        {
            LevelView.X = Level.Size.Width / 2;
            LevelView.Y = Level.Size.Height / 2;
        }

        protected override void Draw()
        {
            Content content = Ogmo.Content;

            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
            content.SpriteBatch.Draw(content.TexBG, DrawBounds, new Rectangle(0, 0, DrawBounds.Width, DrawBounds.Height), this.Focused ? Color.White : NoFocus);
            content.SpriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, this.Focused ? Color.White : NoFocus, 0, new Vector2(content.TexLogo.Width / 2, content.TexLogo.Height / 2), .5f, SpriteEffects.None, 0);
            content.SpriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, LevelView.Matrix);
            content.DrawRectangle(10, 10, Level.Size.Width, Level.Size.Height, new Color(0, 0, 0, .5f));
            content.DrawRectangle(0, 0, Level.Size.Width, Level.Size.Height, Ogmo.Project.BackgroundColor.ToXNA());
            content.SpriteBatch.End();

            //Layers below the current one
            int i;
            for (i = 0; i < Ogmo.LayersWindow.CurrentLayerIndex; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                {
                    content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, LayerEditors[i].DrawMatrix * LevelView.Matrix);
                    LayerEditors[i].Draw(content, false, 1);
                    content.SpriteBatch.End();
                }
            }

            //Current layer, grid and border
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, LayerEditors
                [Ogmo.LayersWindow.CurrentLayerIndex].DrawMatrix * LevelView.Matrix);
            if (Ogmo.MainWindow.EditingGridVisible && LevelView.Zoom >= 1)
                content.DrawGrid(LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].Layer.Definition.Grid, Level.Size, Ogmo.Project.GridColor.ToXNA() * .5f);
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].Draw(content, true, 1);
            content.DrawHollowRect(0, 0, Level.Size.Width + 1, Level.Size.Height, Color.Black);
            content.DrawHollowRect(-1, -1, Level.Size.Width + 3, Level.Size.Height + 2, Color.Black);

            content.SpriteBatch.End();

            //Layers above the current one
            for (; i < LayerEditors.Count; i++)
            {
                if (i < Ogmo.Project.LayerDefinitions.Count && Ogmo.Project.LayerDefinitions[i].Visible)
                {
                    content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, LayerEditors[i].DrawMatrix * LevelView.Matrix);
                    LayerEditors[i].Draw(content, false, LAYER_ABOVE_ALPHA);
                    content.SpriteBatch.End();
                }
            }

            //Draw the camera
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, LevelView.Matrix);
            if (Ogmo.Project.CameraEnabled)
            {
                int w = Ogmo.Project.CameraSize.Width / 8;
                int h = Ogmo.Project.CameraSize.Height / 8;

                int x2 = CameraPosition.X + Ogmo.Project.CameraSize.Width;
                int y2 = CameraPosition.Y + Ogmo.Project.CameraSize.Height;

                content.DrawLine(CameraPosition.X - 1, CameraPosition.Y - 1, CameraPosition.X + w, CameraPosition.Y - 1, Color.Red);
                content.DrawLine(CameraPosition.X, CameraPosition.Y, CameraPosition.X + w, CameraPosition.Y, Color.Red);
                content.DrawLine(CameraPosition.X - 1, CameraPosition.Y - 1, CameraPosition.X - 1, CameraPosition.Y + h, Color.Red);
                content.DrawLine(CameraPosition.X, CameraPosition.Y, CameraPosition.X, CameraPosition.Y + h, Color.Red);

                content.DrawLine(x2 - w, CameraPosition.Y - 1, x2 + 1, CameraPosition.Y - 1, Color.Red);
                content.DrawLine(x2 - w, CameraPosition.Y, x2, CameraPosition.Y, Color.Red);
                content.DrawLine(x2 + 1, CameraPosition.Y, x2 + 1, CameraPosition.Y + h, Color.Red);
                content.DrawLine(x2, CameraPosition.Y, x2, CameraPosition.Y + h, Color.Red);

                content.DrawLine(CameraPosition.X - 1, y2 + 1, CameraPosition.X + w, y2 + 1, Color.Red);
                content.DrawLine(CameraPosition.X - 1, y2, CameraPosition.X + w, y2, Color.Red);
                content.DrawLine(CameraPosition.X - 1, y2, CameraPosition.X - 1, y2 - h, Color.Red);
                content.DrawLine(CameraPosition.X, y2, CameraPosition.X, y2 - h, Color.Red);

                content.DrawLine(x2 - w, y2 + 1, x2 + 1, y2 + 1, Color.Red);
                content.DrawLine(x2 - w, y2, x2, y2, Color.Red);
                content.DrawLine(x2 + 1, y2 - h, x2 + 1, y2 + 1, Color.Red);
                content.DrawLine(x2, y2 - h, x2, y2, Color.Red);
            }

            content.SpriteBatch.End();
        }

        private void DrawLayer(LayerEditor layer, bool current, float alpha)
        {
            
            layer.Draw(Ogmo.Content, current, alpha);
            Ogmo.Content.SpriteBatch.End();
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
            //If a batch is in progress, stop it!
            EndBatch();

            //If you're over the undo limit, chop off an action
            while (UndoStack.Count >= Config.ConfigFile.UndoLimit)
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

            while (UndoStack.Count >= Config.ConfigFile.UndoLimit)
                UndoStack.RemoveFirst();
            UndoStack.AddLast(batch);
            RedoStack.Clear();
        }

        public void BatchPerform(OgmoAction action)
        {
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
            //Update the screen bounds
            DrawBounds = new Rectangle(0, 0, Width, Height);
            LevelView.Origin = new Vector2(Width / 2, Height / 2);
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
                    int x = CameraPosition.X + (int)((e.Location.X - lastMousePoint.X) / LevelView.Zoom);
                    int y = CameraPosition.Y + (int)((e.Location.Y - lastMousePoint.Y) / LevelView.Zoom);
                    CameraPosition = new Point(x, y);
                    lastMousePoint = e.Location;
                    foreach (var ed in LayerEditors)
                        ed.UpdateDrawOffset(CameraPosition);
                }
                else
                {
                    LevelView.X -= (e.Location.X - lastMousePoint.X) / LevelView.Zoom;
                    LevelView.Y -= (e.Location.Y - lastMousePoint.Y) / LevelView.Zoom;
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
                LevelView.ZoomIn();
            else
                LevelView.ZoomOut();
            Ogmo.MainWindow.ZoomLabel.Text = LevelView.ZoomString;
        }

        private void SnapCamera()
        {
            CameraPosition = Ogmo.LayersWindow.CurrentLayer.Definition.SnapToGrid(CameraPosition);
            foreach (var ed in LayerEditors)
                ed.UpdateDrawOffset(CameraPosition);
        }
    }
}
