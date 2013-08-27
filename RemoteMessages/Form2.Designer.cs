namespace RemoteMessages
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.activateReplacement = new System.Windows.Forms.CheckBox();
            this.delayReplacement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.delayUnfocus = new System.Windows.Forms.TextBox();
            this.activateUnfocus = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.delayAutoIP = new System.Windows.Forms.TextBox();
            this.activateAutoIP = new System.Windows.Forms.CheckBox();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceName = new System.Windows.Forms.TextBox();
            this.tips = new System.Windows.Forms.Button();
            this.minimizeToTray = new System.Windows.Forms.CheckBox();
            this.closeToTray = new System.Windows.Forms.CheckBox();
            this.escapeToTray = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.showBalloon = new System.Windows.Forms.CheckBox();
            this.showFlash = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flashCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.delayBalloon = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // activateReplacement
            // 
            this.activateReplacement.AutoSize = true;
            this.activateReplacement.Checked = true;
            this.activateReplacement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateReplacement.Location = new System.Drawing.Point(39, 381);
            this.activateReplacement.Name = "activateReplacement";
            this.activateReplacement.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.activateReplacement.Size = new System.Drawing.Size(225, 24);
            this.activateReplacement.TabIndex = 1;
            this.activateReplacement.Text = "Activate smiley-replacement";
            this.activateReplacement.UseVisualStyleBackColor = true;
            this.activateReplacement.CheckedChanged += new System.EventHandler(this.activateReplacement_CheckedChanged);
            // 
            // delayReplacement
            // 
            this.delayReplacement.Location = new System.Drawing.Point(139, 414);
            this.delayReplacement.Name = "delayReplacement";
            this.delayReplacement.Size = new System.Drawing.Size(100, 20);
            this.delayReplacement.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Delay (in ms) :";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(221, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 46);
            this.label3.TabIndex = 7;
            this.label3.Text = "Replaces the standard smileys :D by their EMOJI equivalent.";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(221, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 52);
            this.label2.TabIndex = 11;
            this.label2.Text = "Automatically unfocuses a conversation after a delay so that it is not marked as " +
                "read.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 503);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Delay (in ms) :";
            // 
            // delayUnfocus
            // 
            this.delayUnfocus.Location = new System.Drawing.Point(139, 500);
            this.delayUnfocus.Name = "delayUnfocus";
            this.delayUnfocus.Size = new System.Drawing.Size(100, 20);
            this.delayUnfocus.TabIndex = 9;
            // 
            // activateUnfocus
            // 
            this.activateUnfocus.AutoSize = true;
            this.activateUnfocus.Checked = true;
            this.activateUnfocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateUnfocus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateUnfocus.Location = new System.Drawing.Point(39, 470);
            this.activateUnfocus.Name = "activateUnfocus";
            this.activateUnfocus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.activateUnfocus.Size = new System.Drawing.Size(183, 24);
            this.activateUnfocus.TabIndex = 8;
            this.activateUnfocus.Text = "Activate auto-unfocus";
            this.activateUnfocus.UseVisualStyleBackColor = true;
            this.activateUnfocus.CheckedChanged += new System.EventHandler(this.activateUnfocus_CheckedChanged);
            // 
            // ok
            // 
            this.ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok.Location = new System.Drawing.Point(140, 571);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(91, 33);
            this.ok.TabIndex = 12;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.Location = new System.Drawing.Point(245, 571);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(91, 33);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.15F);
            this.label5.Location = new System.Drawing.Point(221, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 112);
            this.label5.TabIndex = 17;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label6.Location = new System.Drawing.Point(28, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Delay (in days) :";
            // 
            // delayAutoIP
            // 
            this.delayAutoIP.Enabled = false;
            this.delayAutoIP.Location = new System.Drawing.Point(116, 86);
            this.delayAutoIP.Name = "delayAutoIP";
            this.delayAutoIP.Size = new System.Drawing.Size(100, 20);
            this.delayAutoIP.TabIndex = 15;
            // 
            // activateAutoIP
            // 
            this.activateAutoIP.AutoSize = true;
            this.activateAutoIP.Checked = true;
            this.activateAutoIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateAutoIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateAutoIP.Location = new System.Drawing.Point(39, 243);
            this.activateAutoIP.Name = "activateAutoIP";
            this.activateAutoIP.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.activateAutoIP.Size = new System.Drawing.Size(195, 24);
            this.activateAutoIP.TabIndex = 14;
            this.activateAutoIP.Text = "Activate auto IP-update";
            this.activateAutoIP.UseVisualStyleBackColor = true;
            this.activateAutoIP.CheckedChanged += new System.EventHandler(this.activateAutoIP_CheckedChanged);
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(51, 275);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(85, 13);
            this.deviceNameLabel.TabIndex = 19;
            this.deviceNameLabel.Text = "Device\'s Name :";
            // 
            // deviceName
            // 
            this.deviceName.Location = new System.Drawing.Point(139, 275);
            this.deviceName.Name = "deviceName";
            this.deviceName.Size = new System.Drawing.Size(100, 20);
            this.deviceName.TabIndex = 18;
            // 
            // tips
            // 
            this.tips.Location = new System.Drawing.Point(380, 577);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(119, 23);
            this.tips.TabIndex = 26;
            this.tips.Text = "Tips and shortcuts";
            this.tips.UseVisualStyleBackColor = true;
            this.tips.Click += new System.EventHandler(this.tips_Click);
            // 
            // minimizeToTray
            // 
            this.minimizeToTray.AutoSize = true;
            this.minimizeToTray.Checked = true;
            this.minimizeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeToTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeToTray.Location = new System.Drawing.Point(48, 80);
            this.minimizeToTray.Name = "minimizeToTray";
            this.minimizeToTray.Size = new System.Drawing.Size(383, 20);
            this.minimizeToTray.TabIndex = 28;
            this.minimizeToTray.Text = "Minimize button minimizes Remote Messages to system tray";
            this.minimizeToTray.UseVisualStyleBackColor = true;
            // 
            // closeToTray
            // 
            this.closeToTray.AutoSize = true;
            this.closeToTray.Checked = true;
            this.closeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeToTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeToTray.Location = new System.Drawing.Point(48, 57);
            this.closeToTray.Name = "closeToTray";
            this.closeToTray.Size = new System.Drawing.Size(347, 20);
            this.closeToTray.TabIndex = 29;
            this.closeToTray.Text = "Close button closes Remote Messages to system tray";
            this.closeToTray.UseVisualStyleBackColor = true;
            // 
            // escapeToTray
            // 
            this.escapeToTray.AutoSize = true;
            this.escapeToTray.Checked = true;
            this.escapeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.escapeToTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.escapeToTray.Location = new System.Drawing.Point(48, 103);
            this.escapeToTray.Name = "escapeToTray";
            this.escapeToTray.Size = new System.Drawing.Size(345, 20);
            this.escapeToTray.TabIndex = 30;
            this.escapeToTray.Text = "Escape key closes Remote Messages to system tray";
            this.escapeToTray.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(23, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 98);
            this.panel1.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(35, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 20);
            this.label9.TabIndex = 36;
            this.label9.Text = "Backgrounding";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(35, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "Notifications";
            // 
            // showBalloon
            // 
            this.showBalloon.AutoSize = true;
            this.showBalloon.Checked = true;
            this.showBalloon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBalloon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showBalloon.Location = new System.Drawing.Point(48, 178);
            this.showBalloon.Name = "showBalloon";
            this.showBalloon.Size = new System.Drawing.Size(219, 20);
            this.showBalloon.TabIndex = 38;
            this.showBalloon.Text = "Show balloon notifications in tray";
            this.showBalloon.UseVisualStyleBackColor = true;
            this.showBalloon.CheckedChanged += new System.EventHandler(this.showBalloon_CheckedChanged);
            // 
            // showFlash
            // 
            this.showFlash.AutoSize = true;
            this.showFlash.Checked = true;
            this.showFlash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showFlash.Location = new System.Drawing.Point(48, 203);
            this.showFlash.Name = "showFlash";
            this.showFlash.Size = new System.Drawing.Size(137, 20);
            this.showFlash.TabIndex = 37;
            this.showFlash.Text = "Show icon flashing";
            this.showFlash.UseVisualStyleBackColor = true;
            this.showFlash.CheckedChanged += new System.EventHandler(this.showFlash_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.flashCount);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.delayBalloon);
            this.panel2.Location = new System.Drawing.Point(23, 155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 77);
            this.panel2.TabIndex = 40;
            // 
            // flashCount
            // 
            this.flashCount.Location = new System.Drawing.Point(356, 48);
            this.flashCount.Name = "flashCount";
            this.flashCount.Size = new System.Drawing.Size(100, 20);
            this.flashCount.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(284, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 12);
            this.label11.TabIndex = 48;
            this.label11.Text = "0 = until focus";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Count:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 45;
            this.label8.Text = "Duration (in ms) :";
            // 
            // delayBalloon
            // 
            this.delayBalloon.Location = new System.Drawing.Point(356, 22);
            this.delayBalloon.Name = "delayBalloon";
            this.delayBalloon.Size = new System.Drawing.Size(100, 20);
            this.delayBalloon.TabIndex = 44;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.port);
            this.panel3.Controls.Add(this.delayAutoIP);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(23, 253);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 122);
            this.panel3.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(51, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 45;
            this.label12.Text = "RM\'s Port:";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(115, 53);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 20);
            this.port.TabIndex = 44;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(23, 393);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(480, 62);
            this.panel4.TabIndex = 42;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(23, 483);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(480, 60);
            this.panel5.TabIndex = 43;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 616);
            this.ControlBox = false;
            this.Controls.Add(this.activateAutoIP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.showBalloon);
            this.Controls.Add(this.showFlash);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.escapeToTray);
            this.Controls.Add(this.closeToTray);
            this.Controls.Add(this.minimizeToTray);
            this.Controls.Add(this.tips);
            this.Controls.Add(this.deviceNameLabel);
            this.Controls.Add(this.deviceName);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.delayUnfocus);
            this.Controls.Add(this.activateUnfocus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayReplacement);
            this.Controls.Add(this.activateReplacement);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox activateReplacement;
        private System.Windows.Forms.TextBox delayReplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox delayUnfocus;
        private System.Windows.Forms.CheckBox activateUnfocus;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox delayAutoIP;
        private System.Windows.Forms.CheckBox activateAutoIP;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.TextBox deviceName;
        private System.Windows.Forms.Button tips;
        private System.Windows.Forms.CheckBox minimizeToTray;
        private System.Windows.Forms.CheckBox closeToTray;
        private System.Windows.Forms.CheckBox escapeToTray;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox showBalloon;
        private System.Windows.Forms.CheckBox showFlash;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox flashCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox delayBalloon;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox port;
    }
}