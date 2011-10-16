using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.Tools;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelEditors.Tools.GridTools;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public class ToolsWindow : OgmoWindow
    {
        public Tool CurrentTool { get; private set; }
        public event Ogmo.ToolCallback OnToolChanged;

        private Dictionary<Type, Tool[]> toolsForLayerTypes;
        private Dictionary<Keys, Tool> hotkeys;

        public ToolsWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Top)
        {
            Name = "ToolsWindow";
            Text = "Tools";
            ClientSize = new Size(48, 144);

            CurrentTool = null;

            //Initialize the tool lists
            toolsForLayerTypes = new Dictionary<Type, Tool[]>();
            toolsForLayerTypes.Add(typeof(GridLayerDefinition), new Tool[] { new GridPencilTool(), new GridFloodTool(), new GridRectangleTool() });
            toolsForLayerTypes.Add(typeof(TileLayerDefinition), new Tool[] { });
            toolsForLayerTypes.Add(typeof(ObjectLayerDefinition), new Tool[] { });

            //Init events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
            Ogmo.OnProjectStart += onProjectStart;
        }

        public void SetTool(Tool tool)
        {
            //If the current tool is already of that type, don't bother
            if (CurrentTool == tool)
                return;

            //Set it!
            CurrentTool = tool;
            if (tool != null)
                tool.SwitchTo();

            //Call the event
            if (OnToolChanged != null)
                OnToolChanged(tool);
        }

        protected override void handleKeyDown(KeyEventArgs e)
        {
            if (hotkeys != null && hotkeys.ContainsKey(e.KeyCode))
                SetTool(hotkeys[e.KeyCode]);
        }

        private void onProjectStart(Project project)
        {
            onLayerChanged(null, -1);
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            Controls.Clear();

            if (def != null)
            {
                foreach (var key in toolsForLayerTypes.Keys)
                {
                    if (key == def.GetType())
                    {
                        Tool[] tools = toolsForLayerTypes[key];
                        hotkeys = new Dictionary<Keys, Tool>();

                        for (int i = 0; i < tools.Length; i++)
                        {
                            Controls.Add(new ToolButton(tools[i], (i % 2) * 24, (i / 2) * 24));
                            hotkeys.Add(tools[i].Hotkey, tools[i]);
                        }

                        if (tools.Length > 0)
                            SetTool(tools[0]);
                        else
                            SetTool(null);
                        break;
                    }
                }
            }
        }
    }
}
