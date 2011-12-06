using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Resizers;
using OgmoEditor.LevelEditors.LayersEditors;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class TileLayerEditor : LayerEditor
    {
        public new TileLayer Layer { get; private set; }
        public TileCanvas TileCanvas { get; private set; }

        public TileLayerEditor(LevelEditor levelEditor, TileLayer layer)
            : base(levelEditor, layer)
        {
            Layer = layer;
        }

        public override void Draw(Content content, bool current, float alpha)
        {
            Layer.TileCanvas.Draw(alpha);
            base.Draw(content, current, alpha);
        }

        public override Resizer GetResizer()
        {
            return new TileResizer(this);
        }
    }
}
