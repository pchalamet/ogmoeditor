using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.ProjectEditors.LayerDefinitionEditors;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    public class TileLayerDefinition : LayerDefinition
    {
        public enum TileExportMode { CSV, XML };
        public TileExportMode ExportMode;

        public TileLayerDefinition()
            : base()
        {
            Image = "tile.png";
            ExportMode = TileExportMode.CSV;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new TileLayerDefinitionEditor(this);
        }

        public override LevelData.Layers.Layer GetInstance()
        {
            return new TileLayer(this);
        }

        public override LayerDefinition Clone()
        {
            TileLayerDefinition def = new TileLayerDefinition();
            def.Name = Name;
            def.Grid = Grid;
            def.ExportMode = ExportMode;
            return def;
        }
    }
}
