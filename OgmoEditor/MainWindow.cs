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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectUnload += onProjectUnload;
        }

        /*
         *  Project events
         */
        private void onProjectStart(Project project)
        {
            //Init the tree view
            project.TreeNode.ContextMenuStrip = projectNodeContextMenu;
            MasterTreeView.Nodes.Add(project.TreeNode);
            MasterTreeView.SelectedNode = project.TreeNode;

            //Disable menu items
            closeProjectToolStripMenuItem.Enabled = true;
            newLevelToolStripMenuItem.Enabled = true;
            saveProjectToolStripMenuItem.Enabled = true;
            saveProjectAsToolStripMenuItem.Enabled = true;
        }

        private void onProjectUnload(Project project)
        {
            //Clear the tree view
            MasterTreeView.Nodes.Clear();

            //Disable menu items
            closeProjectToolStripMenuItem.Enabled = false;
            newLevelToolStripMenuItem.Enabled = false;
            saveProjectToolStripMenuItem.Enabled = false;
            saveProjectAsToolStripMenuItem.Enabled = false;
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
            Ogmo.UnloadProject();
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

        /*
         *  Clicking the project node context menu items
         */
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
            Ogmo.UnloadProject();
        }

    }
}
