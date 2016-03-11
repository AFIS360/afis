using AFIS360WebApp.GetPersonServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class PersonDetailForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
            if (accessCntrl.AccessEnrollment == "N") Response.Redirect("/AccessErrorPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetPersonSoapClient getPerson= new GetPersonSoapClient();
            PersonDetail personDetail = getPerson.getPerson(txtBoxPersonId.Text);
            lblFirstName.Text = personDetail.FirstName;
            lblLastName.Text = personDetail.LastName;
            passportPhoto.ImageUrl = "data:image/png;base64," + personDetail.PassportPhoto;
        }
    }
}