using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.ValueEditors;

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
        public bool ShowSlider;

        public IntValueDefinition()
            : base()
        {
            Default = 0;
            Min = 0;
            Max = 100;
            ShowSlider = false;
        }

        public override UserControl GetEditor()
        {
            return new IntValueDefinitionEditor(this);
        }

        public override ValueEditor GetInstanceEditor(Value instance, int x, int y)
        {
            return new IntValueEditor(instance, x, y);
        }

        public override ValueDefinition Clone()
        {
            IntValueDefinition def = new IntValueDefinition();
            def.Name = Name;
            def.Default = Default;
            def.Min = Min;
            def.Max = Max;
            def.ShowSlider = ShowSlider;
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }

    }
}
