using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RemoteMessages
{
    public partial class MainForm
    {
        private System.Windows.Forms.FormWindowState saved_state;

        private int saved_width;
        private int saved_height;
        private double saved_opacity;
        private System.Windows.Forms.FormBorderStyle saved_style;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private bool saved_topMost;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        void Strip_Click(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void toggle_popup()
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                normal_mode();
            else
                popup_mode();

            if (Document != null)
            {

                System.Windows.Forms.HtmlElement elt = Document.GetElementById("messages");
                if (elt != null)
                {
                    System.Windows.Forms.HtmlElementCollection msg = elt.Children;
                    if (msg.Count > 0)
                        msg[msg.Count - 1].ScrollIntoView(false);
                }
            }
        }

        private void normal_mode()
        {
            this.Width = saved_width;
            this.Height = saved_height;
            this.Opacity = saved_opacity;
            this.FormBorderStyle = saved_style;

            this.TopMost = saved_topMost;
            this.WindowState = saved_state;
        }

        private void popup_mode()
        {
            saved_state = this.WindowState;
            saved_topMost = this.TopMost;

            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.TopMost = true;

            saved_width = this.Width;
            saved_height = this.Height;
            saved_opacity = this.Opacity;
            saved_style = this.FormBorderStyle;

            this.Width = 800;
            this.Height = 300;
            this.Opacity = 0.90;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
        }
    }
}
