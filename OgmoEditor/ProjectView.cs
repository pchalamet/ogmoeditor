using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor
{
    public partial class ProjectView : Form
    {
        private TreeNode rightClickedNode;

        public ProjectView()
        {
            InitializeComponent();

            SetProject(null);

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
        }

        public void SetProject(Project project)
        {
            masterTreeView.Nodes.Clear();
            if (project != null)
            {
                masterTreeView.Nodes.Add(project.TreeNode);
                project.TreeNode.ContextMenuStrip = projectNodeContextMenu;
            }
        }

        private void onProjectStart(Project project)
        {
            SetProject(project);
            masterTreeView.SelectedNode = project.TreeNode;

            project.OnLevelAdded += onLevelAdded;
            project.OnLevelClosed += onLevelClosed;
        }

        private void onProjectClose(Project project)
        {
            SetProject(null);

            project.OnLevelAdded -= onLevelAdded;
            project.OnLevelClosed -= onLevelClosed;
        }

        private void onLevelAdded(Level level)
        {
            level.TreeNode.ContextMenuStrip = levelNodeContextMenu;
            masterTreeView.SelectedNode = level.TreeNode;
        }

        private void onLevelClosed(Level level)
        {

        }

        public TreeNode SelectedNode
        {
            get { return masterTreeView.SelectedNode; }
        }

        /*
         *  Tree view events
         */
        private void MasterTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                rightClickedNode = e.Node;
        }

        private void MasterTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == Ogmo.Project.TreeNode)
            {
                Ogmo.Project.TreeNode.Expand();
                (Owner as MainWindow).DisableEditing();
                Ogmo.EditProject();
            }
        }

        /*
         *  Clicking the project/level node context menu items
         */
        private void saveLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.GetLevelFromNode(rightClickedNode).Save();
        }

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

        private void editProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (Owner as MainWindow).DisableEditing();
            Ogmo.EditProject();
        }

        private void saveProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.Save();
        }

        private void saveProjectAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.SaveAs();
        }

        private void closeProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.CloseProject();
        }

        private void newLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.NewLevel();
        }

        private void openLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ogmo.Project.OpenLevel();
        }

        private void ProjectView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Visible = false;
                e.Cancel = true;
            }
        }
    }
}
