using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class PasswordChange : Form
    {
        private ActivityLog activityLog;

        public PasswordChange(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string userId = txtChangePassUserId.Text;
            if (userId != null && userId.Length == 0)
            {
                MessageBox.Show("User ID field can't be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string currentPassword = txtChangePassCurrentPass.Text;
            if (currentPassword != null && currentPassword.Length == 0)
            {
                MessageBox.Show("Current Password field can't be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string newPassword = txtChangePassNewPass.Text;
            if (newPassword != null && newPassword.Length == 0)
            {
                MessageBox.Show("New Password field can't be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Change the password in DB
            DataAccess dataAccess = new DataAccess();
            Status status = dataAccess.updateAFISUserPassword(userId, currentPassword, newPassword);

            if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
            {
                lblPasswordChangeStatusMsg.Text = null;
                lblPasswordChangeStatusMsg.ForeColor = System.Drawing.Color.Green;
                lblPasswordChangeStatusMsg.Text = status.getStatusDesc();
                if (activityLog != null) activityLog.setActivity("Password change for user (" + userId + ") is successful. \n");
            }
            else
            {
                lblPasswordChangeStatusMsg.Text = null;
                lblPasswordChangeStatusMsg.ForeColor = System.Drawing.Color.Red;
                lblPasswordChangeStatusMsg.Text = status.getStatusDesc();
                if (activityLog != null) activityLog.setActivity("Password change for user (" + userId + ") is not successful. \n");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
