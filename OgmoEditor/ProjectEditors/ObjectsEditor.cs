using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.ProjectEditors
{
    public partial class ObjectsEditor : UserControl, IProjectChanger
    {
        private const string NEW_NAME = "NewObject";

        private List<ObjectDefinition> objects;

        public ObjectsEditor()
        {
            InitializeComponent();
        }

        public void LoadFromProject(Project project)
        {
            objects = project.ObjectDefinitions;
            foreach (var o in objects)
                listBox.Items.Add(o.Name);
        }

        private void setControlsFromObject(ObjectDefinition def)
        {
            removeButton.Enabled = true;
            moveUpButton.Enabled = listBox.SelectedIndex > 0;
            moveDownButton.Enabled = listBox.SelectedIndex < listBox.Items.Count - 1;

            nameTextBox.Enabled = true;
            limitTextBox.Enabled = true;
            sizeXTextBox.Enabled = true;
            sizeYTextBox.Enabled = true;
            originXTextBox.Enabled = true;
            originYTextBox.Enabled = true;
            resizableXCheckBox.Enabled = true;
            resizableYCheckBox.Enabled = true;
            rotatableCheckBox.Enabled = true;
            valuesEditor.Enabled = true;
            nodesCheckBox.Enabled = true;
            graphicTypeComboBox.Enabled = true;

            //Basics
            nameTextBox.Text = def.Name;
            limitTextBox.Text = def.Limit.ToString();
            sizeXTextBox.Text = def.Size.Width.ToString();
            sizeYTextBox.Text = def.Size.Height.ToString();
            originXTextBox.Text = def.Origin.X.ToString();
            originYTextBox.Text = def.Origin.Y.ToString();

            //Resizable/rotation
            resizableXCheckBox.Checked = def.ResizableX;
            resizableYCheckBox.Checked = def.ResizableY;
            rotatableCheckBox.Checked = def.Rotatable;
            rotationIncrementTextBox.Text = def.RotateIncrement.ToString();
            RotationFieldsVisible = def.Rotatable;

            //Nodes
            nodesCheckBox.Checked = def.NodesDefinition.Enabled;
            nodeLimitTextBox.Text = def.NodesDefinition.Limit.ToString();
            nodeDrawComboBox.SelectedIndex = (int)def.NodesDefinition.DrawMode;
            NodesFieldsVisible = def.NodesDefinition.Enabled;

            //Values
            valuesEditor.SetList(def.ValueDefinitions);

            //Graphic
            graphicTypeComboBox.SelectedIndex = (int)def.ImageDefinition.DrawMode;
        }

        private void disableControls()
        {
            removeButton.Enabled = false;
            moveUpButton.Enabled = false;
            moveDownButton.Enabled = false;

            nameTextBox.Enabled = false;
            limitTextBox.Enabled = false;
            sizeXTextBox.Enabled = false;
            sizeYTextBox.Enabled = false;
            originXTextBox.Enabled = false;
            originYTextBox.Enabled = false;
            resizableXCheckBox.Enabled = false;
            resizableYCheckBox.Enabled = false;
            rotatableCheckBox.Enabled = false;
            nodesCheckBox.Enabled = false;
            valuesEditor.Enabled = false;
            graphicTypeComboBox.Enabled = false;

            RotationFieldsVisible = false;
            NodesFieldsVisible = false;
        }

        private ObjectDefinition GetDefault()
        {
            ObjectDefinition def = new ObjectDefinition();

            int i = 0;
            string name;

            do
            {
                name = NEW_NAME + i.ToString();
                i++;
            }
            while (objects.Find(o => o.Name == name) != null);

            def.Name = name;

            return def;
        }

        private bool RotationFieldsVisible
        {
            set
            {
                rotationIncrementLabel.Visible = rotationIncrementTextBox.Enabled = rotationIncrementTextBox.Visible = value;
            }
        }

        private bool NodesFieldsVisible
        {
            set
            {
                nodeLimitTextBox.Visible = nodeLimitTextBox.Enabled = value;
                nodeLimitLabel.Visible = value;
                nodeDrawComboBox.Visible = nodeDrawComboBox.Enabled = value;
                nodeDrawLabel.Visible = value;
            }
        }

        private ObjectImageDefinition.DrawModes ImageFieldsVisibility
        {
            set
            {

            }
        }

        /*
         *  Events
         */
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                disableControls();
            else
                setControlsFromObject(objects[listBox.SelectedIndex]);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ObjectDefinition def = GetDefault();
            objects.Add(def);
            listBox.SelectedIndex = listBox.Items.Add(def.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            objects.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            ObjectDefinition temp = objects[index];
            objects[index] = objects[index - 1];
            objects[index - 1] = temp;

            listBox.Items[index] = objects[index].Name;
            listBox.Items[index - 1] = objects[index - 1].Name;
            listBox.SelectedIndex = index - 1;
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            ObjectDefinition temp = objects[index];
            objects[index] = objects[index + 1];
            objects[index + 1] = temp;

            listBox.Items[index] = objects[index].Name;
            listBox.Items[index + 1] = objects[index + 1].Name;
            listBox.SelectedIndex = index + 1;
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].Name = nameTextBox.Text;
            listBox.Items[listBox.SelectedIndex] = nameTextBox.Text;
        }

        private void limitTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref objects[listBox.SelectedIndex].Limit, limitTextBox);
        }

        private void sizeXTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref objects[listBox.SelectedIndex].Size, sizeXTextBox, sizeYTextBox);
        }

        private void originXTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref objects[listBox.SelectedIndex].Origin, originXTextBox, originYTextBox);
        }

        private void resizableXCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].ResizableX = resizableXCheckBox.Checked;
        }

        private void resizableYCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].ResizableY = resizableYCheckBox.Checked;
        }

        private void rotatableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].Rotatable = rotatableCheckBox.Checked;
            RotationFieldsVisible = rotatableCheckBox.Checked;
        }

        private void rotationIncrementTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref objects[listBox.SelectedIndex].RotateIncrement, rotationIncrementTextBox);
        }

        private void nodesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].NodesDefinition.Enabled = nodesCheckBox.Checked;
            NodesFieldsVisible = nodesCheckBox.Checked;
        }

        private void nodeLimitTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref objects[listBox.SelectedIndex].NodesDefinition.Limit, nodeLimitTextBox);
        }

        private void nodeDrawComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].NodesDefinition.DrawMode = (ObjectNodesDefinition.DrawModes)nodeDrawComboBox.SelectedIndex;
        }

        private void graphicTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objects[listBox.SelectedIndex].ImageDefinition.DrawMode = (ObjectImageDefinition.DrawModes)graphicTypeComboBox.SelectedIndex;
        }


    }
}
