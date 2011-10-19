using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.ValueDefinitions;
using System.Xml;

namespace OgmoEditor.LevelData.Layers
{
    public class Value
    {
        public ValueDefinition Definition { get; private set; }
        public string val;

        public Value(ValueDefinition definition)
        {
            Definition = definition;
            val = definition.GetDefault();
        }

        public XmlAttribute GetXML(XmlDocument doc)
        {
            XmlAttribute xml = doc.CreateAttribute(Definition.Name);
            xml.InnerText = val;
            return xml;
        }

        public void SetXML(XmlAttribute xml)
        {
            val = xml.InnerText;
        }
    }
}
