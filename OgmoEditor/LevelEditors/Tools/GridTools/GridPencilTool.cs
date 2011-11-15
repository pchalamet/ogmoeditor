using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.GridActions;
using System.Diagnostics;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridPencilTool : GridTool
    {
        private bool drawing;
        private bool drawMode;

        public GridPencilTool()
            : base("Pencil", "pencil.png", System.Windows.Forms.Keys.P)
        {
            drawing = false;
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawMode = true;
                LevelEditor.StartBatch();
                setCell(location, true);
            }
        }

        public override void OnMouseRightDown(System.Drawing.Point location)
        {
            if (!drawing)
            {
                drawing = true;
                drawMode = false;
                LevelEditor.StartBatch();
                setCell(location, false);
            }
        }

        public override void OnMouseLeftUp(System.Drawing.Point location)
        {
            if (drawing && drawMode)
            {
                drawing = false;
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseRightUp(System.Drawing.Point location)
        {
            if (drawing && !drawMode)
            {
                drawing = false;
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseMove(System.Drawing.Point location)
        {
            if (drawing)
                setCell(location, drawMode);
        }

        private void setCell(System.Drawing.Point location, bool setTo)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            LevelEditor.BatchPerform(new GridDrawAction(LayerEditor.Layer, location.X, location.Y, drawMode));
        }
    }
}
