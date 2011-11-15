using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridSelectionTool : GridTool
    {
        static private readonly OgmoColor DrawColor = new OgmoColor(System.Drawing.Color.Red);

        private bool drawing;
        private Point drawStart;
        private Point drawTo;

        public GridSelectionTool()
            : base("Select", "selection.png", System.Windows.Forms.Keys.S)
        {
            drawing = false;
        }

        public override void Draw(Content content)
        {
            if (drawing)
            {
                Rectangle draw = getRect();
                if (LevelEditor.Level.Bounds.IntersectsWith(draw))
                    content.DrawFillRect(draw, DrawColor.ToXNA() * .5f);
            }
        }

        public override void OnMouseLeftDown(Point location)
        {
            drawing = true;
            drawStart = drawTo = LayerEditor.MouseSnapPosition;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing)
            {
                drawing = false;
                drawTo = LayerEditor.MouseSnapPosition;
                Rectangle select = getRect();

                if (LevelEditor.Level.Bounds.IntersectsWith(select))
                {
                    select = new Rectangle(select.X / LayerEditor.Layer.Definition.Grid.Width, select.Y / LayerEditor.Layer.Definition.Grid.Height, select.Width / LayerEditor.Layer.Definition.Grid.Width, select.Height / LayerEditor.Layer.Definition.Grid.Height);

                    LevelEditor.StartBatch();
                    if (LayerEditor.Layer.Selection != null)
                        LevelEditor.BatchPerform(new GridClearSelectionAction(LayerEditor.Layer));
                    LevelEditor.BatchPerform(new GridSelectAction(LayerEditor.Layer, select));
                    LevelEditor.EndBatch();
                }
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (drawing)
                drawTo = LayerEditor.MouseSnapPosition;
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

            if (r.X + r.Width > LevelEditor.Level.Size.Width)
                r.Width = LevelEditor.Level.Size.Width - r.X;

            if (r.Y + r.Height > LevelEditor.Level.Size.Height)
                r.Height = LevelEditor.Level.Size.Height - r.Y;

            return r;
        }
    }
}
