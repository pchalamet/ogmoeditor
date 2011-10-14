using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class ObjectLayerDefinition : LayerDefinition
    {
        public ObjectLayerDefinition()
            : base("object.png")
        {

        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return null;
        }

        public override LevelData.Layers.Layer GetInstance()
        {
            return new ObjectLayer(this);
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
