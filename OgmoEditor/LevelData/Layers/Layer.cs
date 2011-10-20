using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelEditors;
using System.Drawing;
using OgmoEditor.LevelData.Resizers;

namespace OgmoEditor.LevelData.Layers
{
    public abstract class Layer
    {
        public LayerDefinition Definition { get; private set; }

        public Layer(LayerDefinition definition)
        {
            Definition = definition;
        }

        public abstract XmlElement GetXML(XmlDocument doc);
        public abstract void SetXML(XmlElement xml);
        public abstract LayerEditor GetEditor(LevelEditor editor);
        public abstract Resizer GetResizer();
    }
}
