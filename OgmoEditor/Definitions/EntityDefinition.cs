using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using OgmoEditor.Definitions.ValueDefinitions;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using OgmoEditor.LevelEditors;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace OgmoEditor.Definitions
{
    public class EntityDefinition
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public bool ResizableX;
        [XmlAttribute]
        public bool ResizableY;
        [XmlAttribute]
        public bool Rotatable;
        [XmlAttribute]
        public float RotateIncrement;

        public Size Size;
        public Point Origin;
        public EntityImageDefinition ImageDefinition;
        public List<ValueDefinition> ValueDefinitions;
        public EntityNodesDefinition NodesDefinition;

        private Bitmap bitmap;
        private Bitmap buttonBitmap;

        public EntityDefinition()
        {
            Limit = -1;
            Size = new Size(16, 16);
            RotateIncrement = 15;

            ValueDefinitions = new List<ValueDefinition>();

            ImageDefinition.ImagePath = "";
            ImageDefinition.RectColor = new OgmoColor(255, 0, 0);

            NodesDefinition.Limit = -1;
        }

        public EntityDefinition Clone()
        {
            EntityDefinition def = new EntityDefinition();
            def.Name = Name;
            def.Limit = Limit;
            def.ResizableX = ResizableX;
            def.ResizableY = ResizableY;
            def.Rotatable = Rotatable;
            def.RotateIncrement = RotateIncrement;
            def.Size = Size;
            def.Origin = Origin;
            def.ImageDefinition = ImageDefinition;
            def.ValueDefinitions = new List<ValueDefinition>();
            def.NodesDefinition = NodesDefinition;
            foreach (var d in ValueDefinitions)
                def.ValueDefinitions.Add(d.Clone());
            return def;
        }

        public void Draw(Graphics graphics, Point position, float angle, ImageAttributes attributes)
        {
            //Do transformations for position and rotation
            graphics.TranslateTransform(position.X - Origin.X, position.Y - Origin.Y);
            graphics.RotateTransform(angle);

            //Draw the actual entity
            if (ImageDefinition.Tiled && ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                Rectangle drawTo = Rectangle.Empty;
                for (drawTo.X = 0; drawTo.X < Size.Width; drawTo.X += bitmap.Width)
                {
                    drawTo.Width = Math.Min(bitmap.Width, Size.Width - drawTo.X);
                    for (drawTo.Y = 0; drawTo.Y < Size.Height; drawTo.Y += bitmap.Height)
                    {
                        drawTo.Height = Math.Min(bitmap.Height, Size.Height - drawTo.Y);
                        graphics.DrawImage(bitmap, drawTo, 0, 0, drawTo.Width, drawTo.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
            }
            else
                graphics.DrawImage(bitmap, new Rectangle(0, 0, Size.Width, Size.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);

            //Undo the transformations
            graphics.RotateTransform(-angle);
            graphics.TranslateTransform(-position.X + Origin.X, -position.Y + Origin.Y);
        }

        public void GenerateImages()
        {
            //Dispose old stuff
            if (bitmap != null)
                bitmap.Dispose();
            if (buttonBitmap != null && buttonBitmap != bitmap)
                buttonBitmap.Dispose();

            //Generate the in-editor image
            switch (ImageDefinition.DrawMode)
            {
                case EntityImageDefinition.DrawModes.Rectangle:
                    Bitmap b = new Bitmap(Size.Width, Size.Height);
                    using (Graphics g = Graphics.FromImage(b))
                    {
                        g.FillRectangle(new SolidBrush(ImageDefinition.RectColor), new Rectangle(0, 0, Size.Width, Size.Height));
                    }
                    bitmap = b;
                    break;

                case EntityImageDefinition.DrawModes.Image:
                    if (!File.Exists(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath)))
                        throw new Exception("Entity image could not be loaded!");
                    else
                        bitmap = new Bitmap(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath));
                    break;
            }

            //Generate the button image
            if (ImageDefinition.Tiled && ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                buttonBitmap = new Bitmap(Size.Width, Size.Height);
                using (Graphics g = Graphics.FromImage(buttonBitmap))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImageUnscaledAndClipped(bitmap, new Rectangle(0, 0, Size.Width, Size.Height));
                }
            }
            else
                buttonBitmap = bitmap;
        }

        public Bitmap GetBitmap()
        {
            return (Bitmap)bitmap.Clone();
        }

        public Bitmap GetButtonBitmap()
        {
            return (Bitmap)buttonBitmap.Clone();
        }

        public Texture2D GenerateTexture(GraphicsDevice graphics)
        {
            if (ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Rectangle)
                return Util.CreateRect(graphics, ImageDefinition.RectColor.ToXNA(), Size.Width, Size.Height);
            else if (ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                FileStream stream = new FileStream(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath), FileMode.Open, FileAccess.Read, FileShare.Read);
                Texture2D tex = Texture2D.FromStream(graphics, stream);                
                stream.Close();
                return tex;
            }

            return null;
        }
    }

    [XmlRoot("Image")]
    public struct EntityImageDefinition
    {
        public enum DrawModes { Rectangle, Image };

        [XmlAttribute]
        public DrawModes DrawMode;
        [XmlAttribute]
        public string ImagePath;
        [XmlAttribute]
        public bool Tiled;

        public OgmoColor RectColor;
    }

    [XmlRoot("Nodes")]
    public struct EntityNodesDefinition
    {
        public enum PathMode { None, Path, Circuit, Fan };

        [XmlAttribute]
        public bool Enabled;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public PathMode DrawMode;
        [XmlAttribute]
        public bool Ghost;
    }
}
