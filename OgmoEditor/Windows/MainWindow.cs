using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.ProjectEditors;
using OgmoEditor.LevelEditors;
using OgmoEditor.XNA;
using System.Diagnostics;
using OgmoEditor.LevelData;

namespace OgmoEditor.Windows
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
            Ogmo.OnLevelAdded += onLevelAdded;
            Ogmo.OnLevelClosed += onLevelClosed;
        }

        public void EnableEditing()
        {
            Enabled = true;
            foreach (var f in OwnedForms)
                f.Enabled = true;
        }

        public void DisableEditing()
        {
            Enabled = false;
            foreach (var f in OwnedForms)
                f.Enabled = false;
        }

        public int SelectedLevelIndex
        {
            get { return masterTabControl.SelectedIndex; }
            set { masterTabControl.SelectedIndex = value; }
        }

        public void SetLevel(Level level)
        {
            masterTabControl.SelectedIndex = Ogmo.Levels.IndexOf(level);
        }

        /*
         *  Ogmo event Callbacks
         */
        private void onProjectStart(Project project)
        {
            //Enable menu items
            newProjectToolStripMenuItem.Enabled = false;
            openProjectToolStripMenuItem.Enabled = false;
            closeProjectToolStripMenuItem.Enabled = true;
            editProjectToolStripMenuItem.Enabled = true;
            saveProjectToolStripMenuItem.Enabled = true;
            saveProjectAsToolStripMenuItem.Enabled = true;

            levelToolStripMenuItem.Enabled = true;
            viewToolStripMenuItem.Enabled = true;

            //Enable windows
            Ogmo.LayersWindow.Visible = Ogmo.ToolsWindow.Visible = true;
        }

        private void onProjectClose(Project project)
        {
            //Disable menu items
            newProjectToolStripMenuItem.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;
            closeProjectToolStripMenuItem.Enabled = false;
            editProjectToolStripMenuItem.Enabled = false;
            saveProjectToolStripMenuItem.Enabled = false;
            saveProjectAsToolStripMenuItem.Enabled = false;

            levelToolStripMenuItem.Enabled = false;
            viewToolStripMenuItem.Enabled = false;

            //Disable windows
            Ogmo.LayersWindow.Visible = Ogmo.ToolsWindow.Visible = false;
        }

        private void onLevelAdded(Level level)
        {
            TabPage t = new TabPage(level.Name);
            t.Controls.Add(new LevelEditor(level));
            masterTabControl.TabPages.Add(t);
            masterTabControl.SelectedTab = t;
        }

        private void onLevelClosed(Level level)
        {
            masterTabControl.TabPages.RemoveAt(Ogmo.Levels.IndexOf(level));
        }

        private void refreshLevelState()
        {
            saveLevelToolStripMenuItem.Enabled =
                saveLevelAsToolStripMenuItem.Enabled =
                closeLevelToolStripMenuItem.Enabled =
                duplicateLevelToolStripMenuItem.Enabled =
                closeOtherLevelsToolStripMenuItem.Enabled =
                saveAsImageToolStripMenuItem.Enabled = (masterTabControl.SelectedIndex < Ogmo.Levels.Count);

            if (masterTabControl.SelectedTab != null)
                masterTabControl.SelectedTab.Controls[0].Focus();
        }

        /*
         *  Tab control events
         */
        private void masterTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            refreshLevelState();
        }

        private void masterTabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            refreshLevelState();
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
            Ogmo.NewProject();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CloseProject();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableEditing();
            Ogmo.EditProject();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Project.Save();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ogmo.Project.SaveAs())
                Ogmo.Project.Save();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.LoadProject();
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.NewLevel();
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.OpenLevel();
        }

        private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Levels[SelectedLevelIndex].Save();
        }

        private void saveLevelAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.Levels[SelectedLevelIndex].SaveAs();
        }

        private void closeLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CloseLevel(Ogmo.Levels[SelectedLevelIndex]);
        }

        private void duplicateLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.AddLevel(Ogmo.Levels[SelectedLevelIndex].Duplicate());
        }

        private void closeOtherLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CloseOtherLevels(Ogmo.Levels[SelectedLevelIndex]);
        }

        private void openAllLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.OpenAllLevels();
        }

        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.LayersWindow.Visible = !Ogmo.LayersWindow.Visible;
            Focus();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.ToolsWindow.Visible = !Ogmo.ToolsWindow.Visible;
            Focus();
        }

    }
}
