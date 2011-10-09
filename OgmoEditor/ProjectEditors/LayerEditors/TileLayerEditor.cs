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

namespace OgmoEditor.ProjectEditors.LayerEditors
{
    public partial class TileLayerEditor : UserControl
    {
        private TileLayerDefinition def;

        public TileLayerEditor(TileLayerDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(206, 117);

            Debug.WriteLine(def.MultipleTilesets);
            multipleTilesetsCheckBox.Checked = def.MultipleTilesets;
        }

        private void multipleTilesetsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            def.MultipleTilesets = multipleTilesetsCheckBox.Checked;
        }
    }
}
