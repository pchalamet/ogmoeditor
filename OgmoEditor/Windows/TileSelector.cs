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
        private const int BUFFER = 4;

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
                Selection = 0;
                if (Tileset != null)
                    calculateScale();
                Invalidate();
            }
        }

        private void calculateScale()
        {
            scale = Math.Min((pictureBox.Width - BUFFER) / Tileset.Image.Width, (pictureBox.Height - BUFFER) / Tileset.Image.Height);
        }

        /*
         *  Events
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Tileset != null)
            { 
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(Tileset.Image, pictureBox.Width / 2 - Tileset.Image.Width / 2 * scale, pictureBox.Height / 2 - Tileset.Image.Height / 2 * scale, Tileset.Image.Width * scale, Tileset.Image.Height * scale);
            }
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            if (Tileset != null)
                calculateScale();
        }
    }
}
