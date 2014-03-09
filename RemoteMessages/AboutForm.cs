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
using System.IO;

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
                using (Changelog window = new Changelog(changelog))
                {
                    window.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show(changelog, "Changelog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void contactButton_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Your default mail app is about to be started. \nWould you also like to access your html file (might be useful to help with your problem)? \nClick Yes to open it. \nClicking No will still start your mail app.", "Contact the developper", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                using (StreamWriter w = new StreamWriter("log.html"))
                {
                    w.WriteLine(MainForm.HTML_Backup);
                }
                System.Diagnostics.Process.Start("log.html");
            }
            System.Diagnostics.Process.Start("mailto:romsahel@gmail.com?subject=[RMCLIENT] Suggestion/Bug report");
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

    }
}
