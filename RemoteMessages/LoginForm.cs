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

                this.labelMessage.Text = "The application requires an authentication.";
            }
            else
            {
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
    }
}
