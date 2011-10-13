using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.TileTools
{
    public abstract class TileTool : Tool
    {
        public TileLayerEditor TileLayerEditor { get; private set; }

        public TileTool(string name, TileLayerEditor tileLayerEditor)
            : base(name)
        {
            TileLayerEditor = tileLayerEditor;
        }
    }
}
