using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using OgmoEditor.Definitions;
using System.Drawing;
using System.Xml;
using OgmoEditor.LevelEditors;

namespace OgmoEditor.LevelData.Layers
{
    public class Entity
    {
        public EntityDefinition Definition { get; private set; }
        public Point Position;
        public Size Size;
        public float Angle;

        public Entity(EntityDefinition def, Point position)
        {
            Definition = def;
            Position = position;

            Size = def.Size;
            Angle = 0;
        }

        public Entity(XmlElement xml)
            : this(
                Ogmo.Project.EntityDefinitions.Find(d => d.Name == xml.Name),
                new Point(Convert.ToInt32(xml.Attributes["x"].InnerText),
                Convert.ToInt32(xml.Attributes["y"].InnerText)))
        {

        }

        public XmlElement GetXML(XmlDocument doc)
        {
            XmlElement xml = doc.CreateElement(Definition.Name);
            XmlAttribute a;

            //Position
            a = doc.CreateAttribute("x");
            a.InnerText = Position.X.ToString();
            xml.Attributes.Append(a);
            a = doc.CreateAttribute("y");
            a.InnerText = Position.Y.ToString();
            xml.Attributes.Append(a);

            return xml;
        }

        public void Draw(Content content, float alpha)
        {
            content.DrawEntity(Definition, new Microsoft.Xna.Framework.Rectangle(Position.X, Position.Y, Size.Width, Size.Height), alpha);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height); }
        }

    }
}
