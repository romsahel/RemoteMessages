using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net;

namespace RemoteMessages
{
    public partial class AboutForm : Form
    {
        private string TopCaption = "About " + Application.ProductName;

        public AboutForm(string version)
        {
            InitializeComponent();

            productVersionLabel.Text = version;
        }

        public AboutForm(string TopCaption, string Link)
        {
            InitializeComponent();
            this.productNameLabel.Text = Application.ProductName.Length <= 0 ? "{Product Name}" : Application.ProductName;

            this.TopCaption = TopCaption;
            this.linkLabel.Text = Link;
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(Icon.ExtractAssociatedIcon(Application.ExecutablePath), 20, 8);
            e.Graphics.DrawString(TopCaption, new Font("Segoe UI", 14f), Brushes.Azure, new PointF(70, 10));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel.Text);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            string changelog = "ERROR ! \nCould not reach the server.";
            try
            {
                // Create web client.
                WebClient client = new WebClient();
                client.Proxy = null;
                // Download string.
                changelog = (client.DownloadString(@"http://aerr.github.io/RemoteMessages/VERSION.txt"));
                changelog = changelog.Split(new string[] { "---___---___---" }, StringSplitOptions.RemoveEmptyEntries)[0];
                MessageBox.Show("Changelog:\n" + changelog, "Changelog", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch
            {
                MessageBox.Show(changelog, "Changelog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            DialogResult = System.Windows.Forms.DialogResult.None;
            
        }

        private void contactButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:romsahel@gmail.com?subject=[RMCLIENT] Suggestion/Bug report");
        }
    }
}
