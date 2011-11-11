using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor
{
    static public class Util
    {
        public const float UP = (float)(Math.PI * 1.5);
        public const float DOWN = (float)(Math.PI * .5);
        public const float RIGHT = 0;
        public const float LEFT = (float)Math.PI;
        public const float QUARTER = (float)(Math.PI / 2);
        public const float EIGHTH = (float)(Math.PI / 4);
        public const float DEGTORAD = (float)(Math.PI / 180.0);
        public const float RADTODEG = (float)(180.0 / Math.PI);

        static public GraphicsDevice GraphicsDevice;

        static public string RelativePath(string absPath, string relTo)
        {
            string[] absDirs = absPath.Split(Path.DirectorySeparatorChar);
            string[] relDirs = relTo.Split(Path.DirectorySeparatorChar);

            // Get the shortest of the two paths
            int len = absDirs.Length < relDirs.Length ? absDirs.Length :
            relDirs.Length;

            // Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            // Find common root
            for (index = 0; index < len; index++)
            {
                if (absDirs[index] == relDirs[index]) 
                    lastCommonRoot = index;
                else 
                    break;
            }

            // If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }

            // Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            // Add on the ..
            for (index = lastCommonRoot + 1; index < absDirs.Length; index++)
            {
                if (absDirs[index].Length > 0) 
                    relativePath.Append(".." + Path.DirectorySeparatorChar);
            }

            // Add on the folders
            for (index = lastCommonRoot + 1; index < relDirs.Length - 1; index++)
            {
                relativePath.Append(relDirs[index] + Path.DirectorySeparatorChar);
            }
            relativePath.Append(relDirs[relDirs.Length - 1]);

            return relativePath.ToString();
        }

        static public string DirectoryPath(string filePath)
        {
            string d = Path.DirectorySeparatorChar + Path.GetFileName(filePath);
            return filePath.Remove(filePath.LastIndexOf(d));
        }

        static public Image CropImage(Image image, Rectangle clipRect)
        {
            Bitmap src = image as Bitmap;
            Bitmap target = new Bitmap(clipRect.Width, clipRect.Height);

            using(Graphics g = Graphics.FromImage(target))
            {
               g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), 
                                clipRect,                        
                                GraphicsUnit.Pixel);
            }

            return (Image)target;
        }

        static public Texture2D CropTexture(Texture2D texture, Rectangle clipRect)
        {
            Texture2D tex = new Texture2D(texture.GraphicsDevice, clipRect.Width, clipRect.Height);

            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            Color[] outData = new Color[clipRect.Width * clipRect.Height];

            for (int i = 0; i < clipRect.Width; i++)
                for (int j = 0; j < clipRect.Height; j++)
                    outData[i + (j * clipRect.Width)] = data[i + clipRect.X + ((j + clipRect.Y) * texture.Width)];

            tex.SetData<Color>(outData);
            return tex;
        }

        static public System.Drawing.Rectangle XNAToSystem(Microsoft.Xna.Framework.Rectangle rect)
        {
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        static public Microsoft.Xna.Framework.Rectangle SystemToXNA(System.Drawing.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        static public Texture2D CreateRect(GraphicsDevice device, Microsoft.Xna.Framework.Color color, int width, int height)
        {
            Texture2D texture = new Texture2D(device, width, height, false, SurfaceFormat.Color);

            Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;

            texture.SetData<Microsoft.Xna.Framework.Color>(data);

            return texture;
        }

    }
}
