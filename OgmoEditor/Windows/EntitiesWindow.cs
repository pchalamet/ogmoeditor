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
    public partial class EntitiesWindow : OgmoWindow
    {
        public EntityDefinition CurrentEntity { get; private set; }
        public event Ogmo.EntityCallback OnEntityChanged;

        public EntitiesWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "EntitiesWindow";
            Text = "Entities";
            CurrentEntity = null;

            //Events
            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectEdited += onProjectEdited;
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
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
            ClientSize = new Size(96, (1 + project.EntityDefinitions.Count / 3) * 32);

            foreach (EntityButton b in Controls)
                b.OnRemove();
            Controls.Clear();
            for (int i = 0; i < project.EntityDefinitions.Count; i++)
                Controls.Add(new EntityButton(project.EntityDefinitions[i], (i % 3) * 32, (i / 3) * 32));
        }

        /*
         *  Events
         */
        private void onProjectStart(Project project)
        {
            initFromProject(project);
        }

        private void onProjectEdited(Project project)
        {
            initFromProject(project);
        }

        private void onLayerChanged(LayerDefinition def, int index)
        {
            EditorVisible = def is EntityLayerDefinition;
        }
    }
}
