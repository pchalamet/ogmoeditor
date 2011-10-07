using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
