using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor
{
    public class Ogmo
    {
        public const float VERSION = .5f;

        static void Main(string[] args)
        {
            Application.Run(new MainWindow());
        }
    }
}
