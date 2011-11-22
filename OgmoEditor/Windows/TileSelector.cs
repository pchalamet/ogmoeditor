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
                tileset = value;
                Selection = -1;
                if (Tileset != null)
                    calculateScale();
                pictureBox.Refresh();
            }
        }

        private void calculateScale()
        {
            scale = Math.Min((pictureBox.Width - BUFFER) / tileset.Image.Width, (pictureBox.Height - BUFFER) / tileset.Image.Height);
        }

        public void SetSelection(int to)
        {
            Selection = to;
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
                    g.DrawRectangle(new Pen(Color.Lime), x + r.X * scale, y + r.Y * scale, r.Width * scale, r.Height * scale);
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
