using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.ObjectTools
{
    public abstract class ObjectTool : Tool
    {
        public ObjectLayer ObjectLayer { get; private set; }

        public ObjectTool(string name, ObjectLayer objectLayer)
            : base(name)
        {
            ObjectLayer = objectLayer;
        }
    }
}
