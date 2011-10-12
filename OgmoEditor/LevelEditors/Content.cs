using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;

namespace OgmoEditor.LevelEditors
{
    public class Content
    {
        public Texture2D TexBG { get; private set; }
        public Texture2D TexLogo { get; private set; }

        private GraphicsDevice device;

        public Content(GraphicsDevice device)
        {
            this.device = device;

            TexBG = Read("bg.png");
            TexLogo = Read("logo.png");
        }

        private Texture2D Read(string filename)
        {
            FileStream s = new FileStream(Path.Combine(Ogmo.ProgramDirectory, "Content", filename), FileMode.Open);
            Texture2D tex = Texture2D.FromStream(device, s);
            s.Close();

            return tex;
        }
    }
}
