using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.LevelData.Layers
{
    public class GridSelection
    {
        public GridLayer Layer;
        public Rectangle Area;
        public bool[,] Under;

        public GridSelection(GridLayer layer, Rectangle area)
        {
            Layer = layer;
            Area = area;

            Under = new bool[area.Width, area.Height];
        }
    }
}
