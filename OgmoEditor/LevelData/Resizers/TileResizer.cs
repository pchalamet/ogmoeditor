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

        public override void Resize(System.Drawing.Size to)
        {
            oldTiles = Layer.Tiles;
            Layer.Tiles = new int[to.Width / Layer.Definition.Grid.Width, to.Height / Layer.Definition.Grid.Height];

            for (int i = 0; i < Layer.Tiles.GetLength(0) && i < oldTiles.GetLength(0); i++)
                for (int j = 0; j < Layer.Tiles.GetLength(1) && j < oldTiles.GetLength(1); j++)
                    Layer.Tiles[i, j] = oldTiles[i, j];
        }

        public override void Undo()
        {
            Layer.Tiles = oldTiles;
        }
    }
}
