using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityInsertNodeAction : EntityAction
    {
        private Entity entity;
        private Point node;
        private int index;
        private Point? removed;

        public EntityInsertNodeAction(EntityLayer entityLayer, Entity entity, Point node, int index)
            : base(entityLayer)
        {
            this.entity = entity;
            this.node = node;
            this.index = index;
        }

        public override void Do()
        {
            entity.Nodes.Insert(index, node);

            if (entity.Nodes.Count == entity.Definition.NodesDefinition.Limit + 1)
            {
                removed = entity.Nodes[0];
                entity.Nodes.RemoveAt(0);
            }
            else
                removed = null;
        }

        public override void Undo()
        {
            entity.Nodes.RemoveAt(index);

            if (removed.HasValue)
                entity.Nodes.Insert(0, removed.Value);
        }
    }
}
