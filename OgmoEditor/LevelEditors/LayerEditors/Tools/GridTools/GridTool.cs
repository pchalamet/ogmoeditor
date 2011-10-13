using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools
{
    public abstract class GridTool : Tool
    {
        public GridTool(string name)
            : base(name)
        {

        }

        public GridLayerEditor LayerEditor
        {
            get { return (GridLayerEditor)LevelEditor.LayerEditors[Ogmo.CurrentLayerIndex]; }
        }
    }
}
