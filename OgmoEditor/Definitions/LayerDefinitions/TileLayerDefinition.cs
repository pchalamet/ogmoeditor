using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [Serializable()]
    public class TileLayerDefinition : LayerDefinition
    {
        public bool MultipleTilesets;

        public TileLayerDefinition(string name)
            : base(name)
        {
            MultipleTilesets = false;
        }
    }
}
