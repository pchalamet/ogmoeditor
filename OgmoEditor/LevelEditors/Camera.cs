﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors
{
    public class Camera
    {
        static private readonly float[] ZOOMS = new float[] { .25f, .33f, .5f, .66f, 1, 1.5f, 2, 3 };

        private Matrix matrix;
        private Matrix inverse;

        private Vector2 origin;
        private Vector2 position;
        private float zoom;
        private bool changed;

        public Camera()
        {
            position = Vector2.Zero;
            zoom = 1;

            updateMatrices();
        }

        //Matrix handlers
        private void updateMatrices()
        {
            matrix = Matrix.Identity *
                    Matrix.CreateTranslation(new Vector3(-position, 0)) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(origin, 0));

            inverse = Matrix.Invert(matrix);

            changed = false;
        }

        public Vector2 screenToEditor(Vector2 screenPos)
        {
            if (changed)
                updateMatrices();
            return Vector2.Transform(screenPos, matrix);
        }

        public Vector2 editorToScreen(Vector2 editorPos)
        {
            if (changed)
                updateMatrices();
            return Vector2.Transform(editorPos, inverse);
        }

        public Matrix Matrix
        {
            get
            {
                if (changed)
                    updateMatrices();
                return matrix;
            }
        }

        public Matrix Inverse
        {
            get
            {
                if (changed)
                    updateMatrices();
                return inverse;
            }
        }

        //Get/Sets
        public Vector2 Origin
        {
            get { return origin; }
            set
            {
                changed = true;
                origin = value;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                changed = true;
                position = value;
            }
        }

        public float X
        {
            get { return position.X; }
            set
            {
                changed = true;
                position.X = value;
            }
        }

        public float Y
        {
            get { return position.Y; }
            set
            {
                changed = true;
                position.Y = value;
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                changed = true;
                zoom = value;
            }
        }

        public void ZoomIn()
        {
            int index;
            for (index = 0; index < ZOOMS.Length; index++)
            {
                if (zoom <= ZOOMS[index])
                    break;
            }


            zoom = ZOOMS[Math.Min(ZOOMS.Length - 1, index + 1)];
            changed = true;
        }

        public void ZoomOut()
        {
            int index;
            for (index = 0; index < ZOOMS.Length; index++)
            {
                if (zoom <= ZOOMS[index])
                    break;
            }


            zoom = ZOOMS[Math.Max(0, index - 1)];
            changed = true;
        }
    }
}
