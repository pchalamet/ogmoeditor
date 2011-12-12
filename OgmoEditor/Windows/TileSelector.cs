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
        public int Selection { get; private set; }
        private float scale;

        public TileSelector()
        {
            InitializeComponent();
        }

        public Tileset Tileset
        {
            get { return tileset; }
            set
            {
                if (tileset != null)
                    Selection = value.TransformID(tileset, Selection);
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

        public void SetSelection(int to)
        {
            Selection = to;
            pictureBox.Refresh();
        }

        public void MoveSelectionLeft()
        {
            if (Selection % Tileset.TilesAcross == 0)
                Selection += Tileset.TilesAcross;
            Selection--;
            pictureBox.Refresh();
        }

        public void MoveSelectionRight()
        {
            Selection++;
            if (Selection % Tileset.TilesAcross == 0)
                Selection -= Tileset.TilesAcross;
            pictureBox.Refresh();
        }

        public void MoveSelectionUp()
        {
            Selection -= Tileset.TilesAcross;
            if (Selection < 0)
                Selection += Tileset.TilesTotal;
            pictureBox.Refresh();
        }

        public void MoveSelectionDown()
        {
            Selection += Tileset.TilesAcross;
            if (Selection >= Tileset.TilesTotal)
                Selection -= Tileset.TilesTotal;
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

                if (Selection != -1)
                {
                    Rectangle r = tileset.GetRectFromID(Selection);
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

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (tileset != null)
            {
                Point p = e.Location;
                p.X -= (int)(pictureBox.Width / 2 - tileset.Image.Width / 2 * scale);
                p.Y -= (int)(pictureBox.Height / 2 - tileset.Image.Height / 2 * scale);
                p = new Point((int)(p.X / scale), (int)(p.Y / scale));

                if (!tileset.Bounds.Contains(p))
                {
                    Selection = -1;
                    pictureBox.Refresh();
                    return;
                }

                for (int i = 0; i < tileset.TilesTotal; i++)
                {
                    if (tileset.GetRectFromID(i).Contains(p))
                    {
                        Selection = i;
                        pictureBox.Refresh();
                        return;
                    }
                }
            }
        }
    }
}
