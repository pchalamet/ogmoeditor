namespace OgmoEditor.ProjectEditors.LayerDefinitionEditors
{
    partial class GridLayerDefinitionEditor
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
            this.colorChooser = new OgmoEditor.ColorChooser();
            this.label1 = new System.Windows.Forms.Label();
            this.exportAsRectanglesCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // colorChooser
            // 
            this.colorChooser.Location = new System.Drawing.Point(53, 15);
            this.colorChooser.Name = "colorChooser";
            this.colorChooser.Size = new System.Drawing.Size(108, 28);
            this.colorChooser.TabIndex = 0;
            this.colorChooser.ColorChanged += new OgmoEditor.ColorChooser.ColorCallback(this.colorChooser_ColorChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color";
            // 
            // exportAsRectanglesCheckBox
            // 
            this.exportAsRectanglesCheckBox.AutoSize = true;
            this.exportAsRectanglesCheckBox.Location = new System.Drawing.Point(22, 49);
            this.exportAsRectanglesCheckBox.Name = "exportAsRectanglesCheckBox";
            this.exportAsRectanglesCheckBox.Size = new System.Drawing.Size(128, 17);
            this.exportAsRectanglesCheckBox.TabIndex = 2;
            this.exportAsRectanglesCheckBox.Text = "Export As Rectangles";
            this.exportAsRectanglesCheckBox.UseVisualStyleBackColor = true;
            this.exportAsRectanglesCheckBox.CheckedChanged += new System.EventHandler(this.exportAsRectanglesCheckBox_CheckedChanged);
            // 
            // GridLayerDefinitionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exportAsRectanglesCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorChooser);
            this.Name = "GridLayerDefinitionEditor";
            this.Size = new System.Drawing.Size(353, 358);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorChooser colorChooser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox exportAsRectanglesCheckBox;
    }
}
