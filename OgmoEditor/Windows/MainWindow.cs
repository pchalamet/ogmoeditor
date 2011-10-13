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
        public List<LevelEditor> LevelEditors { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            LevelEditors = new List<LevelEditor>();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
            Ogmo.OnLevelAdded += onLevelAdded;
            Ogmo.OnLevelClosed += onLevelClosed;
            Ogmo.OnLevelChanged += onLevelChanged;
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

        private int SelectedLevelIndex
        {
            get { return MasterTabControl.SelectedIndex; }
            set { MasterTabControl.SelectedIndex = value; }
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

        private void onLevelAdded(Level level, int index)
        {
            TabPage t = new TabPage(level.Name);
            LevelEditor e = new LevelEditor(level);

            LevelEditors.Add(e);
            t.Controls.Add(e);

            MasterTabControl.TabPages.Add(t);
        }

        private void onLevelClosed(Level level, int index)
        {
            MasterTabControl.TabPages.RemoveAt(index);
            LevelEditors.RemoveAt(index);
        }

        private void onLevelChanged(Level level, int index)
        {
            SelectedLevelIndex = index;
            
            //Switch to the editor
            if (index != -1)
                LevelEditors[index].SwitchTo();

            //Refresh stuff
            editToolStripMenuItem.Enabled =
                saveLevelToolStripMenuItem.Enabled =
                saveLevelAsToolStripMenuItem.Enabled =
                closeLevelToolStripMenuItem.Enabled =
                duplicateLevelToolStripMenuItem.Enabled =
                closeOtherLevelsToolStripMenuItem.Enabled =
                saveAsImageToolStripMenuItem.Enabled = index != -1;
        }

        /*
         *  Tab control events
         */
        private void MasterTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Ogmo.SetLevel(e.TabPageIndex);
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

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelEditors[Ogmo.CurrentLevelIndex].Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelEditors[Ogmo.CurrentLevelIndex].Redo();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = LevelEditors[Ogmo.CurrentLevelIndex].CanUndo;
            redoToolStripMenuItem.Enabled = LevelEditors[Ogmo.CurrentLevelIndex].CanRedo;
        }

    }
}
