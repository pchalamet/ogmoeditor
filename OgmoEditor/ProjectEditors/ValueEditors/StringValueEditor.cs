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
    public partial class StringValueEditor : UserControl
    {
        private StringValueDefinition def;

        public StringValueEditor(StringValueDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(99, 53);
        }
    }
}
