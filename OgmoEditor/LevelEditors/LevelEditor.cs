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

namespace OgmoEditor.LevelEditors
{
    public class LevelEditor : GraphicsDeviceControl
    {
        private Level level;
        private Content content;
        private SpriteBatch spriteBatch;

        public LevelEditor(Level level)
        {
            this.level = level;
            this.Size = level.Size;

            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        protected override void Initialize()
        {
            content = new Content();
            content.Load(GraphicsDevice);

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(content.TexBG, new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);
            spriteBatch.End();
        }
    }
}
