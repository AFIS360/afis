using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class CriminalRecord : DataObject
    {

        private string personId;
        private string crimeDetail;
        private DateTime crimeDate;
        private string crimeLocation;
        private string court;
        private string statute;
        private string courtAddress;
        private string caseId;
        private DateTime sentencedDate;
        private DateTime releaseDate;
        private DateTime arrestDate;
        private string arrestAgency;
        private string status;
        private DateTime paroleDate;
        private string criminalAlertLevel;
        private string criminalAlertMsg;
        private string refDocLocation;

        public string PersonId
        {
            get { return personId; }
            set { personId = value; }
        }

        public string CrimeDetail
        {
            get { return crimeDetail; }
            set { crimeDetail = value; }
        }

        public DateTime CrimeDate
        {
            get { return crimeDate; }
            set { crimeDate = value; }
        }

        public string CrimeLocation
        {
            get { return crimeLocation; }
            set { crimeLocation = value; }
        }

        public string Court
        {
            get { return court; }
            set { court = value; }
        }

        public string Statute
        {
            get { return statute; }
            set { statute = value; }
        }

        public string CourtAddress
        {
            get { return courtAddress; }
            set { courtAddress = value; }
        }

        public string CaseId
        {
            get { return caseId; }
            set { caseId = value; }
        }

        public DateTime SentencedDate
        {
            get { return sentencedDate; }
            set { sentencedDate = value; }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }

        public DateTime ArrestDate
        {
            get { return arrestDate; }
            set { arrestDate = value; }
        }

        public string ArrestAgency
        {
            get { return arrestAgency; }
            set { arrestAgency = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime ParoleDate
        {
            get { return paroleDate; }
            set { paroleDate = value; }
        }

        public string CriminalAlertLevel
        {
            get { return criminalAlertLevel; }
            set { criminalAlertLevel = value; }
        }

        public string CriminalAlertMsg
        {
            get { return criminalAlertMsg; }
            set { criminalAlertMsg = value; }
        }

        public string RefDocLocation
        {
            get { return refDocLocation; }
            set { refDocLocation = value; }
        }
    }
}
