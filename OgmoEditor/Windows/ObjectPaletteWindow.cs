using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.Windows
{
    public partial class ObjectPaletteWindow : OgmoWindow
    {
        public ObjectDefinition CurrentObject { get; private set; }
        public event Ogmo.ObjectCallback OnObjectChanged;

        public ObjectPaletteWindow()
            : base(HorizontalSnap.Right, VerticalSnap.Bottom)
        {
            InitializeComponent();
        }
    }
}
