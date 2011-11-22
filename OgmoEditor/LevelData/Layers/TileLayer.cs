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
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor.LevelData.Layers
{
    public class TileLayer : Layer
    {
        public new TileLayerDefinition Definition { get; private set; }
        public Tileset Tileset;
        public int[,] Tiles;
        public RenderTarget2D Texture;

        public TileLayer(Level level, TileLayerDefinition definition)
            : base(level, definition)
        {
            Definition = definition;
            Tileset = Ogmo.Project.Tilesets[0];

            Tiles = new int[Level.Size.Width / definition.Grid.Width, Level.Size.Height / definition.Grid.Height];
            Clear();

            RefreshTexture();
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
                        while (rows[i].LastIndexOf(",-1") == rows[i].Length - 3)
                            rows[i] = rows[i].Substring(0, rows[i].Length - 3);
                    }
                }

                //Throw it in the xml text
                xml.InnerText = string.Join("\n", rows);
            }
            else if (Definition.ExportMode == TileLayerDefinition.TileExportMode.XML)
            {

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
                int x = 0;
                int y = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == ',')
                        x++;
                    else if (s[i] == '\n')
                    {
                        x = 0;
                        y++;
                    }
                    else
                    {
                        int len = s.IndexOf(',', i) - i;
                        Tiles[x, y] = Convert.ToInt32(s.Substring(i, len));

                        x++;
                        i += (len - 1);
                    }
                }
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

            RefreshTexture();
        }

        public void RefreshTexture()
        {
            if (Texture != null)
                Texture.Dispose();
            Texture = new RenderTarget2D(Ogmo.GraphicsDevice, Level.Size.Width, Level.Size.Height);

            Ogmo.GraphicsDevice.SetRenderTarget((RenderTarget2D)Texture);
            Ogmo.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);

            Texture2D tiles = Ogmo.Content.TilesetTextures[Tileset];
            Ogmo.Content.SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.Opaque);
            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    if (Tiles[i, j] != -1)
                        Ogmo.Content.SpriteBatch.Draw(tiles, new Microsoft.Xna.Framework.Vector2(i * Definition.Grid.Width, j * Definition.Grid.Height), Tileset.GetXNARectFromID(Tiles[i, j]), Microsoft.Xna.Framework.Color.White);
                }
            }
            Ogmo.Content.SpriteBatch.End();

            Ogmo.GraphicsDevice.SetRenderTarget(null);
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
