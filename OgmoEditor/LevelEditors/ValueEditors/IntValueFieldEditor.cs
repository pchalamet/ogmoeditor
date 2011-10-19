using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.ProjectEditors;
using OgmoEditor.Definitions.ValueDefinitions;

namespace OgmoEditor.LevelEditors.ValueEditors
{
    public partial class IntValueFieldEditor : ValueEditor
    {
        public IntValueDefinition Definition { get; private set; }

        public IntValueFieldEditor(Value value)
            : base(value)
        {
            Definition = (IntValueDefinition)value.Definition;
            InitializeComponent();

            nameLabel.Text = value.Definition.Name;
            valueTextBox.Text = value.Content.ToString();
        }

        private void valueTextBox_Validated(object sender, EventArgs e)
        {
            OgmoParse.ParseIntToString(ref Value.Content, Definition.Min, Definition.Max, valueTextBox);
        }
    }
}
