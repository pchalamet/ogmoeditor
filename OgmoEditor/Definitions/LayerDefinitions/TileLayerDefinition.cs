using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.ProjectEditors.LayerEditors;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class TileLayerDefinition : LayerDefinition
    {
        public bool MultipleTilesets;

        public TileLayerDefinition()
            : base()
        {
            MultipleTilesets = false;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new TileLayerEditor(this);
        }

        public override LayerDefinition Clone()
        {
            TileLayerDefinition def = new TileLayerDefinition();
            def.Name = Name;
            def.Grid = Grid;
            def.MultipleTilesets = MultipleTilesets;
            return def;
        }
    }
}
