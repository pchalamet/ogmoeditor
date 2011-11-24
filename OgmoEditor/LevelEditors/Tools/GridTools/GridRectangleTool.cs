using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridRectangleTool : GridTool
    {
        private bool drawing;
        private bool drawMode;
        private Point drawStart;
        private Point drawTo;

        public GridRectangleTool()
            : base("Rectangle", "rectangle.png")
        {
            drawing = false;
        }

        public override void Draw(Content content)
        {
            if (drawing)
            {
                Rectangle draw = getRect();
                if (LevelEditor.Level.Bounds.IntersectsWith(draw))
                    content.DrawFillRect(draw, (drawMode ? LayerEditor.Layer.Definition.Color.ToXNA() : LayerEditor.Layer.Definition.Color.Invert().ToXNA()) * .5f);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
            drawMode = true;
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
                LevelEditor.Perform(new GridRectangleAction(LayerEditor.Layer, draw, drawMode));
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

            int width = LayerEditor.Layer.Grid.GetLength(0) * LayerEditor.Layer.Definition.Grid.Width;
            int height = LayerEditor.Layer.Grid.GetLength(1) * LayerEditor.Layer.Definition.Grid.Height;

            if (r.X + r.Width > width)
                r.Width = width - r.X;

            if (r.Y + r.Height > height)
                r.Height = height - r.Y;

            return r;
        }
    }
}
