using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class ObjectLayerDefinition : LayerDefinition
    {
        public ObjectLayerDefinition()
            : base()
        {

        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return null;
        }

        public override LayerDefinition Clone()
        {
            ObjectLayerDefinition def = new ObjectLayerDefinition();
            def.Name = Name;
            def.Grid = Grid;
            return def;
        }
    }
}
