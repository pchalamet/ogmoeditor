using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.ValueEditors;

namespace OgmoEditor.LevelEditors
{
    public partial class LevelProperties : Form
    {
        private Level level;
        private List<string> oldValues;

        public LevelProperties(Level level)
        {
            this.level = level;
            InitializeComponent();

            //Init textboxes
            sizeXTextBox.Text = level.Size.Width.ToString();
            sizeYTextBox.Text = level.Size.Height.ToString();
            minSizeLabel.Text = "Minimum Size: " + level.Project.LevelMinimumSize.Width + " x " + level.Project.LevelMinimumSize.Height;
            maxSizeLabel.Text = "Maximum Size: " + level.Project.LevelMaximumSize.Width + " x " + level.Project.LevelMaximumSize.Height;

            //Values
            if (level.Values != null)
            {
                //Store the old values
                oldValues = new List<string>(level.Values.Count);
                foreach (var v in level.Values)
                    oldValues.Add(v.Content);

                //Create the editors
                ValueEditor ed;
                int yy = 100;
                foreach (var v in level.Values)
                {
                    ed = v.Definition.GetInstanceEditor(v, ClientSize.Width / 2 - 64, yy);
                    Controls.Add(ed);
                    yy += ed.Height;
                }
            }
        }

        private void LevelProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ogmo.MainWindow.EnableEditing();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Restore values to what they were
            if (level.Values != null)
            {
                for (int i = 0; i < level.Values.Count; i++)
                    level.Values[i].Content = oldValues[i];
            }

            Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            //Resize the level?

            Close();
        }
    }
}
