using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RemoteMessages
{
    public partial class LoginForm : Form
    {
        public LoginForm(bool fromGhostMode = false)
        {
            InitializeComponent();

            if (fromGhostMode)
            {
                this.username.Visible = false;
                this.password.Visible = false;
                this.labelUser.Visible = false;
                this.labelPass.Visible = false;
                this.checkRemember.Visible = false;

                this.labelMessage.Text = "The application requires an authentication.";
            }
            else
            {
                if (File.Exists(MainForm.appFolder + "pwd"))
                {
                    checkRemember.Checked = true;
                    username.ForeColor = Color.Black;
                    password.UseSystemPasswordChar = true;
                    password.ForeColor = Color.Black;
                    using (StreamReader reader = new StreamReader(MainForm.appFolder + "pwd"))
                    {
                        this.username.Text = reader.ReadLine();
                        this.password.Text = reader.ReadLine();
                    }
                }
                this.ghostmodePassword.Visible = false;
                this.ghostmodeLabel.Visible = false;
            }
        }


        private void username_Enter(object sender, EventArgs e)
        {
            if (username.ForeColor == Color.Gray)
            {
                username.Text = "";
                username.ForeColor = Color.Black;
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if (username.Text == "")
            {
                username.Text = "Username";
                username.ForeColor = Color.Gray;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (password.ForeColor == Color.Gray)
            {
                password.Text = "";
                password.UseSystemPasswordChar = true;
                password.ForeColor = Color.Black;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (username.Text == "")
            {
                password.Text = "Password";
                password.UseSystemPasswordChar = false;
                password.ForeColor = Color.Gray;
            }
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                OKbutton_Click(null, null);
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            if (checkRemember.Checked)
            {
                using (StreamWriter writer = new StreamWriter(MainForm.appFolder + "pwd"))
                {
                    writer.WriteLine(username.Text);
                    writer.WriteLine(password.Text);
                }
            }
            else
                File.Delete(MainForm.appFolder + "pwd");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public string getUsername() { return username.Text; }
        public string getPassword() { return password.Text; }
        public string getGhostModePassword() { return ghostmodePassword.Text; }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            ghostmodePassword.Focus();
        }

        public void setPasswordClear()
        {
            this.ghostmodePassword.Text = "";
        }

        private void LoginForm_Deactivate(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void checkRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRemember.Checked && !File.Exists(MainForm.appFolder + "pwd"))
            {
                DialogResult result = MessageBox.Show("Enabling this feature will allow the application to save your login account details so that you only have to press OK.\n"
                    + "Be careful though: details are not securely stored which means anyone with access to your computer can know your password.",
                    "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    checkRemember.Checked = false;
            }
        }
    }
}
