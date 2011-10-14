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
    public partial class LayersWindow : Form
    {
        public LayersWindow()
        {
            InitializeComponent();

            Ogmo.OnProjectStart += onProjectStart;
            Ogmo.OnProjectEdited += onProjectEdited;
        }

        private void LayersWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
                Ogmo.MainWindow.Focus();
            }
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

            Controls.Clear();
            for (int i = 0; i < project.LayerDefinitions.Count; i++)
                Controls.Add(new LayerButton(project.LayerDefinitions[i], i * 24));
        }
    }
}
