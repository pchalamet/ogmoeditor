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
        public string Name;
        public string WorkingDirectory;
        public string LastFilename;
        public List<LayerDefinition> LayerDefinitions;
        public Size LevelDefaultSize;
        public Size LevelMinimumSize;
        public Size LevelMaximumSize;

        //Non-serialzed instance vars
        public TreeNode TreeNode;
        public bool Changed { get; private set; }
        public List<Level> Levels { get; private set; }

        //Events
        public event Ogmo.LevelCallback OnLevelAdded;
        public event Ogmo.LevelCallback OnLevelClosed;

        public Project()
        {
            //Init project properties
            Name = Ogmo.NEW_PROJECT_NAME;
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LastFilename = "";
            LayerDefinitions = new List<LayerDefinition>();

            //Init running vars
            InitializeRunningVars();
        }

        /*
         *  Serialization stuff
         */
        public Project(SerializationInfo info, StreamingContext ctxt)
        {
            //Init project properties
            Name = info.GetString("Name");
            WorkingDirectory = info.GetString("WorkingDirectory");
            LastFilename = info.GetString("LastFilename");
            LayerDefinitions = (List<LayerDefinition>)info.GetValue("LayerDefinitions", typeof(List<LayerDefinition>));
            LevelDefaultSize = LevelMinimumSize = LevelMaximumSize = new Size(640, 480);

            //Init running vars
            InitializeRunningVars();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Name", Name);
            info.AddValue("WorkingDirectory", WorkingDirectory);
            info.AddValue("LastFilename", LastFilename);
            info.AddValue("LayerDefinitions", LayerDefinitions);
        }

        private void InitializeRunningVars()
        {
            TreeNode = new TreeNode();
            RemoveChanged();
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

            RemoveChanged();
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

            RemoveChanged();
        }

        private void writeTo(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.OpenOrCreate);
            bf.Serialize(stream, this);
            stream.Close();
        }

        /*
         *  Changed system
         */
        public void RemoveChanged()
        {
            Changed = false;
            TreeNode.Text = Name;
        }

        public void SetChanged()
        {
            Changed = true;
            TreeNode.Text = Name + '*';
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
