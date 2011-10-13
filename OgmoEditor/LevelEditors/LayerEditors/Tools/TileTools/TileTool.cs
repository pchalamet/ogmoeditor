using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.TileTools
{
    public abstract class TileTool : Tool
    {
        public TileLayer TileLayer { get; private set; }

        public TileTool(string name, TileLayer tileLayer)
            : base(name)
        {
            TileLayer = tileLayer;
        }
    }
}
