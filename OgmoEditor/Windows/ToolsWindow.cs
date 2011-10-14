using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.LayerEditors.Tools;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelEditors.LayerEditors.Tools.GridTools;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class ToolsWindow : Form
    {
        private Dictionary<Type, Tool[]> ToolsForLayerTypes;

        public ToolsWindow()
        {
            InitializeComponent();

            //Initialize the tool lists
            ToolsForLayerTypes = new Dictionary<Type, Tool[]>();
            ToolsForLayerTypes.Add(typeof(GridLayerDefinition), new Tool[] { new GridPencilTool(), new GridFloodTool() });
            ToolsForLayerTypes.Add(typeof(TileLayerDefinition), new Tool[] { });
            ToolsForLayerTypes.Add(typeof(ObjectLayerDefinition), new Tool[] { });

            //Init events
            Ogmo.OnLayerChanged += onLayerChanged;
            Ogmo.OnProjectStart += onProjectStart;
        }

        private void onProjectStart(Project project)
        {
            onLayerChanged(project.LayerDefinitions[0], 0);
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            Controls.Clear();

            foreach (var key in ToolsForLayerTypes.Keys)
            {
                if (key == def.GetType())
                {
                    Tool[] tools = ToolsForLayerTypes[key];

                    for (int i = 0; i < tools.Length; i++)
                        Controls.Add(new ToolButton(tools[i], (i % 3) * 24, (i / 3) * 24));

                    if (tools.Length > 0)
                        Ogmo.SetTool(tools[0]);
                    else
                        Ogmo.SetTool(null);
                    break;
                }
            }            
        }

        private void ToolsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }
    }
}
