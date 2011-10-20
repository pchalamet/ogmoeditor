using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace OgmoEditor.LevelEditors
{
    using Color = Microsoft.Xna.Framework.Color;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;
    using OgmoEditor.Definitions;
using OgmoEditor.LevelData.Layers;

    public class Content
    {
        public Texture2D TexPixel { get; private set; }
        public Texture2D TexBG { get; private set; }
        public Texture2D TexLogo { get; private set; }
        public Dictionary<EntityDefinition, Texture2D> ObjectTextures { get; private set; }

        private GraphicsDevice device;
        public SpriteBatch SpriteBatch { get; private set; }

        public Content(GraphicsDevice device)
        {
            this.device = device;
            SpriteBatch = new SpriteBatch(device);

            //Get all the standard textures set up
            TexPixel = CreateRect(device, Color.White, 1, 1);
            TexBG = Read("bg.png");
            TexLogo = Read("logo.png");

            //Generate all the object textures
            ObjectTextures = new Dictionary<EntityDefinition, Texture2D>();
            foreach (EntityDefinition def in Ogmo.Project.EntityDefinitions)
            {
                Texture2D tex = def.GenerateTexture(device);
                if (tex != null)
                    ObjectTextures.Add(def, tex);
            }
        }

        /*
         *  Drawing helpers
         */
        public void DrawEntity(EntityDefinition definition, System.Drawing.Point position, float alpha = 1)
        {
            if (definition.ImageDefinition.Tiled)
                DrawTextureFill(ObjectTextures[definition], new Rectangle(position.X - definition.Origin.X, position.Y - definition.Origin.Y, definition.Size.Width, definition.Size.Height), alpha);
            else
                SpriteBatch.Draw(ObjectTextures[definition], new Rectangle(position.X - definition.Origin.X, position.Y - definition.Origin.Y, definition.Size.Width, definition.Size.Height), Color.White * alpha);
        }

        public void DrawEntity(Entity entity, float alpha = 1)
        {
            if (entity.Definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && entity.Definition.ImageDefinition.Tiled)
                DrawTextureFill(ObjectTextures[entity.Definition], entity.XNABounds, alpha);
            else
                SpriteBatch.Draw(ObjectTextures[entity.Definition], entity.XNABounds, Color.White * alpha);
        }

        public void DrawTextureFill(Texture2D texture, Rectangle fillRect, float alpha = 1)
        {
            Rectangle r = new Rectangle(fillRect.X, fillRect.Y, texture.Width, texture.Height);

            for (r.X = fillRect.X; r.X < fillRect.X + fillRect.Width; r.X += texture.Width)
            {
                bool shortenX = false;
                if (r.X + r.Width > fillRect.X + fillRect.Width)
                {
                    shortenX = true;
                    r.Width = fillRect.X + fillRect.Width - r.X;
                }

                for (r.Y = fillRect.Y; r.Y < fillRect.Y + fillRect.Height; r.Y += texture.Height)
                {
                    bool shortenY = false;
                    if (r.Y + r.Height > fillRect.Y + fillRect.Height)
                    {
                        shortenY = true;
                        r.Height = fillRect.Y + fillRect.Height - r.Y;
                    }

                    SpriteBatch.Draw(texture, r, Color.White * alpha);

                    if (shortenY)
                        r.Height = texture.Height;
                }

                if (shortenX)
                    r.Width = texture.Width;
            }
        }

        public void DrawTextureFillFast(Texture2D texture, Rectangle fillRect, float alpha = 1)
        {
            Rectangle r = new Rectangle(fillRect.X, fillRect.Y, texture.Width, texture.Height);

            for (r.X = fillRect.X; r.X < fillRect.X + fillRect.Width; r.X += texture.Width)
            {
                for (r.Y = fillRect.Y; r.Y < fillRect.Y + fillRect.Height; r.Y += texture.Height)
                {
                    SpriteBatch.Draw(texture, r, Color.White * alpha);
                }
            }
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            SpriteBatch.Draw(TexPixel, new Rectangle(x, y, width, height), color);
        }

        public void DrawRectangle(Rectangle rect, Color color, Vector2 origin, float angle)
        {
            SpriteBatch.Draw(TexPixel, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }

        public void DrawHollowRect(int x, int y, int width, int height, Color color)
        {
            DrawLineAngle(x, y, width, Util.RIGHT, color);
            DrawLineAngle(x, y, height + 1, Util.DOWN, color);
            DrawLineAngle(x, y + height, width, Util.RIGHT, color);
            DrawLineAngle(x + width, y, height, Util.DOWN, color);
        }

        public void DrawFillRect(int x, int y, int width, int height, Color color)
        {
            DrawHollowRect(x, y, width, height, color);
            DrawRectangle(x, y + 1, width - 1, height - 1, color * .3f);
        }

        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            int length = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            float rotation = (float)Math.Atan2(y2 - y1, x2 - x1);

            DrawLineAngle(x1, y1, length, rotation, color);
        }

        public void DrawLineAngle(int x, int y, int length, float rotation, Color color)
        {
            SpriteBatch.Draw(TexPixel, new Vector2(x, y), null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        public void DrawGrid(Size grid, Size size, Color color)
        {
            for (int i = grid.Width; i < size.Width; i += grid.Width)
                DrawLineAngle(i, 2, size.Height - 4, Util.DOWN, color);

            for (int i = grid.Height; i < size.Height; i += grid.Height)
                DrawLineAngle(2, i, size.Width - 4, Util.RIGHT, color);
        }

        /*
         *  Helpers
         */
        private Texture2D Read(string filename)
        {
            FileStream s = new FileStream(Path.Combine(Ogmo.ProgramDirectory, "Content", filename), FileMode.Open);
            Texture2D tex = Texture2D.FromStream(device, s);
            s.Close();

            return tex;
        }

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
