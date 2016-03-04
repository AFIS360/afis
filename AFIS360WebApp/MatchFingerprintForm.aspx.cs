using AFIS360WebApp.GetMatchServiceRef;
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

        }

        protected void BtnMatchLoadFp_Click(object sender, EventArgs e)
        {
            string fpPath = FileUploadMatchFpUpload.FileName;
            TextBox1.Text = fpPath;
            fingerprintImage.Height = 300;
            fingerprintImage.Width = 300;
            fingerprintImage.ImageUrl = "/images/" + fpPath;
        }

        protected void BtnMatchFingerprint_Click(object sender, EventArgs e)
        {
            string imgBase64String = EncodeFile(Server.MapPath(@fingerprintImage.ImageUrl));
            TextBox1.Text = imgBase64String;

            System.Diagnostics.Debug.Write("imgBase64String = " + imgBase64String);
            System.Diagnostics.Debug.Write("fingerprintImage.ImageUrl = " + fingerprintImage.ImageUrl);

            MatchFingerprintSoapClient matchFpSoapClient = new MatchFingerprintSoapClient();
            Match match = matchFpSoapClient.GetMatch(fingerprintImage.ImageUrl, imgBase64String, "[Unknown]", 60);
            TextBox1.Text = match.MatchedPerson.PersonId;
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(File.ReadAllBytes(fileName));
        }
    }
}