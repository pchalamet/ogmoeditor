using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;
using System.Windows.Forms;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("int")]
    public class IntValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public int Default;
        [XmlAttribute]
        public int Min;
        [XmlAttribute]
        public int Max;
        [XmlAttribute]
        public NumberUITypes UIType;

        public IntValueDefinition()
            : base()
        {
            Default = 0;
            Min = int.MinValue;
            Max = int.MaxValue;
            UIType = NumberUITypes.Field;
        }

        public override UserControl GetEditor()
        {
            return new IntValueDefinitionEditor(this);
        }

        public override ValueDefinition Clone()
        {
            IntValueDefinition def = new IntValueDefinition();
            def.Name = Name;
            def.Default = Default;
            def.Min = Min;
            def.Max = Max;
            def.UIType = UIType;
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }

    }
}
