using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("string")]
    public class StringValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public string Default;
        [XmlAttribute]
        public int MaxChars;

        public StringValueDefinition()
            : base()
        {
            Default = "";
            MaxChars = -1;
        }
    }
}
