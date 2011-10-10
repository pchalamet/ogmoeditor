using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.Definitions
{
    public class Tileset
    {
        public string Name;
        private string path;
        public Size TileSize;
        public int TileSep;

        public Tileset()
        {
            Name = "";
            path = "";
            TileSize = new Size(16, 16);
            TileSep = 0;
        }
    }
}
