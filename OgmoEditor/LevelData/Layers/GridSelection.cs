using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors;
using OgmoEditor.LevelEditors.Actions.GridActions;

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

        public void PerformMove(LevelEditor editor, Point move)
        {
            if (Area.X + move.X >= 0 && Area.Y + move.Y >= 0 && Area.X + move.X + Area.Width <= Layer.GridCellsX && Area.Y + move.Y + Area.Height <= Layer.GridCellsY)
                editor.Perform(new GridMoveSelectionAction(Layer, move));
            else
            {
                Rectangle rect = Area;
                if (rect.X + move.X < 0)
                {
                    rect.Width += rect.X + move.X;
                    rect.X = -move.X;
                }
                if (rect.Y + move.Y < 0)
                {
                    rect.Height += rect.Y + move.Y;
                    rect.Y = -move.Y;
                }
                if (rect.X + rect.Width + move.X > Layer.GridCellsX)
                {
                    rect.Width -= Layer.GridCellsX - (rect.X + rect.Width + move.X);
                }
                if (rect.Y + rect.Height + move.Y > Layer.GridCellsY)
                {
                    rect.Height -= Layer.GridCellsY - (rect.Y + rect.Height + move.Y);
                }

                if (rect.Width > 0 && rect.Height > 0)
                {
                    editor.StartBatch();
                    editor.BatchPerform(new GridSelectAction(Layer, rect));
                    editor.BatchPerform(new GridMoveSelectionAction(Layer, move));
                    editor.EndBatch();
                }
            }
        }
    }
}
