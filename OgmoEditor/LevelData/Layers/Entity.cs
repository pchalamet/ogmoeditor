﻿using System;
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
        public EntityLayer Layer { get; private set; }
        public EntityDefinition Definition { get; private set; }
        public Point Position;
        public Size Size;
        public float Angle;
        public List<Value> Values { get; private set; }
        public List<Point> Nodes;
        public uint ID { get; private set; }

        private Bitmap bitmap;

        public Entity(EntityLayer layer, EntityDefinition def, Point position)
        {
            Layer = layer;
            Definition = def;
            ID = layer.GetNewEntityID();

            Position = position;
            Size = def.Size;
            Angle = 0;

            //Init the bitmap
            bitmap = Definition.GetBitmap();

            //Nodes
            if (def.NodesDefinition.Enabled)
                Nodes = new List<Point>();

            //Values
            if (def.ValueDefinitions.Count > 0)
            {
                Values = new List<Value>(def.ValueDefinitions.Count);
                foreach (var d in def.ValueDefinitions)
                    Values.Add(new Value(d));
            }
        }

        public Entity(EntityLayer layer, XmlElement xml)
        {
            Layer = layer;
            Definition = Ogmo.Project.EntityDefinitions.Find(d => d.Name == xml.Name);

            //Init the bitmap
            bitmap = Definition.GetBitmap();

            //ID
            if (xml.Attributes["id"] != null)
                ID = Convert.ToUInt32(xml.Attributes["id"].InnerText);
            else
                ID = layer.GetNewEntityID();                

            //Position
            Position = new Point(Convert.ToInt32(xml.Attributes["x"].InnerText), Convert.ToInt32(xml.Attributes["y"].InnerText));

            //Size
            if (Definition.ResizableX && xml.Attributes["width"] != null)
                Size.Width = Convert.ToInt32(xml.Attributes["width"].InnerText);
            else
                Size.Width = Definition.Size.Width;
            if (Definition.ResizableY && xml.Attributes["height"] != null)
                Size.Height = Convert.ToInt32(xml.Attributes["height"].InnerText);
            else
                Size.Height = Definition.Size.Height;

            //Rotation
            if (Definition.Rotatable && xml.Attributes["angle"] != null)
                Angle = Ogmo.Project.ImportAngle(xml.Attributes["angle"].InnerText);

            //Nodes
            if (Definition.NodesDefinition.Enabled)
            {
                Nodes = new List<Point>();
                foreach (XmlElement node in xml.GetElementsByTagName("node"))
                    Nodes.Add(new Point(Convert.ToInt32(node.Attributes["x"].InnerText), Convert.ToInt32(node.Attributes["y"].InnerText)));
            }

            //Values
            if (Definition.ValueDefinitions.Count > 0)
            {
                Values = new List<Value>(Definition.ValueDefinitions.Count);
                foreach (var d in Definition.ValueDefinitions)
                    Values.Add(new Value(d));
                OgmoParse.ImportValues(Values, xml);
            } 
        }

        public Entity(EntityLayer layer, Entity e)
        {
            Layer = layer;
            Definition = e.Definition;
            ID = layer.GetNewEntityID();

            Position = e.Position;
            Size = e.Size;
            Angle = e.Angle;

            //Nodes
            if (Definition.NodesDefinition.Enabled)
            {
                Nodes = new List<Point>();
                foreach (var p in e.Nodes)
                    Nodes.Add(p);
            }

            //Values
            if (Definition.ValueDefinitions.Count > 0)
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

            //ID
            a = doc.CreateAttribute("id");
            a.InnerText = ID.ToString();
            xml.Attributes.Append(a);

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

            //Nodes
            if (Nodes != null)
            {
                foreach (var p in Nodes)
                {
                    XmlElement node = doc.CreateElement("node");
                    a = doc.CreateAttribute("x");
                    a.InnerText = p.X.ToString();
                    node.Attributes.Append(a);
                    a = doc.CreateAttribute("y");
                    a.InnerText = p.Y.ToString();
                    node.Attributes.Append(a);
                    xml.AppendChild(node);
                }
            }

            //Values
            if (Values != null)
                foreach (var v in Values)
                    xml.Attributes.Append(v.GetXML(doc));

            return xml;
        }

        public void NewDraw(Graphics graphics, int alpha)
        {
            //Draw the actual entity
            if (Definition.ImageDefinition.Tiled && Definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                Rectangle drawTo = Rectangle.Empty;
                for (drawTo.X = 0; drawTo.X < Size.Width; drawTo.X += bitmap.Width)
                {
                    drawTo.Width = Math.Min(bitmap.Width, Size.Width - drawTo.X);
                    for (drawTo.Y = 0; drawTo.Y < Size.Height; drawTo.Y += bitmap.Height)
                    {
                        drawTo.Height = Math.Min(bitmap.Height, Size.Height - drawTo.Y);
                        graphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(drawTo.X + Position.X - Definition.Origin.X, drawTo.Y + Position.Y - Definition.Origin.Y, drawTo.Width, drawTo.Height));
                    }
                }
            }
            else
            {
                graphics.DrawImage(bitmap, new Rectangle(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y, Size.Width, Size.Height));
            }
        }

        public void Draw(float alpha)
        {
            Ogmo.EditorDraw.DrawEntity(this, alpha);

            if (Ogmo.EntitySelectionWindow.IsSelected(this))
                Ogmo.EditorDraw.DrawHollowRect(Position.X - Definition.Origin.X, Position.Y - Definition.Origin.Y - 1, Size.Width + 1, Size.Height + 1, Microsoft.Xna.Framework.Color.Lime);

            /*
             *  Draw nodes
             */
            if (Nodes != null)
            {
                if (Definition.NodesDefinition.Ghost)
                {
                    Point pp = Position;
                    foreach (var p in Nodes)
                    {
                        Position = p;
                        Ogmo.EditorDraw.DrawEntity(this, alpha * .35f);
                    }
                    Position = pp;
                }

                if (Definition.NodesDefinition.DrawMode == EntityNodesDefinition.PathMode.None)
                {
                    foreach (var p in Nodes)
                        Ogmo.EditorDraw.DrawNode(p);
                }
                else if (Definition.NodesDefinition.DrawMode == EntityNodesDefinition.PathMode.Path)
                {
                    if (Nodes.Count > 0)
                    {
                        Ogmo.EditorDraw.DrawLine(Position, Nodes[0], Microsoft.Xna.Framework.Color.Red * .6f);
                        Ogmo.EditorDraw.DrawNode(Nodes[0]);
                    }

                    for (int i = 1; i < Nodes.Count; i++)
                    {
                        Ogmo.EditorDraw.DrawLine(Nodes[i - 1], Nodes[i], Microsoft.Xna.Framework.Color.Red * .6f);
                        Ogmo.EditorDraw.DrawNode(Nodes[i]);
                    }
                }
                else if (Definition.NodesDefinition.DrawMode == EntityNodesDefinition.PathMode.Circuit)
                {
                    if (Nodes.Count > 0)
                    {
                        Ogmo.EditorDraw.DrawLine(Position, Nodes[0], Microsoft.Xna.Framework.Color.Red * .6f);
                        Ogmo.EditorDraw.DrawNode(Nodes[0]);
                    }

                    for (int i = 1; i < Nodes.Count; i++)
                    {
                        Ogmo.EditorDraw.DrawLine(Nodes[i - 1], Nodes[i], Microsoft.Xna.Framework.Color.Red * .6f);
                        Ogmo.EditorDraw.DrawNode(Nodes[i]);
                    }

                    if (Nodes.Count > 1)
                        Ogmo.EditorDraw.DrawLine(Nodes[Nodes.Count - 1], Position, Microsoft.Xna.Framework.Color.Red * .6f);
                }
                else if (Definition.NodesDefinition.DrawMode == EntityNodesDefinition.PathMode.Fan)
                {
                    foreach (var p in Nodes)
                    {
                        Ogmo.EditorDraw.DrawLine(Position, p, Microsoft.Xna.Framework.Color.Red * .6f);
                        Ogmo.EditorDraw.DrawNode(p);
                    }
                }
            }
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
            return new Entity(Layer, this);
        }

        public void MoveNodes(Point move)
        {
            if (Nodes != null)
                for (int i = 0; i < Nodes.Count; i++)
                    Nodes[i] = new Point(Nodes[i].X + move.X, Nodes[i].Y + move.Y);
        }

    }
}
