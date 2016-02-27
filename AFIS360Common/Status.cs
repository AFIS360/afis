using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFIS360Common
{
    public class Status
    {
        private string statusCode = null;
        private string statusDesc = null;
        private long auditLogId = 0;
        public static string STATUS_SUCCESSFUL = "001";
        public static string STATUS_FAILURE = "999";

        public void setStatusCode(string statusCode)
        {
            this.statusCode = statusCode;
        }

        public string getStatusCode()
        {
            return this.statusCode;
        }

        public void setStatusDesc(string statusDesc)
        {
            this.statusDesc = statusDesc;
        }

        public string getStatusDesc()
        {
            return this.statusDesc;
        }

        public void setAuditLogId(long auditLogId)
        {
            this.auditLogId = auditLogId;
        }

        public long getAuditLogId()
        {
            return this.auditLogId;
        }
    }
}
