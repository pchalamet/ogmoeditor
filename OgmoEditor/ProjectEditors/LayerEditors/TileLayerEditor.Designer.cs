namespace OgmoEditor.ProjectEditors.LayerEditors
{
    partial class TileLayerEditor
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
            this.multipleTilesetsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // multipleTilesetsCheckBox
            // 
            this.multipleTilesetsCheckBox.AutoSize = true;
            this.multipleTilesetsCheckBox.Location = new System.Drawing.Point(18, 15);
            this.multipleTilesetsCheckBox.Name = "multipleTilesetsCheckBox";
            this.multipleTilesetsCheckBox.Size = new System.Drawing.Size(124, 17);
            this.multipleTilesetsCheckBox.TabIndex = 0;
            this.multipleTilesetsCheckBox.Text = "Allow multiple tilesets";
            this.multipleTilesetsCheckBox.UseVisualStyleBackColor = true;
            this.multipleTilesetsCheckBox.CheckedChanged += new System.EventHandler(this.multipleTilesetsCheckBox_CheckedChanged);
            // 
            // TileLayerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multipleTilesetsCheckBox);
            this.Name = "TileLayerEditor";
            this.Size = new System.Drawing.Size(353, 358);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox multipleTilesetsCheckBox;
    }
}
