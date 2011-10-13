using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.ProjectEditors.LayerDefinitionEditors;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class GridLayerDefinition : LayerDefinition
    {
        public OgmoColor Color;
        public bool ExportAsRectangles;

        public GridLayerDefinition()
            : base()
        {
            Color = new OgmoColor(0, 0, 0);
            ExportAsRectangles = false;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new GridLayerDefinitionEditor(this);
        }

        public override LevelData.Layers.Layer GetInstance()
        {
            return new GridLayer(this);
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
