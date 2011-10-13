using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.TileTools
{
    public abstract class TileTool : Tool
    {
        public TileTool(string name)
            : base(name)
        {

        }

        public TileLayerEditor LayerEditor
        {
            get { return (TileLayerEditor)LevelEditor.LayerEditors[Ogmo.CurrentLayerIndex]; }
        }
    }
}
