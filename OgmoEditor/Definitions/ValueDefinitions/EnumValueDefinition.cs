using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class EnumValueDefinition : ValueDefinition
    {
        public string[] Elements;

        public EnumValueDefinition()
            : base()
        {
            Elements = new string[] { "default" };
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new EnumValueDefinitionEditor(this);
        }

        public override LevelEditors.ValueEditors.ValueEditor GetInstanceEditor(Value instance, int x, int y)
        {
            return new EnumValueEditor(instance, x, y);
        }

        public override ValueDefinition Clone()
        {
            EnumValueDefinition def = new EnumValueDefinition();
            def.Name = Name;
            def.Elements = (string[])Elements.Clone();
            return def;
        }

        public override string GetDefault()
        {
            return Elements[0];
        }

        public override string ToString()
        {
            return Name + " (enum)";
        }
    }
}
