using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class BoolValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public bool Default;

        public BoolValueDefinition()
            : base()
        {
            Default = false;
        }
    }
}
