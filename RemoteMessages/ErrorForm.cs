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
    public partial class ErrorForm : Form
    {
        public ErrorForm(string msg, bool hideRetryButton)
        {
            InitializeComponent();
            pictureBox1.Image = SystemIcons.Error.ToBitmap();
            label1.Text = msg;
            if (hideRetryButton)
            {
                OKbutton.Location = retryButton.Location;
                retryButton.Visible = false;
            }
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void retryButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }
    }
}
