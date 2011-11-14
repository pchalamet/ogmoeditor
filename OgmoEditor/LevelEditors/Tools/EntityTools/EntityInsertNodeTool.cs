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

        }

        private int GetIndex(Entity entity, Point position)
        {
            return -1;
        }
    }
}
