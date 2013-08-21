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
    public partial class Form2 : Form
    {
        private const int defaultAutoIP = 3;
        private const int defaultReplacement = 500;
        private const int defaultUnfocus = 3000;
        public Form2(bool bA, bool bR, bool bU, int iA, int iR, int iU, string sA)
        {
            InitializeComponent();
            cancel.Select();
            activateAutoIP.Checked = bA;
            activateReplacement.Checked = bR;
            activateUnfocus.Checked = bU;

            delayAutoIP.Enabled = bA;
            delayReplacement.Enabled = bR;
            delayUnfocus.Enabled = bU;

            delayAutoIP.Text = iA.ToString();
            delayReplacement.Text = iR.ToString();
            delayUnfocus.Text = iU.ToString();

            deviceName.Text = sA;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            int x = 0;
            bool error = false;
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
                deviceNameLabel.Text = "Device's Name: ";
            else
                deviceNameLabel.Text = "Device's   IP: ";

        }

        private void activateReplacement_CheckedChanged(object sender, EventArgs e)
        {
            delayReplacement.Enabled = activateReplacement.Checked;
        }

        private void activateUnfocus_CheckedChanged(object sender, EventArgs e)
        {
            delayUnfocus.Enabled = activateUnfocus.Checked;
        }


        public bool getAutoIPActivated() { return activateAutoIP.Checked; }
        public bool getReplacementActivated() { return activateReplacement.Checked; }
        public bool getUnfocusActivated() { return activateUnfocus.Checked; }

        public int getAutoIPDelay() { return Int32.Parse(delayAutoIP.Text); }
        public int getReplacementDelay() { return Int32.Parse(delayReplacement.Text); }
        public int getUnfocusDelay() { return Int32.Parse(delayUnfocus.Text); }

        public string getDeviceName() { return deviceName.Text; }

    }
}
;