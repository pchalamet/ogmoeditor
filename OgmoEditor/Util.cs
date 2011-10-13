using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

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

        static public string GetFilePathRelativeTo(string filePath, string absolutePath)
        {
            string dir = filePath.Remove(filePath.IndexOf(Path.GetFileName(filePath)) - 1);

            string result = GetPathRelativeTo(dir, absolutePath);
            if (result != "")
                result += Path.DirectorySeparatorChar;

            return result + Path.GetFileName(filePath);
        }

        static public string GetPathRelativeTo(string relativePath, string absolutePath)
        {
            if (relativePath == "")
                return absolutePath;

            string[] relative = relativePath.Split(Path.DirectorySeparatorChar);
            string[] absolute = absolutePath.Split(Path.DirectorySeparatorChar);

            int matches;
            for (matches = 0; matches < absolute.Length && matches < relative.Length; matches++)
            {
                if (relative[matches] != absolute[matches])
                    break;
            }

            string[] final;
            if (matches < absolute.Length)
            {
                if (matches >= relative.Length)
                {
                    //The absolute path needs to go back to the file
                    final = new string[absolute.Length - matches];
                    for (int j = 0; j < absolute.Length - matches; j++)
                        final[j] = "..";
                }
                else
                {
                    //The absolute path needs to go back a bit then forward
                    final = new string[relative.Length - matches + (absolute.Length - matches)];
                    int j;

                    //Write the backs
                    for (j = 0; j < (absolute.Length - matches); j++)
                        final[j] = "..";

                    //Write the forwards
                    for (int k = matches; k < relative.Length; k++)
                        final[j + k - matches] = relative[k];
                }
            }
            else
            {
                //The absolute path is all good
                final = new string[relative.Length - matches];
                for (int j = matches; j < relative.Length; j++)
                    final[j - matches] = relative[matches];
            }

            return string.Join(Convert.ToString(Path.DirectorySeparatorChar), final, 0, final.Length);
        }

        static public string GetPathAbsolute(string path, string relativeTo)
        {
            if (path == "")
                return relativeTo;

            string[] rel = path.Split(Path.DirectorySeparatorChar);
            string[] abs = relativeTo.Split(Path.DirectorySeparatorChar);

            int backs = 0;
            int i;
            for (i = 0; i < rel.Length; i++)
            {
                if (rel[i] == "..")
                    backs++;
                else
                    break;
            }

            string[] final = new string[rel.Length + abs.Length - backs * 2];
            for (i = 0; i < abs.Length - backs; i++)
                final[i] = abs[i];

            for (int j = 0; j < rel.Length - backs; j++)
                final[j + i] = rel[backs + j];

            return string.Join(Convert.ToString(Path.DirectorySeparatorChar), final, 0, final.Length);
        }

    }
}
