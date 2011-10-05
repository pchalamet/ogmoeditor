using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [Serializable()]
    public class LayerDefinition
    {
        public string Name;
        public Size Grid;

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
