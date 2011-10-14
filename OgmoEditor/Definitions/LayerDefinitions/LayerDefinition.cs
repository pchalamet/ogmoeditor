using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Definitions.LayerDefinitions
{
    [XmlInclude(typeof(GridLayerDefinition))]
    [XmlInclude(typeof(TileLayerDefinition))]
    [XmlInclude(typeof(ObjectLayerDefinition))]

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

        public virtual UserControl GetEditor()
        {
            throw new NotImplementedException();
        }

        public virtual Layer GetInstance()
        {
            throw new NotImplementedException();
        }

        public virtual LayerDefinition Clone()
        {
            throw new Exception("LayerDefinition subclasses must override virtual method Clone!");
        }

        public Point ConvertToGrid(Point p)
        {
            return new Point(p.X / Grid.Width, p.Y / Grid.Height);
        }

        public Rectangle ConvertToGrid(Rectangle r)
        {
            return new Rectangle(r.X / Grid.Width, r.Y / Grid.Height, r.Width / Grid.Width, r.Height / Grid.Height);
        }

        public Point SnapToGrid(Point p)
        {
            return new Point((p.X / Grid.Width) * Grid.Width, (p.Y / Grid.Height) * Grid.Height);
        }
    }
}
