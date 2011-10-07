using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors;

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
        public const string NEW_LEVEL_NAME = "New Level";
        public const string NEW_LAYER_NAME = "NewLayer";

        public delegate void ProjectCallback(Project project);
        public delegate void LevelCallback(Level level);

        static public readonly MainWindow MainWindow = new MainWindow();
        static public Project Project { get; private set; }
        static public Level CurrentLevel { get; private set; }
        static public event ProjectCallback OnProjectStart;
        static public event ProjectCallback OnProjectClose;
        static public event LevelCallback OnLevelChanged;

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
        static public void NewProject()
        {
            Project project = new Project();
            if (project.SaveAs(false))
            {
                StartProject(project);
                EditProject(true);
            }
        }

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
            XmlSerializer xs = new XmlSerializer(typeof(Project));
            Stream s = dialog.OpenFile();
            Project project = (Project)xs.Deserialize(s);
            s.Close();

            //Start the project
            StartProject(project);
        }

        static public void StartProject(Project project)
        {
            Project = project;

            //Call the added event
            if (OnProjectStart != null)
                OnProjectStart(project);
        }

        static public void CloseProject()
        {
            //Call removed event
            if (OnProjectClose != null)
                OnProjectClose(Project);

            //Remove it!
            Project = null;
        }

        static public void EditProject(bool newProject = false)
        {
            MainWindow.Enabled = false;
            ProjectEditor editor = new ProjectEditor(Ogmo.Project, newProject);
            editor.Show(MainWindow);
        }

        /*
         *  Level stuff
         */
        static public void SetLevel(Level level)
        {
            CurrentLevel = level;

            if (OnLevelChanged != null)
                OnLevelChanged(level);
        }
    }
}
