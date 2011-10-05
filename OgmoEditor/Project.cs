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

namespace OgmoEditor
{
    [Serializable()]
    public class Project : ISerializable
    {
        //Serialized project properties
        private string name;
        public string WorkingDirectory;
        public Size LevelDefaultSize;
        public Size LevelMinimumSize;
        public Size LevelMaximumSize;
        public string LastFilename;
        public List<LayerDefinition> LayerDefinitions;

        //Non-serialzed instance vars
        private bool changed;
        public TreeNode TreeNode;
        public List<Level> Levels { get; private set; }

        //Events
        public event Ogmo.LevelCallback OnLevelAdded;
        public event Ogmo.LevelCallback OnLevelClosed;

        public Project()
        {
            //Init default project properties
            name = Ogmo.NEW_PROJECT_NAME;
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LastFilename = "";
            LayerDefinitions = new List<LayerDefinition>();
            LevelDefaultSize = LevelMinimumSize = LevelMaximumSize = new Size(640, 480);

            //Init running vars
            InitializeRunningVars();
        }

        /*
         *  Serialization stuff
         */
        public Project(SerializationInfo info, StreamingContext ctxt)
        {
            //Init project properties
            name = info.GetString("Name");
            WorkingDirectory = info.GetString("WorkingDirectory");
            LastFilename = info.GetString("LastFilename");
            LayerDefinitions = (List<LayerDefinition>)info.GetValue("LayerDefinitions", typeof(List<LayerDefinition>));
            LevelDefaultSize = (Size)info.GetValue("LevelDefaultSize", typeof(Size));
            LevelMinimumSize = (Size)info.GetValue("LevelMinimumSize", typeof(Size));
            LevelMaximumSize = (Size)info.GetValue("LevelMaximumSize", typeof(Size));

            //Init running vars
            InitializeRunningVars();
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                TreeNode.Text = value;
            }
        }

        public bool Changed
        {
            get { return changed; }
            set
            {
                changed = value;
                TreeNode.Text = Name + (changed ? "*" : "");
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Name", Name);
            info.AddValue("WorkingDirectory", WorkingDirectory);
            info.AddValue("LastFilename", LastFilename);
            info.AddValue("LayerDefinitions", LayerDefinitions);
            info.AddValue("LevelDefaultSize", LevelDefaultSize);
            info.AddValue("LevelMinimumSize", LevelMinimumSize);
            info.AddValue("LevelMaximumSize", LevelMaximumSize);
        }

        private void InitializeRunningVars()
        {
            changed = false;
            TreeNode = new TreeNode(Name);  
            Levels = new List<Level>();
        }

        /*
         *  Saving the project file
         */
        public void Save()
        {
            //If it hasn't been saved yet, go to SaveAs
            if (LastFilename == "")
            {
                SaveAs();
                return;
            }

            writeTo(LastFilename);
            Changed = false;
        }

        public void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = WorkingDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Name;
            dialog.DefaultExt = Ogmo.PROJECT_EXT;
            dialog.OverwritePrompt = true;
            dialog.Filter = Ogmo.PROJECT_FILTER;

            //Show dialog, handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            LastFilename = dialog.FileName;
            writeTo(dialog.FileName);
            Changed = false;
        }

        private void writeTo(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.OpenOrCreate);
            bf.Serialize(stream, this);
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
            var files = Directory.EnumerateFiles(WorkingDirectory, "*.oel");
            foreach (string str in files)
            {
                if (GetLevelByPath(str) == null)
                    AddLevel(new Level(this, str));
            }
        }
    }
}
