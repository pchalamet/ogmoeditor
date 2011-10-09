﻿using System;
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
            Location = new Point(99, 53);

            defaultTextBox.Text = def.Default.ToString();
            roundTextBox.Text = def.Round.ToString();
            minTextBox.Text = def.Min.ToString();
            maxTextBox.Text = def.Max.ToString();
            uiComboBox.SelectedIndex = (int)def.UIType;
        }

        private void defaultTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref def.Default, defaultTextBox);
        }

        private void roundTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref def.Round, defaultTextBox);
        }

        private void minTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref def.Min, defaultTextBox);
        }

        private void maxTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref def.Max, defaultTextBox);
        }

        private void uiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            def.UIType = (FloatValueDefinition.UITypes)uiComboBox.SelectedIndex;
        }
    }
}
