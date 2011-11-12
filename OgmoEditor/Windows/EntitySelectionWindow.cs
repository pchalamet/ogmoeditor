using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Drawing;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;
using System.Diagnostics;
using OgmoEditor.LevelEditors.ValueEditors;

namespace OgmoEditor.Windows
{
    public class EntitySelectionWindow : OgmoWindow
    {
        private const int WIDTH = 128;

        private List<Entity> selection;

        public EntitySelectionWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "EntitySelectionWindow";
            Text = "Selection";
            ClientSize = new Size(WIDTH, 128);
            selection = new List<Entity>();
            onSelectionChanged();

            //Events
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        public override bool ShouldBeVisible()
        {
            return Ogmo.LayersWindow.CurrentLayer is EntityLayer;
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

        public int AmountSelected
        {
            get { return selection.Count; }
        }

        public List<Entity> Selected
        {
            get { return new List<Entity>(selection); }
        }

        public void RefreshContents()
        {
            onSelectionChanged();
        }

        /*
         *  Events
         */
        private void onSelectionChanged()
        {
            Controls.Clear();

            if (selection.Count == 0)
            {
                ClientSize = new Size(WIDTH, 108);

                //No selection label
                Label lbl = new Label();
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Bounds = new Rectangle(0, 0, WIDTH, 108);
                lbl.Text = "No\nSelection";
                Controls.Add(lbl);
            }
            else if (selection.Count == 1)
            {
                //Name label
                Label name = new Label();
                name.Font = new Font(name.Font, FontStyle.Bold);
                name.TextAlign = ContentAlignment.MiddleCenter;
                name.Bounds = new Rectangle(0, 0, WIDTH, 24);
                name.Text = selection[0].Definition.Name;             
                Controls.Add(name);

                //Add the image
                EntitySelectionImage sel = new EntitySelectionImage(selection[0], WIDTH/2 - 16, 24);
                Controls.Add(sel);

                //Entity position
                Label pos = new Label();
                pos.TextAlign = ContentAlignment.MiddleCenter;
                pos.Bounds = new Rectangle(0, 58, WIDTH, 16);
                pos.Text = "( " + selection[0].Position.X.ToString() + ", " + selection[0].Position.Y.ToString() + " )";
                Controls.Add(pos);

                //Entity size
                Label size = new Label();
                size.TextAlign = ContentAlignment.MiddleCenter;
                size.Bounds = new Rectangle(0, 74, WIDTH, 16);
                size.Text = selection[0].Size.Width.ToString() + " x " + selection[0].Size.Height.ToString();
                Controls.Add(size);

                //Entity count
                Label count = new Label();
                count.TextAlign = ContentAlignment.MiddleCenter;
                count.Bounds = new Rectangle(0, 90, WIDTH, 16);
                count.Text = "Count: " + ((EntityLayer)Ogmo.LayersWindow.CurrentLayer).Entities.Count(e => e.Definition == selection[0].Definition).ToString();
                if (selection[0].Definition.Limit > 0)
                    count.Text += " / " + selection[0].Definition.Limit.ToString();
                Controls.Add(count);

                //Value editors
                int yy = 108;
                if (selection[0].Values != null)
                {
                    yy += 8;
                    foreach (var v in selection[0].Values)
                    {
                        ValueEditor ed = v.Definition.GetInstanceEditor(v, 0, yy);
                        Controls.Add(ed);
                        yy += ed.Height;
                    }
                }

                ClientSize = new Size(WIDTH, yy + 4);
            }
            else
            {
                ClientSize = new Size(WIDTH, ((selection.Count - 1) / 4) * 32 + 32);

                for (int i = 0; i < selection.Count; i++)
                {
                    EntitySelectionImage e = new EntitySelectionImage(selection[i], (i % 4) * 32, (i / 4) * 32);
                    Controls.Add(e);
                }
            }

            Ogmo.MainWindow.FocusEditor();
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is EntityLayerDefinition;
        }
    }
}
