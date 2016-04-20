using AFIS360WebApp.AuditLogServiceRef;
using AFIS360WebApp.ValidateUserServiceRef;
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
            // Store ActivityLog 
//            setActivityLog();
            
            // The SignOut method invalidates the authentication cookie.
            FormsAuthentication.SignOut();
            Session["CurrentUser"] = "N/A";
            Response.Redirect("/Login.aspx");
        }

        private void setActivityLog()
        {
            AuditLogServiceRef.User loginUser = (AuditLogServiceRef.User)Session["LoginUser"];

            List<string> activites = (List<string>)Session["Activities"];
            string[] activityArray = activites.ToArray();
            Response.Write("Activity = " + activityArray[0]);
            ActivityLog activityLog = new ActivityLog();
            activityLog.Activity = activityArray;

            AuditLogServiceSoapClient auditLogServiceSoapClient = new AuditLogServiceSoapClient();
            Status status = auditLogServiceSoapClient.updateAuditLog(loginUser, activityLog);
            Response.Write("Status = " + status.StatusDesc);
        }
    }
}