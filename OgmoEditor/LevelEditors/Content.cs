using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors
{
    public class Content
    {
        public Texture2D TexPixel { get; private set; }
        public Texture2D TexBG { get; private set; }
        public Texture2D TexLogo { get; private set; }

        private GraphicsDevice device;

        public Content(GraphicsDevice device)
        {
            this.device = device;

            TexPixel = CreateRect(device, Color.White, 1, 1);
            TexBG = Read("bg.png");
            TexLogo = Read("logo.png");
        }

        private Texture2D Read(string filename)
        {
            FileStream s = new FileStream(Path.Combine(Ogmo.ProgramDirectory, "Content", filename), FileMode.Open);
            Texture2D tex = Texture2D.FromStream(device, s);
            s.Close();

            return tex;
        }

        /*
         *  Drawing helpers
         */
        public void DrawTextureFill(SpriteBatch spriteBatch, Texture2D texture, Rectangle fillRect)
        {
            Rectangle r = new Rectangle(fillRect.X, fillRect.Y, texture.Width, texture.Height);

            for (r.X = fillRect.X; r.X < fillRect.X + fillRect.Width; r.X += texture.Width)
                for (r.Y = fillRect.Y; r.Y < fillRect.Y + fillRect.Height; r.Y += texture.Height)
                    spriteBatch.Draw(texture, r, Color.White);
        }

        public void DrawRectangle(SpriteBatch spriteBatch, int x, int y, int width, int height, Color color)
        {
            spriteBatch.Draw(TexPixel, new Rectangle(x, y, width, height), color);
        }

        /*
         *  Helpers
         */
        static public Texture2D CreateRect(GraphicsDevice device, Color color, int width, int height)
        {
            Texture2D texture = new Texture2D(device, width, height, false, SurfaceFormat.Color);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;

            texture.SetData<Color>(data);

            return texture;
        }
    }
}
