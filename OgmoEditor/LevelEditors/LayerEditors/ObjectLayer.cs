using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class ObjectLayerEditor : LayerEditor
    {
        public new ObjectLayer Layer { get; private set; }

        public ObjectLayerEditor(LevelEditor levelEditor, ObjectLayer layer)
            : base(levelEditor, layer)
        {
            Layer = layer;
        }
    }
}
