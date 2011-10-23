﻿using System;
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
        private const int EDIT_BOUNDS_PADDING = 10;

        public bool EditingGridVisible { get; private set; }
        public List<LevelEditor> LevelEditors { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            EditingGridVisible = true;
            LevelEditors = new List<LevelEditor>();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectClose += onProjectClose;
            Ogmo.OnLevelAdded += onLevelAdded;
            Ogmo.OnLevelClosed += onLevelClosed;
            Ogmo.OnLevelChanged += onLevelChanged;
        }

        public void FocusEditor()
        {
            if (LevelEditors.Count > 0)
                LevelEditors[Ogmo.CurrentLevelIndex].Focus();
            else
                Focus();
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

        public Rectangle EditBounds
        {
            get
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    int titleBar = Size.Height - ClientSize.Height;
                    return new Rectangle(Location.X + 8 + MasterTabControl.Location.X + EDIT_BOUNDS_PADDING,
                        Location.Y + titleBar + MasterTabControl.Location.Y + 10 + EDIT_BOUNDS_PADDING, 
                        MasterTabControl.Width - (EDIT_BOUNDS_PADDING * 2),
                        MasterTabControl.Height - 20 - (EDIT_BOUNDS_PADDING * 2));
                }
                else
                {
                    int border = (Size.Width - ClientSize.Width) / 2;
                    int titleBar = Size.Height - ClientSize.Height - border;
                    return new Rectangle(
                        Location.X + border + MasterTabControl.Location.X + EDIT_BOUNDS_PADDING,
                        Location.Y + 20 + titleBar + MasterTabControl.Location.Y + EDIT_BOUNDS_PADDING,
                        MasterTabControl.Width - (EDIT_BOUNDS_PADDING * 2),
                        MasterTabControl.Height - 20 - (EDIT_BOUNDS_PADDING * 2));
                }
            }
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

            levelToolStripMenuItem.Enabled = true;
            viewToolStripMenuItem.Enabled = true;
        }

        private void onProjectClose(Project project)
        {
            //Disable menu items
            newProjectToolStripMenuItem.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;
            closeProjectToolStripMenuItem.Enabled = false;
            editProjectToolStripMenuItem.Enabled = false;

            levelToolStripMenuItem.Enabled = false;
            viewToolStripMenuItem.Enabled = false;
        }

        private void onLevelAdded(int index)
        {
            TabPage t = new TabPage(Ogmo.Levels[index].Name);
            LevelEditor e = new LevelEditor(Ogmo.Levels[index]);
            LevelEditors.Add(e);
            t.Controls.Add(e);
            MasterTabControl.TabPages.Add(t);
        }

        private void onLevelClosed(int index)
        {
            MasterTabControl.TabPages.RemoveAt(index);
            LevelEditors[index].OnRemove();
            LevelEditors.RemoveAt(index);
        }

        private void onLevelChanged(int index)
        {
            SelectedLevelIndex = index;
            
            //Switch to the editor
            if (index != -1)
                LevelEditors[index].SwitchTo();

            //Refresh stuff
            editToolStripMenuItem.Enabled =
                levelPropertiesToolStripMenuItem.Enabled =
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
            Ogmo.EditProject(false);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.LoadProject();
        }

        private void levelPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CurrentLevel.EditProperties();
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
            Ogmo.CloseLevel(Ogmo.Levels[SelectedLevelIndex], true);
        }

        private void duplicateLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.AddLevel(new Level(Ogmo.Levels[SelectedLevelIndex]));
        }

        private void closeOtherLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.CloseOtherLevels(Ogmo.Levels[SelectedLevelIndex]);
        }

        private void openAllLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogmo.OpenAllLevels();
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

        /*
         *  View-Related Menu Items
         */
        private void viewToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            editingGridToolStripMenuItem.Checked = EditingGridVisible;

            layersToolStripMenuItem.Enabled = Ogmo.LayersWindow.EditorVisible;
            layersToolStripMenuItem.Checked = Ogmo.LayersWindow.UserVisible;

            toolsToolStripMenuItem.Enabled = Ogmo.ToolsWindow.EditorVisible;
            toolsToolStripMenuItem.Checked = Ogmo.ToolsWindow.UserVisible;

            objectsToolStripMenuItem.Enabled = Ogmo.EntitiesWindow.EditorVisible;
            objectsToolStripMenuItem.Checked = Ogmo.EntitiesWindow.UserVisible;
        }

        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ogmo.LayersWindow.EditorVisible)
            {
                Ogmo.LayersWindow.UserVisible = !Ogmo.LayersWindow.UserVisible;
                Focus();
            }
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ogmo.ToolsWindow.EditorVisible)
            {
                Ogmo.ToolsWindow.UserVisible = !Ogmo.ToolsWindow.UserVisible;
                Focus();
            }
        }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ogmo.EntitiesWindow.EditorVisible)
            {
                Ogmo.EntitiesWindow.UserVisible = !Ogmo.EntitiesWindow.UserVisible;
                Focus();
            }
        }

        private void entitySelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ogmo.EntitySelectionWindow.EditorVisible)
            {
                Ogmo.EntitySelectionWindow.UserVisible = !Ogmo.EntitySelectionWindow.UserVisible;
                Focus();
            }
        }

        private void editingGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditingGridVisible = !EditingGridVisible;
        }

    }
}
