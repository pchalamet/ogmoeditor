using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityRotateTool : EntityTool
    {
        private const float MOVE_FACTOR = Util.DEGTORAD;

        private bool moving;
        private int mouseStart;
        private int moved;

        public EntityRotateTool()
            : base("Rotate", "rotate.png", System.Windows.Forms.Keys.O)
        {

        }

        public override void OnMouseLeftDown(Point location)
        {
            if (Ogmo.EntitySelectionWindow.Selected.Count > 0)
            {
                moving = true;
                mouseStart = location.X;
                moved = 0;
                LevelEditor.StartBatch();
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (moving)
            {
                int move = location.X - mouseStart;
                move -= moved;
                if (move != 0)
                {
                    LevelEditor.BatchPerform(new EntityRotateAction(LayerEditor.Layer, Ogmo.EntitySelectionWindow.Selected, (float)move * MOVE_FACTOR));
                    moved = move + moved;
                    Ogmo.EntitySelectionWindow.RefreshContents();
                }
            }
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (moving)
            {
                LevelEditor.BatchPerform(new EntityAngleSnapAction(LayerEditor.Layer, Ogmo.EntitySelectionWindow.Selected));
                LevelEditor.EndBatch();
                moving = false;
            }
        }
    }
}
