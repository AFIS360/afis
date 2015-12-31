namespace AFIS360
{
    partial class PasswordChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordChange));
            this.lblChangePassCurrentPass = new System.Windows.Forms.Label();
            this.lblChangePassNewPass = new System.Windows.Forms.Label();
            this.txtChangePassCurrentPass = new System.Windows.Forms.TextBox();
            this.txtChangePassNewPass = new System.Windows.Forms.TextBox();
            this.lblChangePassUserId = new System.Windows.Forms.Label();
            this.txtChangePassUserId = new System.Windows.Forms.TextBox();
            this.btnChangePassSubmit = new System.Windows.Forms.Button();
            this.btnChangePassClose = new System.Windows.Forms.Button();
            this.lblPasswordChangeStatusMsg = new System.Windows.Forms.Label();
            this.grpBoxChangePass = new System.Windows.Forms.GroupBox();
            this.grpBoxChangePass.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChangePassCurrentPass
            // 
            this.lblChangePassCurrentPass.AutoSize = true;
            this.lblChangePassCurrentPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePassCurrentPass.Location = new System.Drawing.Point(51, 79);
            this.lblChangePassCurrentPass.Name = "lblChangePassCurrentPass";
            this.lblChangePassCurrentPass.Size = new System.Drawing.Size(124, 16);
            this.lblChangePassCurrentPass.TabIndex = 0;
            this.lblChangePassCurrentPass.Text = "Current Password *:";
            // 
            // lblChangePassNewPass
            // 
            this.lblChangePassNewPass.AutoSize = true;
            this.lblChangePassNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePassNewPass.Location = new System.Drawing.Point(66, 114);
            this.lblChangePassNewPass.Name = "lblChangePassNewPass";
            this.lblChangePassNewPass.Size = new System.Drawing.Size(109, 16);
            this.lblChangePassNewPass.TabIndex = 1;
            this.lblChangePassNewPass.Text = "New Password *:";
            // 
            // txtChangePassCurrentPass
            // 
            this.txtChangePassCurrentPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChangePassCurrentPass.Location = new System.Drawing.Point(186, 73);
            this.txtChangePassCurrentPass.Name = "txtChangePassCurrentPass";
            this.txtChangePassCurrentPass.Size = new System.Drawing.Size(141, 22);
            this.txtChangePassCurrentPass.TabIndex = 2;
            this.txtChangePassCurrentPass.UseSystemPasswordChar = true;
            // 
            // txtChangePassNewPass
            // 
            this.txtChangePassNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChangePassNewPass.Location = new System.Drawing.Point(186, 108);
            this.txtChangePassNewPass.Name = "txtChangePassNewPass";
            this.txtChangePassNewPass.Size = new System.Drawing.Size(141, 22);
            this.txtChangePassNewPass.TabIndex = 3;
            this.txtChangePassNewPass.UseSystemPasswordChar = true;
            // 
            // lblChangePassUserId
            // 
            this.lblChangePassUserId.AutoSize = true;
            this.lblChangePassUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePassUserId.Location = new System.Drawing.Point(93, 47);
            this.lblChangePassUserId.Name = "lblChangePassUserId";
            this.lblChangePassUserId.Size = new System.Drawing.Size(82, 16);
            this.lblChangePassUserId.TabIndex = 4;
            this.lblChangePassUserId.Text = "Username *:";
            // 
            // txtChangePassUserId
            // 
            this.txtChangePassUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChangePassUserId.Location = new System.Drawing.Point(186, 41);
            this.txtChangePassUserId.Name = "txtChangePassUserId";
            this.txtChangePassUserId.Size = new System.Drawing.Size(141, 22);
            this.txtChangePassUserId.TabIndex = 1;
            // 
            // btnChangePassSubmit
            // 
            this.btnChangePassSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassSubmit.Location = new System.Drawing.Point(171, 203);
            this.btnChangePassSubmit.Name = "btnChangePassSubmit";
            this.btnChangePassSubmit.Size = new System.Drawing.Size(75, 26);
            this.btnChangePassSubmit.TabIndex = 4;
            this.btnChangePassSubmit.Text = "Submit";
            this.btnChangePassSubmit.UseVisualStyleBackColor = true;
            this.btnChangePassSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnChangePassClose
            // 
            this.btnChangePassClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassClose.Location = new System.Drawing.Point(252, 203);
            this.btnChangePassClose.Name = "btnChangePassClose";
            this.btnChangePassClose.Size = new System.Drawing.Size(75, 26);
            this.btnChangePassClose.TabIndex = 5;
            this.btnChangePassClose.Text = "Close";
            this.btnChangePassClose.UseVisualStyleBackColor = true;
            this.btnChangePassClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPasswordChangeStatusMsg
            // 
            this.lblPasswordChangeStatusMsg.AutoSize = true;
            this.lblPasswordChangeStatusMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordChangeStatusMsg.Location = new System.Drawing.Point(59, 153);
            this.lblPasswordChangeStatusMsg.Name = "lblPasswordChangeStatusMsg";
            this.lblPasswordChangeStatusMsg.Size = new System.Drawing.Size(0, 16);
            this.lblPasswordChangeStatusMsg.TabIndex = 8;
            // 
            // grpBoxChangePass
            // 
            this.grpBoxChangePass.Controls.Add(this.lblChangePassUserId);
            this.grpBoxChangePass.Controls.Add(this.txtChangePassUserId);
            this.grpBoxChangePass.Controls.Add(this.lblPasswordChangeStatusMsg);
            this.grpBoxChangePass.Controls.Add(this.lblChangePassCurrentPass);
            this.grpBoxChangePass.Controls.Add(this.btnChangePassClose);
            this.grpBoxChangePass.Controls.Add(this.txtChangePassCurrentPass);
            this.grpBoxChangePass.Controls.Add(this.btnChangePassSubmit);
            this.grpBoxChangePass.Controls.Add(this.lblChangePassNewPass);
            this.grpBoxChangePass.Controls.Add(this.txtChangePassNewPass);
            this.grpBoxChangePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxChangePass.Location = new System.Drawing.Point(24, 26);
            this.grpBoxChangePass.Name = "grpBoxChangePass";
            this.grpBoxChangePass.Size = new System.Drawing.Size(471, 248);
            this.grpBoxChangePass.TabIndex = 25;
            this.grpBoxChangePass.TabStop = false;
            this.grpBoxChangePass.Text = "Change Password";
            // 
            // PasswordChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 305);
            this.Controls.Add(this.grpBoxChangePass);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PasswordChange";
            this.Text = "Password Change";
            this.grpBoxChangePass.ResumeLayout(false);
            this.grpBoxChangePass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblChangePassCurrentPass;
        private System.Windows.Forms.Label lblChangePassNewPass;
        private System.Windows.Forms.TextBox txtChangePassCurrentPass;
        private System.Windows.Forms.TextBox txtChangePassNewPass;
        private System.Windows.Forms.Label lblChangePassUserId;
        private System.Windows.Forms.TextBox txtChangePassUserId;
        private System.Windows.Forms.Button btnChangePassSubmit;
        private System.Windows.Forms.Button btnChangePassClose;
        private System.Windows.Forms.Label lblPasswordChangeStatusMsg;
        private System.Windows.Forms.GroupBox grpBoxChangePass;
    }
}