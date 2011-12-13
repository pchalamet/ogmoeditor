using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.TileActions;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TileRectangleTool : TileTool
    {
        private bool drawing;
        private bool drawMode;
        private int drawTile;
        private Point drawStart;
        private Point drawTo;

        public TileRectangleTool()
            : base("Rectangle", "rectangle.png")
        {
            drawing = false;
        }

        public override void Draw(EditorDraw content)
        {
            if (drawing)
            {
                Rectangle draw = getRect();
                if (LevelEditor.Level.Bounds.IntersectsWith(draw))
                {
                    if (!drawMode || drawTile == -1)
                        content.DrawFillRect(draw, Microsoft.Xna.Framework.Color.Red * .5f);
                    else
                    {
                        Microsoft.Xna.Framework.Rectangle rect = LayerEditor.Layer.Tileset.GetXNARectFromID(drawTile);
                        for (int i = draw.X; i < draw.X + draw.Width; i += LayerEditor.Layer.Definition.Grid.Width)
                            for (int j = draw.Y; j < draw.Y + draw.Height; j += LayerEditor.Layer.Definition.Grid.Height)
                                content.SpriteBatch.Draw(
                                    content.TilesetTextures[LayerEditor.Layer.Tileset],
                                    new Microsoft.Xna.Framework.Vector2(i, j),
                                    rect,
                                    Microsoft.Xna.Framework.Color.White * .5f);
                    }
                }
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
            drawMode = true;
            drawTile = Ogmo.TilePaletteWindow.Tile;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing && drawMode)
                drawRect();
        }

        public override void OnMouseRightDown(Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
            drawMode = false;
            drawTile = -1;
        }

        public override void OnMouseRightUp(Point location)
        {
            if (drawing && !drawMode)
                drawRect();
        }

        public override void OnMouseMove(Point location)
        {
            if (drawing)
                drawTo = LayerEditor.MouseSnapPosition;
        }

        /*
         *  Helpers
         */
        private void drawRect()
        {
            drawing = false;
            Rectangle draw = getRect();
            if (LevelEditor.Level.Bounds.IntersectsWith(draw))
            {
                draw = LayerEditor.Layer.Definition.ConvertToGrid(draw);
                LevelEditor.Perform(new TileRectangleAction(LayerEditor.Layer, draw, drawTile));
            }
        }

        private Rectangle getRect()
        {
            Rectangle r = new Rectangle();

            //Get the rectangle
            r.X = Math.Min(drawStart.X, drawTo.X);
            r.Y = Math.Min(drawStart.Y, drawTo.Y);
            r.Width = Math.Abs(drawTo.X - drawStart.X) + LayerEditor.Layer.Definition.Grid.Width;
            r.Height = Math.Abs(drawTo.Y - drawStart.Y) + LayerEditor.Layer.Definition.Grid.Height;

            //Enforce Bounds
            if (r.X < 0)
            {
                r.Width += r.X;
                r.X = 0;
            }

            if (r.Y < 0)
            {
                r.Height += r.Y;
                r.Y = 0;
            }

            int width = LayerEditor.Layer.Tiles.GetLength(0) * LayerEditor.Layer.Definition.Grid.Width;
            int height = LayerEditor.Layer.Tiles.GetLength(1) * LayerEditor.Layer.Definition.Grid.Height;

            if (r.X + r.Width > width)
                r.Width = width - r.X;

            if (r.Y + r.Height > height)
                r.Height = height - r.Y;

            return r;
        }
    }
}
