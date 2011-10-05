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

        public void LoadFromProject(Project project)
        {
            layerDefinitions = new List<LayerDefinition>(project.LayerDefinitions);
            foreach (LayerDefinition d in layerDefinitions)
            {
                ListViewItem item = new ListViewItem();
                setItemFromDefinition(ref item, d);
                listView.Items.Add(item);
            }
        }

        public void ApplyToProject(Project project)
        {
            project.LayerDefinitions = layerDefinitions;
        }

        private void setItemFromDefinition(ref ListViewItem item, LayerDefinition definition)
        {
            item.Text = definition.Name;
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

            GridLayerDefinition grid = new GridLayerDefinition(name);
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
            ListViewItem item = new ListViewItem();

            setItemFromDefinition(ref item, def);

            layerDefinitions.Add(def);
            listView.Items.Add(item);

            listView.SelectedIndices.Clear();
            listView.SelectedIndices.Add(item.Index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView.SelectedItems[0];
            int index = item.Index;

            layerDefinitions.RemoveAt(index);
            listView.Items.Remove(item);

            if (listView.Items.Count > 0)
            {
                listView.SelectedIndices.Clear();
                listView.SelectedIndices.Add(Math.Min(listView.Items.Count - 1, index));
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                removeButton.Enabled = true;
                nameTextBox.Enabled = true;
                gridXTextBox.Enabled = true;
                gridYTextBox.Enabled = true;
                typeComboBox.Enabled = true;
                setControlsFromDefinition(layerDefinitions[listView.SelectedIndices[0]]);
            }
            else
            {
                removeButton.Enabled = false;
                nameTextBox.Enabled = false;
                gridXTextBox.Enabled = false;
                gridYTextBox.Enabled = false;
                typeComboBox.Enabled = false;    
            }
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            layerDefinitions[listView.SelectedIndices[0]].Name = nameTextBox.Text;
            listView.SelectedItems[0].Text = nameTextBox.Text;
        }

        private void gridXTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.GetSize(ref layerDefinitions[listView.SelectedIndices[0]].Grid, gridXTextBox, gridYTextBox);
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayerDefinition oldDef = layerDefinitions[listView.SelectedIndices[0]];
            LayerDefinition newDef;

            if (typeComboBox.SelectedIndex == 0)
            {
                newDef = new GridLayerDefinition(oldDef.Name);
            }
            else if (typeComboBox.SelectedIndex == 1)
            {
                newDef = new TileLayerDefinition(oldDef.Name);
            }
            else if (typeComboBox.SelectedIndex == 2)
            {
                newDef = new ObjectLayerDefinition(oldDef.Name);
            }
            else
            {
                newDef = new ShapeLayerDefinition(oldDef.Name);
            }

            newDef.Grid = oldDef.Grid;
            layerDefinitions[listView.SelectedIndices[0]] = newDef;
            setControlsFromDefinition(newDef);
        }

        
    }
}
