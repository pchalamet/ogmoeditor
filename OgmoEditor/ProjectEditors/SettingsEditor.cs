using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    public partial class SettingsEditor : UserControl, IProjectEditor
    {
        public SettingsEditor()
        {
            InitializeComponent();
        }

        public void LoadFromProject(Project project)
        {
            projectNameTextBox.Text = project.Name;
        }

        public void ApplyToProject(Project project)
        {
            project.Name = projectNameTextBox.Text;
        }
    }
}
