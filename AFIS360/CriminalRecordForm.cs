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
    public partial class CriminalRecordForm : Form
    {
        private PersonDetail personDetail;
        private ActivityLog activityLog;

        public CriminalRecordForm(ActivityLog activityLog, PersonDetail personDetail)
        {
            InitializeComponent();
            this.activityLog = activityLog;
            this.personDetail = personDetail;
        }

        private void CriminalRecordForm_Load(object sender, EventArgs e)
        {
            this.lblCriminalRecPersonIdTxt.Text = personDetail.getPersonId();
            this.lblCriminalRecFName.Text = personDetail.getFirstName();
            this.lblCriminalRecLName.Text = personDetail.getLastName();
        }
    }
}
