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
    using OgmoEditor.LevelEditors.Resizers;

    public abstract class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }
        public Point MouseSnapPosition { get; private set; }
        public Vector2 DrawOffset { get; private set; }
        public Matrix DrawMatrix { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
            DrawMatrix = Matrix.Identity;
        }

        public void UpdateDrawOffset(Point cameraPos)
        {
            DrawOffset = new Vector2(cameraPos.X - cameraPos.X * Layer.Definition.ScrollFactor.X, cameraPos.Y - cameraPos.Y * Layer.Definition.ScrollFactor.Y);
            DrawMatrix = Matrix.CreateTranslation(DrawOffset.X, DrawOffset.Y, 0);
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
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftClick(location);
            }
        }

        public void OnMouseLeftDown(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftDown(location);
            }
        }

        public void OnMouseLeftUp(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseLeftUp(location);
            }
        }

        public void OnMouseRightClick(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightClick(location);
            }
        }

        public void OnMouseRightDown(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightDown(location);
            }
        }

        public void OnMouseRightUp(Point location)
        {
            if (Ogmo.ToolsWindow.CurrentTool != null)
            {
                location.X -= (int)DrawOffset.X;
                location.Y -= (int)DrawOffset.Y;
                Ogmo.ToolsWindow.CurrentTool.OnMouseRightUp(location);
            }
        }

        public void OnMouseMove(Point location)
        {
            location.X -= (int)DrawOffset.X;
            location.Y -= (int)DrawOffset.Y;

            MouseSnapPosition = Layer.Definition.SnapToGrid(LevelEditor.MousePosition);

            if (Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.OnMouseMove(location);
        }

        public virtual Resizer GetResizer() { return null; }

        public virtual bool CanCopyOrCut { get { return false; } }
        public virtual void Copy() { }
        public virtual void Cut() { }

        public virtual bool CanSelectAll { get { return false; } }
        public virtual void SelectAll() { }

        public virtual bool CanDeselect { get { return false; } }
        public virtual void Deselect() { }
    }
}
