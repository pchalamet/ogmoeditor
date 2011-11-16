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
        private TileSelector tileSelector;

        public TilePaletteWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "TilePaletteWindow";
            Text = "Tile Palette";
            ClientSize = new Size(160, 192);

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;

            tilesetsComboBox = new ComboBox();
            tilesetsComboBox.Location = new Point(48, 4);
            tilesetsComboBox.Width = 104;
            tilesetsComboBox.Enabled = false;
            tilesetsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(tilesetsComboBox);

            Label tilesetsLabel = new Label();
            tilesetsLabel.Text = "Tileset:";
            tilesetsLabel.Location = new Point(4, 7);
            tilesetsLabel.Size = new Size(44, 20);
            Controls.Add(tilesetsLabel);

            tileSelector = new TileSelector();
            tileSelector.Location = new Point(4, 27);
            tileSelector.Size = new Size(ClientSize.Width - 8, ClientSize.Height - 31);
            tileSelector.Dock = DockStyle.Bottom;
            Controls.Add(tileSelector);

            Resize += new EventHandler(TilePaletteWindow_ResizeEnd);
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
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
            tilesetsComboBox.SelectedIndex = (Ogmo.Project.Tilesets.Count > 0) ? 0 : -1;
            tilesetsComboBox.Enabled = (Ogmo.Project.Tilesets.Count > 1);
        }

        /*
         *  Events
         */
        private void onLayerChanged(LayerDefinition layerDefinition, int index)
        {
            EditorVisible = layerDefinition is TileLayerDefinition;
            if (EditorVisible)
            {
                tilesetsComboBox.SelectedIndex = Ogmo.Project.Tilesets.IndexOf((Ogmo.LayersWindow.CurrentLayer as TileLayer).Tileset);
                tileSelector.Tileset = (Ogmo.LayersWindow.CurrentLayer as TileLayer).Tileset;
            }
        }

        private void TilePaletteWindow_ResizeEnd(object sender, EventArgs e)
        {
            tileSelector.Size = new Size(ClientSize.Width - 8, ClientSize.Height - 34);
        }
    }
}
