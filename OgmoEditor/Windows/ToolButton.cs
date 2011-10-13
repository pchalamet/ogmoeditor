using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OgmoEditor.Windows
{
    public partial class ToolButton : UserControl
    {
        public Type ToolType { get; private set; }

        public ToolButton(string image, Type toolType)
        {
            ToolType = toolType;

            InitializeComponent();
            button.BackgroundImage = Image.FromFile(Path.Combine(Ogmo.ProgramDirectory, "Content\tools", image));
        }

        private void button_Click(object sender, EventArgs e)
        {
            
        }
    }
}
