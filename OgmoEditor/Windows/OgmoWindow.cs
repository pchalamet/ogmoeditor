using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class OgmoWindow : Form
    {
        private const int SNAP_RANGE = 20;
        private const int SNAP_PADDING = 10;

        public OgmoWindow()
        {
            InitializeComponent();
        }

        private void OgmoWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
                Ogmo.MainWindow.Focus();
            }
        }

        private void OgmoWindow_LocationChanged(object sender, EventArgs e)
        {
            Rectangle r = Ogmo.MainWindow.EditBounds;
            r.X += SNAP_PADDING;
            r.Y += SNAP_PADDING;
            r.Width -= SNAP_PADDING * 2;
            r.Height -= SNAP_PADDING * 2;
            Point p = Location;

            if (Math.Abs(p.X - r.X) <= SNAP_RANGE)
                p.X = r.X;
            else if (Math.Abs((p.X + Width) - (r.X + r.Width)) <= SNAP_RANGE)
                p.X = r.X + r.Width - Width;

            if (Math.Abs(p.Y - r.Y) <= SNAP_RANGE)
                p.Y = r.Y;
            else if (Math.Abs((p.Y + Height) - (r.Y + r.Height)) <= SNAP_RANGE)
                p.Y = r.Y + r.Height - Height;

            Location = p;
        }
    }
}
