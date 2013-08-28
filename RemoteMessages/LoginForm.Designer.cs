namespace RemoteMessages
{
    partial class LoginForm
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.OKbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.ghostmodePassword = new System.Windows.Forms.TextBox();
            this.ghostmodeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelMessage.Location = new System.Drawing.Point(13, 13);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(308, 44);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "The application requires an authentication. Please enter a username and a passwor" +
                "d.";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(92, 158);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 3;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbutton.Location = new System.Drawing.Point(186, 158);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 4;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.Color.Gray;
            this.username.Location = new System.Drawing.Point(92, 77);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(169, 22);
            this.username.TabIndex = 1;
            this.username.Text = "Username";
            this.username.Enter += new System.EventHandler(this.username_Enter);
            this.username.Leave += new System.EventHandler(this.username_Leave);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.ForeColor = System.Drawing.Color.Gray;
            this.password.Location = new System.Drawing.Point(92, 109);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(169, 22);
            this.password.TabIndex = 2;
            this.password.Text = "Password";
            this.password.Enter += new System.EventHandler(this.password_Enter);
            this.password.Leave += new System.EventHandler(this.password_Leave);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUser.Location = new System.Drawing.Point(12, 80);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(74, 16);
            this.labelUser.TabIndex = 5;
            this.labelUser.Text = "Username:";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPass.Location = new System.Drawing.Point(12, 112);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(71, 16);
            this.labelPass.TabIndex = 6;
            this.labelPass.Text = "Password:";
            // 
            // ghostmodePassword
            // 
            this.ghostmodePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ghostmodePassword.Location = new System.Drawing.Point(59, 99);
            this.ghostmodePassword.Name = "ghostmodePassword";
            this.ghostmodePassword.Size = new System.Drawing.Size(229, 26);
            this.ghostmodePassword.TabIndex = 7;
            this.ghostmodePassword.UseSystemPasswordChar = true;
            // 
            // ghostmodeLabel
            // 
            this.ghostmodeLabel.AutoSize = true;
            this.ghostmodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ghostmodeLabel.Location = new System.Drawing.Point(66, 73);
            this.ghostmodeLabel.Name = "ghostmodeLabel";
            this.ghostmodeLabel.Size = new System.Drawing.Size(212, 20);
            this.ghostmodeLabel.TabIndex = 8;
            this.ghostmodeLabel.Text = "Please, enter your password:";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.OKbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancelbutton;
            this.ClientSize = new System.Drawing.Size(333, 193);
            this.Controls.Add(this.ghostmodeLabel);
            this.Controls.Add(this.ghostmodePassword);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.labelMessage);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.TextBox ghostmodePassword;
        private System.Windows.Forms.Label ghostmodeLabel;
    }
}