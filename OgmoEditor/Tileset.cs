using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace OgmoEditor
{
    public class Tileset
    {
        public string FilePath;

        [XmlIgnore]
        public Bitmap Bitmap;

        public Tileset()
        {

        }

        public void Load()
        {
            Bitmap = (Bitmap)Bitmap.FromFile(Ogmo.Project.GetPath(FilePath));
        }
    }
}
