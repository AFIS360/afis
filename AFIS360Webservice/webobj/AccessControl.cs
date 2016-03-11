using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AFIS360Webservice.webobj
{
    public class AccessControl
    {
        private string role;
        private string access_login;
        private string access_enrollment;
        private string access_fingerprint_matching;
        private string access_usermgmt;
        private string access_audit;
        private string access_find;
        private string access_data_import;
        private string access_data_export;
        private string access_multi_match;
        private string access_client_setup;

        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        public string AccessLogin
        {
            get { return this.access_login; }
            set { this.access_login = value; }
        }

        public string AccessEnrollment
        {
            get { return this.access_enrollment; }
            set { this.access_enrollment = value; }
        }

        public string AccessFingerprintMatching
        {
            get { return this.access_fingerprint_matching; }
            set { this.access_fingerprint_matching = value; }
        }

        public string AccessUserMgmt
        {
            get { return this.access_usermgmt; }
            set { this.access_usermgmt = value; }
        }

        public string AccessAudit
        {
            get { return this.access_audit; }
            set { this.access_audit = value; }
        }

        public string AccessFind
        {
            get { return this.access_find; }
            set { this.access_find = value; }
        }

        public string AccessDataImport
        {
            get { return this.access_data_import; }
            set { this.access_data_import = value; }
        }

        public string AccessDataExport
        {
            get { return this.access_data_export; }
            set { this.access_data_export = value; }
        }

        public string AccessMultiMatch
        {
            get { return this.access_multi_match; }
            set { this.access_multi_match = value; }
        }

        public string AccessClientSetup
        {
            get { return this.access_client_setup; }
            set { this.access_client_setup = value; }
        }
    }
}