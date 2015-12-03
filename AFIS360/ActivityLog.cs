using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AFIS360
{
    [Serializable]
    public class ActivityLog
    {
        private List<string> activityLogs = null;

        public ActivityLog()
        {
            activityLogs = new List<string>();
        }

        public void setActivityLog(string log)
        {
            this.activityLogs.Add(log);
        }

        public List<string> getActivityLog()
        {
            return this.activityLogs;
        }
    }
}
