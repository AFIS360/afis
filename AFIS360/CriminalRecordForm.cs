using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            this.lblCriminalRecPersonId.Text = personDetail.getPersonId();
            this.lblCriminalRecFName.Text = personDetail.getFirstName();
            this.lblCriminalRecLName.Text = personDetail.getLastName();
        }

        private void exitToolStripMenuItemCriminalRec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCriminalRecClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCriminalRecInsert_Click(object sender, EventArgs e)
        {
            Status status = null;

            try
            {
                if(string.IsNullOrEmpty(txtBoxCriminalRecCaseId.Text))
                {
                    MessageBox.Show("Case Nbr field is required, cannot be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Console.WriteLine("###-->> Adding Criminal Record....");
                CriminalRecord criminalRec = new CriminalRecord();
                criminalRec.PersonId = lblCriminalRecPersonId.Text;
                criminalRec.CaseId = txtBoxCriminalRecCaseId.Text;
                criminalRec.CrimeLocation = txtBoxCriminalRecCrimeLoc.Text;
                criminalRec.CrimeDate = Convert.ToDateTime(dtpCriminalRecCrimeDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.Court = (string)comboBoxCriminalRecCourt.SelectedItem;
                criminalRec.CourtyAddress = txtBoxCriminalRecCourtAddr.Text;
                criminalRec.Statute = txtBoxCriminalRecStatute.Text;
                criminalRec.ArrestDate = Convert.ToDateTime(dtpCriminalRecArrestDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ArrestAgency = (string)comboBoxCriminalRecArrestAgency.SelectedItem;
                criminalRec.SentencedDate = Convert.ToDateTime(dtpCriminalRecSentenceDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ReleaseDate = Convert.ToDateTime(dtpCriminalRecReleaseDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ParoleDate = Convert.ToDateTime(dtpCriminalRecParoleDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.Status = (string)comboBoxCriminalRecStatus.SelectedItem;
                criminalRec.CrimeDetail = richTxtBoxCriminalRecCrimeDetail.Text;
                criminalRec.CriminalAlertLevel = (string)comboBoxCriminalRecAlertLevel.SelectedItem;
                criminalRec.CriminalAlertMsg = txtBoxCriminalRecAlertMsg.Text;
                criminalRec.CreatedBy = AFISMain.user.getPersonId();
                criminalRec.CreationDateTime = DateTime.Now;
                criminalRec.UpdatedBy = null;
                criminalRec.UpdateDateTime = null;

                DataAccess dataAccess = new DataAccess();
                status = dataAccess.storeCriminalRecord(criminalRec);

                Console.WriteLine("###-->> Status = " + status.getStatusDesc());

                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblCriminalRecAddStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                }
                else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblCriminalRecAddStatusMsg.ForeColor = System.Drawing.Color.Red;
                }

                lblCriminalRecAddStatusMsg.Text = status.getStatusDesc();
            }
            catch (Exception exp)
            {
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblCriminalRecAddStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine(exp);
            }
        }

        private void btnCriminalRecClear_Click(object sender, EventArgs e)
        {
            txtBoxCriminalRecCaseId.Text = null;
            txtBoxCriminalRecCrimeLoc.Text = null;
            dtpCriminalRecCrimeDate.Value = DateTime.Now;
            comboBoxCriminalRecCourt.SelectedItem = null;
            txtBoxCriminalRecCourtAddr.Text = null;
            txtBoxCriminalRecStatute.Text = null;
            dtpCriminalRecArrestDate.Value = DateTime.Now;
            comboBoxCriminalRecArrestAgency.SelectedItem = null;
            dtpCriminalRecSentenceDate.Value = DateTime.Now;
            dtpCriminalRecReleaseDate.Value = DateTime.Now;
            dtpCriminalRecParoleDate.Value = DateTime.Now;
            comboBoxCriminalRecStatus.SelectedItem = null;
            richTxtBoxCriminalRecCrimeDetail.Text = null;
            comboBoxCriminalRecAlertLevel.SelectedItem = null;
            txtBoxCriminalRecAlertMsg.Text = null;
            lblCriminalRecAddStatusMsg.Text = null;
        }
    }
}
