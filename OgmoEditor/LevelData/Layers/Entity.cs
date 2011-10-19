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
        public List<Value> Values { get; private set; }

        public Entity(EntityDefinition def, Point position)
        {
            Definition = def;
            Position = position;

            Size = def.Size;
            Angle = 0;

            //Init values
            if (def.ValueDefinitions.Count > 0)
            {
                Values = new List<Value>(def.ValueDefinitions.Count);
                foreach (var d in def.ValueDefinitions)
                    Values.Add(new Value(d));
            }
        }

        public Entity(XmlElement xml)
            : this(
                Ogmo.Project.EntityDefinitions.Find(d => d.Name == xml.Name),
                new Point(Convert.ToInt32(xml.Attributes["x"].InnerText),
                Convert.ToInt32(xml.Attributes["y"].InnerText)))
        {
            //Set values
            foreach (XmlAttribute a in xml.Attributes)
            {
                Value v = Values.Find(val => val.Definition.Name == a.Name);
                if (v != null)
                    v.Content = a.InnerText;
            }
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

            //Get values
            foreach (var v in Values)
                xml.Attributes.Append(v.GetXML(doc));

            return xml;
        }

        public void Draw(Content content, bool current, float alpha)
        {
            content.DrawEntity(Definition, new Microsoft.Xna.Framework.Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height), alpha);

            if (current)
                content.DrawHollowRect(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y - 1, Size.Width + 1, Size.Height + 1, Ogmo.EntitySelectionWindow.IsSelected(this) ? Microsoft.Xna.Framework.Color.Lime : Microsoft.Xna.Framework.Color.Yellow);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height); }
        }

    }
}
