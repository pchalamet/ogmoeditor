using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.TileActions;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TileFloodTool : TileTool
    {
        public TileFloodTool()
            : base("Flood Fill", "flood.png")
        {

        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            if (LayerEditor.Layer.Tiles[location.X, location.Y] != Ogmo.TilePaletteWindow.Tile)
                LevelEditor.Perform(new TileFloodAction(LayerEditor.Layer, location, Ogmo.TilePaletteWindow.Tile));
        }

        public override void OnMouseRightClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            if (LayerEditor.Layer.Tiles[location.X, location.Y] != -1)
                LevelEditor.Perform(new TileFloodAction(LayerEditor.Layer, location, -1));
        }
    }
}
