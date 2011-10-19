using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Actions.EntityActions;

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

        public override void Draw(Content content, bool current, float alpha)
        {
            foreach (Entity e in Layer.Entities)
                e.Draw(content, current, alpha);

            base.Draw(content, current, alpha);
        }

        public override void OnKeyDown(System.Windows.Forms.Keys key)
        {
            base.OnKeyDown(key);

            if (key == System.Windows.Forms.Keys.Delete)
            {
                if (Ogmo.EntitySelectionWindow.AmountSelected > 0)
                    LevelEditor.Perform(new EntityRemoveAction(Layer, Ogmo.EntitySelectionWindow.Selected));
            }
        }
    }
}
