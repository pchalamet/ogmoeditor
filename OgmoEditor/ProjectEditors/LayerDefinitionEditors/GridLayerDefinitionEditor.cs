using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Diagnostics;

namespace OgmoEditor.ProjectEditors.LayerDefinitionEditors
{
    public partial class GridLayerDefinitionEditor : UserControl
    {
        private GridLayerDefinition def;

        public GridLayerDefinitionEditor(GridLayerDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(206, 128);

            colorChooser.Color = def.Color;
            exportModeComboBox.SelectedIndex = (int)def.ExportMode;
            trimZeroesCheckBox.Checked = def.TrimZeroes;

            trimZeroesCheckBox.Enabled = (def.ExportMode == GridLayerDefinition.ExportModes.Bitstring);
        }

        private void colorChooser_ColorChanged(OgmoColor color)
        {
            def.Color = color;
        }

        private void exportModeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            def.ExportMode = (GridLayerDefinition.ExportModes)exportModeComboBox.SelectedIndex;
            trimZeroesCheckBox.Enabled = exportModeComboBox.SelectedIndex == 0;
        }

        private void trimZeroesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            def.TrimZeroes = trimZeroesCheckBox.Checked;
        }
    }
}
