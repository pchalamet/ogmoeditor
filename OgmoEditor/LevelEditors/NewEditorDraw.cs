using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.Definitions;

namespace OgmoEditor.LevelEditors
{
    public class NewEditorDraw
    {
        public Bitmap ImgBG;
        public Bitmap ImgLogo;

        private Pen highlightPen;
        private Pen dashPen;

        //Project images
        public Dictionary<EntityDefinition, Bitmap> EntityImages { get; private set; }
        public Dictionary<Tileset, Bitmap> TilesetImages { get; private set; }

        public NewEditorDraw()
        {
            ImgBG = new Bitmap(BuildPath("bg.png"));
            ImgLogo = new Bitmap(BuildPath("logo.png"));

            EntityImages = new Dictionary<EntityDefinition, Bitmap>();
            TilesetImages = new Dictionary<Tileset, Bitmap>();

            highlightPen = new Pen(Color.Yellow, 3);
            dashPen = new Pen(Color.Teal);
            dashPen.DashPattern = new float[] { 6, 2 };

            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            dashPen.DashOffset -= 1f;
        }

        private string BuildPath(string filename)
        {
            return Path.Combine(Ogmo.ProgramDirectory, "Content", filename);
        }

        public void DrawSelectionRectangle(Graphics graphics, Rectangle rectangle)
        {
            graphics.DrawRectangle(highlightPen, rectangle);
            graphics.DrawRectangle(dashPen, rectangle);
        }

        public void DrawEntity(Entity entity, int alpha)
        {
            /*
            if (EntityTextures.ContainsKey(entity.Definition))
            {
                EntityDefinition definition = entity.Definition;
                System.Drawing.Point position = entity.Position;

                if (definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && definition.ImageDefinition.Tiled)
                {
                    Rectangle drawTo = Rectangle.Empty;
                    Texture2D texture = EntityTextures[definition];

                    for (drawTo.X = 0; drawTo.X < entity.Size.Width; drawTo.X += texture.Width)
                    {
                        drawTo.Width = Math.Min(texture.Width, entity.Size.Width - drawTo.X);
                        for (drawTo.Y = 0; drawTo.Y < entity.Size.Height; drawTo.Y += texture.Height)
                        {
                            drawTo.Height = Math.Min(texture.Height, entity.Size.Height - drawTo.Y);

                            SpriteBatch.Draw(texture,
                                new Rectangle(drawTo.X + position.X, drawTo.Y + position.Y, drawTo.Width, drawTo.Height),
                                new Rectangle(0, 0, drawTo.Width, drawTo.Height),
                                Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
                        }
                    }
                }
                else
                    SpriteBatch.Draw(EntityTextures[definition],
                        new Rectangle(position.X, position.Y, entity.Size.Width, entity.Size.Height), null,
                        Color.White * alpha, entity.Angle * Util.DEGTORAD, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
            }
             */
        }
    }
}
