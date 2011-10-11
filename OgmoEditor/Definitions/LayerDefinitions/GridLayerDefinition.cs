using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.ProjectEditors.LayerEditors;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class GridLayerDefinition : LayerDefinition
    {
        public OgmoColor Color;

        public GridLayerDefinition()
            : base()
        {
            Color = new OgmoColor(0, 0, 0);
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new GridLayerEditor(this);
        }

        public override LayerDefinition Clone()
        {
            GridLayerDefinition def = new GridLayerDefinition();
            def.Name = Name;
            def.Grid = Grid;
            def.Color = Color;
            return def;
        }
    }
}
