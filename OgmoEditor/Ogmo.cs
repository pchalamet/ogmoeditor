using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Xml.Serialization;

namespace OgmoEditor
{
    static public class Ogmo
    {
        public const float VERSION = .5f;
        public const string PROJECT_EXT = ".oep";
        public const string LEVEL_EXT = ".oel";
        public const string PROJECT_FILTER = "Ogmo Editor Project File|*" + PROJECT_EXT;
        public const string LEVEL_FILTER = "Ogmo Editor Level File|*" + LEVEL_EXT;
        public const string NEW_PROJECT_NAME = "New Project";
        public const string NEW_LEVEL_NAME = "Untitled";

        public delegate void ProjectCallback(Project project);
        public delegate void LevelCallback(Level level);

        static public readonly MainWindow MainWindow = new MainWindow();
        static public Project Project { get; private set; }
        static public event ProjectCallback OnProjectStart;
        static public event ProjectCallback OnProjectClose;

        [STAThread]
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

        /*
         *  Project stuff
         */
        static public void LoadProject()
        {
            //Get the file to load from the user
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = PROJECT_FILTER;
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            //Close the current project before loading the new one
            if (Project != null)
                CloseProject();

            //Load it
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = dialog.OpenFile();
            Project project = (Project)bf.Deserialize(s);
            s.Close();

            //Start the project
            StartProject(project);
        }

        static public void StartProject(Project project)
        {
            //Call the added event
            if (OnProjectStart != null)
                OnProjectStart(project);

            Project = project;
        }

        static public void CloseProject()
        {
            //Call removed event
            if (OnProjectClose != null)
                OnProjectClose(Project);

            //Remove it!
            Project = null;
        }
    }
}
