using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Tools.GridTools;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;
using System.Diagnostics;

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

        public override void Draw(Content content, bool current, float alpha)
        {
            for (int i = 0; i < Layer.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Layer.Grid.GetLength(1); j++)
                {
                    if (Layer.Grid[i, j])
                        content.DrawRectangle(i * Layer.Definition.Grid.Width, j * Layer.Definition.Grid.Height, Layer.Definition.Grid.Width, Layer.Definition.Grid.Height, Layer.Definition.Color.ToXNA() * alpha);
                }
            }

            base.Draw(content, current, alpha);
        }

        public override bool CanCopyOrCut()
        {
            return false;
        }

        public override void Copy()
        {
            throw new NotImplementedException();
        }

        public override void Cut()
        {
            throw new NotImplementedException();
        }

        public override void OnKeyDown(System.Windows.Forms.Keys key)
        {
            base.OnKeyDown(key);

            if (key == System.Windows.Forms.Keys.Space && Layer.Selection != null)
                LevelEditor.Perform(new GridClearSelectionAction(Layer));
        }
    }
}
