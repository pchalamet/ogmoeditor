using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.ProjectEditors.LayerDefinitionEditors;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelData;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class GridLayerDefinition : LayerDefinition
    {
        public enum ExportModes { Bitstring, Rectangles };

        public OgmoColor Color;
        public ExportModes ExportMode;
        public bool TrimZeroes;

        public GridLayerDefinition()
            : base()
        {
            Image = "grid.png";
            Color = new OgmoColor(0, 0, 0);
            ExportMode = ExportModes.Bitstring;
            TrimZeroes = false;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new GridLayerDefinitionEditor(this);
        }

        public override LevelData.Layers.Layer GetInstance(Level level)
        {
            return new GridLayer(level, this);
        }

        public override LayerDefinition Clone()
        {
            GridLayerDefinition def = new GridLayerDefinition();
            def.Name = Name;
            def.Grid = Grid;
            def.ScrollFactor = ScrollFactor;
            def.Color = Color;
            def.ExportMode = ExportMode;
            def.TrimZeroes = TrimZeroes;
            return def;
        }
    }
}
