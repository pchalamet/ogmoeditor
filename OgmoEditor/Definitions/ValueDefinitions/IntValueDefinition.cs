using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("int")]
    public class IntValueDefinition : ValueDefinition
    {
        public enum UITypes { Field, Slider };

        [XmlAttribute]
        public int Default;
        [XmlAttribute]
        public int Min;
        [XmlAttribute]
        public int Max;
        [XmlAttribute]
        public UITypes UIType;

        public IntValueDefinition()
            : base()
        {
            Default = 0;
            Min = int.MinValue;
            Max = int.MaxValue;
            UIType = UITypes.Field;
        }

    }
}
