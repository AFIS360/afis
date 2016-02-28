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
    /// Summary description for GetPerson
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetPerson : System.Web.Services.WebService
    {

//        [WebMethod]
        public string HelloWorld()
        {
            return "Hello Mohsin";
        }

        [WebMethod]
        public string getUser(string userId)
        {
            DataAccess dataAccess = new DataAccess();
            User user = dataAccess.getUser(userId);
            return user.getFirstName() + " " + user.getLastName();
        }

    }
}
