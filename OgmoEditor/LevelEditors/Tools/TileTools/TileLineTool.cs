using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.TileActions;

namespace OgmoEditor.LevelEditors.Tools.TileTools
{
    public class TileLineTool : TileTool
    {
        private bool drawing;
        private bool drawMode;
        private Point drawStart;
        private Point mouse;
        private int drawTile;

        public TileLineTool()
            : base("Line", "line.png")
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawStart = LayerEditor.Layer.Definition.ConvertToGrid(location);
            drawing = true;
            drawMode = true;
            drawTile = Ogmo.TilePaletteWindow.Tile;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing && drawMode)
            {
                drawing = false;

                LevelEditor.StartBatch();
                List<Point> pts = getPoints(drawStart, LayerEditor.Layer.Definition.ConvertToGrid(location));
                foreach (var p in pts)
                    LevelEditor.BatchPerform(new TileDrawAction(LayerEditor.Layer, p, drawTile));
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseRightDown(Point location)
        {
            drawStart = LayerEditor.Layer.Definition.ConvertToGrid(location);
            drawing = true;
            drawMode = false;
            drawTile = -1;
        }

        public override void OnMouseRightUp(Point location)
        {
            if (drawing && !drawMode)
            {
                drawing = false;

                LevelEditor.StartBatch();
                List<Point> pts = getPoints(drawStart, LayerEditor.Layer.Definition.ConvertToGrid(location));
                foreach (var p in pts)
                    LevelEditor.BatchPerform(new TileDrawAction(LayerEditor.Layer, p, drawTile));
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseMove(Point location)
        {
            mouse = LayerEditor.Layer.Definition.ConvertToGrid(location);
        }

        public override void Draw()
        {
            if (drawing)
            {
                List<Point> pts = getPoints(drawStart, mouse);
                if (drawTile == -1 || !drawMode)
                {
                    foreach (var p in pts)
                        Ogmo.EditorDraw.DrawRectangle(p.X * LayerEditor.Layer.Definition.Grid.Width, p.Y * LayerEditor.Layer.Definition.Grid.Height, LayerEditor.Layer.Definition.Grid.Width, LayerEditor.Layer.Definition.Grid.Height, Microsoft.Xna.Framework.Color.Red * .5f);
                }
                else
                {
                    Microsoft.Xna.Framework.Rectangle rect = LayerEditor.Layer.Tileset.GetXNARectFromID(drawTile);
                    foreach (var p in pts)
                        Ogmo.EditorDraw.SpriteBatch.Draw(
                            Ogmo.EditorDraw.TilesetTextures[LayerEditor.Layer.Tileset],
                            new Microsoft.Xna.Framework.Vector2(p.X * LayerEditor.Layer.Definition.Grid.Width, p.Y * LayerEditor.Layer.Definition.Grid.Height),
                            rect,
                            Microsoft.Xna.Framework.Color.White * .5f);
                }
            }
        }

        private List<Point> getPoints(Point start, Point end)
        {
            int aX = start.X;
            int aY = start.Y;
            int bX = end.X;
            int bY = end.Y;
            List<Point> points = new List<Point>();

            bool steep = Math.Abs(bY - aY) > Math.Abs(bX - aX);

            if (steep)
            {
                Util.Swap(ref aX, ref aY);
                Util.Swap(ref bX, ref bY);
            }

            if (aX > bX)
            {
                Util.Swap(ref aX, ref bX);
                Util.Swap(ref aY, ref bY);
            }

            int deltaX = bX - aX;
            int deltaY = Math.Abs(bY - aY);
            float error = 0;
            float deltaErr = deltaY / (float)deltaX;
            int yStep = (aY < bY) ? 1 : -1;
            int y = aY;

            for (int x = aX; x <= bX; x++)
            {
                if (x >= 0 && y >= 0)
                {
                    if (steep)
                    {
                        Point p = new Point(y, x);
                        if (IsValidTileCell(p))
                            points.Add(p);
                    }
                    else
                    {
                        Point p = new Point(x, y);
                        if (IsValidTileCell(p))
                            points.Add(p);
                    }
                }

                error += deltaErr;
                if (error >= .5f)
                {
                    y += yStep;
                    error--;
                }
            }

            return points;
        }
    }
}
