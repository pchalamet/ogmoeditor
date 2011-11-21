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
        public bool SetTo { get; private set; }

        private bool was;

        public GridDrawAction(GridLayer gridLayer, int cellX, int cellY, bool setTo)
            : base(gridLayer)
        {
            CellX = cellX;
            CellY = cellY;
            SetTo = setTo;
        }

        public override void Do()
        {
            base.Do();

            was = GridLayer.Grid[CellX, CellY];
            GridLayer.Grid[CellX, CellY] = SetTo;
        }

        public override void Undo()
        {
            base.Undo();

            GridLayer.Grid[CellX, CellY] = was;
        }
    }
}
