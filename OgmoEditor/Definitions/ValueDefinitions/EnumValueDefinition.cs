using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class EnumValueDefinition : ValueDefinition
    {
        [XmlAttribute]
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
    }
}
