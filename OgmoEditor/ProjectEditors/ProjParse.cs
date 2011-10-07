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
        static public void GetInt(ref int to, TextBox box)
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

        static public void GetSize(ref Size to, TextBox x, TextBox y)
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

        static public string Error(string error)
        {
            return "-" + error + "\n";
        }

        static public string CheckString(TextBox x, string name)
        {
            if (x.Text != "")
                return "";
            else
                return Error(name + " can not be left blank");
        }

        static public string CheckPosInt(TextBox x, string name)
        {
            try
            {
                int i = Convert.ToInt32(x.Text);
                if (i > 0)
                    return "";
                else 
                    return Error(name + " must be defined as a positive integer");
            }
            catch
            {
                return Error(name + " must be defined as a positive integer");
            }
        }
    }
}
