using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using OgmoEditor.Definitions.ValueDefinitions;

namespace OgmoEditor.Definitions
{
    public class ObjectDefinition
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public bool ResizableX;
        [XmlAttribute]
        public bool ResizableY;
        [XmlAttribute]
        public bool Rotatable;
        [XmlAttribute]
        public float RotateIncrement;

        public Size Size;
        public Point Origin;
        public ObjectImageDefinition ImageDefinition;
        public List<ValueDefinition> ValueDefinitions;
        public ObjectNodesDefinition NodesDefinition;

        public ObjectDefinition()
        {
            Limit = -1;
            Size = new Size(16, 16);
            RotateIncrement = 15;

            ValueDefinitions = new List<ValueDefinition>();

            ImageDefinition.ImagePath = "";
            ImageDefinition.RectColor = new OgmoColor(255, 0, 0);
            ImageDefinition.ClipRect = new Rectangle(0, 0, 16, 16);

            NodesDefinition.Limit = -1;
        }

        public ObjectDefinition Clone()
        {
            ObjectDefinition def = new ObjectDefinition();
            def.Name = Name;
            def.Limit = Limit;
            def.ResizableX = ResizableX;
            def.ResizableY = ResizableY;
            def.Rotatable = Rotatable;
            def.RotateIncrement = RotateIncrement;
            def.Size = Size;
            def.Origin = Origin;
            def.ImageDefinition = ImageDefinition;
            def.ValueDefinitions = new List<ValueDefinition>();
            foreach (var d in ValueDefinitions)
                def.ValueDefinitions.Add(d.Clone());
            return def;
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

        public OgmoColor RectColor;
        public Rectangle ClipRect;
    }

    [XmlRoot("Nodes")]
    public struct ObjectNodesDefinition
    {
        public enum PathMode { None, Path, Circuit, Fan };

        [XmlAttribute]
        public bool Enabled;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public PathMode DrawMode;
    }
}
