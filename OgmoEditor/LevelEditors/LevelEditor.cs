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

namespace OgmoEditor.LevelEditors
{
    using Point = System.Drawing.Point;
using OgmoEditor.LevelEditors.LayerEditors.Actions;

    public class LevelEditor : GraphicsDeviceControl
    {
        private const int UNDO_LIMIT = 30;

        private Level level;
        private Content content;
        private SpriteBatch spriteBatch;
        private bool mousePanMode;
        private Point lastMousePoint;

        public Camera Camera { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public Rectangle DrawBounds { get; private set; }
        public int CurrentLayer;

        public LinkedList<OgmoAction> UndoStack { get; private set; }
        public LinkedList<OgmoAction> RedoStack { get; private set; }

        public LevelEditor(Level level)
        {
            this.level = level;
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
            content = new Content(GraphicsDevice);

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
            Camera.X = level.Size.Width / 2;
            Camera.Y = level.Size.Height / 2;
        }

        protected override void Draw()
        {
            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);
            content.DrawTextureFill(spriteBatch, content.TexBG, DrawBounds);
            spriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, Color.White, 0, new Vector2(content.TexLogo.Width/2, content.TexLogo.Height/2), 3, SpriteEffects.None, 0);
            spriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Camera.Matrix);
            content.DrawRectangle(spriteBatch, 10, 10, level.Size.Width, level.Size.Height, new Color(0, 0, 0, .5f));
            content.DrawRectangle(spriteBatch, 0, 0, level.Size.Width, level.Size.Height, Ogmo.Project.BackgroundColor.ToXNA());

            //Draw the layers
            foreach (var ed in LayerEditors)
                ed.Draw(spriteBatch);

            //Draw the grid if zoomed at least 100%
            if (Camera.Zoom >= 1)
                content.DrawGrid(spriteBatch, LayerEditors[0].Layer.Definition.Grid, level.Size, Ogmo.Project.GridColor.ToXNA() * .5f);

            spriteBatch.End();
        }

        /*
         *  ACTIONS API
         */
        public void Perform(OgmoAction action)
        {
            if (UndoStack.Count == UNDO_LIMIT)
                UndoStack.RemoveFirst();

            UndoStack.AddLast(action);
            action.Do();
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

                //Add it to the undo stack
                UndoStack.AddLast(action);
            }
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
            LayerEditors[CurrentLayer].OnKeyDown(e.KeyCode);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            //Call the layer event
            LayerEditors[CurrentLayer].OnKeyUp(e.KeyCode);
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[CurrentLayer].OnMouseLeftClick(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[CurrentLayer].OnMouseRightClick(Camera.ScreenToEditor(e.Location));
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            //Call the layer event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                LayerEditors[CurrentLayer].OnMouseLeftDown(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[CurrentLayer].OnMouseRightDown(Camera.ScreenToEditor(e.Location));
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
                LayerEditors[CurrentLayer].OnMouseLeftUp(Camera.ScreenToEditor(e.Location));
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                LayerEditors[CurrentLayer].OnMouseRightUp(Camera.ScreenToEditor(e.Location));
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
            LayerEditors[CurrentLayer].OnMouseMove(Camera.ScreenToEditor(coords));
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
