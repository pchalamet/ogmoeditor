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
        private TreeNode rightClickedNode;
        private Level currentLevel;

        public MainWindow()
        {
            InitializeComponent();
            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
        }

        private void editProject()
        {
            Enabled = false;
            ProjectEditor editor = new ProjectEditor(Ogmo.Project);
            editor.Show(this);
        }

        /*
         *  Ogmo event Callbacks
         */
        private void onProjectStart(Project project)
        {
            //Init the tree view
            project.TreeNode.ContextMenuStrip = projectNodeContextMenu;
            MasterTreeView.Nodes.Add(project.TreeNode);
            MasterTreeView.SelectedNode = project.TreeNode;

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

            //Add events
            project.OnLevelAdded += onLevelAdded;
            project.OnLevelClosed += onLevelClosed;
        }

        private void onProjectClose(Project project)
        {
            //Clear the tree view
            MasterTreeView.Nodes.Clear();

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

            //Remove the level edit view
            if (currentLevel != null)
            {
                Controls.Remove(currentLevel.Control);
                currentLevel = null;
            }

            //Remove events
            project.OnLevelAdded -= onLevelAdded;
            project.OnLevelClosed -= onLevelClosed;
        }

        private void onLevelAdded(Level level)
        {
            level.TreeNode.ContextMenuStrip = levelNodeContextMenu;
            MasterTreeView.SelectedNode = level.TreeNode;
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

                //Remove the level edit view
                if (currentLevel != null)
                {
                    Controls.Remove(currentLevel.Control);
                    currentLevel = null;
                }
            }
            else
            {
                saveLevelToolStripMenuItem.Enabled = true;
                saveLevelAsToolStripMenuItem.Enabled = true;
                closeLevelToolStripMenuItem.Enabled = true;
                duplicateLevelToolStripMenuItem.Enabled = true;
                closeOtherLevelsToolStripMenuItem.Enabled = true;
                saveAsImageToolStripMenuItem.Enabled = true;

                //Add the level edit view
                if (currentLevel != null)
                    Controls.Remove(currentLevel.Control);
                currentLevel = Ogmo.Project.GetLevelFromNode(e.Node);
                Controls.Add(currentLevel.Control);
            }
        }

        private void MasterTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                rightClickedNode = e.Node;
        }

        private void MasterTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == Ogmo.Project.TreeNode)
                editProject();
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
            editProject();
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
            Ogmo.Project.GetLevelFromNode(MasterTreeView.SelectedNode).Save();
        }

        private void saveLevelAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.GetLevelFromNode(MasterTreeView.SelectedNode).SaveAs();
        }

        private void closeLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseLevel(Ogmo.Project.GetLevelFromNode(MasterTreeView.SelectedNode));
        }

        private void duplicateLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.AddLevel(Ogmo.Project.GetLevelFromNode(MasterTreeView.SelectedNode).Duplicate());
        }

        private void closeOtherLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseOtherLevels(Ogmo.Project.GetLevelFromNode(MasterTreeView.SelectedNode));
        }

        private void openAllLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.OpenAllLevels();
        }

        private void projectViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterTreeView.Visible = !MasterTreeView.Visible;
        }

        /*
         *  Clicking the project/level node context menu items
         */
        private void saveLevelAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.GetLevelFromNode(rightClickedNode).SaveAs();
        }

        private void closeLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseLevel(Ogmo.Project.GetLevelFromNode(rightClickedNode));
        }

        private void duplicateLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.AddLevel(Ogmo.Project.GetLevelFromNode(rightClickedNode).Duplicate());
        }

        private void closeOtherLevelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.CloseOtherLevels(Ogmo.Project.GetLevelFromNode(rightClickedNode));
        }
    }
}
