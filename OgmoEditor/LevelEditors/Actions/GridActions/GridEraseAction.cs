using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridEraseAction : GridAction
    {
        public int CellX { get; private set; }
        public int CellY { get; private set; }

        public GridEraseAction(GridLayer gridLayer, int cellX, int cellY)
            : base(gridLayer)
        {
            CellX = cellX;
            CellY = cellY;
        }

        public override string ToString()
        {
            return "Erase" + base.ToString();
        }

        public override void Do()
        {
            GridLayer.Grid[CellX, CellY] = false;
        }

        public override void Undo()
        {
            GridLayer.Grid[CellX, CellY] = true;
        }
    }
}
