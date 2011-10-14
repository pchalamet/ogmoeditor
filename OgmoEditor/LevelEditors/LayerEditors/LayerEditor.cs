using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using OgmoEditor.LevelEditors.Actions;
using OgmoEditor.LevelEditors.Tools;  

namespace OgmoEditor.LevelEditors.LayerEditors
{
    using Point = System.Drawing.Point;

    public class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
        }

        public virtual void Draw(SpriteBatch spriteBatch, float alpha)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.Draw(spriteBatch);
        }

        public void OnKeyDown(Keys key)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnKeyDown(key);
        }

        public void OnKeyUp(Keys key)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnKeyUp(key);
        }

        public void OnMouseLeftClick(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseLeftClick(location);
        }

        public void OnMouseLeftDown(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseLeftDown(location);
        }

        public void OnMouseLeftUp(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseLeftUp(location);
        }

        public void OnMouseRightClick(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseRightClick(location);
        }

        public void OnMouseRightDown(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseRightDown(location);
        }

        public void OnMouseRightUp(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseRightUp(location);
        }

        public void OnMouseMove(Point location)
        {
            if (Ogmo.CurrentTool != null)
                Ogmo.CurrentTool.OnMouseMove(location);
        }
    }
}
