using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;

namespace OgmoEditor
{
    public class Level
    {
        //Running instance variables
        private Project project;
        public string SavePath;
        public TreeNode TreeNode;
        public bool Changed { get; private set; }

        //Actual parameters to be edited/exported
        public Size Size { get; private set; }

        public Level(Project project, string filename)
        {
            this.project = project;

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

            InitializeRunningVars();
        }

        public Level(Project project, XmlDocument xml)
        {
            this.project = project;

            LoadFromXML(xml);
            SavePath = "";

            InitializeRunningVars();
        }

        private void InitializeRunningVars()
        {
            TreeNode = new TreeNode();
            RemoveChanged();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red, 1);
            g.DrawLine(p, 50, 50, 200, 200);
        }

        public string Name
        {
            get
            {
                if (SavePath == "")
                    return Ogmo.NEW_LEVEL_NAME;
                else
                    return Path.GetFileName(SavePath);
            }
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

            RemoveChanged();
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

            RemoveChanged();

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

        /*
         *  Changed system
         */
        private void RemoveChanged()
        {
            Changed = false;
            if (SavePath == "")
                TreeNode.Text = Ogmo.NEW_LEVEL_NAME;
            else
                TreeNode.Text = Path.GetFileName(SavePath);
        }

        public void SetChanged()
        {
            Changed = true;
            TreeNode.Text = Path.GetFileName(SavePath) + '*';
        }

    }
}
