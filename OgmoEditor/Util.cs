using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;

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

    }
}
