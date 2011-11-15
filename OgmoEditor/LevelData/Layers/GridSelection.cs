using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors;

namespace OgmoEditor.LevelData.Layers
{
    public class GridSelection
    {
        public GridLayer Layer;
        public Point Position;
        public bool[,] Grid;

        public GridSelection(GridLayer layer, Rectangle area)
        {
            Layer = layer;
            Position = new Point(area.X, area.Y);

            Grid = new bool[area.Width, area.Height];
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    Grid[i, j] = layer.Grid[i + Position.X, j + Position.Y];
        }

        public GridSelection(GridSelection copyFrom)
        {
            Position = copyFrom.Position;
            Grid = (bool[,])copyFrom.Grid.Clone();
        }

        public void Paste()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    Layer.Grid[i + Position.X, j + Position.Y] = Grid[i, j];
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Position.X, Position.Y, Width, Height);
            }
        }

        public int Width
        {
            get
            {
                return Grid.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return Grid.GetLength(1);
            }
        }
    }
}
