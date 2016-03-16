using AFIS360Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AFIS360Webservice.webobj 
{
    public class ClientSetupInfo : ClientSetup
    {
        private string companyLogoBase64Str;

        public string CompanyLogoBase64Str
        {
            get { return companyLogoBase64Str; }
            set { companyLogoBase64Str = value; }
        }
    }
}