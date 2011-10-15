using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.LayerEditors;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public abstract class TileTool : Tool
    {
        public TileTool(string name, string image, Keys hotkey)
            : base(name, image, hotkey)
        {

        }

        public TileLayerEditor LayerEditor
        {
            get { return (TileLayerEditor)LevelEditor.LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex]; }
        }
    }
}
