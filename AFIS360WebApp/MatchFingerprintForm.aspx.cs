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
    public partial class MatchFingerprintForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
            if (accessCntrl.AccessFingerprintMatching == "N") Response.Redirect("/AccessErrorPage.aspx");

            LabelLoginUserInfo.Text = Session["CurrentUser"].ToString();
            ClearFields();
        }

        protected void BtnMatchLoadFp_Click(object sender, EventArgs e)
        {
            string fpPath = FileUploadMatchFpUpload.FileName;
            FingerprintImage.ImageUrl = "/images/" + fpPath;
            FileUploadMatchFpUpload.SaveAs(Server.MapPath(@FingerprintImage.ImageUrl));
        }

        protected void BtnMatchFingerprint_Click(object sender, EventArgs e)
        {
            string imgBase64String = EncodeFile(Server.MapPath(@FingerprintImage.ImageUrl));
            MatchFingerprintSoapClient matchFpSoapClient = new MatchFingerprintSoapClient();
            Match match = matchFpSoapClient.GetMatch(FingerprintImage.ImageUrl, imgBase64String, "[Unknown]", 60);
            if(match != null && match.MatchedPerson != null)
            {
                LabelPersonID.Text = "Person ID: " + match.MatchedPerson.PersonId + "  ";

                GetPersonSoapClient getPerson = new GetPersonSoapClient();
                PersonDetail personDetail = getPerson.getPerson(match.MatchedPerson.PersonId);
                LabelPersonName.Text = "Name: " + personDetail.FirstName + " " + personDetail.LastName;
                PassportPhoto.ImageUrl = "data:image/png;base64," + personDetail.PassportPhoto;
                LabelAddress.Text = "Address: " + personDetail.StreetAddress + ", " + personDetail.City + ", " + personDetail.State + ", " + personDetail.PostalCode + ", " + personDetail.Country;
            } else
            {
                LabelPersonName.Text = "No match found.";
            }
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(File.ReadAllBytes(fileName));
        }

        private void ClearFields()
        {
            LabelPersonID.Text = null;
            LabelPersonName.Text = null;
            LabelAddress.Text = null;
            PassportPhoto.ImageUrl = null;
//            FingerprintImage.ImageUrl = null;
        }
    }
}