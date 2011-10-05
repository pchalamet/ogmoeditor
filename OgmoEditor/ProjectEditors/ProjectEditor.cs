using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    public partial class ProjectEditor : Form
    {
        private Project project;

        public ProjectEditor(Project project)
        {
            this.project = project;
            InitializeComponent();

            //Load the contents of the editors
            settingsEditor.LoadFromProject(project);

            //Events
            FormClosed += onClose;
        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            settingsEditor.ApplyToProject(project);
            project.Changed = true;
            Close();
        }
    }
}
