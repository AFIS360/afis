using AFIS360WebApp.PersonCriminalRecordServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUserRole"] != null)
            {
                AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
                if (accessCntrl.AccessEnrollment == "Y")
                {
                    String caseNo = Request.QueryString["CaseNo"];
                    displayCaseDetail(caseNo);
                }
                else
                {
                    Response.Redirect("/AccessErrorPage.aspx");
                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        private void displayCaseDetail(string caseNo)
        {
            CriminalRecord[] criminalRecords = (CriminalRecord[])Session["CriminalRecords"];
            if (criminalRecords != null)
            {
                foreach (CriminalRecord criminalRecord in criminalRecords)
                {
                    if (criminalRecord.CaseId == caseNo)
                    {
                        lblCaseID.Text = caseNo;
                        lblCrimeDate.Text = criminalRecord.CrimeDate != null ? criminalRecord.CrimeDate.ToString("MM/dd/yyyy") : "";
                        lblCrimeLoc.Text = criminalRecord.CrimeLocation;
                        lblCourtType.Text = criminalRecord.Court;
                        lblCourtAddress.Text = criminalRecord.CourtAddress;
                        lblStatute.Text = criminalRecord.Statute;
                        lblArrestDate.Text = criminalRecord.ArrestDate != null ? criminalRecord.ArrestDate.ToString("MM/dd/yyyy") : "";
                        lblArrestAgency.Text = criminalRecord.ArrestAgency;
                        lblSentenceDate.Text = criminalRecord.SentencedDate != null ? criminalRecord.SentencedDate.ToString("MM/dd/yyyy") : "";
                        lblReleaseDate.Text = criminalRecord.ReleaseDate != null ? criminalRecord.ReleaseDate.ToString("MM/dd/yyyy") : "";
                        lblParoleDate.Text = criminalRecord.ParoleDate != null ? criminalRecord.ParoleDate.ToString("MM/dd/yyyy") : "";
                        lblCurrentStatus.Text = criminalRecord.Status;
                        lblAlertLevel.Text = criminalRecord.CriminalAlertLevel;
                        lblAlertMessage.Text = criminalRecord.CriminalAlertMsg;
                        lblCrimeDescriptionText.Text = criminalRecord.CrimeDetail;
                    }
                }
            }
        }
    }
}