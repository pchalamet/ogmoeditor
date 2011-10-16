using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.Windows
{
    public partial class ObjectButton : UserControl
    {
        public ObjectDefinition Definition { get; private set; }

        public ObjectButton(ObjectDefinition definition, int x, int y)
        {
            Definition = definition;
            InitializeComponent();

            button.BackgroundImage = Definition.GenerateButtonImage();
        }
    }
}
