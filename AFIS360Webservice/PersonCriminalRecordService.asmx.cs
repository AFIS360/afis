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
    /// Summary description for PersonCriminalRecordService
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PersonCriminalRecordService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<CriminalRecord> GetCriminalRecords(string personId)
        {
            DataAccess dataAccess = new DataAccess();
            List<CriminalRecord> criminalRecords = dataAccess.getCriminalRecords(personId);
            return criminalRecords;
        }
    }
}
