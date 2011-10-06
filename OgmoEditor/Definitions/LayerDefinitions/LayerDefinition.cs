using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [XmlRoot("layer")]
    public class LayerDefinition
    {
        public string Name;
        public Size Grid;

        public LayerDefinition()
        {
            Name = "";
        }

        public LayerDefinition(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name + " - " + GetType().ToString();
        }
    }
}
