﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor.LevelEditors.Tools
{
    public abstract class Tool
    {
        public string Name { get; private set; }
        public string Image { get; private set; }
        public Keys Hotkey { get; private set; }

        public Tool(string name, string image, Keys hotkey)
        {
            Name = name;
            Image = image;
            Hotkey = hotkey;
        }

        public virtual void SwitchTo()
        {

        }

        public virtual void Draw(Content content)
        {

        }

        public virtual void OnKeyDown(Keys key)
        {

        }

        public virtual void OnKeyUp(Keys key)
        {

        }

        public virtual void OnMouseLeftClick(Point location)
        {

        }

        public virtual void OnMouseLeftDown(Point location)
        {

        }

        public virtual void OnMouseLeftUp(Point location)
        {

        }

        public virtual void OnMouseRightClick(Point location)
        {

        }

        public virtual void OnMouseRightDown(Point location)
        {

        }

        public virtual void OnMouseRightUp(Point location)
        {

        }

        public virtual void OnMouseMove(Point location)
        {

        }

        public LevelEditor LevelEditor
        {
            get { return Ogmo.MainWindow.LevelEditors[Ogmo.CurrentLevelIndex]; }
        }
    }
}
