namespace OgmoEditor
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openAllLevelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.closeOtherLevelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MasterTreeView = new System.Windows.Forms.TreeView();
            this.projectNodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.newLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.levelNodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.duplicateLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeOtherLevelsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.projectEditTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mouseCoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.StatusStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.projectNodeContextMenu.SuspendLayout();
            this.levelNodeContextMenu.SuspendLayout();
            this.projectEditTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStatusLabel,
            this.mouseCoordinatesLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 540);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusStrip.Size = new System.Drawing.Size(784, 22);
            this.StatusStrip.TabIndex = 0;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(784, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visitWebsiteToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "Ogmo";
            // 
            // visitWebsiteToolStripMenuItem
            // 
            this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
            this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.visitWebsiteToolStripMenuItem.Text = "Visit Website";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.openAllLevelsToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project...";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Enabled = false;
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As...";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Enabled = false;
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // openAllLevelsToolStripMenuItem
            // 
            this.openAllLevelsToolStripMenuItem.Enabled = false;
            this.openAllLevelsToolStripMenuItem.Name = "openAllLevelsToolStripMenuItem";
            this.openAllLevelsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.openAllLevelsToolStripMenuItem.Text = "Open All Levels";
            this.openAllLevelsToolStripMenuItem.Click += new System.EventHandler(this.openAllLevelsToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLevelToolStripMenuItem,
            this.openLevelToolStripMenuItem,
            this.saveLevelToolStripMenuItem,
            this.saveLevelAsToolStripMenuItem,
            this.closeLevelToolStripMenuItem,
            this.toolStripSeparator5,
            this.closeOtherLevelsToolStripMenuItem,
            this.duplicateLevelToolStripMenuItem,
            this.saveAsImageToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Enabled = false;
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Enabled = false;
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.openLevelToolStripMenuItem.Text = "Open Level...";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Enabled = false;
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.saveLevelToolStripMenuItem_Click);
            // 
            // saveLevelAsToolStripMenuItem
            // 
            this.saveLevelAsToolStripMenuItem.Enabled = false;
            this.saveLevelAsToolStripMenuItem.Name = "saveLevelAsToolStripMenuItem";
            this.saveLevelAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveLevelAsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveLevelAsToolStripMenuItem.Text = "Save Level As...";
            this.saveLevelAsToolStripMenuItem.Click += new System.EventHandler(this.saveLevelAsToolStripMenuItem_Click);
            // 
            // closeLevelToolStripMenuItem
            // 
            this.closeLevelToolStripMenuItem.Enabled = false;
            this.closeLevelToolStripMenuItem.Name = "closeLevelToolStripMenuItem";
            this.closeLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeLevelToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.closeLevelToolStripMenuItem.Text = "Close Level";
            this.closeLevelToolStripMenuItem.Click += new System.EventHandler(this.closeLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(222, 6);
            // 
            // closeOtherLevelsToolStripMenuItem
            // 
            this.closeOtherLevelsToolStripMenuItem.Enabled = false;
            this.closeOtherLevelsToolStripMenuItem.Name = "closeOtherLevelsToolStripMenuItem";
            this.closeOtherLevelsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.closeOtherLevelsToolStripMenuItem.Text = "Close Other Levels";
            this.closeOtherLevelsToolStripMenuItem.Click += new System.EventHandler(this.closeOtherLevelsToolStripMenuItem_Click);
            // 
            // duplicateLevelToolStripMenuItem
            // 
            this.duplicateLevelToolStripMenuItem.Enabled = false;
            this.duplicateLevelToolStripMenuItem.Name = "duplicateLevelToolStripMenuItem";
            this.duplicateLevelToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.duplicateLevelToolStripMenuItem.Text = "Duplicate Level";
            this.duplicateLevelToolStripMenuItem.Click += new System.EventHandler(this.duplicateLevelToolStripMenuItem_Click);
            // 
            // saveAsImageToolStripMenuItem
            // 
            this.saveAsImageToolStripMenuItem.Enabled = false;
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveAsImageToolStripMenuItem.Text = "Save As Image...";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // projectViewToolStripMenuItem
            // 
            this.projectViewToolStripMenuItem.Checked = true;
            this.projectViewToolStripMenuItem.CheckOnClick = true;
            this.projectViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.projectViewToolStripMenuItem.Name = "projectViewToolStripMenuItem";
            this.projectViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.projectViewToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.projectViewToolStripMenuItem.Text = "Project View";
            this.projectViewToolStripMenuItem.Click += new System.EventHandler(this.projectViewToolStripMenuItem_Click);
            // 
            // MasterTreeView
            // 
            this.MasterTreeView.AccessibleName = "";
            this.MasterTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.MasterTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.MasterTreeView.HideSelection = false;
            this.MasterTreeView.Location = new System.Drawing.Point(2, 27);
            this.MasterTreeView.Name = "MasterTreeView";
            this.MasterTreeView.Size = new System.Drawing.Size(150, 510);
            this.MasterTreeView.TabIndex = 2;
            this.MasterTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MasterTreeView_AfterSelect);
            this.MasterTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MasterTreeView_NodeMouseClick);
            // 
            // projectNodeContextMenu
            // 
            this.projectNodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveProjectToolStripMenuItem1,
            this.saveProjectAsToolStripMenuItem1,
            this.closeProjectToolStripMenuItem1,
            this.toolStripSeparator4,
            this.newLevelToolStripMenuItem1,
            this.openLevelToolStripMenuItem1});
            this.projectNodeContextMenu.Name = "ProjectNodeContextMenu";
            this.projectNodeContextMenu.Size = new System.Drawing.Size(164, 120);
            // 
            // saveProjectToolStripMenuItem1
            // 
            this.saveProjectToolStripMenuItem1.Name = "saveProjectToolStripMenuItem1";
            this.saveProjectToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.saveProjectToolStripMenuItem1.Text = "Save Project";
            this.saveProjectToolStripMenuItem1.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem1
            // 
            this.saveProjectAsToolStripMenuItem1.Name = "saveProjectAsToolStripMenuItem1";
            this.saveProjectAsToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.saveProjectAsToolStripMenuItem1.Text = "Save Project As...";
            this.saveProjectAsToolStripMenuItem1.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem1
            // 
            this.closeProjectToolStripMenuItem1.Name = "closeProjectToolStripMenuItem1";
            this.closeProjectToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.closeProjectToolStripMenuItem1.Text = "Close Project";
            this.closeProjectToolStripMenuItem1.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(160, 6);
            // 
            // newLevelToolStripMenuItem1
            // 
            this.newLevelToolStripMenuItem1.Name = "newLevelToolStripMenuItem1";
            this.newLevelToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.newLevelToolStripMenuItem1.Text = "New Level";
            this.newLevelToolStripMenuItem1.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem1
            // 
            this.openLevelToolStripMenuItem1.Name = "openLevelToolStripMenuItem1";
            this.openLevelToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.openLevelToolStripMenuItem1.Text = "Open Level...";
            this.openLevelToolStripMenuItem1.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // levelNodeContextMenu
            // 
            this.levelNodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelToolStripMenuItem1,
            this.saveLevelAsToolStripMenuItem1,
            this.closeLevelToolStripMenuItem1,
            this.toolStripSeparator3,
            this.duplicateLevelToolStripMenuItem1,
            this.closeOtherLevelsToolStripMenuItem1,
            this.saveAsImageToolStripMenuItem1});
            this.levelNodeContextMenu.Name = "levelNodeContextMenu";
            this.levelNodeContextMenu.Size = new System.Drawing.Size(172, 142);
            // 
            // saveLevelToolStripMenuItem1
            // 
            this.saveLevelToolStripMenuItem1.Name = "saveLevelToolStripMenuItem1";
            this.saveLevelToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.saveLevelToolStripMenuItem1.Text = "Save Level";
            this.saveLevelToolStripMenuItem1.Click += new System.EventHandler(this.saveLevelToolStripMenuItem1_Click);
            // 
            // saveLevelAsToolStripMenuItem1
            // 
            this.saveLevelAsToolStripMenuItem1.Name = "saveLevelAsToolStripMenuItem1";
            this.saveLevelAsToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.saveLevelAsToolStripMenuItem1.Text = "Save Level As...";
            this.saveLevelAsToolStripMenuItem1.Click += new System.EventHandler(this.saveLevelAsToolStripMenuItem1_Click);
            // 
            // closeLevelToolStripMenuItem1
            // 
            this.closeLevelToolStripMenuItem1.Name = "closeLevelToolStripMenuItem1";
            this.closeLevelToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.closeLevelToolStripMenuItem1.Text = "Close Level";
            this.closeLevelToolStripMenuItem1.Click += new System.EventHandler(this.closeLevelToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // duplicateLevelToolStripMenuItem1
            // 
            this.duplicateLevelToolStripMenuItem1.Name = "duplicateLevelToolStripMenuItem1";
            this.duplicateLevelToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.duplicateLevelToolStripMenuItem1.Text = "Duplicate Level";
            this.duplicateLevelToolStripMenuItem1.Click += new System.EventHandler(this.duplicateLevelToolStripMenuItem1_Click);
            // 
            // closeOtherLevelsToolStripMenuItem1
            // 
            this.closeOtherLevelsToolStripMenuItem1.Name = "closeOtherLevelsToolStripMenuItem1";
            this.closeOtherLevelsToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.closeOtherLevelsToolStripMenuItem1.Text = "Close Other Levels";
            this.closeOtherLevelsToolStripMenuItem1.Click += new System.EventHandler(this.closeOtherLevelsToolStripMenuItem1_Click);
            // 
            // saveAsImageToolStripMenuItem1
            // 
            this.saveAsImageToolStripMenuItem1.Name = "saveAsImageToolStripMenuItem1";
            this.saveAsImageToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.saveAsImageToolStripMenuItem1.Text = "Save As Image...";
            // 
            // projectEditTabControl
            // 
            this.projectEditTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectEditTabControl.Controls.Add(this.tabPage1);
            this.projectEditTabControl.Controls.Add(this.tabPage2);
            this.projectEditTabControl.Controls.Add(this.tabPage3);
            this.projectEditTabControl.Controls.Add(this.tabPage4);
            this.projectEditTabControl.Controls.Add(this.tabPage5);
            this.projectEditTabControl.Location = new System.Drawing.Point(158, 27);
            this.projectEditTabControl.Name = "projectEditTabControl";
            this.projectEditTabControl.SelectedIndex = 0;
            this.projectEditTabControl.Size = new System.Drawing.Size(626, 510);
            this.projectEditTabControl.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(618, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(618, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Layers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainStatusLabel
            // 
            this.MainStatusLabel.Name = "MainStatusLabel";
            this.MainStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // mouseCoordinatesLabel
            // 
            this.mouseCoordinatesLabel.Name = "mouseCoordinatesLabel";
            this.mouseCoordinatesLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mouseCoordinatesLabel.Size = new System.Drawing.Size(33, 17);
            this.mouseCoordinatesLabel.Text = "(0, 0)";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(618, 484);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tilesets";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(618, 484);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Objects";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(618, 484);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Values";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OgmoEditor.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.projectEditTabControl);
            this.Controls.Add(this.MasterTreeView);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.Text = "Ogmo Editor";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.projectNodeContextMenu.ResumeLayout(false);
            this.levelNodeContextMenu.ResumeLayout(false);
            this.projectEditTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView MasterTreeView;
        private System.Windows.Forms.ContextMenuStrip projectNodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip levelNodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveLevelAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem duplicateLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeOtherLevelsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem duplicateLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeOtherLevelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openAllLevelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectViewToolStripMenuItem;
        private System.Windows.Forms.TabControl projectEditTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel MainStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel mouseCoordinatesLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
    }
}