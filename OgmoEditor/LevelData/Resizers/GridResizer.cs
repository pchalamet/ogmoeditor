using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelData.Resizers
{
    public class GridResizer : Resizer
    {
        public new GridLayer Layer { get; private set; }

        public bool[,] oldGrid;

        public GridResizer(GridLayer gridLayer)
            : base(gridLayer)
        {
            Layer = gridLayer;
        }

        public override void Resize()
        {
            oldGrid = Layer.Grid;
            Layer.Grid = new bool[Layer.Level.Size.Width / Layer.Definition.Grid.Width, Layer.Level.Size.Height / Layer.Definition.Grid.Height];

            for (int i = 0; i < Layer.Grid.GetLength(0) && i < oldGrid.GetLength(0); i++)
                for (int j = 0; j < Layer.Grid.GetLength(1) && j < oldGrid.GetLength(1); j++)
                    Layer.Grid[i, j] = oldGrid[i, j];
        }

        public override void Undo()
        {
            Layer.Grid = oldGrid;
        }
    }
}
