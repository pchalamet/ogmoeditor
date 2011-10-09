using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    public partial class TilesetsEditor : UserControl, IProjectChanger
    {
        public TilesetsEditor()
        {
            InitializeComponent();
        }

        public string ErrorCheck()
        {
            return "";
        }

        public void LoadFromProject(Project project)
        {

        }

        public void ApplyToProject(Project project)
        {

        }
    }
}
