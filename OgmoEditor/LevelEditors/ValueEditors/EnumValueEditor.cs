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
    public partial class EnumValueEditor : ValueEditor
    {
        public EnumValueDefinition Definition { get; private set; }

        public EnumValueEditor(Value value, int x, int y)
            : base(value, x, y)
        {
            Definition = (EnumValueDefinition)value.Definition;
            InitializeComponent();

            nameLabel.Text = Definition.Name;
            
            //Init the combo box
            for (int i = 0; i < Definition.Elements.Length; i++)
            {
                valueComboBox.Items.Add(Definition.Elements[i]);
                if (Value.Content == Definition.Elements[i])
                    valueComboBox.SelectedIndex = i;
            }
        }

        /*
         *  Events
         */
        private void valueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Value.Content = Definition.Elements[valueComboBox.SelectedIndex];
        }
    }
}
