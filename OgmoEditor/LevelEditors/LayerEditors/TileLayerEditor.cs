using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class TileLayerEditor : LayerEditor
    {
        public new TileLayer Layer { get; private set; }

        public TileLayerEditor(LevelEditor levelEditor, TileLayer layer)
            : base(levelEditor, layer)
        {
            Layer = layer;
        }

        public override void Draw(Content content, bool current, float alpha)
        {
            content.SpriteBatch.Draw(Layer.Texture, Microsoft.Xna.Framework.Vector2.Zero, Microsoft.Xna.Framework.Color.White * alpha);
            base.Draw(content, current, alpha);
        }
    }
}
