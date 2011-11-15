using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridLineTool : GridTool
    {
        private bool drawing;
        private bool drawMode;
        private Point drawStart;

        public GridLineTool()
            : base("Line", "line.png", System.Windows.Forms.Keys.L)
        {

        }
    }
}
