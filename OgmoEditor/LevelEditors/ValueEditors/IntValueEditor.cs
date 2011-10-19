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
    public partial class IntValueEditor : ValueEditor
    {
        public IntValueDefinition Definition { get; private set; }

        public IntValueEditor(Value value, int x, int y)
            : base(value, x, y)
        {
            Definition = (IntValueDefinition)value.Definition;
            InitializeComponent();

            nameLabel.Text = Definition.Name;
            valueTextBox.Text = Value.Content;

            //Deal with the slider
            if (Definition.ShowSlider)
            {
                valueTrackBar.Value = Convert.ToInt32(Value.Content);
                valueTrackBar.Minimum = Definition.Min;
                valueTrackBar.Maximum = Definition.Max;
                valueTrackBar.TickFrequency = (Definition.Max - Definition.Min) / 10;
            }
            else
            {
                Controls.Remove(valueTrackBar);
                Size = new Size(128, 24);
            }
        }

        private void handleTextBox()
        {
            OgmoParse.ParseIntToString(ref Value.Content, Definition.Min, Definition.Max, valueTextBox);
            valueTrackBar.Value = Convert.ToInt32(Value.Content);
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

        private void valueTrackBar_Scroll(object sender, EventArgs e)
        {
            Value.Content = valueTextBox.Text = valueTrackBar.Value.ToString();
        }
    }
}
