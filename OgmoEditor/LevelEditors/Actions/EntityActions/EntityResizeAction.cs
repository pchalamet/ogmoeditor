using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityResizeAction : EntityAction
    {
        private List<Entity> entities;
        private Point resize;

        public EntityResizeAction(EntityLayer entityLayer, List<Entity> entities, Point resize)
            : base(entityLayer)
        {
            this.entities = entities;
            this.resize = resize;
        }
    }
}
