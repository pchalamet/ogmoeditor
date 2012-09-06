using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.Definitions;

namespace OgmoEditor.LevelEditors
{
    public class NewEditorDraw
    {
        public Bitmap ImgBG;
        public Bitmap ImgLogo;

        private Pen highlightPen;
        private Pen dashPen;

        //Project images
        public Dictionary<EntityDefinition, Bitmap> EntityImages { get; private set; }
        public Dictionary<Tileset, Bitmap> TilesetImages { get; private set; }

        public NewEditorDraw()
        {
            ImgBG = new Bitmap(BuildPath("bg.png"));
            ImgLogo = new Bitmap(BuildPath("logo.png"));

            EntityImages = new Dictionary<EntityDefinition, Bitmap>();
            TilesetImages = new Dictionary<Tileset, Bitmap>();

            highlightPen = new Pen(Color.Yellow, 3);
            dashPen = new Pen(Color.Teal);
            dashPen.DashPattern = new float[] { 6, 2 };

            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            dashPen.DashOffset -= 1f;
        }

        private string BuildPath(string filename)
        {
            return Path.Combine(Ogmo.ProgramDirectory, "Content", filename);
        }

        public void DrawSelectionRectangle(Graphics graphics, Rectangle rectangle)
        {
            graphics.DrawRectangle(highlightPen, rectangle);
            graphics.DrawRectangle(dashPen, rectangle);
        }
    }
}
