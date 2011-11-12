using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Windows
{
    public class TilePaletteWindow : OgmoWindow
    {       
        public TilePaletteWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "TilePaletteWindow";
            Text = "Tile Palette";
            ClientSize = new Size(64, 144);

            Ogmo.LayersWindow.OnLayerChanged += new Ogmo.LayerCallback(onLayerChanged);
        }

        public override bool ShouldBeVisible()
        {
            return Ogmo.LayersWindow.CurrentLayer is TileLayer;
        }

        /*
         *  Events
         */
        private void onLayerChanged(LayerDefinition layerDefinition, int index)
        {
            EditorVisible = layerDefinition is TileLayerDefinition;
        }
    }
}
