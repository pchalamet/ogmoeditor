﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Windows
{
    public partial class EntitySelectionImage : UserControl
    {
        private Entity entity;
        private Image image;

        public EntitySelectionImage(Entity entity, int x, int y)
        {
            this.entity = entity;
            Location = new Point(x, y);
            InitializeComponent();
            image = entity.Definition.Image;

            toolTip.SetToolTip(pictureBox, entity.Definition.Name);
        }

        /*
         *  Events
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            float scale = Math.Min(ClientSize.Width / (float)image.Width, ClientSize.Height / (float)image.Height);
            int destWidth = (int)(image.Width * scale);
            int destHeight = (int)(image.Height * scale);
            Graphics g = e.Graphics;
            g.DrawImage(image,
                new Rectangle(pictureBox.ClientSize.Width / 2 - destWidth / 2, pictureBox.ClientSize.Height / 2 - destHeight / 2, destWidth, destHeight),
                new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            //click : select only that instance
            //right-click : deselect that instance
            //CTRL + click : select all of instance's type

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    Ogmo.EntitySelectionWindow.SetSelection(((EntityLayer)Ogmo.CurrentLevel.Layers[Ogmo.LayersWindow.CurrentLayerIndex]).Entities.FindAll(en => en.Definition == entity.Definition));
                else
                    Ogmo.EntitySelectionWindow.SetSelection(entity);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Ogmo.EntitySelectionWindow.RemoveFromSelection(entity);
            }
        }
    }
}
