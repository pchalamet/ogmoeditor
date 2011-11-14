using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.EntityActions;
using System.Drawing;
using System.Diagnostics;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityAddNodeTool : EntityTool
    {
        private Point mouse;

        public EntityAddNodeTool()
            : base("Add Node", "addNode.png", System.Windows.Forms.Keys.N)
        {

        }

        public override void OnMouseMove(Point location)
        {
            mouse = LayerEditor.Layer.Definition.SnapToGrid(location);
        }

        public override void OnMouseLeftClick(Point location)
        {
            Point node = LayerEditor.Layer.Definition.SnapToGrid(location);
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(node))
                    LevelEditor.BatchPerform(this, new EntityAddNodeAction(LayerEditor.Layer, e, node));
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

        public override void Draw(Content content)
        {
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(mouse))
                {
                    if (e.Nodes.Count == 0)
                        content.DrawLine(e.Position, mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);
                    else
                        content.DrawLine(e.Nodes[e.Nodes.Count - 1], mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);
                    content.DrawNode(mouse);
                }
            }
        }
    }
}
