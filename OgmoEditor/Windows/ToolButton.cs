using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OgmoEditor.LevelEditors.LayerEditors.Tools;

namespace OgmoEditor.Windows
{
    public partial class ToolButton : UserControl
    {
        public Tool Tool { get; private set; }

        public ToolButton(Tool tool, int x, int y)
        {
            Tool = tool;
            Location = new Point(x, y);

            InitializeComponent();
            button.BackgroundImage = Image.FromFile(Path.Combine(Ogmo.ProgramDirectory, @"Content\tools", Tool.Image));

            Ogmo.OnToolChanged += onToolChanged;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Ogmo.SetTool(Tool);
        }

        private void onToolChanged(Tool tool)
        {
            button.Enabled = (Tool != tool);
        }
    }
}
