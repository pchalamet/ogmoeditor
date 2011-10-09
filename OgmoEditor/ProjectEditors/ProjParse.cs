using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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

        /*
         *  Error checking
         */
        static public string Error(string error)
        {
            return "-" + error + "\n";
        }

        static public string CheckNonblankString(TextBox x, string name)
        {
            if (x.Text != "")
                return "";
            else
                return Error(name + " is blank");
        }

        static public string CheckPosInt(TextBox x, string name)
        {
            try
            {
                int i = Convert.ToInt32(x.Text);
                return CheckPosInt(i, name);
            }
            catch
            {
                return Error(name + " is not a positive integer");
            }
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
