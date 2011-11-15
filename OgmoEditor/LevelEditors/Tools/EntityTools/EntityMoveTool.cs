using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityMoveTool : EntityTool
    {
        private bool moving;
        private Point mouseStart;
        private Point moved;

        public EntityMoveTool()
            : base("Move", "move.png", System.Windows.Forms.Keys.M)
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
                LevelEditor.StartBatch();
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
                    LevelEditor.BatchPerform(new EntityMoveAction(LayerEditor.Layer, Ogmo.EntitySelectionWindow.Selected, move));
                    moved = new Point(move.X + moved.X, move.Y + moved.Y);
                    Ogmo.EntitySelectionWindow.RefreshContents();
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
