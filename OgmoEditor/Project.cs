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

namespace OgmoEditor
{
    [Serializable()]
    public class Project : ISerializable
    {
        public string Name;
        public string WorkingDirectory;
        public string LastFilename;
        public List<LayerDefinition> LayerDefinitions;

        public bool Changed = false;

        public Project()
        {
            Name = "New Project";
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LastFilename = "";
            LayerDefinitions = new List<LayerDefinition>();
        }

        /*
         *  Serialization stuff
         */
        public Project(SerializationInfo info, StreamingContext ctxt)
        {
            Name = info.GetString("Name");
            WorkingDirectory = info.GetString("WorkingDirectory");
            LastFilename = info.GetString("LastFilename");
            LayerDefinitions = (List<LayerDefinition>)info.GetValue("LayerDefinitions", typeof(List<LayerDefinition>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Name", Name);
            info.AddValue("WorkingDirectory", WorkingDirectory);
            info.AddValue("LastFilename", LastFilename);
            info.AddValue("LayerDefinitions", LayerDefinitions);
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
