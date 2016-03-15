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
            User validUser = validUserSoapClient.getValidUser(id, password);

            if (validUser != null)
            {
                isValidUser = true;
                Session["CurrentUser"] = validUser.FirstName + " " + validUser.LastName + " (" + validUser.UserRole + ")";
                //get the user role
                UserAccessControlSoapClient userAccessControlSoapClient = new UserAccessControlSoapClient();
                AccessControl accessCntrl = userAccessControlSoapClient.getAccessControl(validUser.UserRole);
                Session["CurrentUserRole"] = accessCntrl;
            }

            return isValidUser;
        }
    }
}