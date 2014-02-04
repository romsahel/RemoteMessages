namespace RemoteMessages
{
    partial class PreferencesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.activateReplacement = new System.Windows.Forms.CheckBox();
            this.delayReplacement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.delayUnfocus = new System.Windows.Forms.TextBox();
            this.activateUnfocus = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.activateAutoIP = new System.Windows.Forms.CheckBox();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceName = new System.Windows.Forms.TextBox();
            this.minimizeToTray = new System.Windows.Forms.CheckBox();
            this.closeToTray = new System.Windows.Forms.CheckBox();
            this.escapeToTray = new System.Windows.Forms.CheckBox();
            this.panelBackgrounding = new System.Windows.Forms.Panel();
            this.showBalloon = new System.Windows.Forms.CheckBox();
            this.showFlash = new System.Windows.Forms.CheckBox();
            this.panelNotifications = new System.Windows.Forms.Panel();
            this.soundBox = new System.Windows.Forms.ComboBox();
            this.flashCount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.checkSound = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.delayBalloon = new System.Windows.Forms.TextBox();
            this.panelAutoUpdate = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.panelReplacement = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.activateGhostMode = new System.Windows.Forms.CheckBox();
            this.panelGhostMode = new System.Windows.Forms.Panel();
            this.textHotkey = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.checkWin = new System.Windows.Forms.CheckBox();
            this.checkAlt = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkCtrl = new System.Windows.Forms.CheckBox();
            this.navConversations = new System.Windows.Forms.Button();
            this.navBackgrounding = new System.Windows.Forms.Button();
            this.navNotifications = new System.Windows.Forms.Button();
            this.navIP = new System.Windows.Forms.Button();
            this.navGhostMode = new System.Windows.Forms.Button();
            this.navigationPanel = new System.Windows.Forms.Panel();
            this.panelBackgrounding.SuspendLayout();
            this.panelNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.panelAutoUpdate.SuspendLayout();
            this.panelReplacement.SuspendLayout();
            this.panelGhostMode.SuspendLayout();
            this.navigationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // activateReplacement
            // 
            this.activateReplacement.AutoSize = true;
            this.activateReplacement.Checked = true;
            this.activateReplacement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateReplacement.Location = new System.Drawing.Point(39, 32);
            this.activateReplacement.Name = "activateReplacement";
            this.activateReplacement.Size = new System.Drawing.Size(190, 20);
            this.activateReplacement.TabIndex = 1;
            this.activateReplacement.Text = "Activate Auto-Replacement";
            this.activateReplacement.UseVisualStyleBackColor = true;
            this.activateReplacement.CheckedChanged += new System.EventHandler(this.activateReplacement_CheckedChanged);
            // 
            // delayReplacement
            // 
            this.delayReplacement.Location = new System.Drawing.Point(116, 58);
            this.delayReplacement.Name = "delayReplacement";
            this.delayReplacement.Size = new System.Drawing.Size(100, 20);
            this.delayReplacement.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Delay (in ms) :";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 46);
            this.label3.TabIndex = 7;
            this.label3.Text = "Replaces the standard smileys :D by their EMOJI equivalent.";
            // 
            // label21
            // 
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(237, 95);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(224, 52);
            this.label21.TabIndex = 11;
            this.label21.Text = "Automatically unfocuses a conversation after a delay so that it is not marked as " +
                "read.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Delay (in ms) :";
            // 
            // delayUnfocus
            // 
            this.delayUnfocus.Location = new System.Drawing.Point(115, 127);
            this.delayUnfocus.Name = "delayUnfocus";
            this.delayUnfocus.Size = new System.Drawing.Size(100, 20);
            this.delayUnfocus.TabIndex = 9;
            // 
            // activateUnfocus
            // 
            this.activateUnfocus.AutoSize = true;
            this.activateUnfocus.Checked = true;
            this.activateUnfocus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateUnfocus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateUnfocus.Location = new System.Drawing.Point(39, 95);
            this.activateUnfocus.Name = "activateUnfocus";
            this.activateUnfocus.Size = new System.Drawing.Size(158, 20);
            this.activateUnfocus.TabIndex = 8;
            this.activateUnfocus.Text = "Activate Auto-Unfocus";
            this.activateUnfocus.UseVisualStyleBackColor = true;
            this.activateUnfocus.CheckedChanged += new System.EventHandler(this.activateUnfocus_CheckedChanged);
            // 
            // ok
            // 
            this.ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok.Location = new System.Drawing.Point(291, 1014);
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
            this.cancel.Location = new System.Drawing.Point(407, 1014);
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
            this.label5.Location = new System.Drawing.Point(221, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 143);
            this.label5.TabIndex = 17;
            this.label5.Text = "Automatically finds the IP address of your device based on its name.\r\nYou must en" +
                "ter the port you chose on your iDevice.\r\n(This feature might not work depending " +
                "on your network)";
            // 
            // activateAutoIP
            // 
            this.activateAutoIP.AutoSize = true;
            this.activateAutoIP.Checked = true;
            this.activateAutoIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateAutoIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateAutoIP.Location = new System.Drawing.Point(36, 26);
            this.activateAutoIP.Name = "activateAutoIP";
            this.activateAutoIP.Size = new System.Drawing.Size(179, 20);
            this.activateAutoIP.TabIndex = 14;
            this.activateAutoIP.Text = "Activate Auto-IP detection";
            this.activateAutoIP.UseVisualStyleBackColor = true;
            this.activateAutoIP.CheckedChanged += new System.EventHandler(this.activateAutoIP_CheckedChanged);
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(26, 76);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(85, 13);
            this.deviceNameLabel.TabIndex = 19;
            this.deviceNameLabel.Text = "Device\'s Name :";
            // 
            // deviceName
            // 
            this.deviceName.Location = new System.Drawing.Point(114, 76);
            this.deviceName.Name = "deviceName";
            this.deviceName.Size = new System.Drawing.Size(100, 20);
            this.deviceName.TabIndex = 18;
            // 
            // minimizeToTray
            // 
            this.minimizeToTray.AutoSize = true;
            this.minimizeToTray.Checked = true;
            this.minimizeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeToTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeToTray.Location = new System.Drawing.Point(24, 74);
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
            this.closeToTray.Location = new System.Drawing.Point(24, 35);
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
            this.escapeToTray.Location = new System.Drawing.Point(24, 115);
            this.escapeToTray.Name = "escapeToTray";
            this.escapeToTray.Size = new System.Drawing.Size(345, 20);
            this.escapeToTray.TabIndex = 30;
            this.escapeToTray.Text = "Escape key closes Remote Messages to system tray";
            this.escapeToTray.UseVisualStyleBackColor = true;
            // 
            // panelBackgrounding
            // 
            this.panelBackgrounding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBackgrounding.Controls.Add(this.minimizeToTray);
            this.panelBackgrounding.Controls.Add(this.closeToTray);
            this.panelBackgrounding.Controls.Add(this.escapeToTray);
            this.panelBackgrounding.Location = new System.Drawing.Point(153, 22);
            this.panelBackgrounding.Name = "panelBackgrounding";
            this.panelBackgrounding.Size = new System.Drawing.Size(480, 176);
            this.panelBackgrounding.TabIndex = 35;
            // 
            // showBalloon
            // 
            this.showBalloon.AutoSize = true;
            this.showBalloon.Checked = true;
            this.showBalloon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBalloon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showBalloon.Location = new System.Drawing.Point(24, 25);
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
            this.showFlash.Location = new System.Drawing.Point(24, 50);
            this.showFlash.Name = "showFlash";
            this.showFlash.Size = new System.Drawing.Size(137, 20);
            this.showFlash.TabIndex = 37;
            this.showFlash.Text = "Show icon flashing";
            this.showFlash.UseVisualStyleBackColor = true;
            this.showFlash.CheckedChanged += new System.EventHandler(this.showFlash_CheckedChanged);
            // 
            // panelNotifications
            // 
            this.panelNotifications.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNotifications.Controls.Add(this.soundBox);
            this.panelNotifications.Controls.Add(this.flashCount);
            this.panelNotifications.Controls.Add(this.label19);
            this.panelNotifications.Controls.Add(this.trackBarVolume);
            this.panelNotifications.Controls.Add(this.label11);
            this.panelNotifications.Controls.Add(this.checkSound);
            this.panelNotifications.Controls.Add(this.label10);
            this.panelNotifications.Controls.Add(this.label8);
            this.panelNotifications.Controls.Add(this.delayBalloon);
            this.panelNotifications.Controls.Add(this.showBalloon);
            this.panelNotifications.Controls.Add(this.showFlash);
            this.panelNotifications.Location = new System.Drawing.Point(153, 206);
            this.panelNotifications.Name = "panelNotifications";
            this.panelNotifications.Size = new System.Drawing.Size(480, 176);
            this.panelNotifications.TabIndex = 40;
            // 
            // soundBox
            // 
            this.soundBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.soundBox.FormattingEnabled = true;
            this.soundBox.Items.AddRange(new object[] {
            "3 Notes",
            "Calypso"});
            this.soundBox.Location = new System.Drawing.Point(140, 90);
            this.soundBox.Name = "soundBox";
            this.soundBox.Size = new System.Drawing.Size(316, 21);
            this.soundBox.TabIndex = 56;
            // 
            // flashCount
            // 
            this.flashCount.Location = new System.Drawing.Point(358, 48);
            this.flashCount.Name = "flashCount";
            this.flashCount.Size = new System.Drawing.Size(100, 20);
            this.flashCount.TabIndex = 46;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(51, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 13);
            this.label19.TabIndex = 55;
            this.label19.Text = "Volume:";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.LargeChange = 25;
            this.trackBarVolume.Location = new System.Drawing.Point(140, 114);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(316, 45);
            this.trackBarVolume.TabIndex = 54;
            this.trackBarVolume.TickFrequency = 5;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarVolume.Value = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(286, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 12);
            this.label11.TabIndex = 48;
            this.label11.Text = "0 = until focus";
            // 
            // checkSound
            // 
            this.checkSound.AutoSize = true;
            this.checkSound.Checked = true;
            this.checkSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSound.Location = new System.Drawing.Point(24, 91);
            this.checkSound.Name = "checkSound";
            this.checkSound.Size = new System.Drawing.Size(110, 20);
            this.checkSound.TabIndex = 53;
            this.checkSound.Text = "Enable sound";
            this.checkSound.UseVisualStyleBackColor = true;
            this.checkSound.CheckedChanged += new System.EventHandler(this.checkSound_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Count:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(266, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 45;
            this.label8.Text = "Duration (in ms) :";
            // 
            // delayBalloon
            // 
            this.delayBalloon.Location = new System.Drawing.Point(358, 22);
            this.delayBalloon.Name = "delayBalloon";
            this.delayBalloon.Size = new System.Drawing.Size(100, 20);
            this.delayBalloon.TabIndex = 44;
            // 
            // panelAutoUpdate
            // 
            this.panelAutoUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAutoUpdate.Controls.Add(this.label12);
            this.panelAutoUpdate.Controls.Add(this.label5);
            this.panelAutoUpdate.Controls.Add(this.port);
            this.panelAutoUpdate.Controls.Add(this.activateAutoIP);
            this.panelAutoUpdate.Controls.Add(this.deviceNameLabel);
            this.panelAutoUpdate.Controls.Add(this.deviceName);
            this.panelAutoUpdate.Location = new System.Drawing.Point(153, 398);
            this.panelAutoUpdate.Name = "panelAutoUpdate";
            this.panelAutoUpdate.Size = new System.Drawing.Size(480, 176);
            this.panelAutoUpdate.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 45;
            this.label12.Text = "RM\'s Port:";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(111, 130);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 20);
            this.port.TabIndex = 44;
            // 
            // panelReplacement
            // 
            this.panelReplacement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReplacement.Controls.Add(this.label21);
            this.panelReplacement.Controls.Add(this.label4);
            this.panelReplacement.Controls.Add(this.delayUnfocus);
            this.panelReplacement.Controls.Add(this.label3);
            this.panelReplacement.Controls.Add(this.delayReplacement);
            this.panelReplacement.Controls.Add(this.label1);
            this.panelReplacement.Controls.Add(this.activateUnfocus);
            this.panelReplacement.Controls.Add(this.activateReplacement);
            this.panelReplacement.Location = new System.Drawing.Point(154, 590);
            this.panelReplacement.Name = "panelReplacement";
            this.panelReplacement.Size = new System.Drawing.Size(480, 176);
            this.panelReplacement.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(37, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 46;
            this.label13.Text = "Password:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(116, 68);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(100, 20);
            this.password.TabIndex = 45;
            this.password.Text = "password";
            this.password.UseSystemPasswordChar = true;
            // 
            // activateGhostMode
            // 
            this.activateGhostMode.AutoSize = true;
            this.activateGhostMode.Checked = true;
            this.activateGhostMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateGhostMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateGhostMode.Location = new System.Drawing.Point(40, 24);
            this.activateGhostMode.Name = "activateGhostMode";
            this.activateGhostMode.Size = new System.Drawing.Size(147, 20);
            this.activateGhostMode.TabIndex = 44;
            this.activateGhostMode.Text = "Enable Ghost-Mode";
            this.activateGhostMode.UseVisualStyleBackColor = true;
            this.activateGhostMode.CheckedChanged += new System.EventHandler(this.activateGhostMode_CheckedChanged);
            // 
            // panelGhostMode
            // 
            this.panelGhostMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGhostMode.Controls.Add(this.label13);
            this.panelGhostMode.Controls.Add(this.activateGhostMode);
            this.panelGhostMode.Controls.Add(this.textHotkey);
            this.panelGhostMode.Controls.Add(this.password);
            this.panelGhostMode.Controls.Add(this.label15);
            this.panelGhostMode.Controls.Add(this.checkWin);
            this.panelGhostMode.Controls.Add(this.checkAlt);
            this.panelGhostMode.Controls.Add(this.label14);
            this.panelGhostMode.Controls.Add(this.checkCtrl);
            this.panelGhostMode.Location = new System.Drawing.Point(154, 842);
            this.panelGhostMode.Name = "panelGhostMode";
            this.panelGhostMode.Size = new System.Drawing.Size(480, 176);
            this.panelGhostMode.TabIndex = 47;
            // 
            // textHotkey
            // 
            this.textHotkey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textHotkey.Location = new System.Drawing.Point(116, 101);
            this.textHotkey.MaxLength = 1;
            this.textHotkey.Name = "textHotkey";
            this.textHotkey.Size = new System.Drawing.Size(100, 20);
            this.textHotkey.TabIndex = 52;
            this.textHotkey.Text = "H";
            this.textHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(37, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Hotkey:";
            // 
            // checkWin
            // 
            this.checkWin.AutoSize = true;
            this.checkWin.Location = new System.Drawing.Point(47, 132);
            this.checkWin.Name = "checkWin";
            this.checkWin.Size = new System.Drawing.Size(45, 17);
            this.checkWin.TabIndex = 48;
            this.checkWin.Text = "Win";
            this.checkWin.UseVisualStyleBackColor = true;
            // 
            // checkAlt
            // 
            this.checkAlt.AutoSize = true;
            this.checkAlt.Location = new System.Drawing.Point(173, 132);
            this.checkAlt.Name = "checkAlt";
            this.checkAlt.Size = new System.Drawing.Size(38, 17);
            this.checkAlt.TabIndex = 50;
            this.checkAlt.Text = "Alt";
            this.checkAlt.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(221, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(254, 137);
            this.label14.TabIndex = 11;
            this.label14.Text = "Quickly Hide/Show the app by pressing the hotkey of your choice. \r\nIf Ghost-Mode " +
                "is enabled, you won\'t get notifications when the window is hidden.\r\nThe hotkey c" +
                "an still be used when GM is disabled.";
            // 
            // checkCtrl
            // 
            this.checkCtrl.AutoSize = true;
            this.checkCtrl.Location = new System.Drawing.Point(110, 132);
            this.checkCtrl.Name = "checkCtrl";
            this.checkCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkCtrl.TabIndex = 49;
            this.checkCtrl.Text = "Ctrl";
            this.checkCtrl.UseVisualStyleBackColor = true;
            // 
            // navConversations
            // 
            this.navConversations.BackColor = System.Drawing.Color.White;
            this.navConversations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navConversations.Location = new System.Drawing.Point(0, 40);
            this.navConversations.Name = "navConversations";
            this.navConversations.Size = new System.Drawing.Size(133, 23);
            this.navConversations.TabIndex = 48;
            this.navConversations.Text = "Conversations";
            this.navConversations.UseVisualStyleBackColor = false;
            this.navConversations.Click += new System.EventHandler(this.navigation_Click);
            // 
            // navBackgrounding
            // 
            this.navBackgrounding.BackColor = System.Drawing.Color.White;
            this.navBackgrounding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBackgrounding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.navBackgrounding.Location = new System.Drawing.Point(0, 0);
            this.navBackgrounding.Name = "navBackgrounding";
            this.navBackgrounding.Size = new System.Drawing.Size(133, 23);
            this.navBackgrounding.TabIndex = 49;
            this.navBackgrounding.Text = "Backgrounding";
            this.navBackgrounding.UseVisualStyleBackColor = false;
            this.navBackgrounding.Click += new System.EventHandler(this.navigation_Click);
            // 
            // navNotifications
            // 
            this.navNotifications.BackColor = System.Drawing.Color.White;
            this.navNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navNotifications.Location = new System.Drawing.Point(1, 80);
            this.navNotifications.Name = "navNotifications";
            this.navNotifications.Size = new System.Drawing.Size(133, 23);
            this.navNotifications.TabIndex = 51;
            this.navNotifications.Text = "Notifications";
            this.navNotifications.UseVisualStyleBackColor = false;
            this.navNotifications.Click += new System.EventHandler(this.navigation_Click);
            // 
            // navIP
            // 
            this.navIP.BackColor = System.Drawing.Color.White;
            this.navIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navIP.Location = new System.Drawing.Point(1, 118);
            this.navIP.Name = "navIP";
            this.navIP.Size = new System.Drawing.Size(133, 23);
            this.navIP.TabIndex = 50;
            this.navIP.Text = "IP Update";
            this.navIP.UseVisualStyleBackColor = false;
            this.navIP.Click += new System.EventHandler(this.navigation_Click);
            // 
            // navGhostMode
            // 
            this.navGhostMode.BackColor = System.Drawing.Color.White;
            this.navGhostMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navGhostMode.Location = new System.Drawing.Point(1, 153);
            this.navGhostMode.Name = "navGhostMode";
            this.navGhostMode.Size = new System.Drawing.Size(133, 23);
            this.navGhostMode.TabIndex = 52;
            this.navGhostMode.Text = "Ghost Mode";
            this.navGhostMode.UseVisualStyleBackColor = false;
            this.navGhostMode.Click += new System.EventHandler(this.navigation_Click);
            // 
            // navigationPanel
            // 
            this.navigationPanel.Controls.Add(this.navGhostMode);
            this.navigationPanel.Controls.Add(this.navBackgrounding);
            this.navigationPanel.Controls.Add(this.navNotifications);
            this.navigationPanel.Controls.Add(this.navConversations);
            this.navigationPanel.Controls.Add(this.navIP);
            this.navigationPanel.Location = new System.Drawing.Point(13, 22);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.Size = new System.Drawing.Size(134, 176);
            this.navigationPanel.TabIndex = 53;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(647, 1072);
            this.Controls.Add(this.panelGhostMode);
            this.Controls.Add(this.panelNotifications);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.panelBackgrounding);
            this.Controls.Add(this.panelAutoUpdate);
            this.Controls.Add(this.panelReplacement);
            this.Controls.Add(this.navigationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.Padding = new System.Windows.Forms.Padding(10, 2, 10, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.PreferencesForm_HelpButtonClicked);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.panelBackgrounding.ResumeLayout(false);
            this.panelBackgrounding.PerformLayout();
            this.panelNotifications.ResumeLayout(false);
            this.panelNotifications.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.panelAutoUpdate.ResumeLayout(false);
            this.panelAutoUpdate.PerformLayout();
            this.panelReplacement.ResumeLayout(false);
            this.panelReplacement.PerformLayout();
            this.panelGhostMode.ResumeLayout(false);
            this.panelGhostMode.PerformLayout();
            this.navigationPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox activateReplacement;
        private System.Windows.Forms.TextBox delayReplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox delayUnfocus;
        private System.Windows.Forms.CheckBox activateUnfocus;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox activateAutoIP;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.TextBox deviceName;
        private System.Windows.Forms.CheckBox minimizeToTray;
        private System.Windows.Forms.CheckBox closeToTray;
        private System.Windows.Forms.CheckBox escapeToTray;
        private System.Windows.Forms.Panel panelBackgrounding;
        private System.Windows.Forms.CheckBox showBalloon;
        private System.Windows.Forms.CheckBox showFlash;
        private System.Windows.Forms.Panel panelNotifications;
        private System.Windows.Forms.Panel panelAutoUpdate;
        private System.Windows.Forms.Panel panelReplacement;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox flashCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox delayBalloon;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox activateGhostMode;
        private System.Windows.Forms.Panel panelGhostMode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkWin;
        private System.Windows.Forms.CheckBox checkAlt;
        private System.Windows.Forms.CheckBox checkCtrl;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textHotkey;
        private System.Windows.Forms.CheckBox checkSound;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.ComboBox soundBox;
        private System.Windows.Forms.Button navConversations;
        private System.Windows.Forms.Button navBackgrounding;
        private System.Windows.Forms.Button navNotifications;
        private System.Windows.Forms.Button navIP;
        private System.Windows.Forms.Button navGhostMode;
        private System.Windows.Forms.Panel navigationPanel;
    }
}