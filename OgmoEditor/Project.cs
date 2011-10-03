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
            TreeNode = new TreeNode(Name);
            Changed = false;
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

            Changed = false;
            TreeNode.Name = Name;

            BinaryFormatter bf = new BinaryFormatter();
            Stream s = new FileStream(LastFilename, FileMode.OpenOrCreate);
            bf.Serialize(s, this);
            s.Close();
        }

        public void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = WorkingDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Name;
            dialog.DefaultExt = ".oep";
            dialog.OverwritePrompt = true;
            dialog.Filter = "Ogmo Editor Project Files|*.oep";

            //Show dialog, handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            Changed = false;
            LastFilename = dialog.FileName;
            TreeNode.Name = Name;
    
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = dialog.OpenFile();
            bf.Serialize(s, this);
            s.Close();
        }

        public void SetChanged()
        {
            if (Changed)
                return;

            Changed = true;
            TreeNode.Name = Name + '*';
        }

        /*
         *  Level Stuff
         */
        public string GetNewLevelFilename()
        {
            int i = 0;
            string filename;

            do
            {
                filename = WorkingDirectory + @"\" + Ogmo.NEW_LEVEL_NAME + i.ToString() + Ogmo.LEVEL_EXT;
                i++;
            }
            while (File.Exists(filename) || HasLevelWithFilename(filename));              

            return filename;
        }

        public bool HasLevelWithFilename(string filename)
        {
            return Levels.Find(e => e.Filename == filename) != null;
        }

        public bool IsLevelNode(TreeNode node)
        {
            return Levels.Find(e => e.TreeNode == node) != null;
        }

        public void NewLevel()
        {
            AddLevel(new Level(this, GetNewLevelFilename()));  
        }

        public void OpenLevel()
        {
            //Get the file to load from the user
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Ogmo Editor Level Files|*" + Ogmo.LEVEL_EXT;
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            //Load it
            foreach (string f in dialog.FileNames)
            {
                Level level = new Level(this, f);
                AddLevel(level);
            }
        }

        private void AddLevel(Level level)
        {
            Levels.Add(level);
            TreeNode.Nodes.Add(level.TreeNode);
            TreeNode.Expand();

            if (OnLevelAdded != null)
                OnLevelAdded(level);
        }
    }
}
