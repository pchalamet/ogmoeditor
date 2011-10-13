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
using OgmoEditor.XNA;
using System.Reflection;
using OgmoEditor.LevelData;
using OgmoEditor.Windows;

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
        public const string NEW_LEVEL_NAME = "Unsaved Level";
        public const string NEW_LAYER_NAME = "NewLayer";
        public const string IMAGE_FILE_FILTER = "PNG image file|*.png|BMP image file|*.bmp";

        public delegate void ProjectCallback(Project project);
        public delegate void LevelCallback(Level level);

        static public readonly MainWindow MainWindow = new MainWindow();
        static public readonly ToolsWindow ToolsWindow = new ToolsWindow();
        static public readonly LayersWindow LayersWindow = new LayersWindow();
        static public string ProgramDirectory { get; private set; }

        static public Project Project { get; private set; }
        static public List<Level> Levels { get; private set; }

        static public event ProjectCallback OnProjectStart;
        static public event ProjectCallback OnProjectClose;
        static public event Ogmo.LevelCallback OnLevelAdded;
        static public event Ogmo.LevelCallback OnLevelClosed;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            initialize();

            Application.Run(MainWindow);
        }

        static private void initialize()
        {
            ProgramDirectory = Application.ExecutablePath.Remove(Application.ExecutablePath.IndexOf(Path.GetFileName(Application.ExecutablePath)));

            //Initialize directory system
            if (!Directory.Exists("Projects"))
                Directory.CreateDirectory("Projects");

            //The levels holder
            Levels = new List<Level>();

            //The windows
            LayersWindow.Show(MainWindow);          
            ToolsWindow.Show(MainWindow);
            LayersWindow.Visible = ToolsWindow.Visible = false;
        }

        /*
         *  Project stuff
         */
        static public void NewProject()
        {
            Project project = new Project();
            if (project.SaveAs())
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
         *  Level Stuff
         */
        static public Level GetLevelByPath(string path)
        {
            return Levels.Find(e => e.SavePath == path);
        }

        static public void NewLevel()
        {
            AddLevel(new Level(Project, ""));
        }

        static public void OpenLevel()
        {
            //Get the file to load from the user
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = Ogmo.LEVEL_FILTER;
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            //Load it
            foreach (string f in dialog.FileNames)
            {
                Level level = new Level(Project, f);
                AddLevel(level);
            }
        }

        static public void AddLevel(Level level)
        {
            Levels.Add(level);

            if (OnLevelAdded != null)
                OnLevelAdded(level);
        }

        static public void CloseLevel(Level level)
        {
            if (OnLevelClosed != null)
                OnLevelClosed(level);

            Levels.Remove(level);
        }

        static public void CloseOtherLevels(Level level)
        {
            List<Level> temp = new List<Level>(Levels);
            foreach (Level lev in temp)
            {
                if (lev != level)
                    CloseLevel(lev);
            }
        }

        static public void OpenAllLevels()
        {
            var files = Directory.EnumerateFiles(Project.SavedDirectory, "*.oel");
            foreach (string str in files)
            {
                if (GetLevelByPath(str) == null)
                    AddLevel(new Level(Project, str));
            }
        }
    }
}
