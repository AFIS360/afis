using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360Common
{
    public class AppConfig
    {

        private Int32 id;
        private string personId;
        private string dupCheck;


        public Int32 Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string PersonId
        {
            get
            {
                return this.personId;
            }
            set
            {
                this.personId = value;
            }
        }

        public string DupCheck
        {
            get
            {
                return this.dupCheck;
            }
            set
            {
                this.dupCheck = value;
            }
        }

        public bool isDupCheck()
        {
            if (this.dupCheck.Equals("Y"))
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
