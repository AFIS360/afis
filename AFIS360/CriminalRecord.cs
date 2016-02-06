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
        private string crimeType;
        private string crimeCode;
        private DateTime crimeDate;
        private string crimeLocation;
        private string disposition;
        private DateTime dispositionDate;
        private string dispositionCode;
        private string court;
        private string statute;
        private string county;
        private string caseNbr;
        private DateTime sentencedDate;
        private string incarceration;
        private string probation;
        private DateTime earlyReleaseDate;
        private DateTime releaseDate;
        private DateTime lateReleaseDate;
        private DateTime arrestDate;
        private string arrestAgency;
        private string status;
        private DateTime paroleDate;
        private string criminalAlertLevel;
        private string criminalAlertMsg;

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

        public string CrimeType
        {
            get { return crimeType; }
            set { crimeType = value; }
        }

        public string CrimeCode
        {
            get { return crimeCode; }
            set { crimeCode = value; }
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

        public string Disposition
        {
            get { return disposition; }
            set { disposition = value; }
        }

        public string DispositionCode
        {
            get { return dispositionCode; }
            set { dispositionCode = value; }
        }

        public DateTime DispositionDate
        {
            get { return dispositionDate; }
            set { dispositionDate = value; }
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

        public string County
        {
            get { return county; }
            set { county = value; }
        }

        public string CaseNbr
        {
            get { return caseNbr; }
            set { caseNbr = value; }
        }

        public DateTime SentencedDate
        {
            get { return sentencedDate; }
            set { sentencedDate = value; }
        }

        public string Incarceration
        {
            get { return incarceration; }
            set { incarceration = value; }
        }

        public string Probation
        {
            get { return probation; }
            set { probation = value; }
        }

        public DateTime EarlyReleaseDate
        {
            get { return earlyReleaseDate; }
            set { earlyReleaseDate = value; }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }
        public DateTime LateReleaseDate
        {
            get { return lateReleaseDate; }
            set { lateReleaseDate = value; }
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
    }
}
