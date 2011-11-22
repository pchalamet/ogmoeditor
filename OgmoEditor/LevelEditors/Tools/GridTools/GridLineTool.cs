using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridLineTool : GridTool
    {
        private bool drawing;
        private bool drawMode;
        private Point drawStart;
        private Point mouse;

        public GridLineTool()
            : base("Line", "line.png", System.Windows.Forms.Keys.L)
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawStart = LayerEditor.Layer.Definition.ConvertToGrid(location);
            drawing = true;
            drawMode = true;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing && drawMode)
            {
                drawing = false;

                LevelEditor.StartBatch();
                List<Point> pts = getPoints(drawStart, LayerEditor.Layer.Definition.ConvertToGrid(location));
                foreach (var p in pts)
                    LevelEditor.BatchPerform(new GridDrawAction(LayerEditor.Layer, p, true));
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseRightDown(Point location)
        {
            drawStart = LayerEditor.Layer.Definition.ConvertToGrid(location);
            drawing = true;
            drawMode = false;
        }

        public override void OnMouseRightUp(Point location)
        {
            if (drawing && !drawMode)
            {
                drawing = false;

                LevelEditor.StartBatch();
                List<Point> pts = getPoints(drawStart, LayerEditor.Layer.Definition.ConvertToGrid(location));
                foreach (var p in pts)
                    LevelEditor.BatchPerform(new GridDrawAction(LayerEditor.Layer, p, false));
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseMove(Point location)
        {
            mouse = LayerEditor.Layer.Definition.ConvertToGrid(location);
        }

        public override void Draw(Content content)
        {
            if (drawing)
            {
                List<Point> pts = getPoints(drawStart, mouse);
                foreach (var p in pts)
                    content.DrawRectangle(p.X * LayerEditor.Layer.Definition.Grid.Width, p.Y * LayerEditor.Layer.Definition.Grid.Height, LayerEditor.Layer.Definition.Grid.Width, LayerEditor.Layer.Definition.Grid.Height, (drawMode ? LayerEditor.Layer.Definition.Color.ToXNA() : LayerEditor.Layer.Definition.Color.Invert().ToXNA()) * .5f);
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
                        if (y < LayerEditor.Layer.Grid.GetLength(0) && x < LayerEditor.Layer.Grid.GetLength(1))
                            points.Add(new Point(y, x));
                    }
                    else
                    {
                        if (x < LayerEditor.Layer.Grid.GetLength(0) && y < LayerEditor.Layer.Grid.GetLength(1))
                            points.Add(new Point(x, y));
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
