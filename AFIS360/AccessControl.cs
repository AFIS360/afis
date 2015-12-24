using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class AccessControl
    {
        private string access_login_tab;
        private string access_enroll_tab;
        private string access_match_tab;
        private string access_usermgmt_tab;
        private string access_audit_tab;
        private string access_find_tab;

        public void setAccessLoginTab(string access_login_tab)
        {
            this.access_login_tab = access_login_tab;
        }
        public string getAccessLoginTab()
        {
            return this.access_login_tab;
        }
        public bool hasAccessToLoginTab()
        {
            if (this.access_login_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessEnrollTab(string access_enroll_tab)
        {
            this.access_enroll_tab = access_enroll_tab;
        }
        public string getAccessEnrollTab()
        {
            return this.access_enroll_tab;
        }
        public bool hasAccessToEnrollTab()
        {
            if (this.access_enroll_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessMatchTab(string access_match_tab)
        {
            this.access_match_tab = access_match_tab;
        }
        public string getAccessMatchTab()
        {
            return this.access_match_tab;
        }
        public bool hasAccessToMatchTab()
        {
            if (this.access_match_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessUserMgmtTab(string access_usermgmt_tab)
        {
            this.access_usermgmt_tab = access_usermgmt_tab;
        }
        public string getAccessUserMgmtTab()
        {
            return this.access_usermgmt_tab;
        }
        public bool hasAccessToUserMgmtTab()
        {
            if (this.access_usermgmt_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessAuditTab(string access_audit_tab)
        {
            this.access_audit_tab = access_audit_tab;
        }
        public string getAccessAuditTab()
        {
            return this.access_audit_tab;
        }
        public bool hasAccessToAuditTab()
        {
            if (this.access_audit_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessFindTab(string access_find_tab)
        {
            this.access_find_tab = access_find_tab;
        }
        public string getAccessFindTab()
        {
            return this.access_find_tab;
        }
        public bool hasAccessToFindTab()
        {
            if (this.access_find_tab.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
