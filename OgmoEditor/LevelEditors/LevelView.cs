using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using OgmoEditor.LevelData;

namespace OgmoEditor.LevelEditors
{
    public class LevelView
    {
        static public readonly Matrix Identity = new Matrix();
        static private readonly float[] ZOOMS = new float[] { .25f, .33f, .5f, .66f, 1, 1.25f, 1.5f, 2, 2.5f, 3 };

        public LevelEditor LevelEditor { get; private set; }
        public Matrix Matrix { get; private set; }
        public Matrix Inverse { get; private set; }
        public float Zoom { get; private set; }

        private Size oldLevelSize;

        public LevelView(LevelEditor levelEditor)
        {
            LevelEditor = levelEditor;
            Matrix = new Matrix();
            Inverse = new Matrix();
            Zoom = 1;

            Center();
        }

        public void OnParentResized()
        {
            Pan(new Point((LevelEditor.Size.Width - oldLevelSize.Width) / 2, (LevelEditor.Size.Height - oldLevelSize.Height) / 2));
            oldLevelSize = LevelEditor.Size;
        }

        //Transforming points back and forth
        public PointF ScreenToEditor(PointF screenPos)
        {
            PointF[] points = new PointF[] { screenPos };
            Inverse.TransformPoints(points);
            return points[0];
        }

        public Point ScreenToEditor(Point screenPos)
        {
            Point[] points = new Point[] { screenPos };
            Inverse.TransformPoints(points);
            return points[0];
        }

        public PointF EditorToScreen(PointF editorPos)
        {
            PointF[] points = new PointF[] { editorPos };
            Matrix.TransformPoints(points);
            return points[0];
        }

        public Point EditorToScreen(Point editorPos)
        {
            Point[] points = new Point[] { editorPos };
            Matrix.TransformPoints(points);
            return points[0];
        }

        //Transformations
        public void Pan(PointF by)
        {
            PointF[] p = new PointF[] { by };
            Inverse.TransformVectors(p);
            by = p[0];

            Matrix.Translate(by.X, by.Y);
            UpdateInverse();
        }

        public void PanTo(PointF to)
        {
            PointF at = EditorToScreen(PointF.Empty);
            PointF translate = new PointF(to.X - at.X, to.Y - at.Y);
            translate = ScreenToEditor(translate);
            Pan(translate);
        }

        public void Center()
        {
            PanTo(new PointF(LevelEditor.Width / 2 - LevelEditor.Level.Size.Width / 2, LevelEditor.Height / 2 - LevelEditor.Level.Size.Height / 2));
        }

        public void CenterOn(PointF on)
        {
            PanTo(new PointF(LevelEditor.Width / 2 - on.X, LevelEditor.Height / 2 - on.Y));
        }

        private int GetZoomIndex()
        {
            for (int i = 0; i < ZOOMS.Length; i++)
                if (ZOOMS[i] == Zoom)
                    return i;
            throw new Exception("Zoom exception!");
        }

        public void ZoomIn()
        {
            int at = GetZoomIndex();
            if (at == ZOOMS.Length - 1)
                return;

            Zoom = ZOOMS[at + 1];
            float scale = ZOOMS[at + 1] / ZOOMS[at];

            Matrix.Scale(scale, scale);
            UpdateInverse();
        }

        public void ZoomIn(PointF mouseAt)
        {
            ZoomIn();
        }

        public void ZoomOut()
        {
            int at = GetZoomIndex();
            if (at == 0)
                return;

            Zoom = ZOOMS[at - 1];
            float scale = ZOOMS[at - 1] / ZOOMS[at];

            Matrix.Scale(scale, scale);
            UpdateInverse();
        }

        public void ZoomOut(PointF mouseAt)
        {
            ZoomOut();
        }

        public string ZoomString
        {
            get
            {
                return ((int)(Zoom * 100)).ToString() + "%";
            }
        }

        private void UpdateInverse()
        {
            Inverse = Matrix.Clone();
            Inverse.Invert();
        }
    }
}
