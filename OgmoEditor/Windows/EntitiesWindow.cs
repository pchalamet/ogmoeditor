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
using System.Diagnostics;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Windows
{
    public partial class EntitiesWindow : OgmoWindow
    {
        public EntityDefinition CurrentEntity { get; private set; }
        public event Ogmo.EntityCallback OnEntityChanged;

        public EntitiesWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "EntitiesWindow";
            Text = "Entities";
            CurrentEntity = null;

            //Events
            Ogmo.OnProjectStart += initFromProject;
            Ogmo.OnProjectEdited += initFromProject;
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        public override bool ShouldBeVisible()
        {
            return Ogmo.LayersWindow.CurrentLayer is EntityLayer;
        }

        public void SetObject(EntityDefinition def)
        {
            if (CurrentEntity == def)
                return;

            CurrentEntity = def;

            //Call the event
            if (OnEntityChanged != null)
                OnEntityChanged(def);
        }

        private void initFromProject(Project project)
        {
            ClientSize = new Size(96, ((project.EntityDefinitions.Count - 1) / 3) * 32 + 32);

            foreach (EntityButton b in Controls)
                b.OnRemove();
            Controls.Clear();
            for (int i = 0; i < project.EntityDefinitions.Count; i++)
            {
                Controls.Add(new EntityButton(project.EntityDefinitions[i], (i % 3) * 32, (i / 3) * 32));
            }

            SetObject(null);
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
