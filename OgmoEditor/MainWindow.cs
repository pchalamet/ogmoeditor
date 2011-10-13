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

namespace OgmoEditor
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
            newLevelToolStripMenuItem.Enabled = true;
            openLevelToolStripMenuItem.Enabled = true;
            openAllLevelsToolStripMenuItem.Enabled = true;
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

            newLevelToolStripMenuItem.Enabled = false;
            openLevelToolStripMenuItem.Enabled = false;
            openAllLevelsToolStripMenuItem.Enabled = false;
            saveLevelToolStripMenuItem.Enabled = false;
            saveLevelAsToolStripMenuItem.Enabled = false;
            closeLevelToolStripMenuItem.Enabled = false;
            duplicateLevelToolStripMenuItem.Enabled = false;
            closeOtherLevelsToolStripMenuItem.Enabled = false;
            saveAsImageToolStripMenuItem.Enabled = false;
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

    }
}
