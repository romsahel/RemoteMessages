using Microsoft.Win32;
using System;
using System.Linq;
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
        private bool aboutDisplayed = false;
        private string VERSION;
        // The path to the key where Windows looks for startup applications
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public PreferencesForm(bool[] bBackgrounds, bool[] bNotifs, int iB, int iF, bool bA, bool bR, bool bU, int iR, int iU, string sName, string sUrl, bool bGhost, string sGhost, string hotkey, bool soundEnabled, int soundVolume, int soundIndex, bool autoscroll, string version)
        {
            this.VERSION = version;
            InitializeComponent();
            ChangeTab(1);

            navBackgrounding.Select();

            closeToTray.Checked = bBackgrounds[0];
            minimizeToTray.Checked = bBackgrounds[1];
            escapeToTray.Checked = bBackgrounds[2];

            showBalloon.Checked = bNotifs[0];
            delayBalloon.Text = iB.ToString();
            showFlash.Checked = bNotifs[1];
            flashCount.Text = iF.ToString();

            checkSound.Checked = soundEnabled;
            trackBarVolume.Value = soundVolume;

            activateAutoIP.Checked = bA;
            activateReplacement.Checked = bR;
            activateUnfocus.Checked = bU;

            //delayAutoIP.Enabled = bA;
            delayReplacement.Enabled = bR;
            delayUnfocus.Enabled = bU;

            delayReplacement.Text = iR.ToString();
            delayUnfocus.Text = iU.ToString();
            name = sName;
            url = (sUrl.Substring(7)).Split(':')[0];
            port.Text = sUrl.Split(':')[2].Trim('/');

            activateGhostMode.Checked = bGhost;
            password.Text = sGhost;

            if (activateAutoIP.Checked)
                deviceName.Text = name;
            else
                deviceName.Text = url;

            checkWin.Checked = hotkey.Contains('#');
            checkAlt.Checked = hotkey.Contains('!');
            checkCtrl.Checked = hotkey.Contains('^');
            textHotkey.Text = hotkey.Trim(new char[] { '#', '!', '^' });

            checkAutoscroll.Checked = autoscroll;

            if (rkApp.GetValue("RemoteClient") == null) // The value doesn't exist, the application is not set to run at startup
                checkOnstartup.Checked = false;
            else // The value exists, the application is set to run at startup
                checkOnstartup.Checked = true;

            soundBox.SelectedIndex = soundIndex;
        }

        private void ChangeTab(int i)
        {
            SuspendLayout();
            foreach (Control c in this.Controls)
            {
                if (c != ok && c != cancel && c != navigationPanel)
                    c.Visible = false;
            }
            Panel panel;
            switch (i)
            {
                // Backgrounding
                case 1:
                    this.Text = "Preferences - Backgrounding";
                    panelBackgrounding.Visible = true;
                    panel = panelBackgrounding;
                    break;
                // conversation
                case 2:
                    this.Text = "Preferences - Conversations";
                    panelReplacement.Visible = true;

                    panelReplacement.Location = panelBackgrounding.Location;
                    panel = panelReplacement;
                    break;
                // Notifications
                case 3:
                    this.Text = "Preferences - Notifications";
                    panelNotifications.Visible = true;

                    panelNotifications.Location = panelBackgrounding.Location;
                    panel = panelNotifications;
                    break;
                // Auto-IP update
                case 4:
                    this.Text = "Preferences - IP Update";
                    activateAutoIP.Visible = true;
                    panelAutoUpdate.Visible = true;

                    activateAutoIP.Location = new System.Drawing.Point(activateGhostMode.Location.X, 10 + 5);
                    panelAutoUpdate.Location = panelBackgrounding.Location;
                    panel = panelAutoUpdate;
                    break;
                // Ghost mode
                case 5:
                    this.Text = "Preferences - Ghost-Mode";
                    activateGhostMode.Visible = true;
                    panelGhostMode.Visible = true;

                    activateGhostMode.Location = new System.Drawing.Point(activateGhostMode.Location.X, 10 + 5);
                    panelGhostMode.Location = panelBackgrounding.Location;
                    panel = panelGhostMode;
                    break;
                default:
                    panelBackgrounding.Visible = true;
                    panel = panelBackgrounding;
                    break;
            }

            ok.Location = new System.Drawing.Point(this.Width / 2 - ok.Width / 2 - 50, panel.Height + 27);
            cancel.Location = new System.Drawing.Point(this.Width / 2 - cancel.Width / 2 + 50, panel.Height + 27);
            ResumeLayout();
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
                if (checkOnstartup.Checked) // Add the value in the registry so that the application runs at startup
                    rkApp.SetValue("RemoteClient", Application.ExecutablePath.ToString());
                else // Remove the value from the registry so that the application doesn't start
                    rkApp.DeleteValue("RemoteClient", false);

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
        public int[] getNotifMoreOptions() { return new int[] { Int32.Parse(flashCount.Text), Int32.Parse(delayBalloon.Text) }; }
        public bool getAutoScroll() { return checkAutoscroll.Checked; }

        public bool getSoundActivated() { return checkSound.Checked; }
        public int getSoundVolume() { return trackBarVolume.Value; }
        public int getSoundIndex() { return soundBox.SelectedIndex; }

        public bool getAutoIPActivated() { return activateAutoIP.Checked; }
        public bool getReplacementActivated() { return activateReplacement.Checked; }
        public bool getUnfocusActivated() { return activateUnfocus.Checked; }

        public int getReplacementDelay() { return Int32.Parse(delayReplacement.Text); }
        public int getUnfocusDelay() { return Int32.Parse(delayUnfocus.Text); }
        public bool getOnStartupActivated() { return checkOnstartup.Checked; }

        public string getDeviceName() { return deviceName.Text; }
        public string getPort() { return port.Text; }

        public bool getGhostModeActivated() { return activateGhostMode.Checked; }
        public string getPassword() { return password.Text; }
        public string getHotkey()
        {
            string h = "";

            if (checkWin.Checked)
                h += ('#');
            if (checkAlt.Checked)
                h += ('!');
            if (checkCtrl.Checked)
                h += ('^');

            return h + textHotkey.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        private void tips_Click(object sender, EventArgs e)
        {
            string tips = "- Press F1 to display this window.\n"
                + "- Press F2 to check for updates.\n"
                + "- Press F11 to switch to fullscreen mode.\n"
                + "- Press F12 to manually update your IP.\n"
                + "- You can press Ctrl+Enter instead of click the send button.\n"
                + "- Press Alt+1-9 to open the Nth conversation.\n"
                + "- Press Ctrl+E to focus the sending area (editable text).\n"
                + "- If Ghost Mode is disabled, the hotkey you chose can still be used to Show/Hide the window with no login required.\n"
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

        private void activateGhostMode_CheckedChanged(object sender, EventArgs e)
        {
            password.Enabled = activateGhostMode.Checked;
        }


        private void PreferencesForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!aboutDisplayed)
            {
                using (AboutForm about = new AboutForm(VERSION))
                {
                    aboutDisplayed = true;
                    about.ShowDialog();
                    aboutDisplayed = false;
                }
            }
            e.Cancel = true;
        }

        private void checkSound_CheckedChanged(object sender, EventArgs e)
        {
            soundBox.Enabled = checkSound.Checked;
        }

        private void navigation_Click(object sender, EventArgs e)
        {
            switch (((Button)(sender)).Text)
            {
                case "General":
                    ChangeTab(1);
                    break;
                case "Conversations":
                    ChangeTab(2);
                    break;
                case "Notifications":
                    ChangeTab(3);
                    break;
                case "IP Update":
                    ChangeTab(4);
                    break;
                case "Ghost Mode":
                    ChangeTab(5);
                    break;
            }
        }
    }
}
;