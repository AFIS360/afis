using AFIS360Common;
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
    public partial class UserPref : Form
    {
        private ActivityLog activityLog;


        public UserPref(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
            loadUserPref();
        }

        private void loadUserPref()
        {
            chkBoxUserPrefDupCheck.Checked = AFISMain.appConfig.isDupCheck();
        }

        private void btnUserPrefSave_Click(object sender, EventArgs e)
        {
            AFISMain.appConfig.DupCheck = chkBoxUserPrefDupCheck.Checked == true ? "Y" : "N";
            Status status =  new DataAccess().updateUserPref(AFISMain.appConfig);
            Console.WriteLine("###-->> Status = " + status.getStatusDesc());
            lblUserPrefStatus.Text = status.getStatusDesc();
        }

        private void btnUserPrefClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
