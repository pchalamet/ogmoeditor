namespace OgmoEditor.ProjectEditors
{
    partial class ObjectsEditor
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sizeXTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.limitTextBox = new System.Windows.Forms.TextBox();
            this.resizableXCheckBox = new System.Windows.Forms.CheckBox();
            this.resizableYCheckBox = new System.Windows.Forms.CheckBox();
            this.rotatableCheckBox = new System.Windows.Forms.CheckBox();
            this.rotationIncrementTextBox = new System.Windows.Forms.TextBox();
            this.rotationIncrementLabel = new System.Windows.Forms.Label();
            this.sizeYTextBox = new System.Windows.Forms.TextBox();
            this.originYTextBox = new System.Windows.Forms.TextBox();
            this.originXTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.valuesEditor = new OgmoEditor.ProjectEditors.ValuesEditor();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(4, 4);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(184, 420);
            this.listBox.TabIndex = 48;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Enabled = false;
            this.moveDownButton.Location = new System.Drawing.Point(98, 458);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(89, 23);
            this.moveDownButton.TabIndex = 47;
            this.moveDownButton.Text = "Move Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Enabled = false;
            this.moveUpButton.Location = new System.Drawing.Point(4, 458);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(89, 23);
            this.moveUpButton.TabIndex = 46;
            this.moveUpButton.Text = "Move Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(99, 430);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(89, 23);
            this.removeButton.TabIndex = 45;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(4, 430);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(89, 23);
            this.addButton.TabIndex = 44;
            this.addButton.Text = "Create";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Enabled = false;
            this.nameTextBox.Location = new System.Drawing.Point(262, 11);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(133, 20);
            this.nameTextBox.TabIndex = 49;
            this.nameTextBox.Validated += new System.EventHandler(this.nameTextBox_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Size";
            // 
            // sizeXTextBox
            // 
            this.sizeXTextBox.Enabled = false;
            this.sizeXTextBox.Location = new System.Drawing.Point(262, 37);
            this.sizeXTextBox.Name = "sizeXTextBox";
            this.sizeXTextBox.Size = new System.Drawing.Size(42, 20);
            this.sizeXTextBox.TabIndex = 54;
            this.sizeXTextBox.Validated += new System.EventHandler(this.sizeXTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Limit";
            // 
            // limitTextBox
            // 
            this.limitTextBox.Enabled = false;
            this.limitTextBox.Location = new System.Drawing.Point(480, 11);
            this.limitTextBox.Name = "limitTextBox";
            this.limitTextBox.Size = new System.Drawing.Size(62, 20);
            this.limitTextBox.TabIndex = 58;
            this.limitTextBox.Validated += new System.EventHandler(this.limitTextBox_Validated);
            // 
            // resizableXCheckBox
            // 
            this.resizableXCheckBox.AutoSize = true;
            this.resizableXCheckBox.Enabled = false;
            this.resizableXCheckBox.Location = new System.Drawing.Point(232, 68);
            this.resizableXCheckBox.Name = "resizableXCheckBox";
            this.resizableXCheckBox.Size = new System.Drawing.Size(82, 17);
            this.resizableXCheckBox.TabIndex = 59;
            this.resizableXCheckBox.Text = "Resizable X";
            this.resizableXCheckBox.UseVisualStyleBackColor = true;
            this.resizableXCheckBox.CheckedChanged += new System.EventHandler(this.resizableXCheckBox_CheckedChanged);
            // 
            // resizableYCheckBox
            // 
            this.resizableYCheckBox.AutoSize = true;
            this.resizableYCheckBox.Enabled = false;
            this.resizableYCheckBox.Location = new System.Drawing.Point(348, 68);
            this.resizableYCheckBox.Name = "resizableYCheckBox";
            this.resizableYCheckBox.Size = new System.Drawing.Size(82, 17);
            this.resizableYCheckBox.TabIndex = 60;
            this.resizableYCheckBox.Text = "Resizable Y";
            this.resizableYCheckBox.UseVisualStyleBackColor = true;
            this.resizableYCheckBox.CheckedChanged += new System.EventHandler(this.resizableYCheckBox_CheckedChanged);
            // 
            // rotatableCheckBox
            // 
            this.rotatableCheckBox.AutoSize = true;
            this.rotatableCheckBox.Enabled = false;
            this.rotatableCheckBox.Location = new System.Drawing.Point(232, 91);
            this.rotatableCheckBox.Name = "rotatableCheckBox";
            this.rotatableCheckBox.Size = new System.Drawing.Size(72, 17);
            this.rotatableCheckBox.TabIndex = 62;
            this.rotatableCheckBox.Text = "Rotatable";
            this.rotatableCheckBox.UseVisualStyleBackColor = true;
            this.rotatableCheckBox.CheckedChanged += new System.EventHandler(this.rotatableCheckBox_CheckedChanged);
            // 
            // rotationIncrementTextBox
            // 
            this.rotationIncrementTextBox.Location = new System.Drawing.Point(445, 89);
            this.rotationIncrementTextBox.Name = "rotationIncrementTextBox";
            this.rotationIncrementTextBox.Size = new System.Drawing.Size(62, 20);
            this.rotationIncrementTextBox.TabIndex = 63;
            this.rotationIncrementTextBox.Visible = false;
            this.rotationIncrementTextBox.Validated += new System.EventHandler(this.rotationIncrementTextBox_Validated);
            // 
            // rotationIncrementLabel
            // 
            this.rotationIncrementLabel.AutoSize = true;
            this.rotationIncrementLabel.Location = new System.Drawing.Point(342, 92);
            this.rotationIncrementLabel.Name = "rotationIncrementLabel";
            this.rotationIncrementLabel.Size = new System.Drawing.Size(97, 13);
            this.rotationIncrementLabel.TabIndex = 64;
            this.rotationIncrementLabel.Text = "Rotation Increment";
            this.rotationIncrementLabel.Visible = false;
            // 
            // sizeYTextBox
            // 
            this.sizeYTextBox.Enabled = false;
            this.sizeYTextBox.Location = new System.Drawing.Point(328, 37);
            this.sizeYTextBox.Name = "sizeYTextBox";
            this.sizeYTextBox.Size = new System.Drawing.Size(42, 20);
            this.sizeYTextBox.TabIndex = 65;
            this.sizeYTextBox.Validated += new System.EventHandler(this.sizeXTextBox_Validated);
            // 
            // originYTextBox
            // 
            this.originYTextBox.Enabled = false;
            this.originYTextBox.Location = new System.Drawing.Point(500, 37);
            this.originYTextBox.Name = "originYTextBox";
            this.originYTextBox.Size = new System.Drawing.Size(42, 20);
            this.originYTextBox.TabIndex = 69;
            this.originYTextBox.Validated += new System.EventHandler(this.originXTextBox_Validated);
            // 
            // originXTextBox
            // 
            this.originXTextBox.Enabled = false;
            this.originXTextBox.Location = new System.Drawing.Point(434, 37);
            this.originXTextBox.Name = "originXTextBox";
            this.originXTextBox.Size = new System.Drawing.Size(42, 20);
            this.originXTextBox.TabIndex = 67;
            this.originXTextBox.Validated += new System.EventHandler(this.originXTextBox_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(482, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 68;
            this.label6.Text = "x";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(394, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "Origin";
            // 
            // valuesEditor
            // 
            this.valuesEditor.Enabled = false;
            this.valuesEditor.Location = new System.Drawing.Point(210, 288);
            this.valuesEditor.Name = "valuesEditor";
            this.valuesEditor.Size = new System.Drawing.Size(343, 193);
            this.valuesEditor.TabIndex = 70;
            this.valuesEditor.Title = "Values";
            // 
            // ObjectsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valuesEditor);
            this.Controls.Add(this.originYTextBox);
            this.Controls.Add(this.originXTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.sizeYTextBox);
            this.Controls.Add(this.rotationIncrementLabel);
            this.Controls.Add(this.rotationIncrementTextBox);
            this.Controls.Add(this.rotatableCheckBox);
            this.Controls.Add(this.resizableYCheckBox);
            this.Controls.Add(this.resizableXCheckBox);
            this.Controls.Add(this.limitTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sizeXTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Name = "ObjectsEditor";
            this.Size = new System.Drawing.Size(573, 490);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sizeXTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox limitTextBox;
        private System.Windows.Forms.CheckBox resizableXCheckBox;
        private System.Windows.Forms.CheckBox resizableYCheckBox;
        private System.Windows.Forms.CheckBox rotatableCheckBox;
        private System.Windows.Forms.TextBox rotationIncrementTextBox;
        private System.Windows.Forms.Label rotationIncrementLabel;
        private System.Windows.Forms.TextBox sizeYTextBox;
        private System.Windows.Forms.TextBox originYTextBox;
        private System.Windows.Forms.TextBox originXTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ValuesEditor valuesEditor;
    }
}
