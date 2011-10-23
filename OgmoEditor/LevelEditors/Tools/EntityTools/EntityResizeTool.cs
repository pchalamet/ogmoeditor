﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityResizeTool : EntityTool
    {
        private bool moving;
        private Point mouseStart;
        private Point moved;

        public EntityResizeTool()
            : base("Resize", "resize.png", System.Windows.Forms.Keys.R)
        {
            moving = false;
        }

        public override void OnMouseLeftDown(Point location)
        {
            if (Ogmo.EntitySelectionWindow.Selected.Count > 0)
            {
                moving = true;
                mouseStart = location;
                moved = Point.Empty;
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (moving)
            {
                Point move = LayerEditor.Layer.Definition.SnapToGrid(new Point(location.X - mouseStart.X, location.Y - mouseStart.Y));
                move = new Point(move.X - moved.X, move.Y - moved.Y);
                if (move.X != 0 || move.Y != 0)
                {
                    LevelEditor.BatchPerform(this, new EntityResizeAction(LayerEditor.Layer, Ogmo.EntitySelectionWindow.Selected, new Size(move.X, move.Y)));
                    moved = new Point(move.X + moved.X, move.Y + moved.Y);
                }
            }
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (moving)
            {
                LevelEditor.EndBatch();
                moving = false;
            }
        }
    }
}
