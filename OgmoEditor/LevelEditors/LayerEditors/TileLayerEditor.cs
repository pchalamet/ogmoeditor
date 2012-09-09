using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Resizers;
using OgmoEditor.LevelEditors.Actions.TileActions;
using System.Drawing;
using OgmoEditor.Clipboard;
using System.Drawing.Imaging;
using System.Diagnostics;

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

        public override void NewDraw(Graphics graphics, bool current, bool fullAlpha)
        {
            //Get which tiles to draw (the ones that are visible)
            Rectangle visible = GetVisibleGridArea();

            //Draw the tiles
            ImageAttributes attributes = fullAlpha ? Util.FullAlphaAttributes : Util.HalfAlphaAttributes;
            for (int i = visible.X; i < visible.Right; i++)
            {
                for (int j = visible.Y; j < visible.Bottom; j++)
                {
                    if (Layer.Tiles[i, j] != -1)
                    {
                        Rectangle tileRect = Layer.Tileset.TileRects[Layer.Tiles[i, j]];
                        graphics.DrawImage(Layer.Tileset.Bitmap, new Rectangle(i * Layer.Definition.Grid.Width, j * Layer.Definition.Grid.Height, Layer.Definition.Grid.Width, Layer.Definition.Grid.Height), tileRect.X, tileRect.Y, tileRect.Width, tileRect.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
            }

            //Draw the selection box
            if (current && Layer.Selection != null)
                Ogmo.NewEditorDraw.DrawSelectionRectangle(graphics, new Rectangle(
                    Layer.Selection.Area.X * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Y * Layer.Definition.Grid.Height,
                    Layer.Selection.Area.Width * Layer.Definition.Grid.Width,
                    Layer.Selection.Area.Height * Layer.Definition.Grid.Height));
        }

        public override Resizer GetResizer()
        {
            return new TileResizer(this);
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
            LevelEditor.Perform(new TileSelectAction(Layer, new Rectangle(0, 0, Layer.TileCellsX, Layer.TileCellsY)));
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
            LevelEditor.Perform(new TileClearSelectionAction(Layer));
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
            Ogmo.Clipboard = new TileClipboardItem(Layer.Selection.Area, Layer);
        }

        public override void Cut()
        {
            Copy();
            LevelEditor.Perform(new TileDeleteSelectionAction(Layer));
        }
    }
}
