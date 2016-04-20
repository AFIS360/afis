using AFIS360WebApp.AuditLogServiceRef;
using AFIS360WebApp.ClientSetupServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using AFIS360WebApp.ValidateUserServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientSetupServiceSoapClient clientSetupServiceSoapClient = new ClientSetupServiceSoapClient();
            ClientSetupInfo clientSetupInfo = clientSetupServiceSoapClient.GetClientSetup();
            ImageComapnyLogo.ImageUrl = "data:image/png;base64," + clientSetupInfo.CompanyLogoBase64Str;
        }

        protected void UserLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string userName = UserLogin.UserName;
            string password = UserLogin.Password;

            bool validUser = IsValidUser(userName, password);
            if (validUser)
            {
                e.Authenticated = true;

            }
            else
            {
                e.Authenticated = false;
            }
        }

        private bool IsValidUser(string id, string password)
        {
            bool isValidUser = false;
            ValidateUserSoapClient validUserSoapClient = new ValidateUserSoapClient();
            ValidateUserServiceRef.User validUser = validUserSoapClient.getValidUser(id, password);

            if (validUser != null)
            {
                isValidUser = true;
                Session["CurrentUser"] = validUser.FirstName + " " + validUser.LastName + " (" + validUser.UserRole + ")";
//                Session["LoginUser"] = validUser;
                
                //get the user role
                UserAccessControlSoapClient userAccessControlSoapClient = new UserAccessControlSoapClient();
                AccessControl accessCntrl = userAccessControlSoapClient.getAccessControl(validUser.UserRole);
                Session["CurrentUserRole"] = accessCntrl;

                //init the ActivityLog
                initActivityLog(validUser);

                //Log activity
                List<string> activties = new List<string>{ };
                Session["Activities"] = activties;
                activties.Add("Successfully login. \n");
            }

            return isValidUser;
        }

        private void initActivityLog(ValidateUserServiceRef.User validUser)
        {
            AuditLogServiceRef.User auditLogUser = new AuditLogServiceRef.User();
            auditLogUser.PersonId = validUser.PersonId;
            auditLogUser.Username = validUser.Username;
            auditLogUser.UserRole = validUser.UserRole;
            auditLogUser.StationId = validUser.StationId;
            auditLogUser.StationedAddress = validUser.StationedAddress;
            auditLogUser.StationedCity = validUser.StationedCity;
            auditLogUser.StationedCountry = validUser.StationedCountry;

            AuditLogServiceSoapClient auditLogServiceSoapClient = new AuditLogServiceSoapClient();
            Status status = auditLogServiceSoapClient.createAuditLog(auditLogUser);
            Session["Status"] = status;
            auditLogUser.ID = status.AuditLogId;
            Session["LoginUser"] = auditLogUser;

        }
    }
}