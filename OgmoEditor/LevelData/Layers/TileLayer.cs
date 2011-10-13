using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;

namespace OgmoEditor.LevelData.Layers
{
    public class TileLayer : Layer
    {
        public new TileLayerDefinition Definition { get; private set; }

        public TileLayer(TileLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
        }

        public TileLayer(TileLayerDefinition definition, XmlElement xml)
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
