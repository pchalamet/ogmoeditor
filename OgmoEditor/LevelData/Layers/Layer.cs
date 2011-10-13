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

namespace OgmoEditor.LevelData.Layers
{
    public class Layer
    {
        public LayerDefinition Definition { get; private set; }

        public Layer(LayerDefinition definition)
        {
            Definition = definition;
        }

        public virtual XmlElement GetXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public virtual void SetXML(XmlElement xml)
        {
            throw new NotImplementedException();
        }

        public virtual LayerEditor GetEditor(LevelEditor editor)
        {
            throw new NotImplementedException();
        }
    }
}
