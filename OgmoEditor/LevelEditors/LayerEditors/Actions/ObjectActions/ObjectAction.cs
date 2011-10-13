using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Actions.ObjectActions
{
    public abstract class ObjectAction : Action
    {
        public new ObjectLayer Layer { get; private set; }

        public ObjectAction(ObjectLayer layer)
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
