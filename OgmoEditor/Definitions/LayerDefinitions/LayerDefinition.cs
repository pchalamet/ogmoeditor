using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [Serializable()]
    public class LayerDefinition
    {
        public string Name;

        public LayerDefinition(string name)
        {
            Name = name;
        }
    }
}
