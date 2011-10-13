using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools
{
    public class GridPencilTool : GridTool
    {
        public GridPencilTool()
            : base("Grid Pencil")
        {

        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.WithinBounds(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);

            if (LayerEditor.Layer.Grid[location.X, location.Y])
                return;

            GridDrawAction action = new GridDrawAction(LayerEditor.Layer, location.X, location.Y);
            LevelEditor.Perform(action);
        }

        public override void OnMouseRightClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.WithinBounds(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);

            if (!LayerEditor.Layer.Grid[location.X, location.Y])
                return;

            GridEraseAction action = new GridEraseAction(LayerEditor.Layer, location.X, location.Y);
            LevelEditor.Perform(action);
        }
    }
}
