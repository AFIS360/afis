using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AFIS360Webservice.webobj
{
    public class ActivityLog
    {
        private List<string> activityLogs = null;

        public ActivityLog()
        {
            activityLogs = new List<string>();
        }

        public List<string> Activity
        {
            get { return this.activityLogs; }
            set { this.activityLogs = value; }
        }
    }
}