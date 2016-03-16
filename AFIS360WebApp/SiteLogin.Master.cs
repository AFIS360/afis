using AFIS360WebApp.ClientSetupServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class SiteLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientSetupServiceSoapClient clientSetupServiceSoapClient = new ClientSetupServiceSoapClient();
            ClientSetup clientSeup = clientSetupServiceSoapClient.GetClientSetup();
            LabelCompanyName.Text = clientSeup.LegalName;

            if (Session["CurrentUser"] != null)
            {
                AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
                LabelLoginUserInfo.Text = Session["CurrentUser"].ToString();
            }
            else
            {
                LabelLoginUserInfo.Text = "N/A";
            }
        }
    }
}