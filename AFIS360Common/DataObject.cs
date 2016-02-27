using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360Common
{
    public class DataObject
    {
        private string createdBy;
        private DateTime? creationDateTime;
        private string updatedBy;
        private DateTime? updateDateTime;

        public String CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime? CreationDateTime
        {
            get { return creationDateTime; }
            set { creationDateTime = value; }
        }

        public String UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        public DateTime? UpdateDateTime
        {
            get { return updateDateTime; }
            set { updateDateTime = value; }
        }
    }
}
