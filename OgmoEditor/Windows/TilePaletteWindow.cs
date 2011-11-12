using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public class TilePaletteWindow : OgmoWindow
    {       
        public TilePaletteWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "TilePaletteWindow";
            Text = "Tile Palette";
            ClientSize = new Size(160, 192);

            Label tilesetsLabel = new Label();
            tilesetsLabel.Text = "Tileset:";
            tilesetsLabel.Location = new Point(4, 4);
            Controls.Add(tilesetsLabel);

            Ogmo.LayersWindow.OnLayerChanged += new Ogmo.LayerCallback(onLayerChanged);
            Ogmo.OnProjectStart += initFromProject;
        }

        public override bool ShouldBeVisible()
        {
            return Ogmo.LayersWindow.CurrentLayer is TileLayer;
        }

        public void initFromProject(Project project)
        {

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
