using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;
using OgmoEditor.Definitions.LayerDefinitions;

namespace OgmoEditor.Windows
{
    public partial class ObjectsWindow : OgmoWindow
    {
        public ObjectDefinition CurrentObject { get; private set; }
        public event Ogmo.ObjectCallback OnObjectChanged;

        public ObjectsWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "ObjectsWindow";
            Text = "Objects";
            CurrentObject = null;

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        /*
         *  Events
         */
        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is ObjectLayerDefinition;
        }
    }
}
