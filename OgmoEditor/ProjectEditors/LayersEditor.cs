using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;

namespace OgmoEditor.ProjectEditors
{
    public partial class LayersEditor : UserControl, IProjectChanger
    {
        private List<LayerDefinition> layerDefinitions;

        public LayersEditor()
        {
            InitializeComponent();
        }

        public string ErrorCheck()
        {
            string s = "";

            if (layerDefinitions.Count == 0)
                s += ProjParse.Error("No layers are defined");

            return s;
        }

        public void LoadFromProject(Project project)
        {
            layerDefinitions = new List<LayerDefinition>(project.LayerDefinitions);
            foreach (LayerDefinition d in layerDefinitions)
                listBox.Items.Add(d.Name);
        }

        public void ApplyToProject(Project project)
        {
            project.LayerDefinitions = layerDefinitions;
        }

        private void setControlsFromDefinition(LayerDefinition definition)
        {
            nameTextBox.Text = definition.Name;
            gridXTextBox.Text = definition.Grid.Width.ToString();
            gridYTextBox.Text = definition.Grid.Height.ToString();

            if (definition is GridLayerDefinition)
            {
                typeComboBox.SelectedIndex = 0;
            }
            else if (definition is TileLayerDefinition)
            {
                typeComboBox.SelectedIndex = 1;
            }
            else if (definition is ObjectLayerDefinition)
            {
                typeComboBox.SelectedIndex = 2;
            }
            else if (definition is ShapeLayerDefinition)
            {
                typeComboBox.SelectedIndex = 3;
            }
        }

        private LayerDefinition getDefaultLayer()
        {
            int i = 0;
            string name;

            do
            {
                name = Ogmo.NEW_LAYER_NAME + i.ToString();
                i++;
            }
            while (layerNameTaken(name));

            GridLayerDefinition grid = new GridLayerDefinition();
            grid.Name = name;
            grid.Grid = new Size(16, 16);
            return grid;
        }

        private bool layerNameTaken(string name)
        {
            return layerDefinitions.Find(e => e.Name == name) != null;
        }

        /*
         *  Events
         */
        private void addButton_Click(object sender, EventArgs e)
        {
            LayerDefinition def = getDefaultLayer();

            layerDefinitions.Add(def);
            listBox.SelectedIndex = listBox.Items.Add(def.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            layerDefinitions.RemoveAt(index);
            listBox.Items.RemoveAt(index);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            LayerDefinition temp = layerDefinitions[index];
            layerDefinitions[index] = layerDefinitions[index - 1];
            layerDefinitions[index - 1] = temp;

            listBox.Items[index] = layerDefinitions[index].Name;
            listBox.Items[index - 1] = layerDefinitions[index - 1].Name;
            listBox.SelectedIndex = index - 1;
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            LayerDefinition temp = layerDefinitions[index];
            layerDefinitions[index] = layerDefinitions[index + 1];
            layerDefinitions[index + 1] = temp;

            listBox.Items[index] = layerDefinitions[index].Name;
            listBox.Items[index - 1] = layerDefinitions[index + 1].Name;
            listBox.SelectedIndex = index + 1;
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            layerDefinitions[listBox.SelectedIndex].Name = nameTextBox.Text;
            listBox.Items[listBox.SelectedIndex] = (nameTextBox.Text == "" ? "(blank)" : nameTextBox.Text);
        }

        private void gridXTextBox_Validated(object sender, EventArgs e)
        {
            //ProjParse.GetSize(ref layerDefinitions[listBox.SelectedIndex].Grid, gridXTextBox, gridYTextBox);
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayerDefinition oldDef = layerDefinitions[listBox.SelectedIndex];
            LayerDefinition newDef;

            if (typeComboBox.SelectedIndex == 0)
            {
                newDef = new GridLayerDefinition();
                
            }
            else if (typeComboBox.SelectedIndex == 1)
            {
                newDef = new TileLayerDefinition();
            }
            else if (typeComboBox.SelectedIndex == 2)
            {
                newDef = new ObjectLayerDefinition();
            }
            else
            {
                newDef = new ShapeLayerDefinition();
            }

            newDef.Name = oldDef.Name;
            newDef.Grid = oldDef.Grid;
            layerDefinitions[listBox.SelectedIndex] = newDef;
            setControlsFromDefinition(newDef);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                removeButton.Enabled = true;
                moveUpButton.Enabled = listBox.SelectedIndex > 0;
                moveDownButton.Enabled = listBox.SelectedIndex < listBox.Items.Count - 1;
                nameTextBox.Enabled = true;
                gridXTextBox.Enabled = true;
                gridYTextBox.Enabled = true;
                typeComboBox.Enabled = true;
                setControlsFromDefinition(layerDefinitions[listBox.SelectedIndex]);
            }
            else
            {
                removeButton.Enabled = false;
                moveUpButton.Enabled = false;
                moveDownButton.Enabled = false;
                nameTextBox.Enabled = false;
                gridXTextBox.Enabled = false;
                gridYTextBox.Enabled = false;
                typeComboBox.Enabled = false;
            }
        }
    }
}
