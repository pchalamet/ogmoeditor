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
        public bool ShowSlider;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            Round = .1f;
            ShowSlider = false;
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
            def.ShowSlider = ShowSlider;
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }
    }
}
