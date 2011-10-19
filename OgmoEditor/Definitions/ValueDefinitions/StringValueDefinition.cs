using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("string")]
    public class StringValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public string Default;
        [XmlAttribute]
        public int MaxChars;
        [XmlAttribute]
        public bool MultiLine;

        public StringValueDefinition()
            : base()
        {
            Default = "";
            MaxChars = -1;
            MultiLine = false;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new StringValueDefinitionEditor(this);
        }

        public override ValueDefinition Clone()
        {
            StringValueDefinition def = new StringValueDefinition();
            def.Name = Name;
            def.Default = Default;
            def.MaxChars = MaxChars;
            def.MultiLine = MultiLine;
            return def;
        }

        public override string GetDefault()
        {
            return Default;
        }
    }
}
