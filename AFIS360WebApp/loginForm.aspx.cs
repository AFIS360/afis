using AFIS360WebApp.ValidateUserServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class loginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string userName = Login1.UserName;
            string password = Login1.Password;

            bool validUser = IsValidUser(userName, password);
            if (validUser)
            {
                e.Authenticated = true;
            } else
            {
                e.Authenticated = false;
            }
        }

        private bool IsValidUser(string id, string password)
        {
            bool isValidUser = false;
            ValidateUserSoapClient validUserSoapClient = new ValidateUserSoapClient();
            User validUser = validUserSoapClient.getValidUser(id, password);
            if(validUser != null)
            {
                isValidUser = true;
                Session["CurrentUser"] = validUser.FirstName + " " + validUser.LastName;
            } 
                
            return isValidUser;
        }
    }
}