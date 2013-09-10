using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace RemoteMessages
{
    public partial class CustomEmoji : Form
    {
        private string currentEmoji;
        private bool alreadyExists = false;

        public CustomEmoji(string current, string text)
        {
            InitializeComponent();
            this.ActiveControl = textBox1;


            this.currentEmoji = current;
            if (text != null)
            {
                textBox1.Text = text;

                textBox1.Text = textBox1.Text.Replace("&amp;", "&");
                textBox1.Text = textBox1.Text.Replace("&lt;", "<");
                textBox1.Text = textBox1.Text.Replace("&gt;", "<");
                textBox1.Text = textBox1.Text.Replace("&quot;", "\"");
            }
            webBrowser1.Navigate("about:blank");
            webBrowser1.Document.Write(currentEmoji);

            OKbutton.Enabled = (textBox1.Text != "");
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            string all = "";

            textBox1.Text = textBox1.Text.Replace("&", "&amp;");
            textBox1.Text = textBox1.Text.Replace("<", "&lt;");
            textBox1.Text = textBox1.Text.Replace("<", "&gt;");
            textBox1.Text = textBox1.Text.Replace("\"", "&quot;");

            using (StreamReader r = new StreamReader(MainForm.appFolder + "customemoji.cfg"))
            {
                string line = "";
                while (!r.EndOfStream)
                {
                    line = r.ReadLine();
                    string[] parts = line.Split(new string[] { "||--||" }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        if (currentEmoji == parts[0])
                        {
                            line = parts[0] + "||--||" + textBox1.Text;
                            alreadyExists = true;
                        }
                        all += line + '\n'  ;
                    }
                }
            }

            using (StreamWriter w = new StreamWriter(MainForm.appFolder + "customemoji.cfg"))
            {
                if (all != "")
                    w.WriteLine(all);
                if (!alreadyExists)
                    w.WriteLine(currentEmoji + "||--||" + textBox1.Text);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public bool getAlreadyExists() { return alreadyExists; }
        public string getShortcut() { return textBox1.Text; }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OKbutton.Enabled = (textBox1.Text != "");
        }
    }
}
