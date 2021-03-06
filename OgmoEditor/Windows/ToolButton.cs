﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OgmoEditor.LevelEditors.Tools;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class ToolButton : UserControl
    {
        static private readonly OgmoColor Selected = new OgmoColor(150, 220, 255);
        static private readonly OgmoColor NotSelected = new OgmoColor(255, 255, 255);

        public Tool Tool { get; private set; }
        private int num;

        public ToolButton(Tool tool, int x, int y, int num)
        {
            Tool = tool;
            Location = new Point(x, y);
            this.num = num;

            InitializeComponent();
            button.BackgroundImage = Image.FromFile(Path.Combine(Ogmo.ProgramDirectory, @"Content\tools", Tool.Image));
            toolTip.SetToolTip(button, Tool.Name + " (" + (num + 1).ToString() + ")");
            button.BackColor = (tool == Ogmo.ToolsWindow.CurrentTool) ? Selected : NotSelected;

            //Events
            Ogmo.ToolsWindow.OnToolChanged += onToolChanged;
        }

        /*
         *  Events
         */
        private void button_Click(object sender, EventArgs e)
        {
            Ogmo.ToolsWindow.SetTool(Tool);
        }

        private void onToolChanged(Tool tool)
        {
            if (tool == Tool)
            {
                button.BackColor = Selected;
                button.Select();
            }
            else
                button.BackColor = NotSelected;
        }
    }
}
