using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;
using OgmoEditor.LevelEditors.Tools.EntityTools;

namespace OgmoEditor.Windows
{
    public partial class EntityButton : UserControl
    {
        static private readonly OgmoColor Selected = new OgmoColor(150, 220, 255);
        static private readonly OgmoColor NotSelected = new OgmoColor(255, 255, 255);

        public EntityDefinition Definition { get; private set; }

        public EntityButton(EntityDefinition definition, int x, int y)
        {
            Definition = definition;
            InitializeComponent();
            Location = new Point(x, y);
            toolTip.SetToolTip(button, definition.Name);
            button.BackgroundImage = Definition.Image;
            button.BackgroundImageLayout = ImageLayout.Zoom;
            button.BackColor = (definition == Ogmo.EntitiesWindow.CurrentEntity) ? Selected : NotSelected;

            //Events
            Ogmo.EntitiesWindow.OnEntityChanged += onObjectChanged;
        }

        public void OnRemove()
        {
            Ogmo.EntitiesWindow.OnEntityChanged -= onObjectChanged;
        }

        /*
         *  Events
         */
        private void button_Click(object sender, EventArgs e)
        {
            Ogmo.EntitiesWindow.SetObject(Definition);
            Ogmo.ToolsWindow.SetTool(typeof(EntityPlacementTool));
        }

        private void onObjectChanged(EntityDefinition definition)
        {
            if (definition == Definition)
            {
                button.BackColor = Selected;
                Select();
            }
            else
                button.BackColor = NotSelected;
        }
    }
}
