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
using OgmoEditor.ProjectEditors;

namespace OgmoEditor
{
    [XmlRoot("project")]
    public class Project
    {
        //Serialized project properties
        public string Name;
        public OgmoColor BackgroundColor;
        public OgmoColor GridColor;
        public Size LevelDefaultSize;
        public Size LevelMinimumSize;
        public Size LevelMaximumSize;
        public string LastFilename;

        //Definitions
        public List<ValueDefinition> LevelValueDefinitions;
        public List<LayerDefinition> LayerDefinitions;
        public List<Tileset> Tilesets;
        public List<ObjectDefinition> ObjectDefinitions;

        //Events
        public event Ogmo.ProjectCallback OnPathChanged;

        public Project()
        {
            //Init default project properties
            Name = Ogmo.NEW_PROJECT_NAME;
            BackgroundColor = OgmoColor.DefaultBackgroundColor;
            GridColor = OgmoColor.DefaultGridColor;
            LastFilename = "";
            LevelDefaultSize = LevelMinimumSize = LevelMaximumSize = new Size(640, 480);

            //Definitions
            LevelValueDefinitions = new List<ValueDefinition>();
            LayerDefinitions = new List<LayerDefinition>();
            Tilesets = new List<Tileset>();
            ObjectDefinitions = new List<ObjectDefinition>(); 
        }

        public void CloneFrom(Project copy)
        {
            Name = copy.Name;
            BackgroundColor = copy.BackgroundColor;
            LastFilename = copy.LastFilename;
            LevelDefaultSize = copy.LevelDefaultSize;
            LevelMinimumSize = copy.LevelMinimumSize;
            LevelMaximumSize = copy.LevelMaximumSize;

            LevelValueDefinitions = new List<ValueDefinition>();
            foreach (var d in copy.LevelValueDefinitions)
                LevelValueDefinitions.Add(d.Clone());

            LayerDefinitions = new List<LayerDefinition>();   
            foreach (var d in copy.LayerDefinitions)
                LayerDefinitions.Add(d.Clone());
  
            Tilesets = new List<Tileset>();
            foreach (var d in copy.Tilesets)
                Tilesets.Add(d.Clone());

            ObjectDefinitions = new List<ObjectDefinition>();
            foreach (var d in copy.ObjectDefinitions)
                ObjectDefinitions.Add(d.Clone());
        }

        public string ErrorCheck()
        {
            string s = "";

            /*
             *  PROJECT SETTINGS
             */

            s += ProjParse.CheckNonblankString(Name, "Project Name");
            s += ProjParse.CheckPosSize(LevelDefaultSize, "Default Level");
            s += ProjParse.CheckPosSize(LevelMinimumSize, "Minimum Level");
            s += ProjParse.CheckPosSize(LevelMaximumSize, "Maximum Level");
            s += ProjParse.CheckDefinitionList(LevelValueDefinitions, "Level");

            /*
             *  LAYER DEFINITIONS
             */

            //Must have at least 1 layer
            if (LayerDefinitions.Count == 0)
                s += ProjParse.Error("No layers are defined");

            //Check for duplicates and blanks
            s += ProjParse.CheckDefinitionList(LayerDefinitions);

            //Must have a tileset if you have a tile layer
            if (LayerDefinitions.Find(l => l is TileLayerDefinition) != null && Tilesets.Count == 0)
                s += ProjParse.Error("Tile layer(s) are defined, but no tilesets are defined");

            //Must have an object if you have an object layer
            if (LayerDefinitions.Find(l => l is ObjectLayerDefinition) != null && ObjectDefinitions.Count == 0)
                s += ProjParse.Error("Object layer(s) are defined, but no objects are defined");

            /*
             *  TILESETS
             */

            //Check for duplicates and blanks
            s += ProjParse.CheckDefinitionList(Tilesets);
            foreach (var t in Tilesets)
            {
                //File must exist
                s += ProjParse.CheckPath(t.Path, SavedDirectory, "Tileset \"" + t.Name + "\" image file");
            }

            /*
             *  OBJECTS
             */

            //Check for duplicates and blanks
            s += ProjParse.CheckDefinitionList(ObjectDefinitions);
            foreach (var o in ObjectDefinitions)
            {
                //Image file must exist if it is using an image file to draw
                if (o.ImageDefinition.DrawMode == ObjectImageDefinition.DrawModes.Image)
                    s += ProjParse.CheckPath(o.ImageDefinition.ImagePath, SavedDirectory, "Object \"" + o.Name + "\" image file");
            }

            return s;
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

        [XmlIgnore]
        public bool ExportWidth
        {
            get
            {
                return LevelMinimumSize.Width != LevelDefaultSize.Width || LevelMaximumSize.Width != LevelDefaultSize.Width;
            }
        }

        [XmlIgnore]
        public bool ExportHeight
        {
            get
            {
                return LevelMinimumSize.Height != LevelDefaultSize.Height || LevelMaximumSize.Height != LevelDefaultSize.Height;
            }
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
    }
}
