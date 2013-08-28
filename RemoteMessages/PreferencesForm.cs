using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteMessages
{
    public partial class PreferencesForm : Form
    {
        private const int defaultAutoIP = 3;
        private const int defaultReplacement = 500;
        private const int defaultUnfocus = 3000;
        private const int defaultBalloon = 500;
        private const int defaultFlash = 6;
        private string name, url;

        public PreferencesForm(bool[] bBackgrounds, bool[] bNotifs, int iB, int iF, bool bA, bool bR, bool bU, int iA, int iR, int iU, string sName, string sUrl)
        {
            InitializeComponent();
            cancel.Select();

            closeToTray.Checked = bBackgrounds[0];
            minimizeToTray.Checked = bBackgrounds[1];
            escapeToTray.Checked = bBackgrounds[2];

            showBalloon.Checked = bNotifs[0];
            delayBalloon.Text = iB.ToString();
            showFlash.Checked = bNotifs[1];
            flashCount.Text = iF.ToString();

            activateAutoIP.Checked = bA;
            activateReplacement.Checked = bR;
            activateUnfocus.Checked = bU;

            //delayAutoIP.Enabled = bA;
            delayReplacement.Enabled = bR;
            delayUnfocus.Enabled = bU;
            
            delayAutoIP.Text = iA.ToString();
            delayReplacement.Text = iR.ToString();
            delayUnfocus.Text = iU.ToString();
            name = sName;
            url = (sUrl.Substring(7)).Split(':')[0];
            port.Text = sUrl.Split(':')[2];

            if (activateAutoIP.Checked)
                deviceName.Text = name;
            else
                deviceName.Text = url;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            int x = 0;
            bool error = false;

            if (delayBalloon.Enabled && !Int32.TryParse(delayBalloon.Text, out x))
            {
                delayBalloon.Text = defaultBalloon.ToString();
                error = true;
            }
            else if (x < 0)
            {
                delayBalloon.Text = defaultBalloon.ToString();
                error = true;
            }

            if (flashCount.Enabled && !Int32.TryParse(flashCount.Text, out x))
            {
                flashCount.Text = defaultFlash.ToString();
                error = true;
            }
            else if (x < 0)
            {
                flashCount.Text = defaultFlash.ToString();
                error = true;
            }

            if (delayAutoIP.Enabled && !Int32.TryParse(delayAutoIP.Text, out x))
            {
                delayAutoIP.Text = defaultAutoIP.ToString();
                error = true;
            }
            else if (x < 0)
            {
                delayAutoIP.Text = defaultAutoIP.ToString();
                error = true;
            }

            if (delayReplacement.Enabled && !Int32.TryParse(delayReplacement.Text, out x))
            {
                delayReplacement.Text = defaultReplacement.ToString();
                error = true;
            }
            else if (x < 0)
            {
                error = true;
                delayReplacement.Text = defaultReplacement.ToString();
            }

            if (delayUnfocus.Enabled && !Int32.TryParse(delayUnfocus.Text, out x))
            {
                delayUnfocus.Text = defaultUnfocus.ToString();
            }
            else if (x < 0)
            {
                error = true;
                delayUnfocus.Text = defaultUnfocus.ToString();
            }
            if (error)
            {
                MessageBox.Show("You entered invalid data. Delays must be positive integers.",
       "Invalid input",
       MessageBoxButtons.OK,
       MessageBoxIcon.Exclamation,
       MessageBoxDefaultButton.Button1);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void activateAutoIP_CheckedChanged(object sender, EventArgs e)
        {
            delayAutoIP.Enabled = activateAutoIP.Checked;
            if (activateAutoIP.Checked)
            {
                deviceNameLabel.Text = "Device's Name: ";
                deviceName.Text = name;
            }
            else
            {
                deviceNameLabel.Text = "Device's   IP: ";
                deviceName.Text = url;
            }
        }

        private void activateReplacement_CheckedChanged(object sender, EventArgs e)
        {
            delayReplacement.Enabled = activateReplacement.Checked;
        }

        private void activateUnfocus_CheckedChanged(object sender, EventArgs e)
        {
            delayUnfocus.Enabled = activateUnfocus.Checked;
        }


        public bool[] getBackgrounderOptions() { return new bool[] { closeToTray.Checked, minimizeToTray.Checked, escapeToTray.Checked }; }
        public bool[] getNotifOptions() { return new bool[] { showBalloon.Checked, showFlash.Checked }; }

        public bool getAutoIPActivated() { return activateAutoIP.Checked; }
        public bool getReplacementActivated() { return activateReplacement.Checked; }
        public bool getUnfocusActivated() { return activateUnfocus.Checked; }
        
        public int getAutoIPDelay() { return Int32.Parse(delayAutoIP.Text); }
        public int getReplacementDelay() { return Int32.Parse(delayReplacement.Text); }
        public int getUnfocusDelay() { return Int32.Parse(delayUnfocus.Text); }

        public string getDeviceName() { return deviceName.Text; }
        public string getPort() { return port.Text; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        private void tips_Click(object sender, EventArgs e)
        {
            string tips = "Press F1 to display this window.\n"
            + "Press F11 to switch to fullscreen mode.\n"
            + "Press F12 to manually update your IP.\n"
            + "You can press Ctrl+Enter instead of click the send button.\n"
            + "Press Alt+1-9 to open the Nth conversation.\n"
            + "Press Ctrl+E to focus the sending area (editable text).\n"
            ;

            MessageBox.Show(tips, "Tips!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showBalloon_CheckedChanged(object sender, EventArgs e)
        {
            delayBalloon.Enabled = showBalloon.Checked;
        }

        private void showFlash_CheckedChanged(object sender, EventArgs e)
        {
            flashCount.Enabled = showFlash.Checked;
        }
    }
}
;