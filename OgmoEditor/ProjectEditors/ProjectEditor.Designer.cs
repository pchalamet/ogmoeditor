namespace OgmoEditor.ProjectEditors
{
    partial class ProjectEditor
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.objectsTabPage = new System.Windows.Forms.TabPage();
            this.tilesetsTabPage = new System.Windows.Forms.TabPage();
            this.layersTabPage = new System.Windows.Forms.TabPage();
            this.layerListView = new System.Windows.Forms.ListView();
            this.layerPropertiesPanel = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.layerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gridWidthTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gridHeightTextBox = new System.Windows.Forms.TextBox();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.defaultWidthTextBox = new System.Windows.Forms.TextBox();
            this.defaultHeightTextBox = new System.Windows.Forms.TextBox();
            this.minWidthTextBox = new System.Windows.Forms.TextBox();
            this.minHeightTextBox = new System.Windows.Forms.TextBox();
            this.maxWidthTextBox = new System.Windows.Forms.TextBox();
            this.maxHeightTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.workingDirectoryChooser = new System.Windows.Forms.Button();
            this.workingDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lockToDefaultButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.layersTabPage.SuspendLayout();
            this.layerPropertiesPanel.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(497, 527);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(337, 527);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(154, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // objectsTabPage
            // 
            this.objectsTabPage.Location = new System.Drawing.Point(4, 22);
            this.objectsTabPage.Name = "objectsTabPage";
            this.objectsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.objectsTabPage.Size = new System.Drawing.Size(573, 490);
            this.objectsTabPage.TabIndex = 3;
            this.objectsTabPage.Text = "Objects";
            this.objectsTabPage.UseVisualStyleBackColor = true;
            // 
            // tilesetsTabPage
            // 
            this.tilesetsTabPage.Location = new System.Drawing.Point(4, 22);
            this.tilesetsTabPage.Name = "tilesetsTabPage";
            this.tilesetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tilesetsTabPage.Size = new System.Drawing.Size(573, 490);
            this.tilesetsTabPage.TabIndex = 2;
            this.tilesetsTabPage.Text = "Tilesets";
            this.tilesetsTabPage.UseVisualStyleBackColor = true;
            // 
            // layersTabPage
            // 
            this.layersTabPage.Controls.Add(this.layerPropertiesPanel);
            this.layersTabPage.Controls.Add(this.layerListView);
            this.layersTabPage.Location = new System.Drawing.Point(4, 22);
            this.layersTabPage.Name = "layersTabPage";
            this.layersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.layersTabPage.Size = new System.Drawing.Size(573, 490);
            this.layersTabPage.TabIndex = 1;
            this.layersTabPage.Text = "Layers";
            this.layersTabPage.UseVisualStyleBackColor = true;
            // 
            // layerListView
            // 
            this.layerListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.layerListView.Location = new System.Drawing.Point(23, 23);
            this.layerListView.Margin = new System.Windows.Forms.Padding(20);
            this.layerListView.Name = "layerListView";
            this.layerListView.Size = new System.Drawing.Size(198, 444);
            this.layerListView.TabIndex = 0;
            this.layerListView.UseCompatibleStateImageBehavior = false;
            this.layerListView.View = System.Windows.Forms.View.Details;
            // 
            // layerPropertiesPanel
            // 
            this.layerPropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layerPropertiesPanel.Controls.Add(this.layerNameTextBox);
            this.layerPropertiesPanel.Controls.Add(this.gridHeightTextBox);
            this.layerPropertiesPanel.Controls.Add(this.label11);
            this.layerPropertiesPanel.Controls.Add(this.gridWidthTextBox);
            this.layerPropertiesPanel.Controls.Add(this.label13);
            this.layerPropertiesPanel.Controls.Add(this.layerTypeComboBox);
            this.layerPropertiesPanel.Controls.Add(this.label12);
            this.layerPropertiesPanel.Controls.Add(this.label10);
            this.layerPropertiesPanel.Location = new System.Drawing.Point(245, 23);
            this.layerPropertiesPanel.Margin = new System.Windows.Forms.Padding(0, 20, 20, 20);
            this.layerPropertiesPanel.Name = "layerPropertiesPanel";
            this.layerPropertiesPanel.Size = new System.Drawing.Size(323, 430);
            this.layerPropertiesPanel.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Grid";
            // 
            // layerTypeComboBox
            // 
            this.layerTypeComboBox.FormattingEnabled = true;
            this.layerTypeComboBox.Items.AddRange(new object[] {
            "Grid",
            "Tiles",
            "Objects",
            "Shapes"});
            this.layerTypeComboBox.Location = new System.Drawing.Point(63, 71);
            this.layerTypeComboBox.Name = "layerTypeComboBox";
            this.layerTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.layerTypeComboBox.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Type";
            // 
            // gridWidthTextBox
            // 
            this.gridWidthTextBox.Location = new System.Drawing.Point(63, 45);
            this.gridWidthTextBox.Name = "gridWidthTextBox";
            this.gridWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.gridWidthTextBox.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(131, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "x";
            // 
            // gridHeightTextBox
            // 
            this.gridHeightTextBox.Location = new System.Drawing.Point(149, 45);
            this.gridHeightTextBox.Name = "gridHeightTextBox";
            this.gridHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.gridHeightTextBox.TabIndex = 11;
            // 
            // layerNameTextBox
            // 
            this.layerNameTextBox.Location = new System.Drawing.Point(63, 19);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.Size = new System.Drawing.Size(146, 20);
            this.layerNameTextBox.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.lockToDefaultButton);
            this.settingsTabPage.Controls.Add(this.label9);
            this.settingsTabPage.Controls.Add(this.workingDirectoryTextBox);
            this.settingsTabPage.Controls.Add(this.maxHeightTextBox);
            this.settingsTabPage.Controls.Add(this.maxWidthTextBox);
            this.settingsTabPage.Controls.Add(this.minHeightTextBox);
            this.settingsTabPage.Controls.Add(this.minWidthTextBox);
            this.settingsTabPage.Controls.Add(this.defaultHeightTextBox);
            this.settingsTabPage.Controls.Add(this.defaultWidthTextBox);
            this.settingsTabPage.Controls.Add(this.projectNameTextBox);
            this.settingsTabPage.Controls.Add(this.workingDirectoryChooser);
            this.settingsTabPage.Controls.Add(this.label8);
            this.settingsTabPage.Controls.Add(this.label7);
            this.settingsTabPage.Controls.Add(this.label6);
            this.settingsTabPage.Controls.Add(this.label5);
            this.settingsTabPage.Controls.Add(this.label4);
            this.settingsTabPage.Controls.Add(this.label3);
            this.settingsTabPage.Controls.Add(this.label2);
            this.settingsTabPage.Controls.Add(this.label1);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(573, 490);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(144, 42);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(197, 20);
            this.projectNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project Name";
            // 
            // defaultWidthTextBox
            // 
            this.defaultWidthTextBox.Location = new System.Drawing.Point(134, 145);
            this.defaultWidthTextBox.Name = "defaultWidthTextBox";
            this.defaultWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.defaultWidthTextBox.TabIndex = 2;
            // 
            // defaultHeightTextBox
            // 
            this.defaultHeightTextBox.Location = new System.Drawing.Point(220, 145);
            this.defaultHeightTextBox.Name = "defaultHeightTextBox";
            this.defaultHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.defaultHeightTextBox.TabIndex = 3;
            // 
            // minWidthTextBox
            // 
            this.minWidthTextBox.Location = new System.Drawing.Point(134, 171);
            this.minWidthTextBox.Name = "minWidthTextBox";
            this.minWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.minWidthTextBox.TabIndex = 4;
            // 
            // minHeightTextBox
            // 
            this.minHeightTextBox.Location = new System.Drawing.Point(220, 171);
            this.minHeightTextBox.Name = "minHeightTextBox";
            this.minHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.minHeightTextBox.TabIndex = 5;
            // 
            // maxWidthTextBox
            // 
            this.maxWidthTextBox.Location = new System.Drawing.Point(134, 197);
            this.maxWidthTextBox.Name = "maxWidthTextBox";
            this.maxWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.maxWidthTextBox.TabIndex = 6;
            // 
            // maxHeightTextBox
            // 
            this.maxHeightTextBox.Location = new System.Drawing.Point(220, 197);
            this.maxHeightTextBox.Name = "maxHeightTextBox";
            this.maxHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.maxHeightTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "x";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(72, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Level Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Default";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(80, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Minimum";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Maximum";
            // 
            // workingDirectoryChooser
            // 
            this.workingDirectoryChooser.Location = new System.Drawing.Point(315, 72);
            this.workingDirectoryChooser.Name = "workingDirectoryChooser";
            this.workingDirectoryChooser.Size = new System.Drawing.Size(26, 20);
            this.workingDirectoryChooser.TabIndex = 15;
            this.workingDirectoryChooser.Text = "...";
            this.workingDirectoryChooser.UseVisualStyleBackColor = true;
            // 
            // workingDirectoryTextBox
            // 
            this.workingDirectoryTextBox.Location = new System.Drawing.Point(144, 72);
            this.workingDirectoryTextBox.Name = "workingDirectoryTextBox";
            this.workingDirectoryTextBox.ReadOnly = true;
            this.workingDirectoryTextBox.Size = new System.Drawing.Size(165, 20);
            this.workingDirectoryTextBox.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(46, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Working Directory";
            // 
            // lockToDefaultButton
            // 
            this.lockToDefaultButton.Location = new System.Drawing.Point(205, 223);
            this.lockToDefaultButton.Name = "lockToDefaultButton";
            this.lockToDefaultButton.Size = new System.Drawing.Size(107, 23);
            this.lockToDefaultButton.TabIndex = 18;
            this.lockToDefaultButton.Text = "Lock to Default";
            this.lockToDefaultButton.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.layersTabPage);
            this.tabControl.Controls.Add(this.tilesetsTabPage);
            this.tabControl.Controls.Add(this.objectsTabPage);
            this.tabControl.Location = new System.Drawing.Point(2, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(581, 516);
            this.tabControl.TabIndex = 1;
            // 
            // ProjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "ProjectEditor";
            this.Text = "ProjectEditor";
            this.layersTabPage.ResumeLayout(false);
            this.layerPropertiesPanel.ResumeLayout(false);
            this.layerPropertiesPanel.PerformLayout();
            this.settingsTabPage.ResumeLayout(false);
            this.settingsTabPage.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TabPage objectsTabPage;
        private System.Windows.Forms.TabPage tilesetsTabPage;
        private System.Windows.Forms.TabPage layersTabPage;
        private System.Windows.Forms.Panel layerPropertiesPanel;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private System.Windows.Forms.TextBox gridHeightTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox gridWidthTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox layerTypeComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView layerListView;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.Button lockToDefaultButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox workingDirectoryTextBox;
        private System.Windows.Forms.TextBox maxHeightTextBox;
        private System.Windows.Forms.TextBox maxWidthTextBox;
        private System.Windows.Forms.TextBox minHeightTextBox;
        private System.Windows.Forms.TextBox minWidthTextBox;
        private System.Windows.Forms.TextBox defaultHeightTextBox;
        private System.Windows.Forms.TextBox defaultWidthTextBox;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.Button workingDirectoryChooser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl;
    }
}