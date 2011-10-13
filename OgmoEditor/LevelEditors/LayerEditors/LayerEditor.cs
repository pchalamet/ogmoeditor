using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    using Point = System.Drawing.Point;
using OgmoEditor.LevelEditors.LayerEditors.Actions;
    using OgmoEditor.LevelEditors.LayerEditors.Tools;  

    public class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }
        public Tool Tool { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public void OnKeyDown(Keys key)
        {
            if (Tool != null)
                Tool.OnKeyDown(key);
        }

        public void OnKeyUp(Keys key)
        {
            if (Tool != null)
                Tool.OnKeyUp(key);
        }

        public void OnMouseLeftClick(Point location)
        {
            if (Tool != null)
                Tool.OnMouseLeftClick(location);
        }

        public void OnMouseLeftDown(Point location)
        {
            if (Tool != null)
                Tool.OnMouseLeftDown(location);
        }

        public void OnMouseLeftUp(Point location)
        {
            if (Tool != null)
                Tool.OnMouseLeftUp(location);
        }

        public void OnMouseRightClick(Point location)
        {
            if (Tool != null)
                Tool.OnMouseRightClick(location);
        }

        public void OnMouseRightDown(Point location)
        {
            if (Tool != null)
                Tool.OnMouseRightDown(location);
        }

        public void OnMouseRightUp(Point location)
        {
            if (Tool != null)
                Tool.OnMouseRightUp(location);
        }

        public void OnMouseMove(Point location)
        {
            if (Tool != null)
                Tool.OnMouseMove(location);
        }
    }
}
