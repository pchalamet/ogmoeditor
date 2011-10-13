using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools
{
    public abstract class GridTool : Tool
    {
        public GridLayer GridLayer { get; private set; }

        public GridTool(string name, GridLayer gridLayer)
            : base(name)
        {
            GridLayer = gridLayer;
        }
    }
}
