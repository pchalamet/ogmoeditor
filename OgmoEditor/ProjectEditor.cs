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
    public partial class ProjectEditor : UserControl
    {
        private Project project;

        public ProjectEditor(Project project)
        {
            InitializeComponent();
            this.project = project;

            Location = new Point(152, 27);

            //Init settings stuff
            projectNameTextBox.Text = project.Name;
            workingDirectoryTextBox.Text = project.WorkingDirectory;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();
        }

        private void projectNameTextBox_Validated(object sender, EventArgs e)
        {
            project.Name = (sender as TextBox).Text;
        }
    }
}
