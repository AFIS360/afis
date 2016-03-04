using AFIS360Common;
using AFIS360Common.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AFIS360Webservice
{
    /// <summary>
    /// Summary description for ValidateUser
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ValidateUser : System.Web.Services.WebService
    {

        [WebMethod]
        public webobj.User getValidUser(string username, string password)
        {
            DataAccess dataAccess = new DataAccess();
            User user = dataAccess.getValidUser(username, password);
            webobj.User webUser = null;

            if ( user != null)
            {
                webUser = new webobj.User();
                webUser.PersonId = user.getPersonId();
                webUser.FirstName = user.getFirstName();
                webUser.LastName = user.getLastName();
                webUser.Username = user.getUsername();
                webUser.Password = user.getPassword();
                webUser.UserRole = user.getUserRole();
                webUser.StationId = user.getStationId();
                webUser.StationedAddress = user.getStationedAddress();
                webUser.StationedCity = user.getStationedCity();
                webUser.StationedCountry = user.getStationedCountry();
                webUser.ActiveStatus = user.getActiveStatus();
                webUser.ServiceStartDate = user.getServiceStartDate();
                webUser.ServiceEndDate = user.getServiceEndDate();
            }

            return webUser;
        }

    }
}
