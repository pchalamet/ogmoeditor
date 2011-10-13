using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OgmoEditor.LevelEditors.LayerEditors.Tools
{
    public abstract class Tool
    {
        public string Name { get; private set; }

        public Tool(string name)
        {
            Name = name;
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
