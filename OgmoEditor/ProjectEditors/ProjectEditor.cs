using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    public partial class ProjectEditor : Form
    {
        private Project project;

        public ProjectEditor(Project project)
        {
            this.project = project;
            InitializeComponent();
            LoadFromProject();

            //Events
            FormClosed += onClose;
        }

        private void LoadFromProject()
        {

        }

        private void onClose(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        public void Apply()
        {
            Close();
        }

        public void Cancel()
        {
            Close();
        }
    }
}
