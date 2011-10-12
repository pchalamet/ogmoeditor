using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors
{
    static public class XDraw
    {
        static public void TextureFill(SpriteBatch spriteBatch, Texture2D texture, Rectangle fillRect)
        {
            Rectangle r = new Rectangle(fillRect.X, fillRect.Y, texture.Width, texture.Height);

            for (r.X = fillRect.X; r.X < fillRect.X + fillRect.Width; r.X += texture.Width)
                for (r.Y = fillRect.Y; r.Y < fillRect.Y + fillRect.Height; r.Y += texture.Height)
                    spriteBatch.Draw(texture, r, Color.White);
        }
    }
}
