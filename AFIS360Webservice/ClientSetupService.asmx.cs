using AFIS360Common;
using AFIS360Common.dao;
using AFIS360Common.util;
using AFIS360Webservice.webobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AFIS360Webservice
{
    /// <summary>
    /// Summary description for ClientSetupService
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientSetupService : System.Web.Services.WebService
    {

        [WebMethod]
        public ClientSetupInfo GetClientSetup()
        {
            DataAccess dataAccess = new DataAccess();
            ClientSetup clientSetup = dataAccess.getClientSetup();
            ClientSetupInfo clientSetupInfo = new ClientSetupInfo();
            clientSetupInfo.ClientId = clientSetup.ClientId;
            clientSetupInfo.LegalName = clientSetup.LegalName;
            clientSetupInfo.AddressLine = clientSetup.AddressLine;
            clientSetupInfo.City = clientSetup.City;
            clientSetupInfo.State = clientSetup.State;
            clientSetupInfo.PostalCode = clientSetup.PostalCode;
            clientSetupInfo.Country = clientSetup.Country;
            clientSetupInfo.CompanyLogoBase64Str = clientSetup.CompanyLogo != null ? Converter.ImageToBase64(clientSetup.CompanyLogo, System.Drawing.Imaging.ImageFormat.Bmp) : "";
            clientSetupInfo.CompanyLogo = null;
            clientSetupInfo.DataRefreshInterval = clientSetup.DataRefreshInterval;
            return clientSetupInfo;
        }
    }
}
