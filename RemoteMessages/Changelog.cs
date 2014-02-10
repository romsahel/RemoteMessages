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
    public partial class Changelog : Form
    {
        public Changelog(string changelog)
        {
            InitializeComponent();
            label1.Location = new Point((this.Width / 2) - (label1.Width / 2), 20);
            button1.Location = new Point((this.Width / 2) - (button1.Width / 2), 394);
            richTextBox1.Text = changelog.Replace("---___---___---", "");

        }
    }
}
