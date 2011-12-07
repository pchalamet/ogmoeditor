using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelEditors.Resizers;
using System.Drawing;
using OgmoEditor.Definitions;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using OgmoEditor.LevelEditors.LayersEditors;

namespace OgmoEditor.LevelData.Layers
{
    public class TileLayer : Layer
    {
        public new TileLayerDefinition Definition { get; private set; }
        public Tileset Tileset;
        public int[,] Tiles;
        public TileCanvas TileCanvas { get; private set; }

        public TileLayer(Level level, TileLayerDefinition definition)
            : base(level, definition)
        {
            Definition = definition;
            Tileset = Ogmo.Project.Tilesets[0];

            Tiles = new int[Level.Size.Width / definition.Grid.Width, Level.Size.Height / definition.Grid.Height];
            Clear();

            InitCanvas();
        }

        public void InitCanvas()
        {
            TileCanvas = new TileCanvas(this);
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            XmlElement xml = doc.CreateElement(Definition.Name);

            if (Definition.ExportMode == TileLayerDefinition.TileExportMode.CSV || Definition.ExportMode == TileLayerDefinition.TileExportMode.TrimmedCSV)
            {
                //Convert all tile values to CSV
                string[] rows = new string[Tiles.GetLength(1)];
                for (int i = 0; i < Tiles.GetLength(1); i++)
                {
                    string[] tiles = new string[Tiles.GetLength(0)];
                    for (int j = 0; j < Tiles.GetLength(0); j++)
                        tiles[j] = Tiles[j, i].ToString();
                    rows[i] = string.Join(",", tiles);
                }

                //Trim off trailing empties
                if (Definition.ExportMode == TileLayerDefinition.TileExportMode.TrimmedCSV)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        int index = rows[i].LastIndexOf(",-1");
                        while (index != -1 && index == rows[i].Length - 3)
                        {
                            rows[i] = rows[i].Substring(0, rows[i].Length - 3);
                            index = rows[i].LastIndexOf(",-1");
                        }
                        if (rows[i] == "-1")
                            rows[i] = "";
                    }
                }

                //Throw it in the xml text
                xml.InnerText = string.Join("\n", rows);
            }
            else if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML)
            {
                //XML Export
                XmlElement tile;
                XmlAttribute a;
                for (int i = 0; i < Tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < Tiles.GetLength(1); j++)
                    {
                        if (Tiles[i, j] != -1)
                        {
                            tile = doc.CreateElement("tile");

                            a = doc.CreateAttribute("x");
                            a.InnerText = i.ToString();
                            tile.Attributes.Append(a);

                            a = doc.CreateAttribute("y");
                            a.InnerText = j.ToString();
                            tile.Attributes.Append(a);

                            a = doc.CreateAttribute("id");
                            a.InnerText = Tiles[i, j].ToString();
                            tile.Attributes.Append(a);

                            xml.AppendChild(tile);
                        }
                    }
                }              
            }

            return xml;
        }

        public override void SetXML(XmlElement xml)
        {
            Clear();
            if (Definition.ExportMode == TileLayerDefinition.TileExportMode.CSV || Definition.ExportMode == TileLayerDefinition.TileExportMode.TrimmedCSV)
            {
                //CSV Import
                string s = xml.InnerText;

                string[] rows = s.Split('\n');
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] tiles = rows[i].Split(',');
                    if (tiles[0] != "")
                        for (int j = 0; j < tiles.Length; j++)
                            Tiles[j, i] = Convert.ToInt32(tiles[j]);
                }
            }
            else if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML)
            {
                //XML Import
                foreach (XmlElement tile in xml)
                {
                    int x = Convert.ToInt32(tile.Attributes["x"].InnerText);
                    int y = Convert.ToInt32(tile.Attributes["y"].InnerText);
                    int id = Convert.ToInt32(tile.Attributes["id"].InnerText);
                    Tiles[x, y] = id;
                }
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

        public int TileCellsX
        {
            get { return Tiles.GetLength(0); }
        }

        public int TileCellsY
        {
            get { return Tiles.GetLength(1); }
        }
    }
}
