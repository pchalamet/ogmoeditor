using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData;

namespace OgmoEditor.LevelEditors
{
    public partial class LevelProperties : Form
    {
        private Level level;

        public LevelProperties(Level level)
        {
            this.level = level;
            InitializeComponent();
        }
    }
}
