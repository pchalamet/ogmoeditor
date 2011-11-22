using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelData.Resizers
{
    public abstract class Resizer
    {
        public Layer Layer { get; private set; }

        public Resizer(Layer layer)
        {
            Layer = layer;
        }

        public abstract void Resize();
        public abstract void Undo();
    }
}
