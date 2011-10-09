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
        public string[] Elements;

        public EnumValueDefinition()
            : base()
        {
            Elements = new string[] { "default" };
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new EnumValueEditor(this);
        }
    }
}
