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
        public int[,] Tiles;

        public TileLayer(TileLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
            Tileset = Ogmo.Project.Tilesets[0];
            Tiles = new int[Ogmo.Project.LevelDefaultSize.Width / definition.Grid.Width, Ogmo.Project.LevelDefaultSize.Height / definition.Grid.Height];
            Clear();
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            return doc.CreateElement(Definition.Name);
        }

        public override void SetXML(XmlElement xml)
        {
            Clear();
            if (Definition.ExportMode == TileLayerDefinition.TileExportMode.CSV)
            {

            }
            else if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML)
            {

            }
        }

        public void Clear()
        {
            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    Tiles[i, j] = -1;
                }
            }
        }

        public override LayerEditor GetEditor(LevelEditors.LevelEditor editor)
        {
            return new TileLayerEditor(editor, this);
        }

        public override Resizer GetResizer()
        {
            return new TileResizer(this);
        }
    }
}
