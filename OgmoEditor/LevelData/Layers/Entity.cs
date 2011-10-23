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

            //Values
            if (def.ValueDefinitions.Count > 0)
            {
                Values = new List<Value>(def.ValueDefinitions.Count);
                foreach (var d in def.ValueDefinitions)
                    Values.Add(new Value(d));
            }
        }

        public Entity(XmlElement xml)
        {
            Definition = Ogmo.Project.EntityDefinitions.Find(d => d.Name == xml.Name);

            Position = new Point(Convert.ToInt32(xml.Attributes["x"].InnerText), Convert.ToInt32(xml.Attributes["y"].InnerText));

            if (Definition.ResizableX)
                Size.Width = Convert.ToInt32(xml.Attributes["width"].InnerText);
            else
                Size.Width = Definition.Size.Width;
            if (Definition.ResizableY)
                Size.Height = Convert.ToInt32(xml.Attributes["height"].InnerText);
            else
                Size.Height = Definition.Size.Height;

            if (Definition.Rotatable)
                Angle = Ogmo.Project.ImportAngle(xml.Attributes["angle"].InnerText);

            //Values
            if (Values != null)
                OgmoParse.ImportValues(Values, xml);
        }

        public Entity(Entity e)
        {
            Definition = e.Definition;

            Position = e.Position;
            Size = e.Size;
            Angle = e.Angle;

            //Values
            if (e.Values.Count > 0)
            {
                Values = new List<Value>(e.Values.Count);
                foreach (var v in e.Values)
                    Values.Add(new Value(v));
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

            //Size
            if (Definition.ResizableX)
            {
                a = doc.CreateAttribute("width");
                a.InnerText = Size.Width.ToString();
                xml.Attributes.Append(a);
            }
            if (Definition.ResizableY)
            {
                a = doc.CreateAttribute("height");
                a.InnerText = Size.Height.ToString();
                xml.Attributes.Append(a);
            }

            //Rotation
            if (Definition.Rotatable)
            {
                a = doc.CreateAttribute("angle");
                a.InnerText = Ogmo.Project.ExportAngle(Angle);
                xml.Attributes.Append(a);
            }

            //Get values
            if (Values != null)
                foreach (var v in Values)
                    xml.Attributes.Append(v.GetXML(doc));

            return xml;
        }

        public void Draw(Content content, bool current, float alpha)
        {
            content.DrawEntity(this, alpha);

            if (current)
                content.DrawHollowRect(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y - 1, Size.Width + 1, Size.Height + 1, Ogmo.EntitySelectionWindow.IsSelected(this) ? Microsoft.Xna.Framework.Color.Lime : Microsoft.Xna.Framework.Color.Yellow);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height); }
        }

        public Microsoft.Xna.Framework.Rectangle XNABounds
        {
            get { return new Microsoft.Xna.Framework.Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height); }
        }

        public Entity Clone()
        {
            return new Entity(this);
        }

    }
}
