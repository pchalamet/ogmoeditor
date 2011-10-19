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
using OgmoEditor.ProjectEditors;

namespace OgmoEditor.LevelEditors.ValueEditors
{
    public partial class FloatValueEditor : ValueEditor
    {
        public FloatValueDefinition Definition { get; private set; }

        public FloatValueEditor(Value value, int x, int y)
            : base(value, x, y)
        {
            Definition = (FloatValueDefinition)value.Definition;
            InitializeComponent();

            nameLabel.Text = Definition.Name;
            valueTextBox.Text = Value.Content;
        }

        private void handleTextBox()
        {
            OgmoParse.ParseFloatToString(ref Value.Content, Definition.Min, Definition.Max, Definition.Round, valueTextBox);
        }

        /*
         *  Events
         */
        private void valueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                handleTextBox();
                Ogmo.MainWindow.FocusEditor();
            }
        }

        private void valueTextBox_Leave(object sender, EventArgs e)
        {
            handleTextBox();
        }
    }
}
