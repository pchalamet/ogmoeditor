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
        static private readonly OgmoColor Selected = new OgmoColor(150, 220, 255);
        static private readonly OgmoColor NotSelected = new OgmoColor(255, 255, 255);

        public ObjectDefinition Definition { get; private set; }

        public ObjectButton(ObjectDefinition definition, int x, int y)
        {
            Definition = definition;
            InitializeComponent();

            button.BackgroundImage = Definition.GenerateButtonImage();
            button.BackColor = (definition == Ogmo.ObjectsWindow.CurrentObject) ? Selected : NotSelected;

            //Events
            Ogmo.ObjectsWindow.OnObjectChanged += onObjectChanged;
        }

        public void OnRemove()
        {

        }

        /*
         *  Events
         */
        private void button_Click(object sender, EventArgs e)
        {
            Ogmo.ObjectsWindow.SetObject(Definition);
        }

        private void onObjectChanged(ObjectDefinition definition)
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
