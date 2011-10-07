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

namespace OgmoEditor.ProjectEditors
{
    public partial class ValuesEditor : UserControl
    {
        private const string NEW_VALUE = "NewValue";

        private List<ValueDefinition> values;

        public ValuesEditor()
        {
            InitializeComponent();
            values = new List<ValueDefinition>();
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
                values = value;
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = listBox.SelectedIndex != -1;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            IntValueDefinition v = new IntValueDefinition();
            v.Name = getNewName();
            values.Add(v);
            listBox.Items.Add(v.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            values.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
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

            foreach (ValueDefinition v in values)
            {
                if (values.FindAll(e => e.Name == v.Name).Count > 1)
                    s += ProjParse.Error(container + " contains multiple values with the name \"" + v.Name + "\"");
            }

            if (values.Find(e => e.Name == "") != null)
                s += ProjParse.Error(container + " contains value(s) with blank name");

            return s;
        }
    }
}
