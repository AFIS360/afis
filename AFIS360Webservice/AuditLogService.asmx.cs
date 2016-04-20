using AFIS360Common;
using AFIS360Common.dao;
using AFIS360Webservice.webobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AFIS360Webservice
{
    /// <summary>
    /// Summary description for AuditLogService
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuditLogService : System.Web.Services.WebService
    {
        [WebMethod]
        public webobj.Status updateAuditLog(webobj.User webUser, webobj.ActivityLog webActivityLog)
        {
            AFIS360Common.User user = new AFIS360Common.User();
            user.setPersonId(webUser.PersonId);
            user.setId(webUser.Id);

            AFIS360Common.ActivityLog activityLog = new AFIS360Common.ActivityLog();
            List<string> webActivites = webActivityLog.Activity;
            foreach(string webActivity in webActivites)
            {
                activityLog.setActivity(webActivity);
            }

            DataAccess dataaccess = new DataAccess();
            AFIS360Common.Status status = dataaccess.updateUserAuditLog(user, DateTime.Now, 0, activityLog);

            webobj.Status webStatus = new webobj.Status();
            webStatus.StatusCode = status.getStatusCode();
            webStatus.StatusDesc = status.getStatusDesc();
            webStatus.AuditLogId = status.getAuditLogId();
            return webStatus;
        }


        [WebMethod]
        public webobj.Status createAuditLog(webobj.User webUser)
        {
            AFIS360Common.User user = new AFIS360Common.User();
            user.setPersonId(webUser.PersonId);
            user.setUsername(webUser.Username);
            user.setUserRole(webUser.UserRole);

            DataAccess dataaccess = new DataAccess();
            AFIS360Common.Status status = dataaccess.createUserAuditLog(user, DateTime.Now);

            webobj.Status webStatus = new webobj.Status();
            webStatus.StatusCode = status.getStatusCode();
            webStatus.StatusDesc = status.getStatusDesc();
            webStatus.AuditLogId = status.getAuditLogId();
            return webStatus;
        }

    }
}
