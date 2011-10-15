using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor
{
    public partial class ImagePreviewer : UserControl
    {
        public ImagePreviewer()
        {
            InitializeComponent();
        }

        public bool LoadImage(string path)
        {
            return false;
        }

        public void ClearImage()
        {

        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {

        }

        public int ImageWidth
        {
            get { return 0; }
        }

        public int ImageHeight
        {
            get { return 0; }
        }
    }
}
