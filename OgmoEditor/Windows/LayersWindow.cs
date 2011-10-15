using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Windows
{
    public partial class LayersWindow : OgmoWindow
    {
        public int CurrentLayerIndex { get; private set; }
        public event Ogmo.LayerCallback OnLayerChanged;

        public LayersWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Top)
        {
            InitializeComponent();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectEdited += onProjectEdited;

            CurrentLayerIndex = -1;
        }

        public Layer CurrentLayer
        {
            get
            {
                if (Ogmo.CurrentLevel == null)
                    return null;
                else
                    return Ogmo.CurrentLevel.Layers[CurrentLayerIndex];
            }
        }

        public void SetLayer(int index)
        {
            //Can't set to what is already the current layer
            if (index == CurrentLayerIndex)
                return;

            //Make it current
            CurrentLayerIndex = index;

            //Call the event
            if (OnLayerChanged != null)
                OnLayerChanged(index == -1 ? null : Ogmo.Project.LayerDefinitions[index], index);
        }

        private void onProjectStart(Project project)
        {
            initFromProject(project);
        }

        private void onProjectEdited(Project project)
        {
            initFromProject(project);
        }

        private void initFromProject(Project project)
        {
            ClientSize = new Size(120, project.LayerDefinitions.Count * 24);

            foreach (LayerButton b in Controls)
                b.OnRemove();
            Controls.Clear();
            for (int i = 0; i < project.LayerDefinitions.Count; i++)
                Controls.Add(new LayerButton(project.LayerDefinitions[i], i * 24));
        }
    }
}
