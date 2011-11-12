using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors;

namespace OgmoEditor.LevelData
{
    public class Level
    {
        //Running instance variables
        public Project Project { get; private set; }
        public string SavePath;
        private bool changed;

        //Actual parameters to be edited/exported
        public Size Size;
        public List<Layer> Layers { get; private set; }
        public List<Value> Values { get; private set; }

        public Level(Project project, string filename)
        {
            this.Project = project;
            initialize();

            if (File.Exists(filename))
            {
                //Load the level from XML
                XmlDocument doc = new XmlDocument();
                FileStream stream = new FileStream(filename, FileMode.Open);
                doc.Load(stream);
                stream.Close();
                LoadFromXML(doc);

                SavePath = filename;
            }
            else
            {
                //Load the default parameters
                LoadDefault();

                SavePath = "";
            }

            changed = false;
        }

        public Level(Project project, XmlDocument xml)
        {
            this.Project = project;
            initialize();

            LoadFromXML(xml);
            SavePath = "";
            changed = false;
        }

        public Level(Level level)
            : this(level.Project, level.GenerateXML())
        {

        }

        public void CloneFrom(Level level)
        {
            LoadFromXML(level.GenerateXML());
        }

        private void initialize()
        {
            //Initialize layers
            Layers = new List<Layer>();
            foreach (var def in Project.LayerDefinitions)
                Layers.Add(def.GetInstance());

            //Initialize values
            if (Project.LevelValueDefinitions.Count > 0)
            {
                Values = new List<Value>();
                foreach (var def in Project.LevelValueDefinitions)
                    Values.Add(new Value(def));
            }
        }

        public bool Changed
        {
            get { return changed; }
            set
            {
                changed = value;
                Ogmo.MainWindow.MasterTabControl.TabPages[Ogmo.Levels.FindIndex(l => l == this)].Text = Name;
            }
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

        public string SaveName
        {
            get
            {
                string s;
                if (SavePath == "")
                    s = Ogmo.NEW_LEVEL_NAME;
                else
                    s = Path.GetFileName(SavePath);
                return s;
            }
        }

        public bool Saved
        {
            get { return SavePath != ""; }
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

            //Export the size
            XmlAttribute a;
            if (Ogmo.Project.ExportWidth)
            {
                a = doc.CreateAttribute("width");
                a.InnerText = Size.Width.ToString();
                level.Attributes.Append(a);
            }
            if (Ogmo.Project.ExportHeight)
            {
                a = doc.CreateAttribute("height");
                a.InnerText = Size.Height.ToString();
                level.Attributes.Append(a);
            }

            //Export the level values
            if (Values != null)
                foreach (var v in Values)
                    level.Attributes.Append(v.GetXML(doc));

            //Export the layers
            for (int i = 0; i < Layers.Count; i++)
                level.AppendChild(Layers[i].GetXML(doc));

            return doc;
        }

        public void LoadFromXML(XmlDocument xml)
        {
            XmlElement level = (XmlElement)xml.GetElementsByTagName("level")[0];

            //Import the size
            Size size = new Size();
            if (Ogmo.Project.ExportWidth)
                size.Width = Convert.ToInt32(level.Attributes["width"].InnerText);
            else
                size.Width = Ogmo.Project.LevelDefaultSize.Width;
            if (Ogmo.Project.ExportHeight)
                size.Height = Convert.ToInt32(level.Attributes["height"].InnerText);
            else
                size.Height = Ogmo.Project.LevelDefaultSize.Height;
            Size = size;

            //Import the level values
            if (Values != null)
                OgmoParse.ImportValues(Values, level);

            //Import the layers
            foreach (XmlElement e in level.ChildNodes)
            {
                int index = Ogmo.Project.LayerDefinitions.FindIndex(d => d.Name == e.Name);
                Layers[index].SetXML(e);
            }
        }

        public void LoadDefault()
        {
            Size = Project.LevelDefaultSize;
        }

        public void EditProperties()
        {
            Ogmo.MainWindow.DisableEditing();
            LevelProperties lp = new LevelProperties(this);
            lp.Show(Ogmo.MainWindow);
        }

        /*
         *  Saving
         */
        public bool Save()
        {
            //If it hasn't been saved before, do SaveAs instead
            if (SavePath == "")
                return SaveAs();

            writeTo(SavePath);
            return true;
        }

        public bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Project.SavedDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = SaveName;
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

            Changed = false;
        }

    }
}
