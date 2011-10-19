using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Drawing;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public class EntitySelectionWindow : OgmoWindow
    {
        private const int IMAGE_SIZE = 48;

        private List<Entity> selection;
        private ImagePreviewer imageBox;

        public EntitySelectionWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "EntitySelectionWindow";
            Text = "Selection";
            ClientSize = new Size(96, 128);
            selection = new List<Entity>();

            //Init the previewer for single selection
            imageBox = new ImagePreviewer();
            imageBox.Location = new Point(ClientSize.Width / 2 - IMAGE_SIZE / 2, 8);
            imageBox.Size = new Size(IMAGE_SIZE, IMAGE_SIZE);

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        /*
         *  Selection API
         */
        public void SetSelection(Entity e)
        {
            selection.Clear();
            selection.Add(e);
            onSelectionChanged();
        }

        public void SetSelection(List<Entity> e)
        {
            selection.Clear();
            foreach (Entity ee in e)
                selection.Add(ee);
            onSelectionChanged();
        }

        public void AddToSelection(Entity e)
        {
            if (!selection.Contains(e))
                selection.Add(e);
            onSelectionChanged();
        }

        public void AddToSelection(List<Entity> e)
        {
            foreach (Entity ee in e)
            {
                if (!selection.Contains(ee))
                    selection.Add(ee);
            }
            onSelectionChanged();
        }

        public void RemoveFromSelection(Entity e)
        {
            selection.Remove(e);
            onSelectionChanged();
        }

        public void RemoveFromSelection(List<Entity> e)
        {
            foreach (Entity ee in e)
                selection.Remove(ee);
            onSelectionChanged();
        }

        public void ClearSelection()
        {
            selection.Clear();
            onSelectionChanged();
        }

        public bool IsSelected(Entity e)
        {
            return selection.Contains(e);
        }

        public int AmountSelected()
        {
            return selection.Count;
        }

        /*
         *  Events
         */
        private void onSelectionChanged()
        {
            Controls.Clear();
            imageBox.ClearImage();
            if (selection.Count == 0)
            {
                
            }
            else if (selection.Count == 1)
            {
                imageBox.LoadImage(selection[0].Definition.GenerateButtonImage());
                Controls.Add(imageBox);
            }
            else
            {

            }
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is EntityLayerDefinition;
        }
    }
}
