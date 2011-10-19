using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;

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
        public float Round;
        [XmlAttribute]
        public NumberUITypes UIType;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            Round = .1f;
            UIType = NumberUITypes.Field;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new FloatValueDefinitionEditor(this);
        }

        public override ValueDefinition Clone()
        {
            FloatValueDefinition def = new FloatValueDefinition();
            def.Name = Name;
            def.Default = Default;
            def.Min = Min;
            def.Max = Max;
            def.Round = Round;
            def.UIType = UIType;
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }
    }
}
