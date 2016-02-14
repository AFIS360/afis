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
            this.dtpCriminalRecCrimeDate.Value = new DateTime(9998, 01, 01);
            this.dtpCriminalRecArrestDate.Value = new DateTime(9998, 01, 01);
            this.dtpCriminalRecSentenceDate.Value = new DateTime(9998, 01, 01);
            this.dtpCriminalRecReleaseDate.Value = new DateTime(9998, 01, 01);
            this.dtpCriminalRecParoleDate.Value = new DateTime(9998, 01, 01);
            this.btnCriminalRecEdit.Enabled = false;
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
                criminalRec.Court = !string.IsNullOrEmpty((string)comboBoxCriminalRecCourt.SelectedItem) ? (string)comboBoxCriminalRecCourt.SelectedItem : "";
                criminalRec.CourtAddress = txtBoxCriminalRecCourtAddr.Text;
                criminalRec.Statute = txtBoxCriminalRecStatute.Text;
                criminalRec.ArrestDate = Convert.ToDateTime(dtpCriminalRecArrestDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ArrestAgency = !string.IsNullOrEmpty((string)comboBoxCriminalRecArrestAgency.SelectedItem) ? (string)comboBoxCriminalRecArrestAgency.SelectedItem : "";
                criminalRec.SentencedDate = Convert.ToDateTime(dtpCriminalRecSentenceDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ReleaseDate = Convert.ToDateTime(dtpCriminalRecReleaseDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ParoleDate = Convert.ToDateTime(dtpCriminalRecParoleDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.Status = !string.IsNullOrEmpty((string)comboBoxCriminalRecStatus.SelectedItem) ? (string)comboBoxCriminalRecStatus.SelectedItem : "";
                criminalRec.CrimeDetail = richTxtBoxCriminalRecCrimeDetail.Text;
                criminalRec.CriminalAlertLevel = !string.IsNullOrEmpty((string)comboBoxCriminalRecAlertLevel.SelectedItem) ? (string)comboBoxCriminalRecAlertLevel.SelectedItem : "";
                criminalRec.CriminalAlertMsg = txtBoxCriminalRecAlertMsg.Text;
                criminalRec.RefDocLocation = txtBoxCriminalRecRefDocLoc.Text;
                criminalRec.CreatedBy = AFISMain.user.getPersonId();
                criminalRec.CreationDateTime = DateTime.Now;
                criminalRec.UpdatedBy = null;
                criminalRec.UpdateDateTime = null;

                DataAccess dataAccess = new DataAccess();
                status = dataAccess.storeCriminalRecord(criminalRec);

                Console.WriteLine("###-->> Status = " + status.getStatusDesc());

                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                }
                else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Red;
                }

                lblCriminalRecAddEditStatusMsg.Text = status.getStatusDesc();
            }
            catch (Exception exp)
            {
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine(exp);
            }
        }

        private void btnCriminalRecClear_Click(object sender, EventArgs e)
        {
            clearCriminalRecord(sender);
        }

        private void clearCriminalRecord(object source)
        {
            if (source == btnCriminalRecClear)
            {
                txtBoxCriminalRecCaseId.Text = null;
            }

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
            lblCriminalRecAddEditStatusMsg.Text = null;
            txtBoxCriminalRecRefDocLoc.Text = null;
        }

        private void txtBoxCriminalRecCaseId_Leave(object sender, EventArgs e)
        {
            string caseId = txtBoxCriminalRecCaseId.Text;
            string personId = lblCriminalRecPersonId.Text;
            CriminalRecord criminalRec = new DataAccess().getCriminalRecord(personId, caseId);
            if (criminalRec != null)
            {
                //disable the Add Record button, enable the Edit Record button
                btnCriminalRecInsert.Enabled = false;
                btnCriminalRecEdit.Enabled = true;

                txtBoxCriminalRecCrimeLoc.Text = criminalRec.CrimeLocation;
                dtpCriminalRecCrimeDate.Value = criminalRec.CrimeDate;
                comboBoxCriminalRecCourt.SelectedItem = !string.IsNullOrEmpty(criminalRec.Court) ? criminalRec.Court : "";
                txtBoxCriminalRecCourtAddr.Text = criminalRec.CourtAddress;
                txtBoxCriminalRecStatute.Text = criminalRec.Statute;
                dtpCriminalRecArrestDate.Value = criminalRec.ArrestDate;
                comboBoxCriminalRecArrestAgency.SelectedItem = !string.IsNullOrEmpty(criminalRec.ArrestAgency) ? criminalRec.ArrestAgency : "";
                dtpCriminalRecSentenceDate.Value = criminalRec.SentencedDate;
                dtpCriminalRecReleaseDate.Value = criminalRec.ReleaseDate;
                dtpCriminalRecParoleDate.Value = criminalRec.ParoleDate;
                comboBoxCriminalRecStatus.SelectedItem = !string.IsNullOrEmpty(criminalRec.Status) ? criminalRec.Status : "";
                richTxtBoxCriminalRecCrimeDetail.Text = criminalRec.CrimeDetail;
                comboBoxCriminalRecAlertLevel.Text = !string.IsNullOrEmpty(criminalRec.CriminalAlertLevel) ? criminalRec.CriminalAlertLevel : "";
                txtBoxCriminalRecAlertMsg.Text = criminalRec.CriminalAlertMsg;
                txtBoxCriminalRecRefDocLoc.Text = criminalRec.RefDocLocation;
            } else {
                clearCriminalRecord(sender);
                //Enable the Add button but disable the Edit button
                btnCriminalRecInsert.Enabled = true;
                btnCriminalRecEdit.Enabled = false;
            }
        }

        private void btnCriminalRecAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Document Files|*.*;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string docPath = ofd.FileName;
                Console.WriteLine("###-->> Selected file = " + docPath);
                txtBoxCriminalRecRefDocLoc.Text = docPath;
            }
        }//end btnCriminalRecAttachment_Click

        private void btnCriminalRecEditButton_Click(object sender, EventArgs e)
        {
            Status status = null;

            try
            {
                Console.WriteLine("###-->> Upadting criminal record...");
                CriminalRecord criminalRec = new CriminalRecord();
                criminalRec.PersonId = lblCriminalRecPersonId.Text;
                criminalRec.CrimeDetail = richTxtBoxCriminalRecCrimeDetail.Text;
                criminalRec.CrimeDate = Convert.ToDateTime(dtpCriminalRecCrimeDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.CrimeLocation = txtBoxCriminalRecCrimeLoc.Text;
                criminalRec.Court = !string.IsNullOrEmpty((string)comboBoxCriminalRecCourt.SelectedItem) ? (string)comboBoxCriminalRecCourt.SelectedItem : "";
                criminalRec.Statute = txtBoxCriminalRecStatute.Text;
                criminalRec.CourtAddress = txtBoxCriminalRecCourtAddr.Text;
                criminalRec.CaseId = txtBoxCriminalRecCaseId.Text;
                criminalRec.SentencedDate = Convert.ToDateTime(dtpCriminalRecSentenceDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ReleaseDate = Convert.ToDateTime(dtpCriminalRecReleaseDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ArrestDate = Convert.ToDateTime(dtpCriminalRecArrestDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.ArrestAgency = !string.IsNullOrEmpty((string)comboBoxCriminalRecArrestAgency.SelectedItem) ? (string)comboBoxCriminalRecArrestAgency.SelectedItem : "";
                criminalRec.Status = !string.IsNullOrEmpty((string)comboBoxCriminalRecStatus.SelectedItem) ? (string)comboBoxCriminalRecStatus.SelectedItem : "";
                criminalRec.ParoleDate = Convert.ToDateTime(dtpCriminalRecParoleDate.Value.ToString("MM/dd/yyyy"));
                criminalRec.CriminalAlertLevel = !string.IsNullOrEmpty((string)comboBoxCriminalRecAlertLevel.SelectedItem) ? (string)comboBoxCriminalRecAlertLevel.SelectedItem : "";
                criminalRec.CriminalAlertMsg = txtBoxCriminalRecAlertMsg.Text;
                criminalRec.RefDocLocation = txtBoxCriminalRecRefDocLoc.Text;
                criminalRec.CreatedBy = null;
                criminalRec.CreationDateTime = null;
                criminalRec.UpdatedBy = AFISMain.user.getPersonId();
                criminalRec.UpdateDateTime = DateTime.Now;

                status = new DataAccess().updatePersonCriminalRecord(criminalRec);
                Console.WriteLine("###-->> status = " + status.getStatusDesc());

                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                }
                else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Red;
                }

            } catch (Exception exp)
            {
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblCriminalRecAddEditStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine(exp.StackTrace);
            }
            lblCriminalRecAddEditStatusMsg.Text = status.getStatusDesc();
        }
    }
}
