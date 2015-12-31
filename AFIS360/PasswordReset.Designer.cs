namespace AFIS360
{
    partial class PasswordReset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordReset));
            this.grpBoxResetPass = new System.Windows.Forms.GroupBox();
            this.lblResetPassUserId = new System.Windows.Forms.Label();
            this.txtResetPassUserId = new System.Windows.Forms.TextBox();
            this.lblResetPassStatusMsg = new System.Windows.Forms.Label();
            this.btnResetPassClose = new System.Windows.Forms.Button();
            this.btnResetPassSubmit = new System.Windows.Forms.Button();
            this.lblRestPassTempPass = new System.Windows.Forms.Label();
            this.txtResetPassTempPass = new System.Windows.Forms.TextBox();
            this.grpBoxResetPass.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxResetPass
            // 
            this.grpBoxResetPass.Controls.Add(this.lblResetPassUserId);
            this.grpBoxResetPass.Controls.Add(this.txtResetPassUserId);
            this.grpBoxResetPass.Controls.Add(this.lblResetPassStatusMsg);
            this.grpBoxResetPass.Controls.Add(this.btnResetPassClose);
            this.grpBoxResetPass.Controls.Add(this.btnResetPassSubmit);
            this.grpBoxResetPass.Controls.Add(this.lblRestPassTempPass);
            this.grpBoxResetPass.Controls.Add(this.txtResetPassTempPass);
            this.grpBoxResetPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxResetPass.Location = new System.Drawing.Point(15, 23);
            this.grpBoxResetPass.Name = "grpBoxResetPass";
            this.grpBoxResetPass.Size = new System.Drawing.Size(471, 246);
            this.grpBoxResetPass.TabIndex = 26;
            this.grpBoxResetPass.TabStop = false;
            this.grpBoxResetPass.Text = "Reset Password";
            // 
            // lblResetPassUserId
            // 
            this.lblResetPassUserId.AutoSize = true;
            this.lblResetPassUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResetPassUserId.Location = new System.Drawing.Point(93, 70);
            this.lblResetPassUserId.Name = "lblResetPassUserId";
            this.lblResetPassUserId.Size = new System.Drawing.Size(82, 16);
            this.lblResetPassUserId.TabIndex = 4;
            this.lblResetPassUserId.Text = "Username *:";
            // 
            // txtResetPassUserId
            // 
            this.txtResetPassUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResetPassUserId.Location = new System.Drawing.Point(186, 64);
            this.txtResetPassUserId.Name = "txtResetPassUserId";
            this.txtResetPassUserId.Size = new System.Drawing.Size(141, 22);
            this.txtResetPassUserId.TabIndex = 1;
            // 
            // lblResetPassStatusMsg
            // 
            this.lblResetPassStatusMsg.AutoSize = true;
            this.lblResetPassStatusMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResetPassStatusMsg.Location = new System.Drawing.Point(61, 145);
            this.lblResetPassStatusMsg.Name = "lblResetPassStatusMsg";
            this.lblResetPassStatusMsg.Size = new System.Drawing.Size(0, 16);
            this.lblResetPassStatusMsg.TabIndex = 8;
            // 
            // btnResetPassClose
            // 
            this.btnResetPassClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPassClose.Location = new System.Drawing.Point(252, 203);
            this.btnResetPassClose.Name = "btnResetPassClose";
            this.btnResetPassClose.Size = new System.Drawing.Size(75, 26);
            this.btnResetPassClose.TabIndex = 5;
            this.btnResetPassClose.Text = "Close";
            this.btnResetPassClose.UseVisualStyleBackColor = true;
            this.btnResetPassClose.Click += new System.EventHandler(this.btnResetPassClose_Click);
            // 
            // btnResetPassSubmit
            // 
            this.btnResetPassSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPassSubmit.Location = new System.Drawing.Point(171, 203);
            this.btnResetPassSubmit.Name = "btnResetPassSubmit";
            this.btnResetPassSubmit.Size = new System.Drawing.Size(75, 26);
            this.btnResetPassSubmit.TabIndex = 4;
            this.btnResetPassSubmit.Text = "Submit";
            this.btnResetPassSubmit.UseVisualStyleBackColor = true;
            this.btnResetPassSubmit.Click += new System.EventHandler(this.btnResetPassSubmit_Click);
            // 
            // lblRestPassTempPass
            // 
            this.lblRestPassTempPass.AutoSize = true;
            this.lblRestPassTempPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestPassTempPass.Location = new System.Drawing.Point(57, 104);
            this.lblRestPassTempPass.Name = "lblRestPassTempPass";
            this.lblRestPassTempPass.Size = new System.Drawing.Size(118, 16);
            this.lblRestPassTempPass.TabIndex = 1;
            this.lblRestPassTempPass.Text = "Temp Password *:";
            // 
            // txtResetPassTempPass
            // 
            this.txtResetPassTempPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResetPassTempPass.Location = new System.Drawing.Point(186, 98);
            this.txtResetPassTempPass.Name = "txtResetPassTempPass";
            this.txtResetPassTempPass.Size = new System.Drawing.Size(141, 22);
            this.txtResetPassTempPass.TabIndex = 3;
            this.txtResetPassTempPass.UseSystemPasswordChar = true;
            // 
            // PasswordReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 301);
            this.Controls.Add(this.grpBoxResetPass);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PasswordReset";
            this.Text = "Password Reset";
            this.grpBoxResetPass.ResumeLayout(false);
            this.grpBoxResetPass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxResetPass;
        private System.Windows.Forms.Label lblResetPassUserId;
        private System.Windows.Forms.TextBox txtResetPassUserId;
        private System.Windows.Forms.Label lblResetPassStatusMsg;
        private System.Windows.Forms.Button btnResetPassClose;
        private System.Windows.Forms.Button btnResetPassSubmit;
        private System.Windows.Forms.Label lblRestPassTempPass;
        private System.Windows.Forms.TextBox txtResetPassTempPass;
    }
}