using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public partial class LayersWindow : OgmoWindow
    {
        public LayersWindow()
        {
            InitializeComponent();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectEdited += onProjectEdited;
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
