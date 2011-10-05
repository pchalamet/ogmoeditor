namespace OgmoEditor
{
    partial class ProjectView
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
            this.masterTreeView = new System.Windows.Forms.TreeView();
            this.projectNodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editProjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.projectNodeContextMenu.SuspendLayout();
            this.levelNodeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // masterTreeView
            // 
            this.masterTreeView.AccessibleName = "";
            this.masterTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.masterTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.masterTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.masterTreeView.HideSelection = false;
            this.masterTreeView.Location = new System.Drawing.Point(12, 12);
            this.masterTreeView.Name = "masterTreeView";
            this.masterTreeView.ShowPlusMinus = false;
            this.masterTreeView.ShowRootLines = false;
            this.masterTreeView.Size = new System.Drawing.Size(120, 100);
            this.masterTreeView.TabIndex = 4;
            this.masterTreeView.TabStop = false;
            this.masterTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MasterTreeView_NodeMouseClick);
            this.masterTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MasterTreeView_NodeMouseDoubleClick);
            // 
            // projectNodeContextMenu
            // 
            this.projectNodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editProjectToolStripMenuItem1,
            this.saveProjectToolStripMenuItem1,
            this.saveProjectAsToolStripMenuItem1,
            this.closeProjectToolStripMenuItem1,
            this.toolStripSeparator4,
            this.newLevelToolStripMenuItem1,
            this.openLevelToolStripMenuItem1});
            this.projectNodeContextMenu.Name = "ProjectNodeContextMenu";
            this.projectNodeContextMenu.Size = new System.Drawing.Size(164, 142);
            // 
            // editProjectToolStripMenuItem1
            // 
            this.editProjectToolStripMenuItem1.Name = "editProjectToolStripMenuItem1";
            this.editProjectToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.editProjectToolStripMenuItem1.Text = "Edit Project";
            this.editProjectToolStripMenuItem1.Click += new System.EventHandler(this.editProjectToolStripMenuItem1_Click);
            // 
            // saveProjectToolStripMenuItem1
            // 
            this.saveProjectToolStripMenuItem1.Name = "saveProjectToolStripMenuItem1";
            this.saveProjectToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.saveProjectToolStripMenuItem1.Text = "Save Project";
            this.saveProjectToolStripMenuItem1.Click += new System.EventHandler(this.saveProjectToolStripMenuItem1_Click);
            // 
            // saveProjectAsToolStripMenuItem1
            // 
            this.saveProjectAsToolStripMenuItem1.Name = "saveProjectAsToolStripMenuItem1";
            this.saveProjectAsToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.saveProjectAsToolStripMenuItem1.Text = "Save Project As...";
            this.saveProjectAsToolStripMenuItem1.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem1_Click);
            // 
            // closeProjectToolStripMenuItem1
            // 
            this.closeProjectToolStripMenuItem1.Name = "closeProjectToolStripMenuItem1";
            this.closeProjectToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.closeProjectToolStripMenuItem1.Text = "Close Project";
            this.closeProjectToolStripMenuItem1.Click += new System.EventHandler(this.closeProjectToolStripMenuItem1_Click);
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
            this.newLevelToolStripMenuItem1.Click += new System.EventHandler(this.newLevelToolStripMenuItem1_Click);
            // 
            // openLevelToolStripMenuItem1
            // 
            this.openLevelToolStripMenuItem1.Name = "openLevelToolStripMenuItem1";
            this.openLevelToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.openLevelToolStripMenuItem1.Text = "Open Level...";
            this.openLevelToolStripMenuItem1.Click += new System.EventHandler(this.openLevelToolStripMenuItem1_Click);
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
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 124);
            this.Controls.Add(this.masterTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 600);
            this.MinimumSize = new System.Drawing.Size(140, 120);
            this.Name = "ProjectView";
            this.Text = "Project View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectView_FormClosing);
            this.projectNodeContextMenu.ResumeLayout(false);
            this.levelNodeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView masterTreeView;
        private System.Windows.Forms.ContextMenuStrip projectNodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editProjectToolStripMenuItem1;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem duplicateLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeOtherLevelsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem1;
    }
}