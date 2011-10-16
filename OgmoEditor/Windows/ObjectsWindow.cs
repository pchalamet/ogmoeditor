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
    public partial class ObjectsWindow : OgmoWindow
    {
        public ObjectDefinition CurrentObject { get; private set; }
        public event Ogmo.ObjectCallback OnObjectChanged;

        public ObjectsWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            Name = "ObjectsWindow";
            Text = "Objects";
            CurrentObject = null;

            //Events
            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectEdited += onProjectEdited;
            Ogmo.LayersWindow.OnLayerChanged += onLayerChanged;
        }

        public void SetObject(ObjectDefinition def)
        {
            if (CurrentObject == def)
                return;

            CurrentObject = def;

            //Call the event
            if (OnObjectChanged != null)
                OnObjectChanged(def);
        }

        private void initFromProject(Project project)
        {
            ClientSize = new Size(96, (1 + project.ObjectDefinitions.Count / 3) * 32);

            foreach (ObjectButton b in Controls)
                b.OnRemove();
            Controls.Clear();
            for (int i = 0; i < project.ObjectDefinitions.Count; i++)
                Controls.Add(new ObjectButton(project.ObjectDefinitions[i], (i % 3) * 32, (i / 3) * 32));
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
            EditorVisible = def is ObjectLayerDefinition;
        }
    }
}
