using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class GridLayerEditor : LayerEditor
    {
        public new GridLayer Layer { get; private set; }

        public GridLayerEditor(LevelEditor levelEditor, GridLayer layer)
            : base(levelEditor, layer)
        {
            Layer = layer;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Layer.Grid.GetLength(0); i++)
                for (int j = 0; j < Layer.Grid.GetLength(1); j++)
                {
                    if (Layer.Grid[i, j])
                        LevelEditor.Content.DrawRectangle(spriteBatch, i * Layer.Definition.Grid.Width, j * Layer.Definition.Grid.Height, Layer.Definition.Grid.Width, Layer.Definition.Grid.Height, Layer.Definition.Color.ToXNA());
                }
        }
    }
}
