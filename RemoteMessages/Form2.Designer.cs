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
            this.SuspendLayout();
            // 
            // activateReplacement
            // 
            this.activateReplacement.AutoSize = true;
            this.activateReplacement.Checked = true;
            this.activateReplacement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateReplacement.Location = new System.Drawing.Point(23, 138);
            this.activateReplacement.Name = "activateReplacement";
            this.activateReplacement.Size = new System.Drawing.Size(225, 24);
            this.activateReplacement.TabIndex = 1;
            this.activateReplacement.Text = "Activate smiley-replacement";
            this.activateReplacement.UseVisualStyleBackColor = true;
            this.activateReplacement.CheckedChanged += new System.EventHandler(this.activateReplacement_CheckedChanged);
            // 
            // delayReplacement
            // 
            this.delayReplacement.Location = new System.Drawing.Point(139, 168);
            this.delayReplacement.Name = "delayReplacement";
            this.delayReplacement.Size = new System.Drawing.Size(100, 20);
            this.delayReplacement.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Delay (in ms) :";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(254, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 50);
            this.label3.TabIndex = 7;
            this.label3.Text = "Replaces the standard smileys :D by their EMOJI equivalent (delay must be greater" +
                " than loading time).";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 53);
            this.label2.TabIndex = 11;
            this.label2.Text = "Automatically unfocuses a conversation after a delay so that it is not marked as " +
                "read.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Delay (in ms) :";
            // 
            // delayUnfocus
            // 
            this.delayUnfocus.Location = new System.Drawing.Point(139, 260);
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
            this.activateUnfocus.Location = new System.Drawing.Point(23, 230);
            this.activateUnfocus.Name = "activateUnfocus";
            this.activateUnfocus.Size = new System.Drawing.Size(183, 24);
            this.activateUnfocus.TabIndex = 8;
            this.activateUnfocus.Text = "Activate auto-unfocus";
            this.activateUnfocus.UseVisualStyleBackColor = true;
            this.activateUnfocus.CheckedChanged += new System.EventHandler(this.activateUnfocus_CheckedChanged);
            // 
            // ok
            // 
            this.ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok.Location = new System.Drawing.Point(148, 352);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(91, 33);
            this.ok.TabIndex = 12;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.Location = new System.Drawing.Point(263, 352);
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
            this.label5.Location = new System.Drawing.Point(254, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 86);
            this.label5.TabIndex = 17;
            this.label5.Text = "Automatically finds the IP adress of your device based on its name.  The search w" +
                "ill be performed every X day (according to delay). Use F12 to update it manually" +
                ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Delay (in days) :";
            // 
            // delayAutoIP
            // 
            this.delayAutoIP.Location = new System.Drawing.Point(139, 90);
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
            this.activateAutoIP.Location = new System.Drawing.Point(23, 21);
            this.activateAutoIP.Name = "activateAutoIP";
            this.activateAutoIP.Size = new System.Drawing.Size(195, 24);
            this.activateAutoIP.TabIndex = 14;
            this.activateAutoIP.Text = "Activate auto IP-update";
            this.activateAutoIP.UseVisualStyleBackColor = true;
            this.activateAutoIP.CheckedChanged += new System.EventHandler(this.activateAutoIP_CheckedChanged);
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(51, 55);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(85, 13);
            this.deviceNameLabel.TabIndex = 19;
            this.deviceNameLabel.Text = "Device\'s Name :";
            // 
            // deviceName
            // 
            this.deviceName.Location = new System.Drawing.Point(139, 55);
            this.deviceName.Name = "deviceName";
            this.deviceName.Size = new System.Drawing.Size(100, 20);
            this.deviceName.TabIndex = 18;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 397);
            this.ControlBox = false;
            this.Controls.Add(this.deviceNameLabel);
            this.Controls.Add(this.deviceName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.delayAutoIP);
            this.Controls.Add(this.activateAutoIP);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.delayUnfocus);
            this.Controls.Add(this.activateUnfocus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayReplacement);
            this.Controls.Add(this.activateReplacement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remote Messages : Options";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
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
    }
}