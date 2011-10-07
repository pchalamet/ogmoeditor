using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class ColorValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public Color Default;
        [XmlAttribute]
        public bool ExportAlphaChannel;

        public ColorValueDefinition()
            : base()
        {
            Default = new Color();
            ExportAlphaChannel = true;
        }
    }
}
