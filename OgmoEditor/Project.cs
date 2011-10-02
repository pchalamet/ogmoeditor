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

        [NonSerialized()]
        public bool Changed = false;

        public Project()
        {
            Name = "New Project";
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public void Save(string filename)
        {
            Changed = false;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(new FileStream(filename, FileMode.OpenOrCreate), this);
        }
    }
}
