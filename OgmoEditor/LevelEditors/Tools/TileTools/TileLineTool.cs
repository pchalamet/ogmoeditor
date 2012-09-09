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
        private SolidBrush eraseBrush;

        public TileLineTool()
            : base("Line", "line.png")
        {
            drawing = false;
            eraseBrush = new SolidBrush(Color.FromArgb(255/2, Color.Red));
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            if (Ogmo.TilePaletteWindow.Tiles.Length == 0)
                return;

            drawStart = LayerEditor.Layer.Definition.ConvertToGrid(location);
            drawing = true;
            drawMode = true;
            // TODO: Allow user to draw a line of tiles.
            drawTile = Ogmo.TilePaletteWindow.Tiles[0];
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

        public override void Draw(Graphics graphics)
        {
            if (drawing)
            {
                List<Point> pts = getPoints(drawStart, mouse);
                if (drawTile == -1 || !drawMode)
                {
                    foreach (var p in pts)
                        graphics.FillRectangle(eraseBrush, p.X * LayerEditor.Layer.Definition.Grid.Width, p.Y * LayerEditor.Layer.Definition.Grid.Height, LayerEditor.Layer.Definition.Grid.Width, LayerEditor.Layer.Definition.Grid.Height);
                }
                else
                {
                    Rectangle tileRect = LayerEditor.Layer.Tileset.TileRects[drawTile];
                    foreach (var p in pts)
                        graphics.DrawImage(LayerEditor.Layer.Tileset.Bitmap, new Rectangle(p.X * LayerEditor.Layer.Definition.Grid.Width, p.Y * LayerEditor.Layer.Definition.Grid.Height, LayerEditor.Layer.Definition.Grid.Width, LayerEditor.Layer.Definition.Grid.Height), tileRect.X, tileRect.Y, tileRect.Width, tileRect.Height, GraphicsUnit.Pixel, Util.HalfAlphaAttributes);
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
