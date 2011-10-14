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
            : base("Grid Rectangle", "rectangle.png", System.Windows.Forms.Keys.R)
        {
            drawing = false;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (drawing && (LevelEditor.Level.WithinBounds(drawStart) || LevelEditor.Level.WithinBounds(drawTo)))
            {
                Rectangle draw = getRect();

                LevelEditor.Content.DrawRectangle(spriteBatch, draw.X, draw.Y, draw.Width, draw.Height, (drawMode ? LayerEditor.Layer.Definition.Color.ToXNA() : LayerEditor.Layer.Definition.Color.Invert().ToXNA()) * .5f);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawTo = drawStart = LayerEditor.Layer.Definition.SnapToGrid(location);
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
            drawTo = drawStart = LayerEditor.Layer.Definition.SnapToGrid(location);
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
                drawTo = LayerEditor.Layer.Definition.SnapToGrid(location);
        }
        
        /*
         *  Helpers
         */
        private void drawRect()
        {
            drawing = false;
            if (LevelEditor.Level.WithinBounds(drawStart) || LevelEditor.Level.WithinBounds(drawTo))
            {
                Rectangle draw = LayerEditor.Layer.Definition.ConvertToGrid(getRect());
                LevelEditor.Perform(new GridRectangleAction(LayerEditor.Layer, draw, drawMode));
            }
        }

        private Rectangle getRect()
        {
            Rectangle r = new Rectangle();

            //Get the rectangle
            r.X = Math.Min(drawStart.X, drawTo.X);
            r.Y = Math.Min(drawStart.Y, drawTo.Y);
            r.Width = Math.Abs(drawTo.X - drawStart.X);
            r.Height = Math.Abs(drawTo.Y - drawStart.Y);

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
