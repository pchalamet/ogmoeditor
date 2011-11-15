using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridClearSelectionAction : GridAction
    {
        private GridSelection selection;

        public GridClearSelectionAction(GridLayer gridLayer)
            : base(gridLayer)
        {

        }

        public override void Do()
        {
            selection = GridLayer.Selection;
            selection.Paste();
            GridLayer.Selection = null;
        }

        public override void Undo()
        {
            GridLayer.Selection = selection;
            for (int i = selection.Position.X; i < selection.Position.X + selection.Width; i++)
                for (int j = selection.Position.Y; j < selection.Position.Y + selection.Height; j++)
                    GridLayer.Grid[i, j] = false;
        }
    }
}
