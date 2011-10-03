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

        static public readonly MainWindow MainWindow = new MainWindow();
        static public Project Project { get; private set; }

        public delegate void ProjectCallback(Project project);
        static public event ProjectCallback OnProjectStart;
        static public event ProjectCallback OnProjectUnload;

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
            dialog.Filter = "Ogmo Editor Project Files|*.oep";
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            //Close the current project before loading the new one
            if (Project != null)
                UnloadProject();

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

        static public void UnloadProject()
        {
            //Call removed event
            if (OnProjectUnload != null)
                OnProjectUnload(Project);

            //Remove it!
            Project = null;
        }

        /*
         *  Level Stuff
         */
    }
}
