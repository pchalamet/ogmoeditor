using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            //Recent projects
            Config.ConfigFile.CheckRecentProjects();
            for (int i = 0; i < Config.ConfigFile.RecentProjects.Count; i++)
            {
                LinkLabel link = new LinkLabel();
                link.Location = new Point(12, 232 + (i * 20));
                link.LinkColor = Color.Red;
                link.Font = new Font(FontFamily.GenericMonospace, 10);
                link.Size = new Size(200, 16);
                link.Text = Config.ConfigFile.RecentProjects[i].Name;
                link.Name = Config.ConfigFile.RecentProjects[i].Path;
                link.Click += delegate(object sender, EventArgs e) { Ogmo.LoadProject(link.Name); };
                Controls.Add(link);
            }
        }
    }
}
