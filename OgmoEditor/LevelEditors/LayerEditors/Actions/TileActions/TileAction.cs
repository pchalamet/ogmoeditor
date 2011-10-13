using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Actions.TileActions
{
    public abstract class TileAction : Action
    {
        public new TileLayer Layer { get; private set; }

        public TileAction(TileLayer layer)
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
