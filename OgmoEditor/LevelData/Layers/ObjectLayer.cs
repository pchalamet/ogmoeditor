using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;

namespace OgmoEditor.LevelData.Layers
{
    public class ObjectLayer : Layer
    {
        public new ObjectLayerDefinition Definition { get; private set; }

        public ObjectLayer(ObjectLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
        }

        public ObjectLayer(ObjectLayerDefinition definition, XmlElement xml)
            : this(definition)
        {
            throw new NotImplementedException();
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
