using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class GridLayerDefinition : LayerDefinition
    {
        public Color Color;

        public GridLayerDefinition()
            : base()
        {
            Color = Color.Black;
        }
    }
}
