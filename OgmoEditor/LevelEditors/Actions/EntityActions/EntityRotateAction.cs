using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityRotateAction : EntityAction
    {
        private List<Entity> entities;
        private float rotate;
        private List<float> was;

        public EntityRotateAction(EntityLayer entityLayer, List<Entity> entities, float rotate)
            : base(entityLayer)
        {
            this.entities = new List<Entity>(entities);
            this.rotate = rotate;
            was = new List<float>();
            for (int i = 0; i < entities.Count; i++)
                was.Add(0);
        }

        public override void Do()
        {
            base.Do();

            for (int i = 0; i < entities.Count; i++)
            {
                was[i] = entities[i].Angle;
                if (entities[i].Definition.Rotatable)
                    entities[i].Angle = (entities[i].Angle + rotate) % ((float)Math.PI * 2);
            }
        }

        public override void Undo()
        {
            base.Undo();

            for (int i = 0; i < entities.Count; i++)
                entities[i].Angle = was[i];
        }

        public void DoAgain(float add)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Definition.Rotatable)
                    entities[i].Angle = (entities[i].Angle + add) % ((float)Math.PI * 2);
            }
        }
    }
}
