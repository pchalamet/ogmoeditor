using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityAngleSnapAction : EntityAction
    {
        private List<Entity> entities;
        private List<float> was;

        public EntityAngleSnapAction(EntityLayer entityLayer, List<Entity> entities)
            : base(entityLayer)
        {
            this.entities = new List<Entity>(entities);
            was = new List<float>();
            for (int i = 0; i < entities.Count; i++)
                was.Add(0);
        }

        public override void Do()
        {
            base.Do();

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Definition.Rotatable)
                {
                    was[i] = entities[i].Angle;
                    float inc = entities[i].Definition.RotateIncrement * Util.DEGTORAD;
                    entities[i].Angle = (float)Math.Round(entities[i].Angle / inc) * inc;
                }
            }
        }

        public override void Undo()
        {
            base.Undo();

            for (int i = 0; i < entities.Count; i++)
                entities[i].Angle = was[i];
        }
    }
}
