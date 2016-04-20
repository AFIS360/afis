using AFIS360WebApp.GetMatchServiceRef;
using AFIS360WebApp.GetPersonServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["CurrentUserRole"] != null)
            {
                AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
                if (accessCntrl.AccessFingerprintMatching == "N") Response.Redirect("/AccessErrorPage.aspx");
                ClearFields();
                TablePersonDemography.Visible = false;
                TablePersonIdentity.Visible = false;
                lblSatusMsg.Visible = false;
            } else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void BtnMatchLoadFp_Click(object sender, EventArgs e)
        {
            string fpPath = FileUploadMatchFpUpload.FileName;
            if(!string.IsNullOrEmpty(fpPath))
            {
                FingerprintImage.ImageUrl = "/images/" + fpPath;
                FileUploadMatchFpUpload.SaveAs(Server.MapPath(@FingerprintImage.ImageUrl));
            }
        }

        protected void BtnMatchFingerprint_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(FingerprintImage.ImageUrl))
            {
                string imgBase64String = EncodeFile(Server.MapPath(@FingerprintImage.ImageUrl));
                MatchFingerprintSoapClient matchFpSoapClient = new MatchFingerprintSoapClient();
                Match match = matchFpSoapClient.GetMatch(FingerprintImage.ImageUrl, imgBase64String, "[Unknown]", 60);
                if (match != null && match.MatchedPerson != null)
                {
                    TablePersonDemography.Visible = true;
                    TablePersonIdentity.Visible = true;

                    lblPersonID.Text = match.MatchedPerson.PersonId;
                    GetPersonSoapClient getPerson = new GetPersonSoapClient();
                    PersonDetail personDetail = getPerson.getPerson(match.MatchedPerson.PersonId);
                    lblPersonName.Text =  personDetail.FirstName + " " + personDetail.LastName;
                    PassportPhoto.ImageUrl = "data:image/png;base64," + personDetail.PassportPhoto;
                    lblAddress.Text = personDetail.StreetAddress + ", " + personDetail.City + ", " + personDetail.State + ", " + personDetail.PostalCode + ", " + personDetail.Country;
                }
                else
                {
                    lblSatusMsg.Visible = true;
                    lblSatusMsg.Text = "No match found.";
                }
            }
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(File.ReadAllBytes(fileName));
        }

        private void ClearFields()
        {
            lblPersonID.Text = null;
            lblPersonName.Text = null;
            lblAddress.Text = null;
            PassportPhoto.ImageUrl = null;
        }
    }
}