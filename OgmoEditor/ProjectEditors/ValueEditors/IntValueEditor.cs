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
    public partial class IntValueEditor : UserControl
    {
        private IntValueDefinition def;

        public IntValueEditor(IntValueDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(99, 53);

            defaultTextBox.Text = def.Default.ToString();
            minTextBox.Text = def.Min.ToString();
            maxTextBox.Text = def.Max.ToString();
            uiComboBox.SelectedIndex = (int)def.UIType;
        }

        private void defaultTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.GetInt(ref def.Default, defaultTextBox);
        }

        private void minTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.GetInt(ref def.Min, minTextBox);
        }

        private void maxTextBox_TextChanged(object sender, EventArgs e)
        {
            ProjParse.GetInt(ref def.Max, maxTextBox);
        }

        private void uiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            def.UIType = IntValueDefinition.UITypes.Field + uiComboBox.SelectedIndex;
        }
    }
}
