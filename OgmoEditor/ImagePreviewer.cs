using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace OgmoEditor
{
    public partial class ImagePreviewer : UserControl
    {
        private Image image;
        private Rectangle clipRect;

        public ImagePreviewer()
        {
            InitializeComponent();
        }

        public bool LoadImage(string path, Rectangle? clip = null)
        {
            if (File.Exists(path))
            {
                FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                image = Image.FromStream(s);
                s.Close();
                clipRect = clip ?? new Rectangle(0, 0, image.Width, image.Height);
                pictureBox.Refresh();
                return true;
            }
            else
            {
                ClearImage();
                return false;
            }
        }

        public void LoadImage(Image img, Rectangle? clip = null)
        {
            image = img;
            clipRect = clip ?? new Rectangle(0, 0, image.Width, image.Height);
            pictureBox.Refresh();
        }

        public void SetClip(Rectangle r)
        {
            clipRect = r;
            pictureBox.Refresh();
        }

        public void ClearImage()
        {
            image = null;
            pictureBox.Refresh();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (image != null)
            {
                if (clipRect.Width > ClientSize.Width || clipRect.Height > ClientSize.Height)
                {
                    float scale = Math.Min(ClientSize.Width / (float)clipRect.Width, ClientSize.Height / (float)clipRect.Height);
                    int destWidth = (int)(clipRect.Width * scale);
                    int destHeight = (int)(clipRect.Height * scale);
                    g.DrawImage(image,
                        new Rectangle(pictureBox.ClientSize.Width / 2 - destWidth / 2, pictureBox.ClientSize.Height / 2 - destHeight / 2, destWidth, destHeight),
                        clipRect, GraphicsUnit.Pixel);
                }
                else
                    g.DrawImage(image,
                        pictureBox.ClientSize.Width / 2 - clipRect.Width / 2, pictureBox.ClientSize.Height / 2 - clipRect.Height / 2,
                        clipRect, GraphicsUnit.Pixel);
            }
            else
            {
                g.DrawImage(pictureBox.ErrorImage,
                    new Point(pictureBox.ClientSize.Width / 2 - pictureBox.ErrorImage.Width / 2 - 7, pictureBox.ClientSize.Height / 2 - pictureBox.ErrorImage.Height / 2 - 7));
            }
        }

        public int ImageWidth
        {
            get
            {
                if (image == null)
                    return -1;
                else 
                    return clipRect.Width;
            }
        }

        public int ImageHeight
        {
            get
            {
                if (image == null)
                    return -1;
                else
                    return clipRect.Height;
            }
        }
    }
}
