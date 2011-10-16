using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class EntityLayerEditor : LayerEditor
    {
        public new EntityLayer Layer { get; private set; }

        public EntityLayerEditor(LevelEditor levelEditor, EntityLayer layer)
            : base(levelEditor, layer)
        {
            Layer = layer;
        }

        public override void Draw(Content content, float alpha)
        {
            foreach (Entity e in Layer.Entities)
                e.Draw(content, alpha);

            base.Draw(content, alpha);
        }
    }
}
