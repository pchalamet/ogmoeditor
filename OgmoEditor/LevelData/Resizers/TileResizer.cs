using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelData.Resizers
{
    public class TileResizer : Resizer
    {
        public new TileLayer Layer { get; private set; }

        public int[,] oldTiles;

        public TileResizer(TileLayer tileLayer)
            : base(tileLayer)
        {
            Layer = tileLayer;
        }

        public override void Resize()
        {
            oldTiles = Layer.Tiles;
            Layer.Tiles = new int[Layer.Level.Size.Width / Layer.Definition.Grid.Width, Layer.Level.Size.Height / Layer.Definition.Grid.Height];

            for (int i = 0; i < Layer.Tiles.GetLength(0); i++)
                for (int j = 0; j < Layer.Tiles.GetLength(1); j++)
                    Layer.Tiles[i, j] = -1;

            for (int i = 0; i < Layer.Tiles.GetLength(0) && i < oldTiles.GetLength(0); i++)
                for (int j = 0; j < Layer.Tiles.GetLength(1) && j < oldTiles.GetLength(1); j++)
                    Layer.Tiles[i, j] = oldTiles[i, j];

            Layer.RefreshTexture();
        }

        public override void Undo()
        {
            Layer.Tiles = oldTiles;
        }
    }
}
