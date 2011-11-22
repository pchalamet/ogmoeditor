using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityMoveAction : EntityAction
    {
        private List<Entity> entities;
        private Point move;

        public EntityMoveAction(EntityLayer entityLayer, List<Entity> entities, Point move)
            : base(entityLayer)
        {
            this.entities = new List<Entity>(entities);
            this.move = move;
        }

        public override void Do()
        {
            base.Do();

            foreach (Entity e in entities)
            {
                e.Position = new Point(e.Position.X + move.X, e.Position.Y + move.Y);
                e.MoveNodes(move);
            }
        }

        public override void Undo()
        {
            base.Undo();

            foreach (Entity e in entities)
            {
                e.Position = new Point(e.Position.X - move.X, e.Position.Y - move.Y);
                e.MoveNodes(new Point(-move.X, -move.Y));
            }
        }

        /*
         *  To prevent a million EntityMoveAction instances sitting in the undo stack, the EntityMoveTool
         *  just tells one action to add a bit to its target position as the user continues to move the
         *  entities around.
         */
        public void DoAgain(Point add)
        {
            foreach (Entity e in entities)
            {
                e.Position = new Point(e.Position.X + add.X, e.Position.Y + add.Y);
                e.MoveNodes(add);
            }
            move = new Point(move.X + add.X, move.Y + add.Y);
        }
    }
}
