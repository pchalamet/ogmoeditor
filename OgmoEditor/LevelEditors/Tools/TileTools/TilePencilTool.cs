using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.TileActions;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TilePencilTool : TileTool
    {
        private bool drawing;
        private bool drawMode;
        private TileDrawAction drawAction;

        public TilePencilTool()
            : base("Pencil", "pencil.png")
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawMode = true;

                setTiles(location, Ogmo.TilePaletteWindow.Tiles);
            }
        }

        public override void OnMouseRightDown(Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawMode = false;

                setTiles(location, null);
            }
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing && drawMode)
            {
                drawing = false;
                drawAction = null;
            }
        }

        public override void OnMouseRightUp(Point location)
        {
            if (drawing && !drawMode)
            {
                drawing = false;
                drawAction = null;
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (drawing)
                setTiles(location, drawMode ? Ogmo.TilePaletteWindow.Tiles : null);
        }

        private void setTiles(Point location, Rectangle? setTo)
        {
            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            if (!IsValidTileCell(location))
                return;

            if (!setTo.HasValue)
            {
                if (LayerEditor.Layer.Tiles[location.X, location.Y] != -1)
                {
                    if (drawAction == null)
                        LevelEditor.Perform(drawAction = new TileDrawAction(LayerEditor.Layer, location, -1));
                    else
                        drawAction.DoAgain(location, -1);
                }
            }
            else if (setTo.Value.Area() == 1)
            {
                int id = LayerEditor.Layer.Tileset.GetIDFromCell(setTo.Value.Location);
                if (LayerEditor.Layer.Tiles[location.X, location.Y] != id)
                {
                    if (drawAction == null)
                        LevelEditor.Perform(drawAction = new TileDrawAction(LayerEditor.Layer, location, id));
                    else
                        drawAction.DoAgain(location, id);
                }
            }
            else
            {
                //Draw the tiles
                int i = 0;
                for (int x = 0; x < setTo.Value.Width; x += 1)
                {
                    for (int y = 0; y < setTo.Value.Height; y += 1)
                    {
                        int id = LayerEditor.Layer.Tileset.GetIDFromCell(new Point(setTo.Value.X + x, setTo.Value.Y + y));
                        if (LayerEditor.Layer.Tiles[location.X + x, location.Y + y] != id)
                        {
                            if (drawAction == null)
                                LevelEditor.Perform(drawAction = new TileDrawAction(LayerEditor.Layer, location, id));
                            else
                                drawAction.DoAgain(new Point(location.X + x, location.Y + y), id);
                        }
                        i++;
                    }
                }
            }
        }
    }
}
