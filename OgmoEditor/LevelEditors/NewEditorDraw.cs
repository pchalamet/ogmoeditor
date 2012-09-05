using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace OgmoEditor.LevelEditors
{
    public class NewEditorDraw
    {
        public Bitmap ImgBG;
        public Bitmap ImgLogo;

        public NewEditorDraw()
        {
            ImgBG = new Bitmap(BuildPath("bg.png"));
            ImgLogo = new Bitmap(BuildPath("logo.png"));
        }

        private string BuildPath(string filename)
        {
            return Path.Combine(Ogmo.ProgramDirectory, "Content", filename);
        }
    }
}
