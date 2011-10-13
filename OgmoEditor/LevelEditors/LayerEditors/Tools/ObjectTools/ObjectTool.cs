using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.ObjectTools
{
    public abstract class ObjectTool : Tool
    {
        public ObjectTool(string name)
            : base(name)
        {

        }

        public ObjectLayerEditor LayerEditor
        {
            get { return (ObjectLayerEditor)LevelEditor.LayerEditors[Ogmo.CurrentLayerIndex]; }
        }
    }
}
