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

            valuesEditor.Title = "Level Values";
        }

        public string ErrorCheck()
        {
            string s = "";
            s += ProjParse.CheckString(projectNameTextBox, "Project Name");
            s += ProjParse.CheckPosInt(defaultWidthTextBox, "Default Level Width");
            s += ProjParse.CheckPosInt(defaultHeightTextBox, "Default Level Height");
            s += ProjParse.CheckPosInt(minWidthTextBox, "Minimum Level Width");
            s += ProjParse.CheckPosInt(minHeightTextBox, "Minimum Level Height");
            s += ProjParse.CheckPosInt(maxWidthTextBox, "Maximum Level Width");
            s += ProjParse.CheckPosInt(maxHeightTextBox, "Maximum Level Height");
            s += valuesEditor.ErrorCheck("Level");
            return s;
        }

        public void LoadFromProject(Project project)
        {
            projectNameTextBox.Text = project.Name;
            defaultWidthTextBox.Text = project.LevelDefaultSize.Width.ToString();
            defaultHeightTextBox.Text = project.LevelDefaultSize.Height.ToString();
            minWidthTextBox.Text = project.LevelMinimumSize.Width.ToString();
            minHeightTextBox.Text = project.LevelMinimumSize.Height.ToString();
            maxWidthTextBox.Text = project.LevelMaximumSize.Width.ToString();
            maxHeightTextBox.Text = project.LevelMaximumSize.Height.ToString();
            valuesEditor.Values = project.LevelValuesDefinitions;
        }

        public void ApplyToProject(Project project)
        {
            project.Name = projectNameTextBox.Text;
            ProjParse.GetSize(ref project.LevelDefaultSize, defaultWidthTextBox, defaultHeightTextBox);
            ProjParse.GetSize(ref project.LevelMinimumSize, minWidthTextBox, minHeightTextBox);
            ProjParse.GetSize(ref project.LevelMaximumSize, maxWidthTextBox, maxHeightTextBox);
            project.LevelValuesDefinitions = valuesEditor.Values;
        }

    }
}
