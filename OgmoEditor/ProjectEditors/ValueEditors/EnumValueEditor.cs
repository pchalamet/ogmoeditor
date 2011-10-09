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
    public partial class EnumValueEditor : UserControl
    {
        private EnumValueDefinition def;

        public EnumValueEditor(EnumValueDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(99, 53);

            elementsTextBox.Text = string.Join("\n", def.Elements, 0, def.Elements.Length);
        }

        private void elementsTextBox_Validated(object sender, EventArgs e)
        {
            def.Elements = elementsTextBox.Text.Split('\n');
        }


    }
}
