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

namespace OgmoEditor.LevelEditors
{
    public class LevelEditor : GraphicsDeviceControl
    {
        private Level level;
        private Content content;
        private SpriteBatch spriteBatch;
        private RenderTarget2D levelCanvas;

        public Camera Camera { get; private set; }
        public Rectangle DrawBounds { get; private set; }

        public LevelEditor(Level level)
        {
            this.level = level;
            this.Size = level.Size;

            //Setup
            Dock = System.Windows.Forms.DockStyle.Fill;
        }

        protected override void Initialize()
        {
            //Init the screen bounds and camera
            Camera = new Camera();
            Camera.Origin = new Vector2(Width / 2, Height / 2);
            centerCamera();
            DrawBounds = new Rectangle(0, 0, Width, Height);
            initLevelCanvas();

            //Load the content
            content = new Content(GraphicsDevice);

            //Create the spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Events
            Application.Idle += delegate { Invalidate(); };
            this.Resize += onResize;
            this.MouseClick += onMouseClick;
            this.MouseMove += onMouseMove;
            this.MouseWheel += onMouseWheel;
        }

        private void initLevelCanvas()
        {
            if (levelCanvas != null)
                levelCanvas.Dispose();
            levelCanvas = new RenderTarget2D(GraphicsDevice, level.Size.Width, level.Size.Height); 
        }

        private void centerCamera()
        {
            Camera.X = level.Size.Width / 2;
            Camera.Y = level.Size.Height / 2;
        }

        protected override void Draw()
        {
            //Draw the level onto the level canvas
            GraphicsDevice.SetRenderTarget(levelCanvas);
            GraphicsDevice.Clear(Ogmo.Project.BackgroundColor.toXNA());
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);
            
            spriteBatch.End();

            //Draw the background and logo
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);
            content.DrawTextureFill(spriteBatch, content.TexBG, DrawBounds);
            spriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, Color.White, 0, new Vector2(content.TexLogo.Width/2, content.TexLogo.Height/2), 3, SpriteEffects.None, 0);
            spriteBatch.End();

            //Draw the level onto the control, positioned and scaled by the camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Camera.Matrix);
            spriteBatch.Draw(levelCanvas, Vector2.Zero, Color.White);
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

        private void onMouseMove(object sender, MouseEventArgs e)
        {

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
