using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("float")]
    public class FloatValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public float Default;
        [XmlAttribute]
        public float Min;
        [XmlAttribute]
        public float Max;
        [XmlAttribute]
        public float Inc;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            Inc = .1f;
        }
    }
}
