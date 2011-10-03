using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor
{
    public class Level : Panel
    {
        private Project project;

        public TreeNode TreeNode;

        public Level(Project project)
            : base()
        {
            this.project = project;
            Size = project.LevelDefaultSize;

            Paint += new PaintEventHandler(levelPaint);
        }

        private void levelPaint(object sender, PaintEventArgs e)
        {

        }
    }
}
