using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class TileSelector : UserControl
    {
        private const int BUFFER = 6;

        private Tileset tileset;
        private int[] ids;
        private float scale;
        private Point? selectionStart = null;

        public int[] Selection
        {
            get { return this.ids; }
            private set
            {
                if (value == null)
                    this.ids = new int[] { };
                else
                    this.ids = value;
            }
        }

        public int SelectionWidth
        {
            get;
            private set;
        }

        public int SelectionHeight
        {
            get;
            private set;
        }

        public TileSelector()
        {
            InitializeComponent();
            this.Selection = new int[] { };
        }

        public Tileset Tileset
        {
            get { return tileset; }
            set
            {
                if (tileset != null)
                    Selection = value.TransformIDs(tileset, Selection);
                tileset = value;              
                if (Tileset != null)
                    calculateScale();
                pictureBox.Refresh();
            }
        }

        private void calculateScale()
        {
            scale = Math.Min((pictureBox.Width - BUFFER) / (float)tileset.Image.Width, (pictureBox.Height - BUFFER) / (float)tileset.Image.Height);
        }

        public void SetSelection(int[] to)
        {
            Selection = to;
            pictureBox.Refresh();
        }

        private enum ShiftMode
        {
            Left,
            Right,
            Up,
            Down
        }

        private int ShiftID(int id, ShiftMode mode)
        {
            switch (mode)
            {
                case ShiftMode.Left:
                    if (id % Tileset.TilesAcross == 0)
                        id += Tileset.TilesAcross;
                    id--;
                    break;
                case ShiftMode.Right:
                    id++;
                    if (id % Tileset.TilesAcross == 0)
                        id -= Tileset.TilesAcross;
                    break;
                case ShiftMode.Up:
                    id -= Tileset.TilesAcross;
                    if (id < 0)
                        id += Tileset.TilesTotal;
                    break;
                case ShiftMode.Down:
                    id += Tileset.TilesAcross;
                    if (id >= Tileset.TilesTotal)
                        id -= Tileset.TilesTotal;
                    break;
            }
            return id;
        }

        public void MoveSelectionLeft()
        {
            this.Selection = this.Selection.Select(value => this.ShiftID(value, ShiftMode.Left)).ToArray();
            pictureBox.Refresh();
        }

        public void MoveSelectionRight()
        {
            this.Selection = this.Selection.Select(value => this.ShiftID(value, ShiftMode.Right)).ToArray();
            pictureBox.Refresh();
        }

        public void MoveSelectionUp()
        {
            this.Selection = this.Selection.Select(value => this.ShiftID(value, ShiftMode.Up)).ToArray();
            pictureBox.Refresh();
        }

        public void MoveSelectionDown()
        {
            this.Selection = this.Selection.Select(value => this.ShiftID(value, ShiftMode.Down)).ToArray();
            pictureBox.Refresh();
        }

        /*
         *  Events
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Tileset != null)
            { 
                Graphics g = e.Graphics;

                float x = pictureBox.Width / 2 - tileset.Image.Width / 2 * scale;
                float y = pictureBox.Height / 2 - tileset.Image.Height / 2 * scale;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(tileset.Image, x, y, tileset.Image.Width * scale, tileset.Image.Height * scale);

                foreach (int s in this.Selection)
                {
                    Rectangle r = tileset.GetRectFromID(s);
                    g.DrawRectangle(new Pen(Color.Yellow, 3), x + r.X * scale, y + r.Y * scale, r.Width * scale, r.Height * scale);
                    Pen p = new Pen(Color.Black);
                    p.DashPattern = new float[] { 6, 2 };
                    g.DrawRectangle(p, x + r.X * scale, y + r.Y * scale, r.Width * scale, r.Height * scale);
                }
            }
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            if (Tileset != null)
                calculateScale();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (tileset == null)
                return;

            selectionStart = this.ResolveTilePoint(e.Location);

            for (int i = 0; i < tileset.TilesTotal; i++)
            {
                if (tileset.GetRectFromID(i).Contains(selectionStart.Value))
                {
                    Selection = new int[] { i };
                    SelectionWidth = 1;
                    SelectionHeight = 1;
                    pictureBox.Refresh();
                }
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (tileset == null || !selectionStart.HasValue)
                return;

            Point end = this.ResolveTilePoint(e.Location);
            Point start = new Point(this.selectionStart.Value.X, this.selectionStart.Value.Y);

            // Snap the points to within the bounds.
            start = this.SnapPoint(start, tileset.Bounds);
            end = this.SnapPoint(end, tileset.Bounds);

            // Create a rectangle of our selection bounds.
            Rectangle sbounds = new Rectangle(
                new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y)),
                new Size(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y))
                );

            // Create a temporary list to store IDs.
            List<int> newids = new List<int>();

            // Loop over the tileset, adding IDs as we find them.
            this.SelectionWidth = 0;
            this.SelectionHeight = 0;
            int width = 0;
            int height = 0;
            for (int i = 0; i < tileset.TilesTotal; i++)
                if (sbounds.IntersectsWith(tileset.GetRectFromID(i)))
                {
                    // Update the width / height if we need to.
                    Rectangle r = tileset.GetRectFromID(i);
                    if (width < r.X + r.Width)
                    {
                        width = r.X + r.Width;
                        this.SelectionWidth += 1;
                    }
                    if (height < r.Y + r.Height)
                    {
                        height = r.Y + r.Height;
                        this.SelectionHeight += 1;
                    }

                    // Add the new ID.
                    newids.Add(i);
                }

            // .. and set the selection values.
            Selection = newids.ToArray();
            pictureBox.Refresh();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.selectionStart = null;
        }

        private Point ResolveTilePoint(Point p)
        {
            p.X -= (int)(pictureBox.Width / 2 - tileset.Image.Width / 2 * scale);
            p.Y -= (int)(pictureBox.Height / 2 - tileset.Image.Height / 2 * scale);
            return new Point((int)(p.X / scale), (int)(p.Y / scale));
        }

        private Point SnapPoint(Point point, Rectangle to)
        {
            Point pt = new Point(point.X, point.Y);
            if (pt.X < to.X)
                pt.X = to.X;
            if (pt.Y < to.Y)
                pt.Y = to.Y;
            if (pt.X >= to.X + to.Width)
                pt.X = to.X + to.Width;
            if (pt.Y >= to.Y + to.Height)
                pt.Y = to.Y + to.Height;
            return pt;
        }
    }
}
