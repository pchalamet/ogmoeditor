using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;

namespace OgmoEditor.ProjectEditors.LayerEditors
{
    public partial class GridLayerEditor : UserControl
    {
        private GridLayerDefinition def;

        public GridLayerEditor(GridLayerDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(206, 117);
        }
    }
}
