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
        public TileSelection Selection;

        public TileLayer(Level level, TileLayerDefinition definition)
            : base(level, definition)
        {
            Definition = definition;
            Tileset = Ogmo.Project.Tilesets[0];

            Tiles = new int[Math.Max(Level.Size.Width / definition.Grid.Width, 1), Math.Max(Level.Size.Height / definition.Grid.Height, 1)];
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

            //Save which tileset is being used for this layer
            XmlAttribute tileset = doc.CreateAttribute("tileset");
            tileset.InnerText = Tileset.Name;
            xml.Attributes.Append(tileset);

            //Save the export mode
            XmlAttribute export = doc.CreateAttribute("exportMode");
            export.InnerText = Definition.ExportMode.ToString();
            xml.Attributes.Append(export);

            if (Definition.ExportMode == TileLayerDefinition.TileExportMode.CSV || Definition.ExportMode == TileLayerDefinition.TileExportMode.TrimmedCSV)
            {
                //Convert all tile values to CSV
                string[] rows = new string[Tiles.GetLength(1)];
                for (int i = 0; i < Tiles.GetLength(1); i++)
                {
                    string[] tiles = new string[Tiles.GetLength(0)];
                    for (int j = 0; j < Tiles.GetLength(0); j++)
                    {
                        tiles[j] = Tiles[j, i].ToString();
                    }
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
            else if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML || Definition.ExportMode == TileLayerDefinition.TileExportMode.XMLCoords)
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

                            if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML)
                            {
                                a = doc.CreateAttribute("id");
                                a.InnerText = Tiles[i, j].ToString();
                                tile.Attributes.Append(a);
                            }
                            else
                            {
                                Point p = Tileset.GetCellFromID(Tiles[i, j]);

                                a = doc.CreateAttribute("tx");
                                a.InnerText = p.X.ToString();
                                tile.Attributes.Append(a);

                                a = doc.CreateAttribute("ty");
                                a.InnerText = p.Y.ToString();
                                tile.Attributes.Append(a);
                            }

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

            //Load the tileset
            string tilesetName = xml.Attributes["tileset"].InnerText;
            Tileset = Ogmo.Project.Tilesets.Find(t => t.Name == tilesetName);

            //Get the export mode
            TileLayerDefinition.TileExportMode exportMode;
            if (xml.Attributes["exportMode"] != null)
                exportMode = (TileLayerDefinition.TileExportMode)Enum.Parse(typeof(TileLayerDefinition.TileExportMode), xml.Attributes["exportMode"].InnerText);
            else
                exportMode = Definition.ExportMode;

            if (exportMode == TileLayerDefinition.TileExportMode.CSV || exportMode == TileLayerDefinition.TileExportMode.TrimmedCSV)
            {
                //CSV Import
                string s = xml.InnerText;

                string[] rows = s.Split('\n');
                if (rows.Length > Tiles.GetLength(1)) Array.Resize(ref rows, Tiles.GetLength(1));
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] tiles = rows[i].Split(',');
                    if (tiles.Length > Tiles.GetLength(0)) Array.Resize(ref tiles, Tiles.GetLength(0));
                    if (tiles[0] != "")
                        for (int j = 0; j < tiles.Length; j++)
                            Tiles[j, i] = Convert.ToInt32(tiles[j]);
                }
            }
            else if (exportMode == TileLayerDefinition.TileExportMode.XML || exportMode == TileLayerDefinition.TileExportMode.XMLCoords)
            {
                //XML Import
                foreach (XmlElement tile in xml)
                {
                    int x = Convert.ToInt32(tile.Attributes["x"].InnerText);
                    int y = Convert.ToInt32(tile.Attributes["y"].InnerText);

                    if (tile.Attributes["id"] != null)
                    {
                        int id = Convert.ToInt32(tile.Attributes["id"].InnerText);
                        Tiles[x, y] = id;
                    }
                    else if (tile.Attributes["tx"] != null && tile.Attributes["ty"] != null)
                    {
                        int tx = Convert.ToInt32(tile.Attributes["tx"].InnerText);
                        int ty = Convert.ToInt32(tile.Attributes["ty"].InnerText);
                        Tiles[x, y] = Tileset.GetIDFromCell(new Point(tx, ty));
                    }
                }
            }

            TileCanvas.RefreshAll();
        }

        public Rectangle GetTilesRectangle(Point start, Point end)
        {
            Rectangle r = new Rectangle();

            //Get the rectangle
            r.X = Math.Min(start.X, end.X);
            r.Y = Math.Min(start.Y, end.Y);
            r.Width = Math.Abs(end.X - start.X) + Definition.Grid.Width;
            r.Height = Math.Abs(end.Y - start.Y) + Definition.Grid.Height;

            //Enforce Bounds
            if (r.X < 0)
            {
                r.Width += r.X;
                r.X = 0;
            }

            if (r.Y < 0)
            {
                r.Height += r.Y;
                r.Y = 0;
            }

            int width = Tiles.GetLength(0) * Definition.Grid.Width;
            int height = Tiles.GetLength(1) * Definition.Grid.Height;

            if (r.X + r.Width > width)
                r.Width = width - r.X;

            if (r.Y + r.Height > height)
                r.Height = height - r.Y;

            return r;
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
