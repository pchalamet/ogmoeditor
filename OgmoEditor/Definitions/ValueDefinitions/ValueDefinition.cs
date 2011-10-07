using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlInclude(typeof(BoolValueDefinition))]
    [XmlInclude(typeof(EnumValueDefinition))]
    [XmlInclude(typeof(FloatValueDefinition))]
    [XmlInclude(typeof(IntValueDefinition))]
    [XmlInclude(typeof(StringValueDefinition))]

    public class ValueDefinition
    {
        [XmlAttribute]
        public string Name;

        public ValueDefinition()
        {
            Name = "";
        }
    }
}
