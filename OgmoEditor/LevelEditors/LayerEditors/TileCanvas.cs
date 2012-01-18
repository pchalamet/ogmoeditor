using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayersEditors
{
    public class TileCanvas : IDisposable
    {
        private const int MAX_TEXTURE_SIZE = 2048;

        public TileLayer TileLayer { get; private set; }
        private List<TextureInfo> Textures;

        public TileCanvas(TileLayer tileLayer)
        {
            TileLayer = tileLayer;

            //Init the textures array
            Size texSize = new Size(MAX_TEXTURE_SIZE, MAX_TEXTURE_SIZE);

            Size maxSize = new Size(TileLayer.Definition.Grid.Width * TileLayer.TileCellsX, TileLayer.Definition.Grid.Height * TileLayer.TileCellsY);

            // TODO: Clip the dimensions of the tiles that draw over the edges of the level.
            Textures = new List<TextureInfo>();
            for (int i = 0; i < maxSize.Width; i += texSize.Width)
            {
                for (int j = 0; j < maxSize.Height; j += texSize.Height)
                {
                    RenderTarget2D tex = new RenderTarget2D(
                        Ogmo.EditorDraw.GraphicsDevice, 
                        Math.Min(maxSize.Width - i, texSize.Width), 
                        Math.Min(maxSize.Height - j, texSize.Height));
                    Textures.Add(new TextureInfo(tex, new Point(i, j)));
                }
            }

            RefreshAll();
        }

        public void Dispose()
        {
            foreach (var t in Textures)
                t.Texture.Dispose();
        }

        public void Draw(float alpha)
        {
            Microsoft.Xna.Framework.Rectangle destRect = new Microsoft.Xna.Framework.Rectangle();
            Microsoft.Xna.Framework.Rectangle sourceRect = new Microsoft.Xna.Framework.Rectangle();
            foreach (var t in Textures)
            {
                destRect.X = t.Position.X;
                destRect.Y = t.Position.Y;
                sourceRect.Width = destRect.Width = t.Texture.Width;
                sourceRect.Height = destRect.Height = t.Texture.Height;
                
                if (destRect.Width + destRect.X > Ogmo.CurrentLevel.Bounds.Width)
                {
                    sourceRect.Width -= destRect.Width + destRect.X - Ogmo.CurrentLevel.Bounds.Width;
                    destRect.Width -= destRect.Width + destRect.X - Ogmo.CurrentLevel.Bounds.Width;
                }
                if (destRect.Height + destRect.Y > Ogmo.CurrentLevel.Bounds.Height)
                {
                    sourceRect.Height -= destRect.Height + destRect.Y - Ogmo.CurrentLevel.Bounds.Height;
                    destRect.Height -= destRect.Height + destRect.Y - Ogmo.CurrentLevel.Bounds.Height;
                }

                Ogmo.EditorDraw.SpriteBatch.Draw(t.Texture, destRect, sourceRect, Microsoft.Xna.Framework.Color.White * alpha);
            }
        }

        public void RefreshAll()
        {
            foreach (var t in Textures)
                RefreshTexture(t);
        }

        public void RefreshTiles(params Point[] tiles)
        {
            int found = 0;

            foreach (var t in Textures)
            {
                foreach (var tile in tiles)
                {
                    if (IsTileInTexture(t, tile))
                    {
                        found++;
                        RefreshTexture(t);
                        break;
                    }

                    if (found == tiles.Length)
                        break;
                }
            }
        }

        private void RefreshTexture(TextureInfo texture)
        {
            Ogmo.GraphicsDevice.SetRenderTarget(texture.Texture);
            Ogmo.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);

            Texture2D tiles = Ogmo.EditorDraw.TilesetTextures[TileLayer.Tileset];
            Ogmo.EditorDraw.SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.Opaque);

            int offsetX = texture.Position.X / TileLayer.Definition.Grid.Width;
            int offsetY = texture.Position.Y / TileLayer.Definition.Grid.Height;
            int tilesWidth = texture.Texture.Width / TileLayer.Definition.Grid.Width;
            int tilesHeight = texture.Texture.Height / TileLayer.Definition.Grid.Height;

            for (int i = 0; i < tilesWidth; i++)
            {
                for (int j = 0; j < tilesHeight; j++)
                {
                    if (TileLayer.Tiles[i + offsetX, j + offsetY] != -1)
                        Ogmo.EditorDraw.SpriteBatch.Draw(
                            tiles, 
                            new Microsoft.Xna.Framework.Vector2(i * TileLayer.Definition.Grid.Width, j * TileLayer.Definition.Grid.Height), 
                            TileLayer.Tileset.GetXNARectFromID(TileLayer.Tiles[i + offsetX, j + offsetY]), 
                            Microsoft.Xna.Framework.Color.White);
                }
            }
            Ogmo.EditorDraw.SpriteBatch.End();

            Ogmo.GraphicsDevice.SetRenderTarget(null);
        }

        private bool IsTileInTexture(TextureInfo texture, Point tile)
        {
            //TODO
            return true;
        }

        private struct TextureInfo
        {
            public RenderTarget2D Texture;
            public Point Position;

            public TextureInfo(RenderTarget2D texture, Point position)
            {
                Texture = texture;
                Position = position;
            }
        }
    }
}
