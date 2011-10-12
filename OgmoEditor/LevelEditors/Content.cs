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

        public void Load(GraphicsDevice device)
        {
            FileStream s;

            s = new FileStream(Path.Combine(Ogmo.ProgramDirectory, @"Content\bg.png"), FileMode.Open);
            TexBG = Texture2D.FromStream(device, s);
            s.Close();
        }
    }
}
