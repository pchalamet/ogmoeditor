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
            foreach (Entity e in entities)
                e.Position = new Point(e.Position.X + move.X, e.Position.Y + move.Y);
        }

        public override void Undo()
        {
            foreach (Entity e in entities)
                e.Position = new Point(e.Position.X - move.X, e.Position.Y - move.Y);
        }

        public override bool Appendable
        {
            get
            {
                return true;
            }
        }

        public override void Append(OgmoAction action)
        {
            EntityMoveAction a = action as EntityMoveAction;
            move = new Point(move.X + a.move.X, move.Y + a.move.Y);
        }
    }
}
