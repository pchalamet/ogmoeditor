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
using OgmoEditor.LevelEditors.LayerEditors.Tools;

namespace OgmoEditor.LevelEditors
{
    using Point = System.Drawing.Point;

    public class LevelEditor : GraphicsDeviceControl
    {
        private const int UNDO_LIMIT = 50;

        private SpriteBatch spriteBatch;
        private bool mousePanMode;
        private Point lastMousePoint;

        public Level Level { get; private set; }
        public Content Content { get; private set; }
        public Camera Camera { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public Rectangle DrawBounds { get; private set; }

        public LinkedList<OgmoAction> UndoStack { get; private set; }
        public LinkedList<OgmoAction> RedoStack { get; private set; }

        private Tool batcher;
        private ActionBatch batch;

        public LevelEditor(Level level)
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

            //Load the content
            Content = new Content(GraphicsDevice);

            //Create the spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Events
            Application.Idle += delegate { Invalidate(); };
            this.Resize += onResize;
            this.MouseClick += onMouseClick;
            this.MouseDown += onMouseDown;
            this.MouseUp += onMouseUp;
            this.MouseMove += onMouseMove;
            this.MouseWheel += onMouseWheel;
            this.KeyDown += onKeyDown;
            this.KeyUp += onKeyUp;
        }

        private void centerCamera()
        {
            Camera.X = Level.Size.Width / 2;
            Camera.Y = Level.Size.Height / 2;
        }

        protected override void Draw()
        {
            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);
            Content.DrawTextureFill(spriteBatch, Content.TexBG, DrawBounds);
            spriteBatch.Draw(Content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, Color.White, 0, new Vector2(Content.TexLogo.Width/2, Content.TexLogo.Height/2), 3, SpriteEffects.None, 0);
            spriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Camera.Matrix);
            Content.DrawRectangle(spriteBatch, 10, 10, Level.Size.Width, Level.Size.Height, new Color(0, 0, 0, .5f));
            Content.DrawRectangle(spriteBatch, 0, 0, Level.Size.Width, Level.Size.Height, Ogmo.Project.BackgroundColor.ToXNA());

            //Draw the layers
            foreach (var ed in LayerEditors)
                ed.Draw(spriteBatch);

            //Draw the grid if zoomed at least 100%
            if (Camera.Zoom >= 1)
                Content.DrawGrid(spriteBatch, LayerEditors[0].Layer.Definition.Grid, Level.Size, Ogmo.Project.GridColor.ToXNA() * .5f);

            spriteBatch.End();
        }

        public void SwitchTo()
        {
            Focus();
        }

        /*
         *  ACTIONS API
         */
        public void Perform(OgmoAction action)
        {
            //If a batch is in progress, stop it!
            BatchEnd();

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

        public void BatchPerform(Tool tool, OgmoAction action)
        {
            //Start the batch if it isn't in progress
            if (batcher != tool)
            {
                batcher = tool;
                batch = new ActionBatch();

                //If you're over the undo limit, chop off an action
                if (UndoStack.Count == UNDO_LIMIT)
                    UndoStack.RemoveFirst();

                //If the level is so-far unchanged, change it and store that fact
                if (!Level.Changed)
                {
                    action.LevelWasChanged = false;
                    Level.Changed = true;
                }

                //Add the batch action to the undo stack
                UndoStack.AddLast(batch);

                //Clear the redo stack
                RedoStack.Clear();
            }

            //Add the new action to the batch and do it!
            batch.Add(action);
            action.Do();
        }

        public void BatchEnd()
        {
            batcher = null;
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
            //Call the tool window keypress checker
            Ogmo.ToolsWindow.KeyPress(e.KeyCode);

            //Call the layer event
            LayerEditors[Ogmo.CurrentLayerIndex].OnKeyDown(e.KeyCode);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            //Call the layer event
            LayerEditors[Ogmo.CurrentLayerIndex].OnKeyUp(e.KeyCode);
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseLeftClick(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseRightClick(Camera.ScreenToEditor(e.Location));
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseLeftDown(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseRightDown(Camera.ScreenToEditor(e.Location));
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
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseLeftUp(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[Ogmo.CurrentLayerIndex].OnMouseRightUp(Camera.ScreenToEditor(e.Location));
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
            Point coords = Camera.ScreenToEditor(e.Location);
            Ogmo.MainWindow.MouseCoordinatesLabel.Text = "( " + coords.X.ToString() + ", " + coords.Y.ToString() + " )";

            //Call the layer event
            LayerEditors[Ogmo.CurrentLayerIndex].OnMouseMove(coords);
        }

        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Camera.ZoomIn();
            else
                Camera.ZoomOut();
        }
    }
}
