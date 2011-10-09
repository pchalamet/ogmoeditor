using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("float")]
    public class FloatValueDefinition : ValueDefinition
    {
        public enum UITypes { Field, Slider };

        [XmlAttribute]
        public float Default;
        [XmlAttribute]
        public float Min;
        [XmlAttribute]
        public float Max;
        [XmlAttribute]
        public float Round;
        [XmlAttribute]
        public UITypes UIType;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            Round = .1f;
            UIType = UITypes.Field;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new FloatValueEditor(this);
        }
    }
}
