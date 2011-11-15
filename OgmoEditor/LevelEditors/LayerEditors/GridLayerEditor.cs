using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Tools.GridTools;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;
using System.Diagnostics;
using OgmoEditor.Clipboard;

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
            Rectangle sel;
            if (Layer.Selection == null)
                sel = Rectangle.Empty;
            else
                sel = Layer.Selection.Bounds;

            for (int i = 0; i < Layer.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Layer.Grid.GetLength(1); j++)
                {
                    if (Layer.Grid[i, j] && !sel.Contains(new Point(i, j)))
                        content.DrawRectangle(i * Layer.Definition.Grid.Width, j * Layer.Definition.Grid.Height, Layer.Definition.Grid.Width, Layer.Definition.Grid.Height, Layer.Definition.Color.ToXNA() * alpha);
                }
            }

            if (sel != Rectangle.Empty)
                content.DrawHollowRect(sel.X * Layer.Definition.Grid.Width, sel.Y * Layer.Definition.Grid.Height, sel.Width * Layer.Definition.Grid.Width, sel.Height * Layer.Definition.Grid.Height, Microsoft.Xna.Framework.Color.Yellow);

            base.Draw(content, current, alpha);
        }

        public override bool CanCopyOrCut()
        {
            return Layer.Selection != null;
        }

        public override void Copy()
        {
            Ogmo.Clipboard = new GridClipboardItem(Layer.Selection);
        }

        public override void Cut()
        {
            Ogmo.Clipboard = new GridClipboardItem(Layer.Selection);
            LevelEditor.Perform(new GridClearSelectionAction(Layer));
        }

        public override void OnKeyDown(System.Windows.Forms.Keys key)
        {
            base.OnKeyDown(key);

            if (key == System.Windows.Forms.Keys.Space && Layer.Selection != null)
                LevelEditor.Perform(new GridClearSelectionAction(Layer));
        }
    }
}
