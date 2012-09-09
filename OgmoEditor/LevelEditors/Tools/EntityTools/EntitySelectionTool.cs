using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntitySelectionTool : EntityTool
    {
        private Point mouseStart;
        private bool drawing;

        public EntitySelectionTool()
            : base("Select", "selection.png")
        {

        }

        public override void NewDraw(Graphics graphics)
        {
            if (drawing)
            {
                int x = Math.Min(mouseStart.X, LevelEditor.MousePosition.X);
                int y = Math.Min(mouseStart.Y, LevelEditor.MousePosition.Y);
                int w = Math.Max(mouseStart.X, LevelEditor.MousePosition.X) - x;
                int h = Math.Max(mouseStart.Y, LevelEditor.MousePosition.Y) - y;

                Ogmo.NewEditorDraw.DrawSelectionRectangle(graphics, new Rectangle(x, y, w, h));
            }
        }

        public override void Draw()
        {
            if (drawing)
            {
                int x = Math.Min(mouseStart.X, LevelEditor.MousePosition.X);
                int y = Math.Min(mouseStart.Y, LevelEditor.MousePosition.Y);
                int w = Math.Max(mouseStart.X, LevelEditor.MousePosition.X) - x;
                int h = Math.Max(mouseStart.Y, LevelEditor.MousePosition.Y) - y;

                Ogmo.EditorDraw.DrawFillRect(x, y, w, h, Microsoft.Xna.Framework.Color.Yellow);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawing = true;
            mouseStart = location;
        }

        public override void OnMouseRightDown(Point location)
        {
            Ogmo.EntitySelectionWindow.ClearSelection();
        }

        public override void OnMouseLeftUp(Point location)
        {
            drawing = false;

            int x = Math.Min(mouseStart.X, LevelEditor.MousePosition.X);
            int y = Math.Min(mouseStart.Y, LevelEditor.MousePosition.Y);
            int w = Math.Max(mouseStart.X, LevelEditor.MousePosition.X) - x;
            int h = Math.Max(mouseStart.Y, LevelEditor.MousePosition.Y) - y;
            Rectangle r = new Rectangle(x, y, w, h);

            List<Entity> hit = LayerEditor.Layer.Entities.FindAll(e => e.Bounds.IntersectsWith(r));

            if (Util.Ctrl)
                Ogmo.EntitySelectionWindow.ToggleSelection(hit);
            else
                Ogmo.EntitySelectionWindow.SetSelection(hit);
        }
    }
}
