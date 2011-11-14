using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityAddNodeAction : EntityAction
    {
        private Entity entity;
        private Point node;
        private Point? removed;

        public EntityAddNodeAction(EntityLayer entityLayer, Entity entity, Point node)
            : base(entityLayer)
        {
            this.entity = entity;
            this.node = node;
        }

        public override void Do()
        {
            if (entity.Nodes.Count == entity.Definition.NodesDefinition.Limit)
            {
                removed = entity.Nodes[0];
                entity.Nodes.RemoveAt(0);
            }
            else
                removed = null;

            entity.Nodes.Add(node);
        }

        public override void Undo()
        {
            entity.Nodes.RemoveAt(entity.Nodes.Count - 1);
            if (removed.HasValue)
                entity.Nodes.Insert(0, removed.Value);
        }
    }
}
