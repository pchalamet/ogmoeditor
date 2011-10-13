using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;

namespace OgmoEditor.ProjectEditors.LayerDefinitionEditors
{
    public partial class GridLayerDefinitionEditor : UserControl
    {
        private GridLayerDefinition def;

        public GridLayerDefinitionEditor(GridLayerDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(206, 117);

            colorChooser.Color = def.Color;
        }

        private void colorChooser_ColorChanged(OgmoColor color)
        {
            def.Color = color;
        }
    }
}
