using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;

namespace OgmoEditor.LevelData.Layers
{
    public class GridLayer : Layer
    {
        public new GridLayerDefinition Definition { get; private set; }

        public GridLayer(GridLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
        }

        public GridLayer(GridLayerDefinition definition, XmlElement xml)
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
