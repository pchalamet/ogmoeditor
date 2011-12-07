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

        public void SetUnderFromGrid()
        {
            for (int i = 0; i < Area.Width; i++)
                for (int j = 0; j < Area.Height; j++)
                    Under[i, j] = Layer.Grid[i + Area.X, j + Area.Y];
        }

        public bool[,] GetBitsFromGrid()
        {
            bool[,] bits = new bool[Area.Width, Area.Height];
            for (int i = 0; i < Area.Width; i++)
                for (int j = 0; j < Area.Height; j++)
                    bits[i, j] = Layer.Grid[i + Area.X, j + Area.Y];
            return bits;
        }
    }
}
