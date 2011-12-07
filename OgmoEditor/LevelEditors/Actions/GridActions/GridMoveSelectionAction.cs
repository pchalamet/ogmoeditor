using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridMoveSelectionAction : GridAction
    {
        private Point move;

        public GridMoveSelectionAction(GridLayer gridLayer, Point move)
            : base(gridLayer)
        {
            this.move = move;
        }
    }
}
