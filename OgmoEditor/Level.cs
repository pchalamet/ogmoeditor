using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace OgmoEditor
{
    public class Level : Panel
    {
        private Project project;
        public string SavePath;
        public TreeNode TreeNode;
        public bool Changed { get; private set; }

        public Level(Project project, string filename)
            : base()
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
            : base()
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
            Paint += new PaintEventHandler(levelPaint);
        }

        private void levelPaint(object sender, PaintEventArgs e)
        {

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

        public void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = project.WorkingDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Path.GetFileName(SavePath);
            dialog.DefaultExt = Ogmo.LEVEL_EXT;
            dialog.OverwritePrompt = true;
            dialog.Filter = Ogmo.LEVEL_FILTER;

            //Handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            SavePath = dialog.FileName;
            writeTo(dialog.FileName);

            RemoveChanged();
        }

        private void writeTo(string filename)
        {
            //Generate the XML and write it!            
            XmlDocument doc = GenerateXML();
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate);
            XmlWriter writer = XmlWriter.Create(stream);
            doc.WriteTo(writer);
            stream.Close();
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
