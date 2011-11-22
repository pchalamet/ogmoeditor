using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.TileActions;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TilePencilTool : TileTool
    {
        public TilePencilTool()
            : base("Pencil", "pencil.png", System.Windows.Forms.Keys.P)
        {

        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            LevelEditor.Perform(new TileDrawAction(LayerEditor.Layer, location, Ogmo.TilePaletteWindow.Tile));
            LayerEditor.Layer.RefreshTexture();
        }
    }
}
