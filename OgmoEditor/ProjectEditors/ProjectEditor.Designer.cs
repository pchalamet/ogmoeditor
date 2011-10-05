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
            this.applyButton = new System.Windows.Forms.Button();
            this.objectsTabPage = new System.Windows.Forms.TabPage();
            this.tilesetsTabPage = new System.Windows.Forms.TabPage();
            this.layersTabPage = new System.Windows.Forms.TabPage();
            this.layerPropertiesPanel = new System.Windows.Forms.Panel();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.gridHeightTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gridWidthTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.layerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.layerListView = new System.Windows.Forms.ListView();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.layersTabPage.SuspendLayout();
            this.layerPropertiesPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(497, 527);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(337, 527);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(154, 23);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.saveButton_Click);
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
            // layerNameTextBox
            // 
            this.layerNameTextBox.Location = new System.Drawing.Point(63, 19);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.Size = new System.Drawing.Size(146, 20);
            this.layerNameTextBox.TabIndex = 0;
            // 
            // gridHeightTextBox
            // 
            this.gridHeightTextBox.Location = new System.Drawing.Point(149, 45);
            this.gridHeightTextBox.Name = "gridHeightTextBox";
            this.gridHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.gridHeightTextBox.TabIndex = 11;
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
            // gridWidthTextBox
            // 
            this.gridWidthTextBox.Location = new System.Drawing.Point(63, 45);
            this.gridWidthTextBox.Name = "gridWidthTextBox";
            this.gridWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.gridWidthTextBox.TabIndex = 9;
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Grid";
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
            // settingsTabPage
            // 
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(573, 490);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
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
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.applyButton);
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
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
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
        private System.Windows.Forms.TabControl tabControl;
    }
}