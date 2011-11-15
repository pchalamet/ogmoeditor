using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridSetSelectionAction : GridAction
    {
        private GridSelection selection;

        public GridSetSelectionAction(GridLayer gridLayer, GridSelection selection)
            : base(gridLayer)
        {
            this.selection = selection;
        }

        public override void Do()
        {
            GridLayer.Selection = selection;
        }

        public override void Undo()
        {
            GridLayer.Selection = null;
        }
    }
}
