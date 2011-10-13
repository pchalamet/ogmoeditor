using OgmoEditor.ProjectEditors.ValueDefinitionEditors;
namespace OgmoEditor.ProjectEditors
{
    partial class SettingsEditor
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
            this.maxHeightTextBox = new System.Windows.Forms.TextBox();
            this.maxWidthTextBox = new System.Windows.Forms.TextBox();
            this.minHeightTextBox = new System.Windows.Forms.TextBox();
            this.minWidthTextBox = new System.Windows.Forms.TextBox();
            this.defaultHeightTextBox = new System.Windows.Forms.TextBox();
            this.defaultWidthTextBox = new System.Windows.Forms.TextBox();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.backgroundColorChooser = new OgmoEditor.ColorChooser();
            this.valuesEditor = new OgmoEditor.ProjectEditors.ValueDefinitionEditors.ValueDefinitionsEditor();
            this.colorChooser1 = new OgmoEditor.ColorChooser();
            this.SuspendLayout();
            // 
            // maxHeightTextBox
            // 
            this.maxHeightTextBox.Location = new System.Drawing.Point(207, 193);
            this.maxHeightTextBox.Name = "maxHeightTextBox";
            this.maxHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.maxHeightTextBox.TabIndex = 26;
            this.maxHeightTextBox.Validated += new System.EventHandler(this.maxWidthTextBox_TextChanged);
            // 
            // maxWidthTextBox
            // 
            this.maxWidthTextBox.Location = new System.Drawing.Point(121, 193);
            this.maxWidthTextBox.Name = "maxWidthTextBox";
            this.maxWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.maxWidthTextBox.TabIndex = 25;
            this.maxWidthTextBox.TextChanged += new System.EventHandler(this.maxWidthTextBox_TextChanged);
            // 
            // minHeightTextBox
            // 
            this.minHeightTextBox.Location = new System.Drawing.Point(207, 167);
            this.minHeightTextBox.Name = "minHeightTextBox";
            this.minHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.minHeightTextBox.TabIndex = 24;
            this.minHeightTextBox.Validated += new System.EventHandler(this.minWidthTextBox_Validated);
            // 
            // minWidthTextBox
            // 
            this.minWidthTextBox.Location = new System.Drawing.Point(121, 167);
            this.minWidthTextBox.Name = "minWidthTextBox";
            this.minWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.minWidthTextBox.TabIndex = 23;
            this.minWidthTextBox.Validated += new System.EventHandler(this.minWidthTextBox_Validated);
            // 
            // defaultHeightTextBox
            // 
            this.defaultHeightTextBox.Location = new System.Drawing.Point(207, 141);
            this.defaultHeightTextBox.Name = "defaultHeightTextBox";
            this.defaultHeightTextBox.Size = new System.Drawing.Size(62, 20);
            this.defaultHeightTextBox.TabIndex = 22;
            this.defaultHeightTextBox.Validated += new System.EventHandler(this.defaultWidthTextBox_Validated);
            // 
            // defaultWidthTextBox
            // 
            this.defaultWidthTextBox.Location = new System.Drawing.Point(121, 141);
            this.defaultWidthTextBox.Name = "defaultWidthTextBox";
            this.defaultWidthTextBox.Size = new System.Drawing.Size(62, 20);
            this.defaultWidthTextBox.TabIndex = 21;
            this.defaultWidthTextBox.Validated += new System.EventHandler(this.defaultWidthTextBox_Validated);
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(131, 31);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(197, 20);
            this.projectNameTextBox.TabIndex = 19;
            this.projectNameTextBox.Validated += new System.EventHandler(this.projectNameTextBox_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Maximum";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Minimum";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Default";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Level Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Project Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Background Color";
            // 
            // backgroundColorChooser
            // 
            this.backgroundColorChooser.Location = new System.Drawing.Point(127, 57);
            this.backgroundColorChooser.Name = "backgroundColorChooser";
            this.backgroundColorChooser.Size = new System.Drawing.Size(108, 28);
            this.backgroundColorChooser.TabIndex = 36;
            this.backgroundColorChooser.ColorChanged += new OgmoEditor.ColorChooser.ColorCallback(this.backgroundColorChooser_ColorChanged);
            // 
            // valuesEditor
            // 
            this.valuesEditor.Location = new System.Drawing.Point(57, 250);
            this.valuesEditor.Name = "valuesEditor";
            this.valuesEditor.Size = new System.Drawing.Size(341, 191);
            this.valuesEditor.TabIndex = 34;
            this.valuesEditor.Title = "Level Values";
            // 
            // colorChooser1
            // 
            this.colorChooser1.Location = new System.Drawing.Point(127, 57);
            this.colorChooser1.Name = "colorChooser1";
            this.colorChooser1.Size = new System.Drawing.Size(108, 28);
            this.colorChooser1.TabIndex = 36;
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backgroundColorChooser);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.valuesEditor);
            this.Controls.Add(this.maxHeightTextBox);
            this.Controls.Add(this.maxWidthTextBox);
            this.Controls.Add(this.minHeightTextBox);
            this.Controls.Add(this.minWidthTextBox);
            this.Controls.Add(this.defaultHeightTextBox);
            this.Controls.Add(this.defaultWidthTextBox);
            this.Controls.Add(this.projectNameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SettingsEditor";
            this.Size = new System.Drawing.Size(573, 490);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox maxHeightTextBox;
        private System.Windows.Forms.TextBox maxWidthTextBox;
        private System.Windows.Forms.TextBox minHeightTextBox;
        private System.Windows.Forms.TextBox minWidthTextBox;
        private System.Windows.Forms.TextBox defaultHeightTextBox;
        private System.Windows.Forms.TextBox defaultWidthTextBox;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ValueDefinitionsEditor valuesEditor;
        private System.Windows.Forms.Label label9;
        private ColorChooser backgroundColorChooser;
        private ColorChooser colorChooser1;
    }
}
