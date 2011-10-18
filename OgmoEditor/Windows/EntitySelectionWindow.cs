using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Drawing;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Windows
{
    public class EntitySelectionWindow : OgmoWindow
    {
        private List<Entity> selection;

        public EntitySelectionWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "EntitySelectionWindow";
            Text = "Selection";
            ClientSize = new Size(96, 128);
            selection = new List<Entity>();

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        /*
         *  Selection API
         */
        public void SetSelection(Entity e)
        {
            selection.Clear();
            AddToSelection(e);
        }

        public void SetSelection(List<Entity> e)
        {
            selection.Clear();
            AddToSelection(e);
        }

        public void AddToSelection(Entity e)
        {
            selection.Add(e);
            onSelectionChanged();
        }

        public void AddToSelection(List<Entity> e)
        {
            foreach (Entity ee in e)
                selection.Add(ee);
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

        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is EntityLayerDefinition;
        }
    }
}
