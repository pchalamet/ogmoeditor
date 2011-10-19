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
using System.Diagnostics;

namespace OgmoEditor.LevelEditors.ValueEditors
{
    public partial class IntValueFieldEditor : ValueEditor
    {
        public IntValueDefinition Definition { get; private set; }

        public IntValueFieldEditor(Value value, int x, int y)
            : base(value, x, y)
        {
            Definition = (IntValueDefinition)value.Definition;
            InitializeComponent();

            nameLabel.Text = value.Definition.Name;
            valueTextBox.Text = value.Content.ToString();
        }

        private void handleInput()
        {
            OgmoParse.ParseIntToString(ref Value.Content, Definition.Min, Definition.Max, valueTextBox);         
        }

        /*
         *  Events
         */
        private void valueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                handleInput();
                Ogmo.MainWindow.FocusEditor();
            }
        }

        private void valueTextBox_Leave(object sender, EventArgs e)
        {
            handleInput();
        }
    }
}
