using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [Serializable()]
    public class GridLayerDefinition : LayerDefinition
    {
        public Color Color;

        public GridLayerDefinition(string name)
            : base(name)
        {
      
        }
    }
}
