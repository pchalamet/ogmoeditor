using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.Windows
{
    public class TilePaletteWindow : OgmoWindow
    {
        private ComboBox tilesetsComboBox;

        public TilePaletteWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "TilePaletteWindow";
            Text = "Tile Palette";
            ClientSize = new Size(160, 192);

            tilesetsComboBox = new ComboBox();
            tilesetsComboBox.Location = new Point(48, 4);
            tilesetsComboBox.Width = 104;
            tilesetsComboBox.Enabled = false;
            tilesetsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(tilesetsComboBox);

            Label tilesetsLabel = new Label();
            tilesetsLabel.Text = "Tileset:";
            tilesetsLabel.Location = new Point(4, 7);           
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
            tilesetsComboBox.Items.Clear();
            foreach (Tileset t in Ogmo.Project.Tilesets)
                tilesetsComboBox.Items.Add(t.Name);
            tilesetsComboBox.SelectedIndex = 0;
            tilesetsComboBox.Enabled = (Ogmo.Project.Tilesets.Count > 1);
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
