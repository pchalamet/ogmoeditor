using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.ValueDefinitions;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.ValueEditors
{
    public partial class BoolValueEditor : ValueEditor
    {
        public BoolValueDefinition Definition { get; private set; }

        public BoolValueEditor(Value value, int x, int y)
            : base(value, x, y)
        {
            Definition = (BoolValueDefinition)value.Definition;
            InitializeComponent();

            valueCheckBox.Text = Definition.Name;
            valueCheckBox.Location = new Point(64 - valueCheckBox.Size.Width / 2, 5);
            valueCheckBox.Checked = Convert.ToBoolean(Value.Content);
        }

        /*
         *  Events
         */
        private void valueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Value.Content = valueCheckBox.Checked.ToString();
        }
    }
}
