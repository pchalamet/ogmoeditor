using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.Definitions
{
    public class Tileset
    {
        public const string FILE_FILTER = "PNG image file|*.png|BMP image file|*.bmp";

        public string Name;
        public string Path;
        public Size TileSize;
        public int TileSep;

        public Tileset()
        {
            Name = "";
            Path = "";
            TileSize = new Size(16, 16);
            TileSep = 0;
        }

        public string ErrorCheck()
        {
            return "";
        }

        public Tileset Clone()
        {
            Tileset set = new Tileset();
            set.Name = Name;
            set.Path = Path;
            set.TileSize = TileSize;
            set.TileSep = TileSep;
            return set;
        }
    }
}
