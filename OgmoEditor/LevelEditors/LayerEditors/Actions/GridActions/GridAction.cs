using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Actions.GridActions
{
    public abstract class GridAction : Action
    {
        public new GridLayer Layer { get; private set; }

        public GridAction(GridLayer layer)
            : base(layer)
        {
            Layer = layer;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
