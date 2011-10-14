using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;
using System.IO;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class LayerButton : UserControl
    {
        static private readonly OgmoColor NotSelected = new OgmoColor(220, 220, 220);
        static private readonly OgmoColor Selected = new OgmoColor(255, 125, 50);

        public LayerDefinition LayerDefinition { get; private set; }

        public LayerButton(LayerDefinition definition, int y)
        {
            LayerDefinition = definition;
            InitializeComponent();
            Location = new Point(0, y);
            pictureBox.Image = Image.FromFile(Path.Combine(Ogmo.ProgramDirectory, @"Content\layers", LayerDefinition.Image));
            layerNameLabel.Text = definition.Name;

            //Init state
            layerNameLabel.BackColor = Ogmo.Project.LayerDefinitions[Ogmo.CurrentLayerIndex] == LayerDefinition ? Selected : NotSelected;
            visibleCheckBox.Checked = LayerDefinition.Visible;

            //Add events
            ParentChanged += onParentChanged;
            Ogmo.OnLayerChanged += onLayerChanged;
        }

        private void onParentChanged(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                //Clean up events
                Ogmo.OnLayerChanged -= onLayerChanged;
            }
        }

        private void onLayerChanged(LayerDefinition layer, int index)
        {
            Debug.WriteLine("cleanup!");
            layerNameLabel.BackColor = layer == LayerDefinition ? Selected : NotSelected;
        }

        private void visibleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LayerDefinition.Visible = visibleCheckBox.Checked;
        }

    }
}
