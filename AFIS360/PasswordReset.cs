﻿using AFIS360Common;
using AFIS360Common.dao;
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
        private ActivityLog activityLog;

        public PasswordReset(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnResetPassSubmit_Click(object sender, EventArgs e)
        {
            string userId = txtResetPassUserId.Text;
            if (userId != null && userId.Length == 0)
            {
                MessageBox.Show("User ID field can't be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tempPassword = txtResetPassTempPass.Text;
            if (tempPassword != null && tempPassword.Length == 0)
            {
                MessageBox.Show("New Password field can't be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Retset the password in DB
            DataAccess dataAccess = new DataAccess();
            Status status = dataAccess.resetAFISUserPassword(userId, tempPassword);

            if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
            {
                lblResetPassStatusMsg.Text = null; //Clear the previous message first.
                lblResetPassStatusMsg.ForeColor = System.Drawing.Color.Green;
                lblResetPassStatusMsg.Text = status.getStatusDesc();
                activityLog.setActivity("Password reset for user (" + userId + ") is successfully. \n");
            }
            else
            {
                lblResetPassStatusMsg.Text = null; //Clear the previous message first.
                lblResetPassStatusMsg.ForeColor = System.Drawing.Color.Red;
                lblResetPassStatusMsg.Text = status.getStatusDesc();
                activityLog.setActivity("Password reset for user (" + userId + ") is unsuccessfully.\n");
            }

        }

        private void btnResetPassClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
