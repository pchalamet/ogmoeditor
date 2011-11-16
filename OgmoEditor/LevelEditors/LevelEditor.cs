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
        private const int UNDO_LIMIT = 60;
        private const float LAYER_ABOVE_ALPHA = .5f;

        private bool mousePanMode;
        private Point lastMousePoint;

        public Level Level { get; private set; }
        public Camera Camera { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public Rectangle DrawBounds { get; private set; }
        public new Point MousePosition { get; private set; }

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
            Camera = new Camera();
            Camera.Origin = new Vector2(Width / 2, Height / 2);
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
            Camera.X = Level.Size.Width / 2;
            Camera.Y = Level.Size.Height / 2;
        }

        protected override void Draw()
        {
            Content content = Ogmo.Content;

            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
            content.SpriteBatch.Draw(content.TexBG, DrawBounds, new Rectangle(0, 0, DrawBounds.Width, DrawBounds.Height), Color.White);
            content.SpriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, Color.White, 0, new Vector2(content.TexLogo.Width / 2, content.TexLogo.Height / 2), 3, SpriteEffects.None, 0);
            content.SpriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            content.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, Camera.Matrix);
            content.DrawRectangle(10, 10, Level.Size.Width, Level.Size.Height, new Color(0, 0, 0, .5f));
            content.DrawRectangle(0, 0, Level.Size.Width, Level.Size.Height, Ogmo.Project.BackgroundColor.ToXNA());

            //Draw the grid if turned on and editor is zoomed at least 100%
            if (Ogmo.MainWindow.EditingGridVisible && Camera.Zoom >= 1)
                content.DrawGrid(LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].Layer.Definition.Grid, Level.Size, Ogmo.Project.GridColor.ToXNA() * .5f);

            //Draw the layers
            int i;
            for (i = 0; i < Ogmo.LayersWindow.CurrentLayerIndex; i++)
            {
                if (Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].Draw(content, false, 1);
            }
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].Draw(content, true, 1);
            for (; i < LayerEditors.Count; i++)
            {
                if (i < Ogmo.Project.LayerDefinitions.Count && Ogmo.Project.LayerDefinitions[i].Visible)
                    LayerEditors[i].Draw(content, false,  LAYER_ABOVE_ALPHA);
            }

            content.SpriteBatch.End();
        }

        public void SwitchTo()
        {
            Focus();
            Ogmo.MainWindow.ZoomLabel.Text = Camera.ZoomString;
        }

        /*
         *  ACTIONS API
         */
        public void Perform(OgmoAction action)
        {
            //If a batch is in progress, stop it!
            EndBatch();

            //If you're over the undo limit, chop off an action
            if (UndoStack.Count == UNDO_LIMIT)
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

            if (UndoStack.Count == UNDO_LIMIT)
                UndoStack.RemoveFirst();
            UndoStack.AddLast(batch);
            RedoStack.Clear();
        }

        public void BatchPerform(OgmoAction action)
        {
            if (UndoStack.Last.Value.Appendable && UndoStack.Last.Value.GetType() == action.GetType())
            {
                //Append the action
                OgmoAction last = UndoStack.Last.Value;
                action.Undo();
                last.Append(action);                
                action.Do();
            }
            else
            {
                batch.Add(action);
                action.Do();
            }
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
            Camera.Origin = new Vector2(Width / 2, Height / 2);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            //Call the layer event
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnKeyDown(e.KeyCode);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            //Call the layer event
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnKeyUp(e.KeyCode);
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftClick(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightClick(Camera.ScreenToEditor(e.Location));
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftDown(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightDown(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                //Enter mouse move mode
                mousePanMode = true;
                lastMousePoint = e.Location;
            }
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseLeftUp(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseRightUp(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                //Exit mouse move mode
                mousePanMode = false;
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            //Pan the camera if in move mode
            if (mousePanMode)
            {
                Camera.X -= (e.Location.X - lastMousePoint.X) / Camera.Zoom;
                Camera.Y -= (e.Location.Y - lastMousePoint.Y) / Camera.Zoom;
                lastMousePoint = e.Location;
            }

            //Update the mouse coord display
            MousePosition = Camera.ScreenToEditor(e.Location);
            Point gridPos = Ogmo.Project.LayerDefinitions[Ogmo.LayersWindow.CurrentLayerIndex].ConvertToGrid(MousePosition);
            Ogmo.MainWindow.MouseCoordinatesLabel.Text = "Mouse: ( " + MousePosition.X.ToString() + ", " + MousePosition.Y.ToString() + " )";
            Ogmo.MainWindow.GridCoordinatesLabel.Text = "Grid: ( " + gridPos.X.ToString() + ", " + gridPos.Y.ToString() + " )";

            //Call the layer event
            LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].OnMouseMove(MousePosition);
        }

        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Camera.ZoomIn();
            else
                Camera.ZoomOut();
            Ogmo.MainWindow.ZoomLabel.Text = Camera.ZoomString;
        }
    }
}
