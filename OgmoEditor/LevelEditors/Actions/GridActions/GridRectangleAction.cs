using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridRectangleAction : GridAction
    {
        public int CellX { get; private set; }
        public int CellY { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool SetTo { get; private set; }

        private bool[,] was;

        public GridRectangleAction(GridLayer gridLayer, int cellX, int cellY, int width, int height, bool setTo)
            : base(gridLayer)
        {
            CellX = cellX;
            CellY = cellY;
            Width = width;
            Height = height;
            SetTo = setTo;
        }

        public override void Do()
        {
            was = new bool[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    was[i, j] = GridLayer.Grid[CellX + i, CellY + j];
                    GridLayer.Grid[CellX + i, CellY + j] = SetTo;
                }
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    GridLayer.Grid[CellX + i, CellY + j] = was[i, j];
                }
            }
        }
    }
}
