using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RemoteMessages
{
    public partial class NotificationForm : Form
    {
        private Thread fadeIn;
        private Thread fadeOut;

        private System.Windows.Forms.Timer timerFadeOut;
        private const double fadeRateDefault = 0.0001;
        private double fadeRate = fadeRateDefault;

        private bool isMouseIn;
        private bool isButtonClicked;

        private string lastNotification;

        public NotificationForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Opacity = 0;
            
            timerFadeOut = new System.Windows.Forms.Timer();
            timerFadeOut.Tick += new EventHandler(startFadeOut);

            //Prepare the fade in/out threads that will be called later
            ThreadStart fadeInStart = new ThreadStart(FadeIn);
            fadeIn = new Thread(fadeInStart);

            ThreadStart fadeOutStart = new ThreadStart(FadeOut);
            fadeOut = new Thread(fadeOutStart);

            isMouseIn = false;
        }

        public void ShowNotification(string name, string msg, int delay, string lastHashCode)
        {
            this.lastNotification = lastHashCode;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.labelName.Text = name;
            this.labelMsg.Text = msg;

            //this.Show();

            timerFadeOut.Interval = delay;

            fadeRate = fadeRateDefault;
            isButtonClicked = false;
            isMouseIn = false;

            beginFadeIn();
        }

        private void startFadeOut(object sender, EventArgs e)
        {
            timerFadeOut.Stop();

            beginFadeOut();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            //Cancel running fading threads to avoid overflowing the stack
            if (fadeIn.ThreadState == ThreadState.Running)
                fadeIn.Abort();

            if (fadeOut.ThreadState == ThreadState.Running)
                fadeOut.Abort();

            if (timerFadeOut.Enabled)
                timerFadeOut.Stop();

            isButtonClicked = true;
            isMouseIn = false;

            beginFadeOut();
            fadeRate = 0.00035;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FadeIn()
        {
            for (double i = this.Opacity; i <= 1; i += fadeRate)
            {
                ChangeOpacity(i);
            }

            if (this.Opacity != 1.00)
                ChangeOpacity(1.00);

            if (!isMouseIn)
                this.Invoke(new MethodInvoker(() => timerFadeOut.Start()));
        }
        private void FadeOut()
        {
            for (double i = this.Opacity; i >= 0 && !isMouseIn; i -= fadeRate)
            {
                ChangeOpacity(i);
            }

            if (isMouseIn)
            {
                return;
            }
            else if (this.Opacity != 0)
                ChangeOpacity(0);
            
            this.Invoke(new MethodInvoker(() => this.Hide()));
        }
        private delegate void ChangeOpacityDelegate(double value);
        private void ChangeOpacity(double value)
        {
            if (this.InvokeRequired)
            {
                ChangeOpacityDelegate del = new ChangeOpacityDelegate(ChangeOpacity);
                object[] parameters = { value };

                this.Invoke(del, value);
            }
            else
            {
                //Your code goes here, in this case:
                this.Opacity = value;
            }
        }

        private void NotificationForm_MouseEnter(object sender, EventArgs e)
        {
            if (!isButtonClicked)
            {
                isMouseIn = true;

                //Cancel running fading threads to avoid overflowing the stack
                if (fadeIn.ThreadState == ThreadState.Running)
                    fadeIn.Abort();

                if (fadeOut.ThreadState == ThreadState.Running)
                    fadeOut.Abort();

                if (timerFadeOut.Enabled)
                    timerFadeOut.Stop();

                beginFadeIn();
                fadeRate = 0.001;
            }
        }

        private void NotificationForm_MouseLeave(object sender, EventArgs e)
        {
            if (!isButtonClicked)
            {
                //If the mouse goes over a control in the form, the MouseLeave event (this one)
                //is called. We do not want to fade out if the mouse is still pointing at something
                //inside the form. This is a quick dirty fix, just check if the mouse is within
                //the form's bounds
                Point pt = PointToClient(Cursor.Position);
                if (ClientRectangle.Contains(pt)) return;

                isMouseIn = false;

                //Cancel running fading threads to avoid overflowing the stack
                if (fadeIn.ThreadState == ThreadState.Running)
                    fadeIn.Abort();

                if (fadeOut.ThreadState == ThreadState.Running)
                    fadeOut.Abort();

                if (timerFadeOut.Enabled)
                    timerFadeOut.Stop();

                timerFadeOut.Start();
            }
        }

        private void beginFadeOut()
        {
            fadeRate = fadeRateDefault;

            //Begin fading out
            ThreadStart fadeOutStart = new ThreadStart(FadeOut);
            fadeOut = new Thread(fadeOutStart);

            fadeOut.Start();
        }

        private void beginFadeIn()
        {
            fadeRate = fadeRateDefault;

            //Begin fading in
            ThreadStart fadeInStart = new ThreadStart(FadeIn);
            fadeIn = new Thread(fadeInStart);

            fadeIn.Start();
        }

        private void NotificationForm_Click(object sender, EventArgs e)
        {
            buttonClose_Click(sender, e);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void labelMsg_Click(object sender, EventArgs e)
        {
            NotificationForm_Click(this, e);
        }

        private void labelName_Click(object sender, EventArgs e)
        {
            NotificationForm_Click(this, e);
        }

        public bool hasAlreadyBeenDisplayed(string current)
        {
            return current.Equals(lastNotification);
        }
    }
}
