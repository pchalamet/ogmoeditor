using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class BoolValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public bool Default;

        public BoolValueDefinition()
            : base()
        {
            Default = false;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new BoolValueDefinitionEditor(this);
        }

        public override ValueDefinition Clone()
        {
            BoolValueDefinition def = new BoolValueDefinition();
            def.Name = Name;
            def.Default = Default;           
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }
    }
}
