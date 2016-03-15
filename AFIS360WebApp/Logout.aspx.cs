using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The SignOut method invalidates the authentication cookie.
            FormsAuthentication.SignOut();
            Session["CurrentUser"] = "N/A";
            Response.Redirect("/Login.aspx");
        }
    }
}