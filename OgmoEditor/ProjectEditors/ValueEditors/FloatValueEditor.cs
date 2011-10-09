using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.ValueDefinitions;

namespace OgmoEditor.ProjectEditors.ValueEditors
{
    public partial class FloatValueEditor : UserControl
    {
        private FloatValueDefinition def;

        public FloatValueEditor(FloatValueDefinition def)
        {
            this.def = def;
            InitializeComponent();
        }
    }
}
