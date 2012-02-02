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

        public override void DrawLocal(bool current, float alpha)
        {
            //Draw the grid cells
            Rectangle rect = new Rectangle();
            for (int i = 0; i < Layer.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Layer.Grid.GetLength(1); j++)
                {
                    if (Layer.Grid[i, j])
                    {
                        rect.X = i * Layer.Definition.Grid.Width;
                        rect.Y = j * Layer.Definition.Grid.Height;
                        rect.Width = Layer.Definition.Grid.Width;
                        rect.Height = Layer.Definition.Grid.Height;
                        if(rect.X + rect.Width > Layer.Level.Size.Width) rect.Width = Layer.Level.Size.Width - rect.X;
                        if(rect.Y + rect.Height > Layer.Level.Size.Height) rect.Height = Layer.Level.Size.Height - rect.Y;

                        Ogmo.EditorDraw.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, Layer.Definition.Color.ToXNA() * alpha);
                    }
                }
            }

            //Draw the selection rectangle
            if (Layer.Selection != null)
                Ogmo.EditorDraw.DrawFillRect(
                    Layer.Selection.Area.X * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Y * Layer.Definition.Grid.Height,
                    Layer.Selection.Area.Width * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Height * Layer.Definition.Grid.Height,
                    Microsoft.Xna.Framework.Color.Yellow * alpha);
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
            LevelEditor.Perform(new GridSelectAction(Layer, new Rectangle(0, 0, Layer.GridCellsX, Layer.GridCellsY)));
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

        public override bool CanCopyOrCut
        {
            get
            {
                return Layer.Selection != null;
            }
        }

        public override void Copy()
        {
            Ogmo.Clipboard = new GridClipboardItem(Layer.Selection.Area, Layer);
        }

        public override void Cut()
        {
            Copy();
            LevelEditor.Perform(new GridDeleteSelectionAction(Layer));
        }

        public override void OnKeyDown(System.Windows.Forms.Keys key)
        {
            base.OnKeyDown(key);

            if (key == System.Windows.Forms.Keys.Delete)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(new GridDeleteSelectionAction(Layer));
            }
            else if (key == System.Windows.Forms.Keys.D)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(Layer.Selection.GetMoveAction(new Point(1, 0)));
            }
            else if (key == System.Windows.Forms.Keys.S)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(Layer.Selection.GetMoveAction(new Point(0, 1)));
            }
            else if (key == System.Windows.Forms.Keys.W)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(Layer.Selection.GetMoveAction(new Point(0, -1)));
            }
            else if (key == System.Windows.Forms.Keys.A)
            {
                if (Layer.Selection != null)
                    LevelEditor.Perform(Layer.Selection.GetMoveAction(new Point(-1, 0)));
            }
        }
    }
}
