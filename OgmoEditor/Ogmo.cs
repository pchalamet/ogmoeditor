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
        public const string IMAGE_FILE_FILTER = "PNG image file|*.png|BMP image file|*.bmp";

        public enum FinishProjectEditAction { None, CloseProject, SaveProject };

        public delegate void ProjectCallback(Project project);
        public delegate void LevelCallback(int index);
        public delegate void LayerCallback(LayerDefinition layerDefinition, int index);
        public delegate void ToolCallback(Tool tool);
        public delegate void EntityCallback(EntityDefinition objectDefinition);

        static public PresentationParameters Parameters;
        static public GraphicsDevice GraphicsDevice { get; private set; }
        static public Content Content { get; private set; }

        static public MainWindow MainWindow { get; private set; }
        static public ToolsWindow ToolsWindow { get; private set; }
        static public LayersWindow LayersWindow { get; private set; }
        static public TilePaletteWindow TilePaletteWindow { get; private set; }
        static public EntitiesWindow EntitiesWindow { get; private set; }
        static public EntitySelectionWindow EntitySelectionWindow { get; private set; }

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

            Application.Run(MainWindow);
        }

        static private void initialize()
        {
            //Figure out the program directory
            ProgramDirectory = Application.ExecutablePath.Remove(Application.ExecutablePath.IndexOf(Path.GetFileName(Application.ExecutablePath)));

            //Initialize directory system
            if (!Directory.Exists("Projects"))
                Directory.CreateDirectory("Projects");

            //Load the config file
            Config.Load();

            //The levels holder
            Levels = new List<Level>();
            CurrentLevelIndex = -1;

            //The windows
            MainWindow = new MainWindow();
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
            GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, Parameters);
            Content = new Content(GraphicsDevice);

            //Add the exit event
            Application.ApplicationExit += onApplicationExit;
        }

        static void onApplicationExit(object sender, EventArgs e)
        {
            Config.Save();
        }

        /*
         *  Project stuff
         */
        static public void NewProject()
        {
            Project project = new Project();
            project.InitDefault();
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

            LoadProject(dialog.FileName);
        }

        static public void LoadProject(string filename)
        {
            //Close the current project before loading the new one
            if (Project != null)
                CloseProject();

            //Load it
            XmlSerializer xs = new XmlSerializer(typeof(Project));
            Stream s = new FileStream(filename, FileMode.Open);
            Project project = (Project)xs.Deserialize(s);
            s.Close();
            project.Filename = filename;

            //Start the project
            StartProject(project);
            Content.LoadProjectTextures(Project);

            //Start a blank level and start at the first layer
            LayersWindow.SetLayer(0);
            NewLevel();

            Ogmo.MainWindow.StatusText = "Opened project " + Ogmo.Project.Name;
            Config.ConfigFile.UpdateRecentProjects(Project);
            GC.Collect();
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

        static public void EditProject(bool newProject)
        {
            //Warn!
            if (Ogmo.Levels.Count > 0 && Ogmo.Levels.Find(e => e.Changed) != null)
            {
                if (MessageBox.Show(MainWindow, "Warning: All levels must be closed if any changes to the project are made. You have unsaved changes in some open levels which will be lost. Still edit the project?", "Warning!", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }

            //Disable the main window
            MainWindow.DisableEditing();

            //Show the project editor
            ProjectEditor editor = new ProjectEditor(Ogmo.Project, newProject);
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
                Content.LoadProjectTextures(Project);

                //Call the event
                if (OnProjectEdited != null)
                    OnProjectEdited(Project);

                //Start a blank level
                NewLevel();

                //Set the status message
                Ogmo.MainWindow.StatusText = "Edited project " + Ogmo.Project.Name + ", all levels closed";
                Config.ConfigFile.UpdateRecentProjects(Project);
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

            //Load it
            foreach (string f in dialog.FileNames)
            {
                Level level = new Level(Project, f);
                AddLevel(level);
            }

            //Set it to the current level
            SetLevel(Levels.Count - 1);

            //Set the status message
            string[] files = new string[dialog.FileNames.Length];
            for (int i = 0; i < dialog.FileNames.Length; i++)
                files[i] = Path.GetFileName(dialog.FileNames[i]);
            Ogmo.MainWindow.StatusText = "Opened level(s) " + String.Join(", ", files);
        }

        static public void AddLevel(Level level)
        {
            //Add it
            Levels.Add(level);

            //Call the event
            if (OnLevelAdded != null)
                OnLevelAdded(Levels.Count - 1);
        }

        static public bool CloseLevel(Level level, bool askToSave)
        {
            //If it's changed, ask to save it
            if (askToSave && level.Changed)
            {
                DialogResult result = MessageBox.Show(MainWindow, "Save changes to \"" + level.SaveName + "\" before closing it?", "Unsaved Changes!", MessageBoxButtons.YesNoCancel);
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
