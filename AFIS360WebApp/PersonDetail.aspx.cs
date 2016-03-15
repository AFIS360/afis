using AFIS360WebApp.GetPersonServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
            if (accessCntrl.AccessEnrollment == "N") Response.Redirect("/AccessErrorPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetPersonSoapClient getPerson = new GetPersonSoapClient();
            PersonDetail personDetail = getPerson.getPerson(txtBoxPersonId.Text);
            lblFirstName.Text = personDetail.FirstName;
            lblLastName.Text = personDetail.LastName;
            passportPhoto.ImageUrl = "data:image/png;base64," + personDetail.PassportPhoto;
        }
    }
}