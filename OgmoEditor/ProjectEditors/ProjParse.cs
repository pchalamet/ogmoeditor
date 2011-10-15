using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using OgmoEditor.Definitions.ValueDefinitions;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.Definitions;
using System.IO;

namespace OgmoEditor.ProjectEditors
{
    static class ProjParse
    {
        static public void Parse(ref int to, TextBox box)
        {
            try
            {
                to = Convert.ToInt32(box.Text);
            }
            catch
            {
                box.Text = to.ToString();
            }
        }

        static public void Parse(ref float to, TextBox box)
        {
            try
            {
                to = Convert.ToSingle(box.Text);
            }
            catch
            {
                box.Text = to.ToString();
            }
        }

        static public void Parse(ref Size to, TextBox x, TextBox y)
        {
            try
            {
                to.Width = Convert.ToInt32(x.Text);
            }
            catch
            {
                x.Text = to.Width.ToString();
            }

            try
            {
                to.Height = Convert.ToInt32(y.Text);
            }
            catch
            {
                y.Text = to.Height.ToString();
            }
        }

        static public void Parse(ref Point to, TextBox x, TextBox y)
        {
            try
            {
                to.X = Convert.ToInt32(x.Text);
            }
            catch
            {
                x.Text = to.X.ToString();
            }

            try
            {
                to.Y = Convert.ToInt32(y.Text);
            }
            catch
            {
                y.Text = to.Y.ToString();
            }
        }

        static public void Parse(ref Rectangle to, TextBox x, TextBox y, TextBox w, TextBox h)
        {
            try
            {
                to.X = Convert.ToInt32(x.Text);
            }
            catch
            {
                x.Text = to.X.ToString();
            }

            try
            {
                to.Y = Convert.ToInt32(y.Text);
            }
            catch
            {
                y.Text = to.Y.ToString();
            }

            try
            {
                to.Width = Convert.ToInt32(w.Text);
            }
            catch
            {
                w.Text = to.Width.ToString();
            }

            try
            {
                to.Height = Convert.ToInt32(h.Text);
            }
            catch
            {
                h.Text = to.Height.ToString();
            }
        }

        static public void Parse(ref OgmoColor to, TextBox box)
        {
            try
            {
                to = new OgmoColor(box.Text);
            }
            catch
            {
                box.Text = to.ToString();
            }
        }

        /*
         *  Error checking
         */
        static public string Error(string error)
        {
            return "-" + error + "\n";
        }

        static public string CheckDefinitionList(List<ValueDefinition> defs, string container)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (ValueDefinition v in defs)
            {
                if (v.Name != "" && !found.Contains(v.Name) && defs.FindAll(e => e.Name == v.Name).Count > 1)
                {
                    s += ProjParse.Error(container + " contains multiple values with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (defs.Find(e => e.Name == "") != null)
                s += ProjParse.Error(container + " contains value(s) with blank name");

            return s;
        }

        static public string CheckDefinitionList(List<LayerDefinition> defs)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (LayerDefinition v in defs)
            {
                if (v.Name != "" && !found.Contains(v.Name) && defs.FindAll(e => e.Name == v.Name).Count > 1)
                {
                    s += ProjParse.Error("There are multiple layers with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (defs.Find(e => e.Name == "") != null)
                s += ProjParse.Error("There are layer(s) with blank name");

            return s;
        }

        static public string CheckDefinitionList(List<Tileset> defs)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (Tileset v in defs)
            {
                if (v.Name != "" && !found.Contains(v.Name) && defs.FindAll(e => e.Name == v.Name).Count > 1)
                {
                    s += ProjParse.Error("There are multiple tilesets with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (defs.Find(e => e.Name == "") != null)
                s += ProjParse.Error("There are tileset(s) with blank name");

            return s;
        }

        static public string CheckDefinitionList(List<ObjectDefinition> defs)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (ObjectDefinition v in defs)
            {
                if (v.Name != "" && !found.Contains(v.Name) && defs.FindAll(e => e.Name == v.Name).Count > 1)
                {
                    s += ProjParse.Error("There are multiple objects with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (defs.Find(e => e.Name == "") != null)
                s += ProjParse.Error("There are object(s) with blank name");

            return s;
        }

        static public string CheckPath(string relPath, string absPath, string name)
        {
            if (!File.Exists(Path.Combine(absPath, relPath)))
                return Error(name + " does not exist");
            else
                return "";
        }

        static public string CheckNonblankString(string s, string name)
        {
            if (s != "")
                return "";
            else
                return Error(name + " is blank");
        }

        static public string CheckPosInt(int i, string name)
        {
            if (i > 0)
                return "";
            else
                return Error(name + " is not a positive integer");
        }

        static public string CheckPosSize(Size size, string name)
        {
            string s = "";
            s += CheckPosInt(size.Width, name + " Width");
            s += CheckPosInt(size.Height, name + " Height");
            return s;
        }
    }
}
