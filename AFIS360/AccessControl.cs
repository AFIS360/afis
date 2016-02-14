using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class AccessControl
    {
        private string role;
        private string access_login_tab;
        private string access_enroll_tab;
        private string access_match_tab;
        private string access_usermgmt_tab;
        private string access_audit_tab;
        private string access_find_tab;
        private string access_data_import;
        private string access_data_export;
        private string access_multi_match;
        private string access_client_setup;

        public void setRole(string role)
        {
            this.role = role;
        }

        public string getRole()
        {
            return this.role;
        }

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
        public void setAccessDataImport(string access_data_import)
        {
            this.access_data_import = access_data_import;
        }
        public string getAccessDataImport()
        {
            return this.access_data_import;
        }
        public bool hasAccessToDataImport()
        {
            if (this.access_data_import.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessDataExport(string access_data_export)
        {
            this.access_data_export = access_data_export;
        }
        public string getAccessDataExport()
        {
            return this.access_data_export;
        }
        public bool hasAccessToDataExport()
        {
            if (this.access_data_export.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessMultiMatch(string access_multi_match)
        {
            this.access_multi_match = access_multi_match;
        }
        public string getAccessMultiMatch()
        {
            return this.access_multi_match;
        }
        public bool hasAccessToMultiMatch()
        {
            if (this.access_multi_match.Equals("Y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setAccessClientSetup(string access_client_setup)
        {
            this.access_client_setup = access_client_setup;
        }
        public string getAccessClientSetup()
        {
            return this.access_client_setup;
        }
        public bool hasAccessToClientSetup()
        {
            if (this.access_client_setup.Equals("Y"))
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
