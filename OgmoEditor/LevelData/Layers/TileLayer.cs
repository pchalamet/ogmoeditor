using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelData.Resizers;
using System.Drawing;
using OgmoEditor.Definitions;

namespace OgmoEditor.LevelData.Layers
{
    public class TileLayer : Layer
    {
        public new TileLayerDefinition Definition { get; private set; }
        public Tileset Tileset;

        public TileLayer(TileLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
            Tileset = Ogmo.Project.Tilesets[0];
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            return doc.CreateElement(Definition.Name);
        }

        public override void SetXML(XmlElement xml)
        {
            
        }

        public override LayerEditor GetEditor(LevelEditors.LevelEditor editor)
        {
            return new TileLayerEditor(editor, this);
        }

        public override Resizer GetResizer()
        {
            return null;
        }
    }
}
