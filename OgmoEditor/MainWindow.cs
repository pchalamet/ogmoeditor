using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.ProjectEditors;

namespace OgmoEditor
{
    public partial class MainWindow : Form
    {
        private Level currentLevel;
        private ProjectView projectView;

        public MainWindow()
        {
            InitializeComponent();

            projectView = new ProjectView();
            projectView.Show(this);
            projectView.Visible = false;

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
        }

        /*
         *  Ogmo event Callbacks
         */
        private void onProjectStart(Project project)
        {
            //Init the tree view
            projectView.Visible = true;
            
            //Enable menu items
            newProjectToolStripMenuItem.Enabled = false;
            openProjectToolStripMenuItem.Enabled = false;
            closeProjectToolStripMenuItem.Enabled = true;
            editProjectToolStripMenuItem.Enabled = true;
            saveProjectToolStripMenuItem.Enabled = true;
            saveProjectAsToolStripMenuItem.Enabled = true;
            newLevelToolStripMenuItem.Enabled = true;
            openLevelToolStripMenuItem.Enabled = true;
            openAllLevelsToolStripMenuItem.Enabled = true;
            projectViewToolStripMenuItem.Enabled = true;

            //Add events
            project.OnLevelAdded += onLevelAdded;
            project.OnLevelClosed += onLevelClosed;
        }

        private void onProjectClose(Project project)
        {
            //Remove project view
            projectView.Visible = false;

            //Disable menu items
            newProjectToolStripMenuItem.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;
            closeProjectToolStripMenuItem.Enabled = false;
            editProjectToolStripMenuItem.Enabled = false;
            saveProjectToolStripMenuItem.Enabled = false;
            saveProjectAsToolStripMenuItem.Enabled = false;

            newLevelToolStripMenuItem.Enabled = false;
            openLevelToolStripMenuItem.Enabled = false;
            openAllLevelsToolStripMenuItem.Enabled = false;
            saveLevelToolStripMenuItem.Enabled = false;
            saveLevelAsToolStripMenuItem.Enabled = false;
            closeLevelToolStripMenuItem.Enabled = false;
            duplicateLevelToolStripMenuItem.Enabled = false;
            closeOtherLevelsToolStripMenuItem.Enabled = false;
            saveAsImageToolStripMenuItem.Enabled = false;
            projectViewToolStripMenuItem.Enabled = false;

            //No current level
            if (currentLevel != null)
                currentLevel = null;

            //Remove events
            project.OnLevelAdded -= onLevelAdded;
            project.OnLevelClosed -= onLevelClosed;
        }

        private void onLevelAdded(Level level)
        {
            
        }

        private void onLevelClosed(Level level)
        {
            
        }

        /*
         *  Tree view events
         */
        private void MasterTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == Ogmo.Project.TreeNode)
            {
                saveLevelToolStripMenuItem.Enabled = false;
                saveLevelAsToolStripMenuItem.Enabled = false;
                closeLevelToolStripMenuItem.Enabled = false;
                duplicateLevelToolStripMenuItem.Enabled = false;
                closeOtherLevelsToolStripMenuItem.Enabled = false;
                saveAsImageToolStripMenuItem.Enabled = false;

                //No current level
                if (currentLevel != null)
                    currentLevel = null;
            }
            else
            {
                saveLevelToolStripMenuItem.Enabled = true;
                saveLevelAsToolStripMenuItem.Enabled = true;
                closeLevelToolStripMenuItem.Enabled = true;
                duplicateLevelToolStripMenuItem.Enabled = true;
                closeOtherLevelsToolStripMenuItem.Enabled = true;
                saveAsImageToolStripMenuItem.Enabled = true;

                //Make the level the current one
                currentLevel = Ogmo.Project.GetLevelFromNode(e.Node);
            }
        }

        /*
         *  Clicking the File context menu itmes
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.StartProject(new Project());
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CloseProject();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.EditProject();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.Save();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.SaveAs();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.LoadProject();
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.NewLevel();
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.OpenLevel();
        }

        private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.GetLevelFromNode(projectView.SelectedNode).Save();
        }

        private void saveLevelAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.GetLevelFromNode(projectView.SelectedNode).SaveAs();
        }

        private void closeLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseLevel(Ogmo.Project.GetLevelFromNode(projectView.SelectedNode));
        }

        private void duplicateLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.AddLevel(Ogmo.Project.GetLevelFromNode(projectView.SelectedNode).Duplicate());
        }

        private void closeOtherLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseOtherLevels(Ogmo.Project.GetLevelFromNode(projectView.SelectedNode));
        }

        private void openAllLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.OpenAllLevels();
        }

        /*
         *  View events
         */
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectViewToolStripMenuItem.Checked = projectView.Visible;
        }

        private void projectViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectView.Visible = !projectView.Visible;
            Focus();
        }

    }
}
