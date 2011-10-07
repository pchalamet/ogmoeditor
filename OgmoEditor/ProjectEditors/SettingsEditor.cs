using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

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
            relativePathCheckbox.Checked = project.WorkingDirectoryRelative;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();

            updateWarningVisibilities();
        }

        public void ApplyToProject(Project project)
        {
            project.Name = projectNameTextBox.Text;
            project.WorkingDirectory = workingDirectoryTextBox.Text;
            project.WorkingDirectoryRelative = relativePathCheckbox.Checked;
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

            if (relativePathCheckbox.Checked)
                workingDirectoryTextBox.Text = Util.GetPathRelativeTo(dialog.SelectedPath, Ogmo.Project.SavedDirectory);
            else
                workingDirectoryTextBox.Text = dialog.SelectedPath;

            updateWarningVisibilities();
        }

        private void relativePathCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            updateWarningVisibilities();

            if (relativePathCheckbox.Checked)
                workingDirectoryTextBox.Text = Util.GetPathRelativeTo(workingDirectoryTextBox.Text, Ogmo.Project.SavedDirectory);
            else
                workingDirectoryTextBox.Text = Util.GetPathAbsolute(workingDirectoryTextBox.Text, Ogmo.Project.SavedDirectory);
        }

        private void updateWarningVisibilities()
        {
            if (relativePathCheckbox.Checked)
            {
                projectUnsavedLabel.Visible = (Ogmo.Project.LastFilename == "");
                if (Ogmo.Project.LastFilename == "")
                    pathExistsLabel.Visible = true;
                else
                    pathExistsLabel.Visible = !Directory.Exists(Ogmo.Project.SavedDirectory + workingDirectoryTextBox.Text);
            }
            else
            {
                projectUnsavedLabel.Visible = false;
                pathExistsLabel.Visible = !Directory.Exists(workingDirectoryTextBox.Text);
            }
        }
    }
}
