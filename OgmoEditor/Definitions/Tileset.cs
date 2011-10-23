using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace OgmoEditor.Definitions
{
    public class Tileset
    {
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
