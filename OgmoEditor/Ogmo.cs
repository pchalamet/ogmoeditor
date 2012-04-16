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
using OgmoEditor.LevelData.Layers;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelEditors.Tools;
using OgmoEditor.Definitions;
using OgmoEditor.LevelEditors;
using Microsoft.Xna.Framework.Graphics;
using OgmoEditor.Clipboard;
using System.Xml;
using System.Net;

namespace OgmoEditor
{
    static public class Ogmo
    {
        public const string PROJECT_EXT = ".oep";
        public const string LEVEL_EXT = ".oel";
        public const string PROJECT_FILTER = "Ogmo Editor Project File|*" + PROJECT_EXT;
        public const string LEVEL_FILTER = "Ogmo Editor Level File|*" + LEVEL_EXT;
        public const string NEW_PROJECT_NAME = "New Project";
        public const string NEW_LEVEL_NAME = "Unsaved Level";
        public const string IMAGE_FILE_FILTER = "PNG image file|*.png|BMP image file|*.bmp";
        private const int RECENT_PROJECT_LIMIT = 10;

        public enum FinishProjectEditAction { None, CloseProject, SaveProject, LoadAndSaveProject };
        public enum ProjectEditMode { NormalEdit, NewProject, ErrorOnLoad };

        public delegate void ProjectCallback(Project project);
        public delegate void LevelCallback(int index);
        public delegate void LayerCallback(LayerDefinition layerDefinition, int index);
        public delegate void ToolCallback(Tool tool);
        public delegate void EntityCallback(EntityDefinition objectDefinition);

        static public PresentationParameters Parameters;
        static public GraphicsDevice GraphicsDevice { get; private set; }
        static public EditorDraw EditorDraw { get; private set; }

        static public MainWindow MainWindow { get; private set; }
        static public ToolsWindow ToolsWindow { get; private set; }
        static public LayersWindow LayersWindow { get; private set; }
        static public TilePaletteWindow TilePaletteWindow { get; private set; }
        static public EntitiesWindow EntitiesWindow { get; private set; }
        static public EntitySelectionWindow EntitySelectionWindow { get; private set; }

        static private string toLoad;
        static public string ProgramDirectory { get; private set; }
        static public Project Project { get; private set; }
        static public List<Level> Levels { get; private set; }
        static public int CurrentLevelIndex { get; private set; }
        static public ClipboardItem Clipboard;

        static public event ProjectCallback OnProjectStart;
        static public event ProjectCallback OnProjectClose;
        static public event ProjectCallback OnProjectEdited;
        static public event LevelCallback OnLevelAdded;
        static public event LevelCallback OnLevelClosed;
        static public event LevelCallback OnLevelChanged;        

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            initialize();

            if (args.Length > 0 && File.Exists(args[0]) && Path.GetExtension(args[0]) == ".oep")
                toLoad = args[0];
            else
                toLoad = "";

            Application.Run(MainWindow);
        }

        static private void initialize()
        {
            //Figure out the program directory
            ProgramDirectory = Application.ExecutablePath.Remove(Application.ExecutablePath.IndexOf(Path.GetFileName(Application.ExecutablePath)));

            //Make sure the recent project list is valid
            InitRecentProjects();

            //The levels holder
            Levels = new List<Level>();
            CurrentLevelIndex = -1;

            //The windows
            MainWindow = new MainWindow();
            MainWindow.Shown += new EventHandler(MainWindow_Shown);
            LayersWindow = new LayersWindow();
            ToolsWindow = new ToolsWindow();
            TilePaletteWindow = new TilePaletteWindow();
            EntitiesWindow = new EntitiesWindow();
            EntitySelectionWindow = new EntitySelectionWindow();

            LayersWindow.Show(MainWindow);
            ToolsWindow.Show(MainWindow);
            TilePaletteWindow.Show(MainWindow);
            EntitiesWindow.Show(MainWindow);
            EntitySelectionWindow.Show(MainWindow);
            LayersWindow.EditorVisible = ToolsWindow.EditorVisible = TilePaletteWindow.EditorVisible = EntitiesWindow.EditorVisible = EntitySelectionWindow.EditorVisible = false;

            //Set up graphics stuff
            Parameters = new PresentationParameters();
            Parameters.BackBufferWidth = Math.Max(Ogmo.MainWindow.ClientSize.Width, 1);
            Parameters.BackBufferHeight = Math.Max(Ogmo.MainWindow.ClientSize.Height, 1);
            Parameters.BackBufferFormat = SurfaceFormat.Color;
            Parameters.DepthStencilFormat = DepthFormat.Depth24;
            Parameters.DeviceWindowHandle = Ogmo.MainWindow.Handle;
            Parameters.PresentationInterval = PresentInterval.Immediate;
            Parameters.IsFullScreen = false;
            GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, Parameters);
            EditorDraw = new EditorDraw(GraphicsDevice);

            //Add the exit event
            Application.ApplicationExit += onApplicationExit;
        }

        static void MainWindow_Shown(object sender, EventArgs e)
        {
            if (toLoad != "")
            {
                LoadProject(toLoad);
                toLoad = "";
            }
        }

        static void onApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        static public void LogException(Exception e)
        {
            string logPath = Path.Combine(Ogmo.ProgramDirectory, "errorLog.txt");

            FileStream file = new FileStream(logPath, FileMode.Append);
            StreamWriter logStream = new StreamWriter(file);
            logStream.Write(e.ToString() + "\r\n\r\n===============================\r\n\r\n");
            logStream.Close();
            file.Close();
        }

        /*
         *  Project stuff
         */
        static public void NewProject()
        {
            Project = new Project();
            Project.InitDefault();
            if (Project.SaveAs())
            {
                StartProject(Project);
                EditProject(ProjectEditMode.NewProject);
            }
        }

        static public void LoadProject()
        {
            //Get the file to load from the user
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = PROJECT_FILTER;
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            LoadProject(dialog.FileName);
        }

        static public void LoadProject(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show(MainWindow, "Project file could not be loaded because it does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Close the current project before loading the new one
            if (Project != null)
                CloseProject();

            //Load it
            XmlSerializer xs = new XmlSerializer(typeof(Project));
            Stream s = new FileStream(filename, FileMode.Open);
            Project = (Project)xs.Deserialize(s);
            s.Close();
            Project.Filename = filename;

            //Error check
            string errors = Project.ErrorCheck();
            if (errors == "")
                FinishProjectEdit(FinishProjectEditAction.LoadAndSaveProject);
            else
            {
                MessageBox.Show(MainWindow, "Project could not be loaded because of the following errors:\n" + errors + "\nFix the errors to continue with loading.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EditProject(ProjectEditMode.ErrorOnLoad);
            }
        }

        static public void StartProject(Project project)
        {
            Ogmo.MainWindow.RemoveStartPage();

            Project = project;

            //Call the added event
            if (OnProjectStart != null)
                OnProjectStart(project);
        }

        static public void CloseProject()
        {
            //Close all the open levels
            CloseAllLevels();

            //Set the status message
            Ogmo.MainWindow.StatusText = "Closed project " + Ogmo.Project.Name;

            //Call closed event
            if (OnProjectClose != null)
                OnProjectClose(Project);

            //Remove it!
            Project = null;

            //Tool and layer selection can be cleared now
            LayersWindow.SetLayer(-1);
            ToolsWindow.ClearTool();

            //Force a garbage collection
            Ogmo.MainWindow.AddStartPage();
            System.GC.Collect();
        }

        static public void EditProject(ProjectEditMode editMode)
        {
            //Warn!
            if (Ogmo.Levels.Count > 0 && Ogmo.Levels.Find(e => e.Changed) != null)
            {
                if (MessageBox.Show(MainWindow, "Warning: All levels must be closed if any changes to the project are made. You have unsaved changes in some open levels which will be lost. Still edit the project?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;
            }

            //Disable the main window
            MainWindow.DisableEditing();

            //Show the project editor
            ProjectEditor editor = new ProjectEditor(Project, editMode);
            editor.Show(MainWindow);
        }

        static public void FinishProjectEdit(FinishProjectEditAction action = FinishProjectEditAction.None)
        {
            //Re-activate the main window
            MainWindow.EnableEditing();

            if (action == FinishProjectEditAction.CloseProject)
                CloseProject();
            else if (action == FinishProjectEditAction.SaveProject)
            {
                //Close all the levels
                CloseAllLevels();

                //Save the project
                Project.Save();
                EditorDraw.LoadProjectTextures(Project);

                //Call the event
                if (OnProjectEdited != null)
                    OnProjectEdited(Project);

                //Start a blank level
                NewLevel();

                //Set the layer
                Ogmo.LayersWindow.SetLayer(0);

                //Set the status message
                Ogmo.MainWindow.StatusText = "Edited project " + Ogmo.Project.Name + ", all levels closed";
                UpdateRecentProjects(Project);
                GC.Collect();
            }
            else if (action == FinishProjectEditAction.LoadAndSaveProject)
            {
                //Start the project and save it
                StartProject(Project);
                Project.Save();
                EditorDraw.LoadProjectTextures(Project);

                //Start a blank level and start at the first layer
                LayersWindow.SetLayer(0);
                NewLevel();

                Ogmo.MainWindow.StatusText = "Opened project " + Ogmo.Project.Name;
                UpdateRecentProjects(Project);
                GC.Collect();
            }
        }

        /*
         *  Level Stuff
         */
        static public Level CurrentLevel
        {
            get
            {
                if (CurrentLevelIndex == -1)
                    return null;
                else
                    return Levels[CurrentLevelIndex];
            }
        }

        static public void SetLevel(int index)
        {
            //Can't set to what is already the current level
            if (index == CurrentLevelIndex)
                return;

            //Make it current
            CurrentLevelIndex = index;

            //Call the event
            if (OnLevelChanged != null)
                OnLevelChanged(index);
        }
        
        static public Level GetLevelByPath(string path)
        {
            return Levels.Find(e => e.SavePath == path);
        }

        static public void NewLevel()
        {
            AddLevel(new Level(Project, ""));
            SetLevel(Levels.Count - 1);
        }

        static public void OpenLevel()
        {
            //Get the file to load from the user
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = Ogmo.LEVEL_FILTER;
            dialog.InitialDirectory = Ogmo.Project.SavedDirectory;
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            //If the only open level is an empty one, close it
            if (Ogmo.Levels.Count == 1 && Ogmo.Levels[0].IsEmpty)
                Ogmo.CloseLevel(Ogmo.Levels[0], false);

            //Load it, unless it's already open
            foreach (string f in dialog.FileNames)
            {
                int levelID = -1;
                for (int i = 0; i < Ogmo.Levels.Count; i++)
                {
                    if (Ogmo.Levels[i].SavePath == f)
                    {
                        levelID = i;
                        break;
                    }
                }

                if (levelID == -1)
                {
                    Level level = new Level(Project, f);
                    AddLevel(level);
                    SetLevel(Levels.Count - 1);
                }
                else
                    SetLevel(levelID);
            }

            //Set the status message
            string[] files = new string[dialog.FileNames.Length];
            for (int i = 0; i < dialog.FileNames.Length; i++)
                files[i] = Path.GetFileName(dialog.FileNames[i]);
            Ogmo.MainWindow.StatusText = "Opened level(s) " + String.Join(", ", files);
        }

        static public bool AddLevel(Level level)
        {
            //Can't if past level limit
            if (Ogmo.Levels.Count >= Properties.Settings.Default.LevelLimit)
            {
                MessageBox.Show(Ogmo.MainWindow, "Couldn't add level because the level limit was exceeded! You can change the level limit in the Preferences menu.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Add it
            Levels.Add(level);

            //Call the event
            if (OnLevelAdded != null)
                OnLevelAdded(Levels.Count - 1);

            return true;
        }

        static public bool CloseLevel(Level level, bool askToSave)
        {
            //If it's changed, ask to save it
            if (askToSave && level.Changed)
            {
                DialogResult result = MessageBox.Show(MainWindow, "Save changes to \"" + level.SaveName + "\" before closing it?", "Unsaved Changes!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return false;
                else if (result == DialogResult.Yes)
                    return level.Save();
            }

            //Remove it
            int index = Levels.IndexOf(level);
            Levels.Remove(level);

            //Call the event
            if (OnLevelClosed != null)
                OnLevelClosed(index);

            //Set the current level to another one if that was the current one
            if (CurrentLevelIndex == index)
                SetLevel(Math.Min(index, Levels.Count - 1));
            else if (CurrentLevelIndex > index)
                CurrentLevelIndex--;

            //Force a garbage collection
            System.GC.Collect();

            //Set the status text
            Ogmo.MainWindow.StatusText = "Closed level " + level.SaveName;

            return true;
        }

        static public void CloseAllLevels()
        {
            while (Levels.Count > 0)
                CloseLevel(Levels[0], false);

            Ogmo.MainWindow.StatusText = "Closed all levels";
        }

        static public void CloseOtherLevels(Level level)
        {
            List<Level> temp = new List<Level>(Levels);
            foreach (Level lev in temp)
            {
                if (lev != level)
                {
                    if (!CloseLevel(lev, true))
                        return;
                }
            }
        }

        /*
         *  Recent Project Stuff
         */

        static public void InitRecentProjects()
        {
            if (Properties.Settings.Default.RecentProjects == null || Properties.Settings.Default.RecentProjectNames == null || Properties.Settings.Default.RecentProjects.Count != Properties.Settings.Default.RecentProjectNames.Count)
            {
                Properties.Settings.Default.RecentProjects = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentProjectNames = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();
            }
        }

        static public void ClearRecentProjects()
        {
            Properties.Settings.Default.RecentProjects.Clear();
            Properties.Settings.Default.RecentProjectNames.Clear();
        }

        static public void CheckRecentProjects()
        {
            for (int i = 0; i < Properties.Settings.Default.RecentProjects.Count; i++)
            {
                if (!File.Exists(Properties.Settings.Default.RecentProjects[i]))
                {
                    Properties.Settings.Default.RecentProjects.RemoveAt(i);
                    Properties.Settings.Default.RecentProjectNames.RemoveAt(i);
                    i--;
                }
            }
        }

        static public void UpdateRecentProjects(Project project)
        {
            for (int i = 0; i < Properties.Settings.Default.RecentProjects.Count; i++)
            {
                if (Properties.Settings.Default.RecentProjects[i] == project.Filename)
                {
                    Properties.Settings.Default.RecentProjects.RemoveAt(i);
                    Properties.Settings.Default.RecentProjectNames.RemoveAt(i);
                    break;
                }
            }

            Properties.Settings.Default.RecentProjects.Insert(0, project.Filename);
            Properties.Settings.Default.RecentProjectNames.Insert(0, project.Name);
            if (Properties.Settings.Default.RecentProjects.Count > RECENT_PROJECT_LIMIT)
            {
                Properties.Settings.Default.RecentProjects.RemoveAt(RECENT_PROJECT_LIMIT);
                Properties.Settings.Default.RecentProjectNames.RemoveAt(RECENT_PROJECT_LIMIT);
            }
        }
    }
}
