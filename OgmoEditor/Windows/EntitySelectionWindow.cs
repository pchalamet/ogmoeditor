using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Drawing;

namespace OgmoEditor.Windows
{
    public class EntitySelectionWindow : OgmoWindow
    {
        public EntitySelectionWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "EntitySelectionWindow";
            Text = "Selection";

            ClientSize = new Size(96, 128);

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        /*
         *  Events
         */
        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is EntityLayerDefinition;
        }
    }
}
