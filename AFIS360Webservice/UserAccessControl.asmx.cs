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
    /// Summary description for UserAccessControl
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserAccessControl : System.Web.Services.WebService
    {

        [WebMethod]
        public webobj.AccessControl getAccessControl(string role)
        {
            DataAccess dataAccess = new DataAccess();
            AccessControl accessCntrl = dataAccess.getAcessCntrl(role);
            webobj.AccessControl webAccessCntrl = null;

            if (accessCntrl != null)
            {
                webAccessCntrl = new webobj.AccessControl();
                webAccessCntrl.Role = accessCntrl.getRole();
                webAccessCntrl.AccessLogin = accessCntrl.getAccessLoginTab();
                webAccessCntrl.AccessEnrollment = accessCntrl.getAccessEnrollTab();
                webAccessCntrl.AccessFingerprintMatching = accessCntrl.getAccessMatchTab();
                webAccessCntrl.AccessUserMgmt = accessCntrl.getAccessUserMgmtTab();
                webAccessCntrl.AccessFind = accessCntrl.getAccessFindTab();
                webAccessCntrl.AccessAudit = accessCntrl.getAccessAuditTab();
                webAccessCntrl.AccessDataImport = accessCntrl.getAccessDataImport();
                webAccessCntrl.AccessDataExport = accessCntrl.getAccessDataExport();
                webAccessCntrl.AccessMultiMatch = accessCntrl.getAccessMultiMatch();
                webAccessCntrl.AccessClientSetup = accessCntrl.getAccessClientSetup();
            }

            return webAccessCntrl;
        }
    }
}
