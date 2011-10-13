using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Actions
{
    public abstract class Action
    {
        public Layer Layer { get; private set; }

        public Action(Layer layer)
        {
            Layer = layer;
        }

        public abstract void Do();
        public abstract void Undo();

        public override string ToString()
        {
            return "On " + Layer.Definition.Name + ": ";
        }
    }
}
