using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class AuditLog
    {
        private string userId;
        private string username;
        private DateTime loginDateTime;
        private DateTime? logoutDateTime;
        private ActivityLog activityLog;

        public void setUserId(string userId)
        {
            this.userId = userId;
        }

        public string getUserId()
        {
            return this.userId;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getUsername()
        {
            return this.username;
        }

        public void setLoginDateTime(DateTime loginDateTime)
        {
            this.loginDateTime = loginDateTime;
        }

        public DateTime getLoginDateTime()
        {
            return this.loginDateTime;
        }

        public void setLogoutDateTime(DateTime? logoutDateTime)
        {
            this.logoutDateTime = logoutDateTime;
        }

        public DateTime? getLogoutDateTime()
        {
            return this.logoutDateTime;
        }

        public void setActivityLog(ActivityLog activityLog)
        {
            this.activityLog = activityLog;
        }

        public ActivityLog getActivityLog()
        {
            return this.activityLog;
        }

    }
}
