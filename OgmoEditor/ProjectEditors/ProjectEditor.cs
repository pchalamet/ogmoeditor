using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Diagnostics;

namespace OgmoEditor.ProjectEditors
{
    public partial class ProjectEditor : Form
    {
        private Project project;
        private bool newProject;

        public ProjectEditor(Project project, bool newProject = false)
        {
            this.project = project;
            this.newProject = newProject;
            InitializeComponent();

            //Load the contents of the editors
            settingsEditor.LoadFromProject(project);
            layersEditor.LoadFromProject(project);
            tilesetsEditor.LoadFromProject(project);
            objectsEditor.LoadFromProject(project);

            //Events
            FormClosed += onClose;
        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            if (newProject)
                Ogmo.CloseProject();
            (Owner as MainWindow).EnableEditing();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string errors = "";
            errors += settingsEditor.ErrorCheck();
            errors += layersEditor.ErrorCheck();
            if (errors != "")
            {
                MessageBox.Show(this, "Project could not be saved because of the following errors:\n" + errors);
                return;
            }

            settingsEditor.ApplyToProject(project);
            layersEditor.ApplyToProject(project);
            tilesetsEditor.ApplyToProject(project);
            objectsEditor.ApplyToProject(project);
            project.Changed = true;

            if (newProject)
            {
                newProject = false;
                project.Save();
            }

            Close();
        }
    }
}
