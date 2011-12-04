using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public partial class PreferencesWindow : Form
    {
        public PreferencesWindow()
        {
            InitializeComponent();
        }

        private void PreferencesWindow_Shown(object sender, EventArgs e)
        {
            maximizeCheckBox.Checked = Config.ConfigFile.StartMaximized;
            updatesCheckBox.Checked = Config.ConfigFile.CheckForUpdates;
            undoLimitTextBox.Text = Config.ConfigFile.UndoLimit.ToString();

            clearHistoryButton.Enabled = Config.ConfigFile.RecentProjects.Count > 0;
        }

        private void PreferencesWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.ConfigFile.StartMaximized = maximizeCheckBox.Checked;
            Config.ConfigFile.CheckForUpdates = updatesCheckBox.Checked;
            OgmoParse.Parse(ref Config.ConfigFile.UndoLimit, undoLimitTextBox);

            Config.Save();
            Ogmo.MainWindow.EnableEditing();
        }

        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            Config.ConfigFile.ClearRecentProjects();
            clearHistoryButton.Enabled = false;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
