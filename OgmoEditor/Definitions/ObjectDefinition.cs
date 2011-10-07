using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace OgmoEditor.Definitions
{
    public class ObjectDefinition
    {
        [XmlAttribute]
        public string Name;

        public Size Size;
        public Point Origin;
        public ObjectImageDefinition ImageDefinition;

        public ObjectDefinition()
        {
            
        }
    }

    [XmlRoot("Image")]
    public struct ObjectImageDefinition
    {
        public enum DrawModes { Rectangle, Image };

        [XmlAttribute]
        public DrawModes DrawMode;
        [XmlAttribute]
        public string ImagePath;
        [XmlAttribute]
        public bool Tiled;

        public Rectangle ClipRect;
    }
}
