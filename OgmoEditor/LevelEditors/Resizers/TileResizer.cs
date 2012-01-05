using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.LayerEditors;

namespace OgmoEditor.LevelEditors.Resizers
{
    public class TileResizer : Resizer
    {
        public new TileLayerEditor Editor { get; private set; }

        public int[,] oldTiles;

        public TileResizer(TileLayerEditor tileEditor)
            : base(tileEditor)
        {
            Editor = tileEditor;
        }

        public override void Resize()
        {
            TileLayer layer = Editor.Layer;

            oldTiles = layer.Tiles;
            layer.Tiles = new int[layer.Level.Size.Width / layer.Definition.Grid.Width, layer.Level.Size.Height / layer.Definition.Grid.Height];

            for (int i = 0; i < layer.Tiles.GetLength(0); i++)
                for (int j = 0; j < layer.Tiles.GetLength(1); j++)
                    layer.Tiles[i, j] = -1;

            for (int i = 0; i < layer.Tiles.GetLength(0) && i < oldTiles.GetLength(0); i++)
                for (int j = 0; j < layer.Tiles.GetLength(1) && j < oldTiles.GetLength(1); j++)
                    layer.Tiles[i, j] = oldTiles[i, j];

            Editor.Layer.InitCanvas();
        }

        public override void Undo()
        {
            Editor.Layer.Tiles = oldTiles;
        }
    }
}
