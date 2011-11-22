using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.Tools;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelEditors.Tools.GridTools;
using OgmoEditor.LevelEditors.Tools.EntityTools;
using OgmoEditor.LevelEditors.Tools.TileTools;

namespace OgmoEditor.Windows
{
    public class ToolsWindow : OgmoWindow
    {
        public Tool CurrentTool { get; private set; }
        public event Ogmo.ToolCallback OnToolChanged;

        private Dictionary<Type, Tool[]> toolsForLayerTypes;
        private Dictionary<Keys, Tool> hotkeys;
        private Tool[] tools;

        public ToolsWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Top)
        {
            Name = "ToolsWindow";
            Text = "Tools";
            ClientSize = new Size(48, 144);

            CurrentTool = null;

            //Initialize the tool lists
            toolsForLayerTypes = new Dictionary<Type, Tool[]>();
            toolsForLayerTypes.Add(typeof(GridLayerDefinition), new Tool[] { new GridPencilTool(), new GridFloodTool(), new GridRectangleTool(), new GridLineTool() });
            toolsForLayerTypes.Add(typeof(TileLayerDefinition), new Tool[] { new TilePencilTool(), new TileEyedropperTool() });
            toolsForLayerTypes.Add(typeof(EntityLayerDefinition), new Tool[] { new EntityPlacementTool(), new EntityEraseTool(), new EntitySelectionTool(), new EntityMoveTool(), new EntityResizeTool(), new EntityRotateTool(), new EntityAddNodeTool(), new EntityInsertNodeTool() });

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
            Ogmo.OnLevelAdded += onLevelAdded;
        }

        public void SetTool(Tool tool)
        {
            if (CurrentTool == tool)
                return;

            //Set it!
            CurrentTool = tool;
            if (tool != null)
                tool.SwitchTo();

            //Call the event
            if (OnToolChanged != null)
                OnToolChanged(tool);

            Ogmo.MainWindow.FocusEditor();
        }

        public void SetTool(Type toolType)
        {
            if (tools != null)
            {
                for (int i = 0; i < tools.Length; i++)
                {
                    if (tools[i].GetType() == toolType)
                    {
                        SetTool(tools[i]);
                        return;
                    }
                }
            }
        }

        public void ClearTool()
        {
            tools = null;
            SetTool((Tool)null);
        }

        protected override void handleKeyDown(KeyEventArgs e)
        {
            if (hotkeys != null && hotkeys.ContainsKey(e.KeyCode))
                SetTool(hotkeys[e.KeyCode]);
        }

        /*
         *  Events
         */
        private void onLevelAdded(int index)
        {
            EditorVisible = true;
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            Controls.Clear();

            if (def != null)
            {
                tools = toolsForLayerTypes[def.GetType()];
                hotkeys = new Dictionary<Keys, Tool>();

                for (int i = 0; i < tools.Length; i++)
                {
                    Controls.Add(new ToolButton(tools[i], (i % 2) * 24, (i / 2) * 24));
                    hotkeys.Add(tools[i].Hotkey, tools[i]);
                }

                if (tools.Length > 0)
                    SetTool(tools[0]); 
                else
                    ClearTool();
            }
        }
    }
}
