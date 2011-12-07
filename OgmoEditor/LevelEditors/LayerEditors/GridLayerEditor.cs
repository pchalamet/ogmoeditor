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
using OgmoEditor.LevelEditors.Resizers;

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
            //Draw the grid cells
            for (int i = 0; i < Layer.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Layer.Grid.GetLength(1); j++)
                {
                    if (Layer.Grid[i, j])
                        content.DrawRectangle(i * Layer.Definition.Grid.Width, j * Layer.Definition.Grid.Height, Layer.Definition.Grid.Width, Layer.Definition.Grid.Height, Layer.Definition.Color.ToXNA() * alpha);
                }
            }

            //Draw the selection rectangle
            if (Layer.Selection != null)
                content.DrawFillRect(
                    Layer.Selection.Area.X * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Y * Layer.Definition.Grid.Height,
                    Layer.Selection.Area.Width * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Height * Layer.Definition.Grid.Height,
                    Microsoft.Xna.Framework.Color.Yellow * alpha);

            base.Draw(content, current, alpha);
        }

        public override Resizer GetResizer()
        {
            return new GridResizer(this);
        }

        public override bool CanSelectAll
        {
            get
            {
                return true;
            }
        }

        public override void SelectAll()
        {
            LevelEditor.Perform(new GridSelectAction(Layer, new Rectangle(0, 0, Layer.Grid.GetLength(0), Layer.Grid.GetLength(1))));
        }

        public override bool CanDeselect
        {
            get
            {
                return Layer.Selection != null;
            }
        }

        public override void Deselect()
        {
            LevelEditor.Perform(new GridClearSelectionAction(Layer));
        }

        public override void OnKeyDown(System.Windows.Forms.Keys key)
        {
            base.OnKeyDown(key);

            if (key == System.Windows.Forms.Keys.Delete)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(new GridDeleteSelectionAction(Layer));
            }
        }
    }
}
