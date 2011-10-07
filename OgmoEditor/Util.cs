using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OgmoEditor
{
    static public class Util
    {
        static public string GetFilePathRelativeTo(string filePath, string absolutePath)
        {
            string dir = filePath.Remove(filePath.IndexOf(Path.GetFileName(filePath)) - 1);
            return GetPathRelativeTo(dir, absolutePath) + Path.DirectorySeparatorChar + Path.GetFileName(filePath);
        }

        static public string GetPathRelativeTo(string relativePath, string absolutePath)
        {
            if (relativePath == "")
                return absolutePath;

            string[] relative = relativePath.Split(Path.DirectorySeparatorChar);
            string[] absolute = absolutePath.Split(Path.DirectorySeparatorChar);

            int i;
            for (i = 0; i < absolute.Length && i < relative.Length; i++)
            {
                if (relative[i] != absolute[i])
                    break;
            }

            string[] final;
            if (i < absolute.Length)
            {
                if (i >= relative.Length)
                {
                    //The absolute path needs to go back to the file
                    final = new string[absolute.Length - i];
                    for (int j = 0; j < absolute.Length - i; j++)
                        final[j] = "..";
                }
                else
                {
                    //The absolute path needs to go back a bit then forward
                    final = new string[relative.Length - i + (absolute.Length - i)];
                    int j;

                    //Write the backs
                    for (j = 0; j < (absolute.Length - i); j++)
                        final[j] = "..";

                    //Write the forwards
                    for (int k = i; k < relative.Length; k++)
                        final[j + k - i] = relative[k];
                }
            }
            else
            {
                //The absolute path is all good
                final = new string[relative.Length - i];
                for (int j = i; j < relative.Length; j++)
                    final[j - i] = relative[i];
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
