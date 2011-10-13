using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools
{
    public class GridPencilTool : GridTool
    {
        public GridPencilTool(GridLayerEditor gridLayerEditor)
            : base("Grid Pencil", gridLayerEditor)
        {

        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!GridLayerEditor.LevelEditor.Level.WithinBounds(location))
                return;

            location = GridLayerEditor.Layer.Definition.ConvertToGrid(location);

            if (GridLayerEditor.Layer.Grid[location.X, location.Y])
                return;

            GridDrawAction action = new GridDrawAction(GridLayerEditor.Layer, location.X, location.Y);
            GridLayerEditor.LevelEditor.Perform(action);
        }

        public override void OnMouseRightClick(System.Drawing.Point location)
        {
            if (!GridLayerEditor.LevelEditor.Level.WithinBounds(location))
                return;

            location = GridLayerEditor.Layer.Definition.ConvertToGrid(location);

            if (!GridLayerEditor.Layer.Grid[location.X, location.Y])
                return;

            GridEraseAction action = new GridEraseAction(GridLayerEditor.Layer, location.X, location.Y);
            GridLayerEditor.LevelEditor.Perform(action);
        }
    }
}
