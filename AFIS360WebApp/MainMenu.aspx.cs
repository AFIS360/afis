using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
/*
            if (Session["CurrentUser"] != null)
            {
                AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
                ApplyAccessControl(accessCntrl);
            }
*/
        }
/*
        private void ApplyAccessControl(AccessControl accessCntrl)
        {
            if (accessCntrl.AccessEnrollment == "Y")
            {
                HyperLinkSearhPerson.Visible = true;
                ImageSearchIcon.Visible = true;
            }
            else
            {
                HyperLinkSearhPerson.Visible = false;
                ImageSearchIcon.Visible = false;
            }

            if (accessCntrl.AccessFingerprintMatching == "Y")
            {
                HyperLinkMatchFingerprint.Visible = true;
                ImageFingerprintMatchIcon.Visible = true;
            }
            else
            {
                HyperLinkMatchFingerprint.Visible = false;
                ImageFingerprintMatchIcon.Visible = false;
            }
        }
*/
    }
}