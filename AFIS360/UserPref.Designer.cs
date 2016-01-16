namespace AFIS360
{
    partial class UserPref
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPref));
            this.chkBoxUserPrefDupCheck = new System.Windows.Forms.CheckBox();
            this.tlpUserPref = new System.Windows.Forms.TableLayoutPanel();
            this.btnUserPrefSave = new System.Windows.Forms.Button();
            this.btnUserPrefClose = new System.Windows.Forms.Button();
            this.lblUserPrefStatus = new System.Windows.Forms.Label();
            this.tlpUserPref.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBoxUserPrefDupCheck
            // 
            this.chkBoxUserPrefDupCheck.AutoSize = true;
            this.chkBoxUserPrefDupCheck.Location = new System.Drawing.Point(3, 3);
            this.chkBoxUserPrefDupCheck.Name = "chkBoxUserPrefDupCheck";
            this.chkBoxUserPrefDupCheck.Size = new System.Drawing.Size(622, 29);
            this.chkBoxUserPrefDupCheck.TabIndex = 0;
            this.chkBoxUserPrefDupCheck.Text = "Perform duplicate check on each fingerprint insert or update.";
            this.chkBoxUserPrefDupCheck.UseVisualStyleBackColor = true;
            // 
            // tlpUserPref
            // 
            this.tlpUserPref.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tlpUserPref.ColumnCount = 1;
            this.tlpUserPref.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUserPref.Controls.Add(this.chkBoxUserPrefDupCheck, 0, 0);
            this.tlpUserPref.Location = new System.Drawing.Point(50, 55);
            this.tlpUserPref.Name = "tlpUserPref";
            this.tlpUserPref.RowCount = 2;
            this.tlpUserPref.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUserPref.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUserPref.Size = new System.Drawing.Size(724, 100);
            this.tlpUserPref.TabIndex = 1;
            // 
            // btnUserPrefSave
            // 
            this.btnUserPrefSave.Location = new System.Drawing.Point(355, 566);
            this.btnUserPrefSave.Name = "btnUserPrefSave";
            this.btnUserPrefSave.Size = new System.Drawing.Size(129, 57);
            this.btnUserPrefSave.TabIndex = 2;
            this.btnUserPrefSave.Text = "Save";
            this.btnUserPrefSave.UseVisualStyleBackColor = true;
            this.btnUserPrefSave.Click += new System.EventHandler(this.btnUserPrefSave_Click);
            // 
            // btnUserPrefClose
            // 
            this.btnUserPrefClose.Location = new System.Drawing.Point(499, 566);
            this.btnUserPrefClose.Name = "btnUserPrefClose";
            this.btnUserPrefClose.Size = new System.Drawing.Size(129, 57);
            this.btnUserPrefClose.TabIndex = 3;
            this.btnUserPrefClose.Text = "Close";
            this.btnUserPrefClose.UseVisualStyleBackColor = true;
            this.btnUserPrefClose.Click += new System.EventHandler(this.btnUserPrefClose_Click);
            // 
            // lblUserPrefStatus
            // 
            this.lblUserPrefStatus.AutoSize = true;
            this.lblUserPrefStatus.Location = new System.Drawing.Point(50, 439);
            this.lblUserPrefStatus.Name = "lblUserPrefStatus";
            this.lblUserPrefStatus.Size = new System.Drawing.Size(79, 25);
            this.lblUserPrefStatus.TabIndex = 4;
            this.lblUserPrefStatus.Text = "Status:";
            // 
            // UserPref
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 928);
            this.Controls.Add(this.lblUserPrefStatus);
            this.Controls.Add(this.btnUserPrefClose);
            this.Controls.Add(this.btnUserPrefSave);
            this.Controls.Add(this.tlpUserPref);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserPref";
            this.Text = " User Preference";
            this.tlpUserPref.ResumeLayout(false);
            this.tlpUserPref.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBoxUserPrefDupCheck;
        private System.Windows.Forms.TableLayoutPanel tlpUserPref;
        private System.Windows.Forms.Button btnUserPrefSave;
        private System.Windows.Forms.Button btnUserPrefClose;
        private System.Windows.Forms.Label lblUserPrefStatus;
    }
}