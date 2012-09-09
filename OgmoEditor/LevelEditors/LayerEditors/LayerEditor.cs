﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Windows.Forms;
using System.Diagnostics;
using OgmoEditor.LevelEditors.Actions;
using OgmoEditor.LevelEditors.Tools;
using OgmoEditor.LevelEditors.Resizers;
using System.Drawing; 

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public abstract class LayerEditor
    {
        public const int LAYER_ABOVE_ALPHA = 255/2;

        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }
        public Point MouseSnapPosition { get; private set; }
        public PointF DrawOffset { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
        }

        public void UpdateDrawOffset(Point cameraPos)
        {
            DrawOffset = new PointF(cameraPos.X - cameraPos.X * Layer.Definition.ScrollFactor.X, cameraPos.Y - cameraPos.Y * Layer.Definition.ScrollFactor.Y);
        }

        public void InternalDraw(Graphics graphics, bool current, bool fullAlpha)
        {
            graphics.TranslateTransform(DrawOffset.X, DrawOffset.Y);
            NewDraw(graphics, current, fullAlpha);
            graphics.TranslateTransform(-DrawOffset.X, -DrawOffset.Y);

            if (current && Ogmo.ToolsWindow.CurrentTool != null)
                Ogmo.ToolsWindow.CurrentTool.NewDraw(graphics);
        }

        public abstract void NewDraw(Graphics graphics, bool current, bool fullAlpha);

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
