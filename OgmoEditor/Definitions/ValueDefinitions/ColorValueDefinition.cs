using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class ColorValueDefinition : ValueDefinition
    {
        public OgmoColor Default;

        public ColorValueDefinition()
            : base()
        {
            Default = new OgmoColor(255, 255, 255);
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new ColorValueEditor(this);
        }

        public override string ErrorCheck()
        {
            string s = base.ErrorCheck();
            return s;
        }

        public override ValueDefinition Clone()
        {
            ColorValueDefinition def = new ColorValueDefinition();
            def.Name = Name;
            def.Default = Default;
            return def;
        }
    }
}
