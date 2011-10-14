using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools.ObjectTools
{
    public abstract class ObjectTool : Tool
    {
        public ObjectTool(string name, string image, Keys hotkey)
            : base(name, image, hotkey)
        {

        }

        public ObjectLayerEditor LayerEditor
        {
            get { return (ObjectLayerEditor)LevelEditor.LayerEditors[Ogmo.CurrentLayerIndex]; }
        }
    }
}
