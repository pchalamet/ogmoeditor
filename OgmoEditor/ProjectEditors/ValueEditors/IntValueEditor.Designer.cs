namespace OgmoEditor.ProjectEditors.ValueEditors
{
    partial class IntValueEditor
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
            this.label6 = new System.Windows.Forms.Label();
            this.uiComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.defaultTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "UI";
            // 
            // uiComboBox
            // 
            this.uiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiComboBox.FormattingEnabled = true;
            this.uiComboBox.Items.AddRange(new object[] {
            "Field",
            "Slider"});
            this.uiComboBox.Location = new System.Drawing.Point(60, 69);
            this.uiComboBox.Name = "uiComboBox";
            this.uiComboBox.Size = new System.Drawing.Size(77, 21);
            this.uiComboBox.TabIndex = 14;
            this.uiComboBox.SelectedIndexChanged += new System.EventHandler(this.uiComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Max";
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(163, 42);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(63, 20);
            this.maxTextBox.TabIndex = 12;
            this.maxTextBox.TextChanged += new System.EventHandler(this.maxTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Min";
            // 
            // minTextBox
            // 
            this.minTextBox.Location = new System.Drawing.Point(60, 42);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(63, 20);
            this.minTextBox.TabIndex = 10;
            this.minTextBox.Validated += new System.EventHandler(this.minTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Default";
            // 
            // defaultTextBox
            // 
            this.defaultTextBox.Location = new System.Drawing.Point(60, 15);
            this.defaultTextBox.Name = "defaultTextBox";
            this.defaultTextBox.Size = new System.Drawing.Size(77, 20);
            this.defaultTextBox.TabIndex = 8;
            this.defaultTextBox.Validated += new System.EventHandler(this.defaultTextBox_Validated);
            // 
            // IntValueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uiComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.minTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.defaultTextBox);
            this.Name = "IntValueEditor";
            this.Size = new System.Drawing.Size(239, 104);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox uiComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox defaultTextBox;
    }
}
