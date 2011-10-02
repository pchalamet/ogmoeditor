using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OgmoEditor
{
    static public class Ogmo
    {
        public const float VERSION = .5f;

        static public readonly MainWindow MainWindow = new MainWindow();

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            InitializeDirectories();

            Application.Run(MainWindow);
        }

        /*
         *  Create the directory system
         */
        static public void InitializeDirectories()
        {
            if (!Directory.Exists("Projects"))
                Directory.CreateDirectory("Projects");
        }

        static public Project LoadProject(string filename)
        {
            if (!File.Exists(filename))
                throw new Exception("Loading a project file that does not exist!");

            BinaryFormatter bf = new BinaryFormatter();
            return (Project)bf.Deserialize(new FileStream(filename, FileMode.Open));
        }

        static public void AddProject(Project project)
        {
            TreeView tree = (TreeView)MainWindow.Controls["MasterTreeView"];
            tree.Nodes.Add(new TreeNode(project.Name));
        }
    }
}
