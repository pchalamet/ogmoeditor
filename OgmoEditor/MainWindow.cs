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
            Ogmo.OnProjectChange += onProjectChange;
            Ogmo.OnProjectAdd += onProjectAdd;
            Ogmo.OnProjectRemove += onProjectRemove;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.AddProject(new Project()); 
        }

        private void onProjectChange(Project project, int projectID)
        {
            MasterTreeView.SelectedNode = MasterTreeView.Nodes[projectID];
        }

        private void onProjectAdd(Project project, int projectID)
        {
            MasterTreeView.Nodes.Add(new TreeNode(project.Name));
        }

        private void onProjectRemove(Project project, int projectID)
        {
            MasterTreeView.Nodes.RemoveAt(projectID);
        }

        private void MasterTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Ogmo.CurrentProjectID = MasterTreeView.Nodes.IndexOf(e.Node);
        }
    }
}
