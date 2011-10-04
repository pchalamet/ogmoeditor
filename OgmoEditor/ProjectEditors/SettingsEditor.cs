using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    public partial class SettingsEditor : UserControl, IProjectChanger
    {
        public SettingsEditor()
        {
            InitializeComponent();
        }

        public void LoadFromProject(Project project)
        {
            projectNameTextBox.Text = project.Name;
            workingDirectoryTextBox.Text = project.WorkingDirectory;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();
        }

        public void ApplyToProject(Project project)
        {
            project.Name = projectNameTextBox.Text;
            project.WorkingDirectory = workingDirectoryTextBox.Text;
            ProjParse.GetSize(ref project.LevelDefaultSize, defaultWidthTextBox, defaultHeightTextBox);
            ProjParse.GetSize(ref project.LevelMinimumSize, minWidthTextBox, minHeightTextBox);
            ProjParse.GetSize(ref project.LevelMaximumSize, maxWidthTextBox, maxHeightTextBox);
        }

        private void workingDirectoryChooser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = workingDirectoryTextBox.Text;

            //Handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            workingDirectoryTextBox.Text = dialog.SelectedPath;
        }
    }
}
