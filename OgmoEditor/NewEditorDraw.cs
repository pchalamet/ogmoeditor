using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.Definitions;

namespace OgmoEditor
{
    static public class DrawUtil
    {
        static public Bitmap ImgBG;
        static public Bitmap ImgLogo;
        static public Bitmap ImgBroken;

        static private Pen highlightPen;
        static private Pen dashPen;
        static private SolidBrush nodeBrush;
        static public Pen NodePathPen { get; private set; }
        static public Pen NodeNewPathPen { get; private set; }
        static public Pen CameraRectPen { get; private set; }

        static public void Initialize()
        {
            ImgBG = new Bitmap(BuildPath("bg.png"));
            ImgLogo = new Bitmap(BuildPath("logo.png"));
            ImgBroken = new Bitmap(BuildPath("broken.png"));

            //Pens and brushes
            highlightPen = new Pen(Color.Yellow, 2);
            dashPen = new Pen(Color.Teal);
            dashPen.DashPattern = new float[] { 6, 2 };
            nodeBrush = new SolidBrush(Color.Yellow);
            NodePathPen = new Pen(Color.Yellow, 1);
            NodePathPen.DashPattern = new float[] { 3, 1 };
            NodeNewPathPen = new Pen(Color.FromArgb(255 / 2, Color.Yellow), 1);
            NodeNewPathPen.DashPattern = new float[] { 3, 1 };
            CameraRectPen = new Pen(Color.FromArgb(255 / 2, Color.Red), 2);
            CameraRectPen.DashPattern = new float[] { 4, 2 };

            //Updates the selection box brush
            Application.Idle += new EventHandler(Application_Idle);
        }

        static private void Application_Idle(object sender, EventArgs e)
        {
            dashPen.DashOffset -= .35f;
        }

        static private string BuildPath(string filename)
        {
            return Path.Combine(Ogmo.ProgramDirectory, "Content", filename);
        }

        static public void DrawSelectionRectangle(Graphics graphics, Rectangle rectangle)
        {
            graphics.DrawRectangle(highlightPen, rectangle);
            graphics.DrawRectangle(dashPen, rectangle);
        }

        static public void DrawNode(Graphics graphics, Point point)
        {
            graphics.FillEllipse(nodeBrush, new Rectangle(point.X - 2, point.Y - 2, 4, 4));
        }
    }
}
