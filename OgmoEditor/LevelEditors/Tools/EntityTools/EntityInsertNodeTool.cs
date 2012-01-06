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
            : base("Insert Node", "insertNode.png")
        {

        }

        public override void OnMouseLeftClick(Point location)
        {
            Point node = LayerEditor.MouseSnapPosition;

            LevelEditor.StartBatch();
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(node))
                    LevelEditor.BatchPerform(new EntityInsertNodeAction(LayerEditor.Layer, e, node, GetIndex(e, node)));
            }
            LevelEditor.EndBatch();
        }

        public override void OnMouseRightClick(Point location)
        {
            Point node = LayerEditor.MouseSnapPosition;

            LevelEditor.StartBatch();
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled)
                {
                    int index = e.Nodes.IndexOf(node);
                    if (index != -1)
                        LevelEditor.BatchPerform(new EntityRemoveNodeAction(LayerEditor.Layer, e, index));
                }
            }
            LevelEditor.EndBatch();
        }

        public override void Draw()
        {
            Point mouse = LayerEditor.MouseSnapPosition;

            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(mouse))
                {
                    int index = GetIndex(e, mouse);

                    if (index == 0)
                        Ogmo.EditorDraw.DrawLine(e.Position, mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);
                    else
                        Ogmo.EditorDraw.DrawLine(e.Nodes[index - 1], mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);

                    if (index < e.Nodes.Count)
                        Ogmo.EditorDraw.DrawLine(e.Nodes[index], mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);

                    Ogmo.EditorDraw.DrawNode(mouse);
                }
            }
        }

        private int GetIndex(Entity entity, Point insert)
        {
            //If the entity has no nodes, insert at 0
            if (entity.Nodes.Count == 0)
                return 0;

            //If the entity has just one node, figure out whether to insert or add
            if (entity.Nodes.Count == 1)
            {
                double angleDiff = Util.AngleDifference(Util.Angle(entity.Nodes[0], insert), Util.Angle(entity.Nodes[0], entity.Position));
                if (Math.Abs(angleDiff) < Math.PI / 2)
                    return 0;
                else
                    return 1;
            }

            //Find which node is closest to the insertion point
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

            //If the closest is the entity's position, insert it as the first point
            if (closest == -1)
            {
                return 0;
            }

            //If the closest is the final node, figure out whether to insert or add
            if (closest == entity.Nodes.Count - 1)
            {
                double angleDiff = Util.AngleDifference(Util.Angle(entity.Nodes[closest], insert), Util.Angle(entity.Nodes[closest], entity.Nodes[closest - 1]));
                if (Math.Abs(angleDiff) < Math.PI / 2)
                    return closest;
                else
                    return closest + 1;
            }

            //Closest is a middle node, with nodes before and after it. Figure out which side to insert on by getting which angle is closer
            Point back = (closest == 0) ? entity.Position : entity.Nodes[closest - 1];
            return closest + GetDirection(entity.Nodes[closest], insert, back, entity.Nodes[closest + 1]);
        }

        private int GetDirection(Point at, Point insert, Point back, Point forward)
        {
            double i = Util.Angle(at, insert);
            double b = Util.Angle(at, back);
            double f = Util.Angle(at, forward);

            if (Math.Abs(Util.AngleDifference(i, b)) < Math.Abs(Util.AngleDifference(i, f)))
                return 0;
            else
                return 1;
        }
    }
}
