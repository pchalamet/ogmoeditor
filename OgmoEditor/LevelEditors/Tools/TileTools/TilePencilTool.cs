using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.TileActions;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TilePencilTool : TileTool
    {
        private bool drawing;
        private int[] drawTiles;
        private int drawTilesWidth;
        private int drawTilesHeight;
        private bool drawButton;
        private TileDrawAreaAction drawAction;

        public TilePencilTool()
            : base("Pencil", "pencil.png")
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawButton = true;
                // TODO: Allow the user to draw multiple tiles with a single pencil use.
                drawTiles = Ogmo.TilePaletteWindow.Tiles;
                drawTilesWidth = Ogmo.TilePaletteWindow.TilesWidth;
                drawTilesHeight = Ogmo.TilePaletteWindow.TilesHeight;
                setTiles(location, drawTiles);
            }
        }

        public override void OnMouseRightDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawButton = false;
                drawTiles = new int[] { };
                drawTilesWidth = 0;
                drawTilesHeight = 0;
                setTiles(location, drawTiles);
            }
        }

        public override void OnMouseLeftUp(System.Drawing.Point location)
        {
            if (drawing && drawButton)
            {
                drawing = false;
                drawAction = null;
            }
        }

        public override void OnMouseRightUp(System.Drawing.Point location)
        {
            if (drawing && !drawButton)
            {
                drawing = false;
                drawAction = null;
            }
        }

        public override void OnMouseMove(System.Drawing.Point location)
        {
            if (drawing)
                setTiles(location, drawTiles);
        }

        private void setTiles(System.Drawing.Point location, int[] setTo)
        {
            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            if (!IsValidTileCell(location))
                return;

            // Check to see if all the tiles are already the same.
            int i = 0;
            bool skip = true;
            for (int x = 0; x < this.drawTilesWidth; x += 1)
            {
                for (int y = 0; y < this.drawTilesHeight; y += 1)
                {
                    if (LayerEditor.Layer.Tiles[location.X + x, location.Y + y] != setTo[i])
                    {
                        skip = false;
                        break;
                    }
                    i += 1;
                }
                if (!skip) break;
            }
            if (skip) return;
            
            // Draw all of the tiles.
            if (drawAction == null)
                LevelEditor.Perform(drawAction = new TileDrawAreaAction(LayerEditor.Layer, location, new System.Drawing.Size(drawTilesWidth, drawTilesHeight), setTo));
            else
                drawAction.DoAgain(location);
        }
    }
}
