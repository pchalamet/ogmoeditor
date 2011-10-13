﻿using System;
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

    public class LevelEditor : GraphicsDeviceControl
    {
        private Level level;
        private Content content;
        private SpriteBatch spriteBatch;
        private bool mouseMoveMode;
        private Point lastMousePoint;

        public Camera Camera { get; private set; }
        public List<LayerEditor> LayerEditors { get; private set; }
        public Rectangle DrawBounds { get; private set; }

        public LevelEditor(Level level)
        {
            this.level = level;
            this.Size = level.Size;
            Dock = System.Windows.Forms.DockStyle.Fill;

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
            content.DrawRectangle(spriteBatch, 0, 0, level.Size.Width, level.Size.Height, Ogmo.Project.BackgroundColor.toXNA());

            //Draw the layers
            foreach (var ed in LayerEditors)
                ed.Draw(spriteBatch);

            //Draw the grid if zoomed at least 100%
            if (Camera.Zoom >= 1)
                content.DrawGrid(spriteBatch, LayerEditors[0].Layer.Definition.Grid, level.Size, Color.White);

            spriteBatch.End();
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

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            //Enter mouse move mode
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                mouseMoveMode = true;
                lastMousePoint = e.Location;
            }
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            //Exit mouse move mode
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                mouseMoveMode = false;
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseMoveMode)
            {
                Camera.X -= (e.Location.X - lastMousePoint.X) / Camera.Zoom;
                Camera.Y -= (e.Location.Y - lastMousePoint.Y) / Camera.Zoom;
                lastMousePoint = e.Location;
            }
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
