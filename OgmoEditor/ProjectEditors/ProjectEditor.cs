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
        private Project oldProject;
        private Project newProject;
        private bool autoSave;

        public ProjectEditor(Project project, bool autoSave = false)
        {
            this.oldProject = project;
            this.autoSave = autoSave;
            InitializeComponent();

            newProject = new Project();
            newProject.CloneFrom(oldProject);

            //Load the contents of the editors
            settingsEditor.LoadFromProject(newProject);
            layersEditor.LoadFromProject(newProject);
            tilesetsEditor.LoadFromProject(newProject);
            objectsEditor.LoadFromProject(newProject);

            //Events
            FormClosed += onClose;
        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            if (autoSave)
                Ogmo.CloseProject();
            (Owner as MainWindow).EnableEditing();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Check for errors
            string errors = newProject.ErrorCheck();
            if (errors != "")
            {
                MessageBox.Show(this, "Project could not be saved because of the following errors:\n" + errors);
                return;
            }

            //Copy the edited project onto the old one
            oldProject.CloneFrom(newProject);

            //Save it if in auto-save mode. Otherwise, just mark it as changed
            if (autoSave)
            {
                autoSave = false;
                oldProject.Save();
            }
            else
                oldProject.Changed = true;

            //Close the project editor
            Close();
        }
    }
}
