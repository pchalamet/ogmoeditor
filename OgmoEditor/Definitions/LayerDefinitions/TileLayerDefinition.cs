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
    }
}
