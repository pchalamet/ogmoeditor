using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityInsertNodeTool : EntityTool
    {
        public EntityInsertNodeTool()
            : base("Insert Node", "insertNode.png", System.Windows.Forms.Keys.I)
        {
            public EntityInsertNodeAction(EntityLayer entityLayer, Entity entity, Point node)
            : base(entityLayer)
        {
            this.entity = entity;
            this.node = node;
        }
        }
    }
}
