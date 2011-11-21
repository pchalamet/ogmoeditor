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

    public abstract class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }
        public Point MouseSnapPosition { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
        }

        public virtual void Draw(Content content, bool current, float alpha)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.Draw(content);
        }

        public virtual void OnKeyDown(Keys key)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnKeyDown(key);
        }

        public void OnKeyUp(Keys key)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnKeyUp(key);
        }

        public void OnMouseLeftClick(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftClick(location);
        }

        public void OnMouseLeftDown(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftDown(location);
        }

        public void OnMouseLeftUp(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftUp(location);
        }

        public void OnMouseRightClick(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightClick(location);
        }

        public void OnMouseRightDown(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightDown(location);
        }

        public void OnMouseRightUp(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightUp(location);
        }

        public void OnMouseMove(Point location)
        {
            MouseSnapPosition = Layer.Definition.SnapToGrid(LevelEditor.MousePosition);

            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseMove(location);
        }

        public virtual bool CanCopyOrCut { get { return false; } }
        public virtual void Copy() { }
        public virtual void Cut() { }

        public virtual bool CanSelectAll { get { return false; } }
        public virtual void SelectAll() { }

        public virtual bool CanDeselect { get { return false; } }
        public virtual void Deselect() { }
    }
}
