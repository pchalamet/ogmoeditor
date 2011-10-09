using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.ValueDefinitions;
using System.Diagnostics;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.ProjectEditors
{
    public partial class ValuesEditor : UserControl
    {
        private const string NEW_VALUE = "NewValue";

        private List<ValueDefinition> values;
        private UserControl valueEditor;

        public ValuesEditor()
        {
            InitializeComponent();
            values = new List<ValueDefinition>();
            valueEditor = null;
        }

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        public List<ValueDefinition> Values
        {
            get { return values; }
            set
            {
                values = new List<ValueDefinition>(value);
                foreach (ValueDefinition v in values)
                    listBox.Items.Add(v.Name);
            }
        }

        private void setControlsFromValue(ValueDefinition v)
        {
            removeButton.Enabled = true;

            //Set the name
            nameTextBox.CausesValidation = true;
            nameTextBox.Enabled = true; 
            nameTextBox.Text = v.Name;

            //Set the type
            typeComboBox.CausesValidation = true;
            typeComboBox.Enabled = true;

            //Set the value editor UI
            if (valueEditor != null)
            {
                Controls.Remove(valueEditor);
                valueEditor = null;
            }

            if (v is IntValueDefinition)
            {
                typeComboBox.SelectedIndex = 0;
                valueEditor = new IntValueEditor(v as IntValueDefinition);
                Controls.Add(valueEditor);
            }
            else if (v is BoolValueDefinition)
            {
                typeComboBox.SelectedIndex = 1;
                valueEditor = null;
            }
            else if (v is FloatValueDefinition)
            {
                typeComboBox.SelectedIndex = 2;
                valueEditor = null;
            }
            else if (v is StringValueDefinition)
            {
                typeComboBox.SelectedIndex = 3;
                valueEditor = null;
            }
            else if (v is EnumValueDefinition)
            {
                typeComboBox.SelectedIndex = 4;
                valueEditor = null;
            }
            else if (v is ColorValueDefinition)
            {
                typeComboBox.SelectedIndex = 5;
                valueEditor = null;
            }
        }

        private void disableControls()
        {
            removeButton.Enabled = false;

            nameTextBox.CausesValidation = false;
            nameTextBox.Enabled = false;        
            nameTextBox.Text = "";

            typeComboBox.CausesValidation = false;
            typeComboBox.Enabled = false;

            if (valueEditor != null)
            {
                Controls.Remove(valueEditor);
                valueEditor = null;
            }
        }

        private string getNewName()
        {
            int i = 0;
            string name;

            do
            {
                name = NEW_VALUE + i.ToString();
                i++;
            }
            while (values.Find(e => e.Name == name) != null);

            return name;
        }

        public string ErrorCheck(string container)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (ValueDefinition v in values)
            {
                if (v.Name != "" && !found.Contains(v.Name) && values.FindAll(e => e.Name == v.Name).Count > 1)
                {                  
                    s += ProjParse.Error(container + " contains multiple values with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (values.Find(e => e.Name == "") != null)
                s += ProjParse.Error(container + " contains value(s) with blank name");

            return s;
        }

        /*
         *  Events
         */
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
                setControlsFromValue(values[listBox.SelectedIndex]);
            else
                disableControls();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            IntValueDefinition v = new IntValueDefinition();
            v.Name = getNewName();
            values.Add(v);
            listBox.SelectedIndex = listBox.Items.Add(v.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            values.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            values[listBox.SelectedIndex].Name = nameTextBox.Text;
            listBox.Items[listBox.SelectedIndex] = nameTextBox.Text;
        }

        private void typeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ValueDefinition oldDef = values[listBox.SelectedIndex];
            ValueDefinition newDef = null;

            if (typeComboBox.SelectedIndex == 0)
                newDef = new IntValueDefinition();
            else if (typeComboBox.SelectedIndex == 1)
                newDef = new BoolValueDefinition();
            else if (typeComboBox.SelectedIndex == 2)
                newDef = new FloatValueDefinition();
            else if (typeComboBox.SelectedIndex == 3)
                newDef = new StringValueDefinition();
            else if (typeComboBox.SelectedIndex == 4)
                newDef = new EnumValueDefinition();
            else if (typeComboBox.SelectedIndex == 5)
                newDef = new ColorValueDefinition();

            newDef.Name = oldDef.Name;
            values[listBox.SelectedIndex] = newDef;
            setControlsFromValue(newDef);
        }
    }
}
