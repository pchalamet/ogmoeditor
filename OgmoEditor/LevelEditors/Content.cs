using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Drawing;
using OgmoEditor.Definitions;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors
{
    using Color = Microsoft.Xna.Framework.Color;
    using Rectangle = Microsoft.Xna.Framework.Rectangle; 

    public class Content
    {
        public Texture2D TexPixel { get; private set; }
        public Texture2D TexBG { get; private set; }
        public Texture2D TexLogo { get; private set; }
        public Dictionary<EntityDefinition, Texture2D> EntityTextures { get; private set; }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Content(GraphicsDevice device)
        {
            GraphicsDevice = device;
            SpriteBatch = new SpriteBatch(device);

            //Get all the standard textures set up
            TexPixel = Util.CreateRect(device, Color.White, 1, 1);
            TexBG = Read("bg.png");
            TexLogo = Read("logo.png");

            EntityTextures = new Dictionary<EntityDefinition, Texture2D>();
        }

        public void LoadEntityTextures(Project project)
        {
            foreach (var kv in EntityTextures)
                kv.Value.Dispose();

            EntityTextures.Clear();

            foreach (EntityDefinition def in project.EntityDefinitions)
            {
                Texture2D tex = def.GenerateTexture(GraphicsDevice);
                if (tex != null)
                    EntityTextures.Add(def, tex);
            }
        }

        /*
         *  Drawing helpers
         */
        public void DrawEntity(EntityDefinition definition, System.Drawing.Point position, float alpha = 1)
        {
            if (EntityTextures.ContainsKey(definition))
            {
                if (definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && definition.ImageDefinition.Tiled)
                    SpriteBatch.Draw(EntityTextures[definition], 
                        new Rectangle(position.X, position.Y, definition.Size.Width, definition.Size.Height), 
                        new Rectangle(0, 0, definition.Size.Width, definition.Size.Height), 
                        Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
                else
                    SpriteBatch.Draw(EntityTextures[definition], 
                        new Rectangle(position.X, position.Y, definition.Size.Width, definition.Size.Height), null, 
                        Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
            }
        }

        public void DrawEntity(Entity entity, float alpha = 1)
        {
            if (EntityTextures.ContainsKey(entity.Definition))
            {
                EntityDefinition definition = entity.Definition;
                System.Drawing.Point position = entity.Position;

                if (definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && definition.ImageDefinition.Tiled)
                    SpriteBatch.Draw(EntityTextures[definition], 
                        new Rectangle(position.X, position.Y, entity.Size.Width, entity.Size.Height), 
                        new Rectangle(0, 0, entity.Size.Width, entity.Size.Height),
                        Color.White * alpha, entity.Angle, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
                else
                    SpriteBatch.Draw(EntityTextures[definition], 
                        new Rectangle(position.X, position.Y, entity.Size.Width, entity.Size.Height), null,
                        Color.White * alpha, entity.Angle, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
            }
        }

        public void DrawNode(System.Drawing.Point node)
        {
            DrawRectangle(node.X, node.Y, 3, 3, Color.Red);
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

        public void DrawHollowRect(System.Drawing.Rectangle rect, Color color)
        {
            DrawHollowRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawHollowRect(int x, int y, int width, int height, Color color)
        {
            DrawLineAngle(x, y, width, Util.RIGHT, color);
            DrawLineAngle(x, y, height + 1, Util.DOWN, color);
            DrawLineAngle(x, y + height, width, Util.RIGHT, color);
            DrawLineAngle(x + width, y, height, Util.DOWN, color);
        }

        public void DrawFillRect(System.Drawing.Rectangle rect, Color color)
        {
            DrawFillRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawFillRect(Rectangle rect, Color color)
        {
            DrawFillRect(rect.X, rect.Y, rect.Width, rect.Height, color);
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

        public void DrawLine(System.Drawing.Point p1, System.Drawing.Point p2, Color color)
        {
            DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
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
            Texture2D tex = Texture2D.FromStream(GraphicsDevice, s);
            s.Close();

            return tex;
        }
    }
}
