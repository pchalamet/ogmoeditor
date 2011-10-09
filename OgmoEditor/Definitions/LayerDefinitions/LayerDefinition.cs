using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [XmlInclude(typeof(GridLayerDefinition))]
    [XmlInclude(typeof(TileLayerDefinition))]
    [XmlInclude(typeof(ObjectLayerDefinition))]
    [XmlInclude(typeof(ShapeLayerDefinition))]

    public class LayerDefinition
    {
        static public readonly List<Type> LAYER_TYPES = new List<Type>(new Type[] { typeof(GridLayerDefinition), typeof(TileLayerDefinition), typeof(ObjectLayerDefinition) });
        static public readonly List<string> LAYER_NAMES = new List<string>(new string[] { "Grid", "Tiles", "Objects" });

        public string Name;
        public Size Grid;

        public LayerDefinition()
        {
            Name = "";
        }

        public override string ToString()
        {
            return Name + " - " + GetType().ToString();
        }
    }
}
