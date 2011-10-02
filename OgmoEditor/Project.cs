using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace OgmoEditor
{
    [Serializable()]
    public class Project
    {
        public string Name;
        public string WorkingDirectory;
        public string LastFilename;

        [NonSerialized()]
        public bool Changed = false;

        public Project()
        {
            Name = "New Project";
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LastFilename = "";
        }

        public void Save()
        {
            //If it hasn't been saved yet, go to SaveAs
            if (LastFilename == "")
            {
                SaveAs();
                return;
            }

            Changed = false;
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

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            LastFilename = dialog.FileName;

            Changed = false;
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = dialog.OpenFile();
            bf.Serialize(s, this);
            s.Close();
        }
    }
}
