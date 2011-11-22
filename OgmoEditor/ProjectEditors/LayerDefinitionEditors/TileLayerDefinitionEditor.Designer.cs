namespace OgmoEditor.ProjectEditors.LayerDefinitionEditors
{
    partial class TileLayerDefinitionEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exportModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trimEmptyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // exportModeComboBox
            // 
            this.exportModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exportModeComboBox.FormattingEnabled = true;
            this.exportModeComboBox.Items.AddRange(new object[] {
            "CSV",
            "XML"});
            this.exportModeComboBox.Location = new System.Drawing.Point(89, 12);
            this.exportModeComboBox.Name = "exportModeComboBox";
            this.exportModeComboBox.Size = new System.Drawing.Size(81, 21);
            this.exportModeComboBox.TabIndex = 0;
            this.exportModeComboBox.SelectionChangeCommitted += new System.EventHandler(this.exportModeComboBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Export Mode";
            // 
            // trimEmptyCheckBox
            // 
            this.trimEmptyCheckBox.AutoSize = true;
            this.trimEmptyCheckBox.Location = new System.Drawing.Point(89, 39);
            this.trimEmptyCheckBox.Name = "trimEmptyCheckBox";
            this.trimEmptyCheckBox.Size = new System.Drawing.Size(78, 17);
            this.trimEmptyCheckBox.TabIndex = 2;
            this.trimEmptyCheckBox.Text = "Trim Empty";
            this.trimEmptyCheckBox.UseVisualStyleBackColor = true;
            this.trimEmptyCheckBox.CheckedChanged += new System.EventHandler(this.trimEmptyCheckBox_CheckedChanged);
            // 
            // TileLayerDefinitionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trimEmptyCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exportModeComboBox);
            this.Name = "TileLayerDefinitionEditor";
            this.Size = new System.Drawing.Size(353, 358);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox exportModeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox trimEmptyCheckBox;

    }
}
