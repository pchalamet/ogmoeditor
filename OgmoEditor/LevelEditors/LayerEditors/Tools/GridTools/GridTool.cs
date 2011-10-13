using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools
{
    public abstract class GridTool : Tool
    {
        public GridLayerEditor GridLayerEditor { get; private set; }

        public GridTool(string name, GridLayerEditor gridLayerEditor)
            : base(name)
        {
            GridLayerEditor = gridLayerEditor;
        }
    }
}
