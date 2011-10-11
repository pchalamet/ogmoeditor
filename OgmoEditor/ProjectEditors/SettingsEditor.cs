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
            backgroundColorChooser.Color = project.BackgroundColor;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();

            valuesEditor.SetList(project.LevelValueDefinitions);
        }

        public void ApplyToProject(Project project)
        {
            project.Name = projectNameTextBox.Text;
            project.BackgroundColor = backgroundColorChooser.Color;
            ProjParse.Parse(ref project.LevelDefaultSize, defaultWidthTextBox, defaultHeightTextBox);
            ProjParse.Parse(ref project.LevelMinimumSize, minWidthTextBox, minHeightTextBox);
            ProjParse.Parse(ref project.LevelMaximumSize, maxWidthTextBox, maxHeightTextBox);
        }

    }
}
