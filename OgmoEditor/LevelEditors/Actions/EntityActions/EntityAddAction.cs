using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityAddAction : EntityAction
    {
        private Entity added;
        private Entity removed;

        public EntityAddAction(EntityLayer entityLayer, Entity entity)
            : base(entityLayer)
        {
            added = entity;
        }

        public override void Do()
        {
            //Enforce entity limit defined by the entity definition
            if (Ogmo.ObjectsWindow.CurrentEntity.Limit > 0 && EntityLayer.Entities.Count(e => e.Definition == Ogmo.ObjectsWindow.CurrentEntity) == Ogmo.ObjectsWindow.CurrentEntity.Limit)
                EntityLayer.Entities.Remove(removed = EntityLayer.Entities.Find(e => e.Definition == Ogmo.ObjectsWindow.CurrentEntity));

            //Place the entity
            EntityLayer.Entities.Add(added);
        }

        public override void Undo()
        {
            //Remove the entity
            EntityLayer.Entities.Remove(added);

            //Re-add the one removed due to an entity limit
            if (removed != null)
                EntityLayer.Entities.Add(removed);
        }
    }
}
