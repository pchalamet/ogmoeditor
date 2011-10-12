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

namespace OgmoEditor.LevelEditors
{
    public class LevelEditor : GraphicsDeviceControl
    {
        private Level level;
        private Content content;
        private SpriteBatch spriteBatch;

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
            //Init the screen bounds
            DrawBounds = new Rectangle(0, 0, Width, Height);

            //Load the content
            content = new Content(GraphicsDevice);

            //Create the spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Events
            this.Resize += onResize;
        }

        protected override void Draw()
        {
            //Draw the background and logo
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);
            XDraw.TextureFill(spriteBatch, content.TexBG, DrawBounds);
            spriteBatch.Draw(content.TexLogo, new Vector2(DrawBounds.Width / 2, DrawBounds.Height / 2), null, Color.White, 0, new Vector2(content.TexLogo.Width/2, content.TexLogo.Height/2), 3, SpriteEffects.None, 0);
            spriteBatch.End();

            //Draw the level background

        }

        /*
         *  EVENTS
         */
        private void onResize(object sender, EventArgs e)
        {
            //Update the screen bounds
            DrawBounds = new Rectangle(0, 0, Width, Height);
        }
    }
}
