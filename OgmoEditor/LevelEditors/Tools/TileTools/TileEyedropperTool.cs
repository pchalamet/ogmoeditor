using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TileEyedropperTool : TileTool
    {
        public TileEyedropperTool()
            : base("Eyedropper", "eyedropper.png")
        {

        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            Ogmo.TilePaletteWindow.Tiles = new int[] { LayerEditor.Layer.Tiles[location.X, location.Y] };
        }
    }
}
