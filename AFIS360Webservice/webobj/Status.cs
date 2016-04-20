using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AFIS360Webservice.webobj
{
    public class Status
    {
        private string statusCode = null;
        private string statusDesc = null;
        private long auditLogId = 0;
        public static string STATUS_SUCCESSFUL = "001";
        public static string STATUS_FAILURE = "999";

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string StatusDesc
        {
            get { return this.statusDesc; }
            set { this.statusDesc = value; }
        }

        public long AuditLogId
        {
            get { return this.auditLogId; }
            set { this.auditLogId = value; }
        }
    }
}