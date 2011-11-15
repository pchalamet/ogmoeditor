using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.GridActions
{
    public class GridSelectAction : GridAction
    {
        private Rectangle area;

        public GridSelectAction(GridLayer gridLayer, Rectangle area)
            : base(gridLayer)
        {
            this.area = area;
        }

        public override void Do()
        {
            GridLayer.Selection = new GridSelection(GridLayer, area);
            for (int i = area.X; i < area.X + area.Width; i++)
                for (int j = area.Y; j < area.Y + area.Height; j++)
                    GridLayer.Grid[i, j] = false;
        }

        public override void Undo()
        {
            GridLayer.Selection.Paste();
            GridLayer.Selection = null;
        }
    }
}
