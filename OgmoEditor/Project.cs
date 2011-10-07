using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Runtime.Serialization;
using System.Drawing;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections;
using OgmoEditor.Definitions.ValueDefinitions;
using OgmoEditor.Definitions;

namespace OgmoEditor
{
    [XmlRoot("project")]
    public class Project
    {
        //Serialized project properties
        private string name;
        public Size LevelDefaultSize;
        public Size LevelMinimumSize;
        public Size LevelMaximumSize;
        public string LastFilename;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                TreeNode.Text = value;
            }
        }

        //Definitions
        public List<LayerDefinition> LayerDefinitions;
        public List<ValueDefinition> LevelValueDefinitions;
        public List<ObjectDefinition> ObjectDefinitions;
        public List<Tileset> Tilesets;

        //Non-serialzed instance vars
        private bool changed;
        [XmlIgnore]
        public TreeNode TreeNode;
        [XmlIgnore]
        public List<Level> Levels { get; private set; }

        //Events
        public event Ogmo.LevelCallback OnLevelAdded;
        public event Ogmo.LevelCallback OnLevelClosed;
        public event Ogmo.ProjectCallback OnPathChanged;

        public Project()
        {
            //Init default project properties
            name = Ogmo.NEW_PROJECT_NAME;
            LastFilename = "";
            LevelDefaultSize = LevelMinimumSize = LevelMaximumSize = new Size(640, 480);

            //Definitions
            LayerDefinitions = new List<LayerDefinition>();
            LevelValueDefinitions = new List<ValueDefinition>();
            ObjectDefinitions = new List<ObjectDefinition>();
            Tilesets = new List<Tileset>();

            //Init running vars
            InitializeRunningVars();
        }

        private void InitializeRunningVars()
        {
            changed = false;
            TreeNode = new TreeNode(Name);
            Levels = new List<Level>();
        }

        [XmlIgnore]
        public bool Changed
        {
            get { return changed; }
            set
            {
                changed = value;
                TreeNode.Text = Name + (changed ? "*" : "");
            }
        }

        [XmlIgnore]
        public string SavedDirectory
        {
            get
            {
                string dir = LastFilename;
                if (dir == "")
                    return "";

                string filename = Path.GetFileName(dir);
                return dir.Remove(dir.IndexOf(filename) - 1);
            }
        }

        public string GetPath(string path)
        {
            return SavedDirectory + Path.DirectorySeparatorChar + path;
        }

        /*
         *  Saving the project file
         */
        public void Save()
        {
            //If it hasn't been saved yet, go to SaveAs
            if (LastFilename == "")
            {
                if (!SaveAs())
                    return;
            }

            writeTo(LastFilename);
            Changed = false;
        }

        public bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (LastFilename == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = SavedDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Name;
            dialog.DefaultExt = Ogmo.PROJECT_EXT;
            dialog.OverwritePrompt = true;
            dialog.Filter = Ogmo.PROJECT_FILTER;

            //Show dialog, handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            LastFilename = dialog.FileName;
            if (OnPathChanged != null)
                OnPathChanged(this);

            return true;
        }

        private void writeTo(string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Project));
            Stream stream = new FileStream(filename, FileMode.Create);
            xs.Serialize(stream, this);
            stream.Close();
        }

        /*
         *  Level Stuff
         */
        public bool IsLevelNode(TreeNode node)
        {
            return Levels.Find(e => e.TreeNode == node) != null;
        }

        public Level GetLevelFromNode(TreeNode node)
        {
            return Levels.Find(e => e.TreeNode == node);
        }

        public Level GetLevelByPath(string path)
        {
            return Levels.Find(e => e.SavePath == path);
        }

        public void NewLevel()
        {
            AddLevel(new Level(this, Ogmo.NEW_LEVEL_NAME));  
        }

        public void OpenLevel()
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
                Level level = new Level(this, f);
                AddLevel(level);
            }
        }

        public void AddLevel(Level level)
        {
            Levels.Add(level);
            TreeNode.Nodes.Add(level.TreeNode);
            TreeNode.Expand();

            if (OnLevelAdded != null)
                OnLevelAdded(level);
        }

        public void CloseLevel(Level level)
        {
            Levels.Remove(level);
            TreeNode.Nodes.Remove(level.TreeNode);

            if (OnLevelClosed != null)
                OnLevelClosed(level);
        }

        public void CloseOtherLevels(Level level)
        {
            List<Level> temp = new List<Level>(Levels);
            foreach (Level lev in temp)
            {
                if (lev != level)
                    CloseLevel(lev);
            }
        }

        public void OpenAllLevels()
        {
            var files = Directory.EnumerateFiles(SavedDirectory, "*.oel");
            foreach (string str in files)
            {
                if (GetLevelByPath(str) == null)
                    AddLevel(new Level(this, str));
            }
        }
    }
}
