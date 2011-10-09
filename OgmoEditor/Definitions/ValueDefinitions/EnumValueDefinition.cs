using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class EnumValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        string[] Values;
        [XmlAttribute]
        int DefaultIndex;

        public EnumValueDefinition()
            : base()
        {
            Values = new string[] { "default" };
            DefaultIndex = 0;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new EnumValueEditor(this);
        }
    }
}
