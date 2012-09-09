using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

namespace OgmoEditor.Windows
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();

            if (ApplicationDeployment.IsNetworkDeployed)
                versionLabel.Text = "Version " + ApplicationDeployment.CurrentDeployment.CurrentVersion;
            else
                versionLabel.Text = "Debug Mode";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Ogmo.MainWindow.Activate();
            Ogmo.MainWindow.EnableEditing();
        }

        private void donateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/ca/cgi-bin/webscr?cmd=_flow&SESSION=gpkCEwnsaFTNafsXmAjTezJPc_I4qb2Y-2e6mCJfxG1G4-9pCD1U_SBBYBK&dispatch=5885d80a13c0db1f8e263663d3faee8da6a0e86558d6153d7722c6eea13ecd7b");
        }

        private void websiteButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ogmoeditor.com/");
        }
    }
}
