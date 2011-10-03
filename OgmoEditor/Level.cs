using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace OgmoEditor
{
    public class Level : Panel
    {
        private Project project;
        public string Filename;
        public TreeNode TreeNode;
        public bool Changed { get; private set; }

        public Level(Project project, string filename)
            : base()
        {
            this.project = project;
            Filename = filename;

            if (File.Exists(Filename))
            {
                //Load the level from XML
            }
            else
            {
                //Load the default parameters
                Size = project.LevelDefaultSize;
            }

            TreeNode = new TreeNode(Path.GetFileName(Filename));
            Changed = false;
            Paint += new PaintEventHandler(levelPaint);
        }

        private void levelPaint(object sender, PaintEventArgs e)
        {

        }


    }
}
