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
        private int drawTile;
        private bool drawButton;
        private TileDrawAction drawAction;

        public TilePencilTool()
            : base("Pencil", "pencil.png", System.Windows.Forms.Keys.P)
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawButton = true;
                drawTile = Ogmo.TilePaletteWindow.Tile;
                setTile(location, drawTile);
            }
        }

        public override void OnMouseRightDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawButton = false;
                drawTile = -1;
                setTile(location, drawTile);
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
                setTile(location, drawTile);
        }

        private void setTile(System.Drawing.Point location, int setTo)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            if (LayerEditor.Layer.Tiles[location.X, location.Y] == setTo)
                return;

            if (drawAction == null)
                LevelEditor.Perform(drawAction = new TileDrawAction(LayerEditor.Layer, location, setTo));
            else
                drawAction.DoAgain(location);
        }
    }
}
