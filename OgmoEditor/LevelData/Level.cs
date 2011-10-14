using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelData
{
    public class Level
    {
        //Running instance variables
        private Project project;
        public string SavePath;
        public bool Changed;

        //Actual parameters to be edited/exported
        public Size Size { get; private set; }
        public List<Layer> Layers { get; private set; }

        public Level(Project project, string filename)
        {
            this.project = project;

            //Initialize layers
            Layers = new List<Layer>();
            foreach (var def in project.LayerDefinitions)
                Layers.Add(def.GetInstance());

            if (File.Exists(filename))
            {
                //Load the level from XML
                XmlDocument doc = new XmlDocument();
                doc.Load(new FileStream(filename, FileMode.Open));
                LoadFromXML(doc);

                SavePath = filename;
            }
            else
            {
                //Load the default parameters
                LoadDefault();

                SavePath = "";
            }

            Changed = false;
        }

        public Level(Project project, XmlDocument xml)
        {
            this.project = project;

            LoadFromXML(xml);
            SavePath = "";
            Changed = false;
        }

        public string Name
        {
            get
            {
                string s;
                if (SavePath == "")
                    s = Ogmo.NEW_LEVEL_NAME;
                else
                    s = Path.GetFileName(SavePath);
                if (Changed)
                    s += "*";
                return s;
            }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Size.Width, Size.Height); }
        }

        /*
         *  XML
         */
        public XmlDocument GenerateXML()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement level = doc.CreateElement("level");
            doc.AppendChild(level);

            return doc;
        }

        public void LoadFromXML(XmlDocument xml)
        {

        }

        public void LoadDefault()
        {
            Size = project.LevelDefaultSize;
        }

        /*
         *  Saving
         */
        public void Save()
        {
            //If it hasn't been saved before, do SaveAs instead
            if (SavePath == "")
            {
                SaveAs();
                return;
            }

            writeTo(SavePath);
        }

        public bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = project.SavedDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Name;
            dialog.DefaultExt = Ogmo.LEVEL_EXT;
            dialog.OverwritePrompt = true;
            dialog.Filter = Ogmo.LEVEL_FILTER;

            //Handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            SavePath = dialog.FileName;
            writeTo(dialog.FileName);

            return true;
        }

        private void writeTo(string filename)
        {
            //Generate the XML and write it!            
            XmlDocument doc = GenerateXML();
            doc.Save(filename);
        }

        public Level Duplicate()
        {
            return new Level(project, GenerateXML());
        }

    }
}
