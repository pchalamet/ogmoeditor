using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityInsertNodeTool : EntityTool
    {
        public EntityInsertNodeTool()
            : base("Insert Node", "insertNode.png", System.Windows.Forms.Keys.I)
        {

        }

        public override void OnMouseLeftClick(Point location)
        {
            Point node = LayerEditor.Layer.Definition.SnapToGrid(location);
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && !e.Nodes.Contains(node))
                    LevelEditor.BatchPerform(this, new EntityInsertNodeAction(LayerEditor.Layer, e, node, GetIndex(e, node)));
            }
            LevelEditor.EndBatch();
        }

        public override void OnMouseRightClick(Point location)
        {
            Point node = LayerEditor.Layer.Definition.SnapToGrid(location);
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled)
                {
                    int index = e.Nodes.IndexOf(node);
                    if (index != -1)
                        LevelEditor.BatchPerform(this, new EntityRemoveNodeAction(LayerEditor.Layer, e, index));
                }
            }
            LevelEditor.EndBatch();
        }

        private int GetIndex(Entity entity, Point insert)
        {
            int closest = -1;
            int dist = Util.DistanceSquared(entity.Position, insert);
            for (int i = 0; i < entity.Nodes.Count; i++)
            {
                int temp = Util.DistanceSquared(entity.Nodes[i], insert);
                if (temp < dist)
                {
                    dist = temp;
                    closest = i;
                }
            }

            return closest + 1;
        }


    }
}
