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
using OgmoEditor.Windows;

namespace OgmoEditor.ProjectEditors
{
    public partial class ProjectEditor : Form
    {
        private Project oldProject;
        private Project newProject;
        private bool autoClose;
        private Ogmo.FinishProjectEditAction finishAction;

        public ProjectEditor(Project project, bool autoClose = false)
        {
            this.oldProject = project;
            this.autoClose = autoClose;
            InitializeComponent();

            newProject = new Project();
            newProject.CloneFrom(oldProject);

            if (autoClose)
                finishAction = Ogmo.FinishProjectEditAction.CloseProject;
            else
                finishAction = Ogmo.FinishProjectEditAction.None;

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
            Ogmo.FinishProjectEdit(finishAction);
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

            //Save the project after closing the form
            finishAction = Ogmo.FinishProjectEditAction.SaveProject;

            //Close the project editor
            Close();
        }
    }
}
