using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.Windows
{
    public partial class TileSelector : UserControl
    {
        private Tileset tileset;
        public int Selection { get; private set; }

        public TileSelector()
        {
            InitializeComponent();
        }

        public Tileset Tileset
        {
            get { return tileset; }
            set
            {
                tileset = Tileset;
                Selection = 0;
            }
        }
    }
}
