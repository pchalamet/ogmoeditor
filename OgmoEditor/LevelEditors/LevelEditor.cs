using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OgmoEditor.LevelEditors;
using OgmoEditor.XNA;

namespace OgmoEditor.LevelEditors
{
    public class LevelEditor : GraphicsDeviceControl
    {
        private Level level;

        public LevelEditor(Level level)
        {
            this.level = level;
            this.Size = level.Size;
        }

        protected override void Initialize()
        {

        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
        }
    }
}
