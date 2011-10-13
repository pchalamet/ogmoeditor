using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridDrawAction : GridAction
    {
        public int CellX { get; private set; }
        public int CellY { get; private set; }

        public GridDrawAction(GridLayer gridLayer, int cellX, int cellY)
            : base(gridLayer)
        {
            CellX = cellX;
            CellY = cellY;
        }

        public override string ToString()
        {
            return "Draw" + base.ToString();
        }

        public override void Do()
        {
            GridLayer.Grid[CellX, CellY] = true;
        }

        public override void Undo()
        {
            GridLayer.Grid[CellX, CellY] = false;
        }
    }
}
