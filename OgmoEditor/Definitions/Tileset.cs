using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor.Definitions
{
    public class Tileset
    {
        public string Name;
        public string FilePath;
        public Size TileSize;
        public int TileSep;

        private Image image;

        public Tileset()
        {
            TileSize = new Size(16, 16);
            FilePath = "";
            TileSep = 0;
        }

        public Tileset Clone()
        {
            Tileset set = new Tileset();
            set.Name = Name;
            set.FilePath = FilePath;
            set.TileSize = TileSize;
            set.TileSep = TileSep;
            return set;
        }

        public void SetFilePath(string to)
        {
            FilePath = to;
            image = generateImage();
        }

        public Rectangle GetRectFromID(int id)
        {
            int y = id / TilesAcross;
            int x = id % TilesAcross;

            return new Rectangle(x * (TileSize.Width + TileSep), y * (TileSize.Height + TileSep), TileSize.Width, TileSize.Height);
        }

        public Microsoft.Xna.Framework.Rectangle GetXNARectFromID(int id)
        {
            int y = id / TilesAcross;
            int x = id % TilesAcross;

            return new Microsoft.Xna.Framework.Rectangle(x * (TileSize.Width + TileSep), y * (TileSize.Height + TileSep), TileSize.Width, TileSize.Height);
        }

        public int TilesAcross
        {
            get
            {
                int across = 0;
                for (int i = 0; i + TileSize.Width <= Size.Width; i += TileSize.Width + TileSep)
                    across++;
                return across;
            }
        }

        public int TilesDown
        {
            get
            {
                int down = 0;
                for (int i = 0; i + TileSize.Height <= Size.Height; i += TileSize.Height + TileSep)
                    down++;
                return down;
            }
        }

        public int TilesTotal
        {
            get
            {
                return TilesAcross * TilesDown;
            }
        }

        private Image generateImage()
        {
            if (!File.Exists(Path.Combine(Ogmo.Project.SavedDirectory, FilePath)))
                return null;
            else
            {
                FileStream s = new FileStream(Path.Combine(Ogmo.Project.SavedDirectory, FilePath), FileMode.Open, FileAccess.Read, FileShare.Read);
                Image image = Image.FromStream(s);
                s.Close();
                return image;
            }
        }

        public Texture2D GenerateTexture(GraphicsDevice graphics)
        {
            FileStream stream = new FileStream(Path.Combine(Ogmo.Project.SavedDirectory, FilePath), FileMode.Open, FileAccess.Read, FileShare.Read);
            Texture2D tex = Texture2D.FromStream(graphics, stream);
            stream.Close();
            return tex;
        }

        public Rectangle Bounds
        {
            get
            {
                if (Image == null)
                    return Rectangle.Empty;
                else
                    return new Rectangle(0, 0, Image.Width, Image.Height);
            }
        }

        public Size Size
        {
            get
            {
                if (Image == null)
                    return Size.Empty;
                else
                    return Image.Size;
            }
        }

        public Image Image
        {
            get
            {
                if (image == null)
                {
                    image = generateImage();
                }
                return image;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public int GetIDFromCell(Point cell)
        {
            if (cell.X >= TilesAcross)
                return -1;
            if (cell.Y >= TilesDown)
                return -1;

            return cell.X + cell.Y * TilesAcross;
        }

        public int[] GetIDsFromCells(Point[] cell)
        {
            return cell.Select(value => this.GetIDFromCell(value)).ToArray();
        }

        public Point GetCellFromID(int id)
        {
            return new Point(id % TilesAcross, id / TilesAcross);
        }

        public Point[] GetCellsFromIDs(int[] id)
        {
            return id.Select(value => this.GetCellFromID(value)).ToArray();
        }

        public int TransformID(Tileset from, int id)
        {
            if (id == -1)
                return -1;

            return GetIDFromCell(from.GetCellFromID(id));
        }

        public int[] TransformIDs(Tileset from, int[] id)
        {
            if (id.Length == 0)
                return new int[] { };

            return GetIDsFromCells(from.GetCellsFromIDs(id));
        }

        public int[,] TransformMap(Tileset from, int[,] ids)
        {
            int[,] transformed = new int[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < ids.GetLength(0); i++)
                for (int j = 0; j < ids.GetLength(1); j++)
                    transformed[i, j] = TransformID(from, ids[i, j]);
            return transformed;
        }
    }
}
