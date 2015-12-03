using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class PasswordReset : Form
    {
        public PasswordReset()
        {
            InitializeComponent();
        }

        private void btnResetPassSubmit_Click(object sender, EventArgs e)
        {
            string userId = txtResetPassUserId.Text;
            if (userId != null && userId.Length == 0) MessageBox.Show("User ID field can't be empty.");
            string tempPassword = txtResetPassTempPass.Text;
            if (tempPassword != null && tempPassword.Length == 0) MessageBox.Show("New Password field can't be empty.");

            DataAccess dataAccess = new DataAccess();
            Status status = dataAccess.resetAFISUserPassword(userId, tempPassword);
            if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
            {
                lblResetPassStatusMsg.ForeColor = System.Drawing.Color.Green;
                lblResetPassStatusMsg.Text = status.getStatusDesc();
            }
            else
            {
                lblResetPassStatusMsg.ForeColor = System.Drawing.Color.Red;
                lblResetPassStatusMsg.Text = status.getStatusDesc();
            }

        }

        private void btnResetPassClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
