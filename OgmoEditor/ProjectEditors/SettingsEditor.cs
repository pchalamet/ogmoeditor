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
        private Project project;

        public SettingsEditor()
        {
            InitializeComponent();
        }

        public void LoadFromProject(Project project)
        {
            this.project = project;

            projectNameTextBox.Text = project.Name;
            backgroundColorChooser.Color = project.BackgroundColor;
            gridColorChooser.Color = project.GridColor;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();

            valuesEditor.SetList(project.LevelValueDefinitions);
        }

        private void projectNameTextBox_Validated(object sender, EventArgs e)
        {
            project.Name = projectNameTextBox.Text;
        }

        private void backgroundColorChooser_ColorChanged(OgmoColor color)
        {
            project.BackgroundColor = color;
        }

        private void defaultWidthTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref project.LevelDefaultSize, defaultWidthTextBox, defaultHeightTextBox);
        }

        private void minWidthTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref project.LevelMinimumSize, minWidthTextBox, minHeightTextBox);
        }

        private void maxWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            ProjParse.Parse(ref project.LevelMaximumSize, maxWidthTextBox, maxHeightTextBox);
        }

        private void gridColorChooser_ColorChanged(OgmoColor color)
        {
            project.GridColor = color;
        }

    }
}
