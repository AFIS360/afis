using SourceAFIS.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360Webservice.webobj
{
    public class User : Person
    {
        long id;
        string personId;
        string fname;
        string lname;
        string username;
        string password;
        string userRole;
        string stationId;
        string stationedAddress;
        string stationedCity;
        string stationedCountry;
        string activeStatus;
        DateTime serviceStartDate;
        DateTime serviceEndDate;

        public long ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string PersonId
        {
            get { return this.personId; }
            set { this.personId = value; }
        }

        public string FirstName
        {
            get { return this.fname; }
            set { this.fname = value; }
        }

        public string LastName
        {
            get { return this.lname; }
            set { this.lname = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string UserRole
        {
            get { return this.userRole; }
            set { this.userRole = value; }
        }

        public void setStationId(string stationId)
        {
            this.stationId = stationId;
        }

        public string getStationId()
        {
            return this.stationId;
        }

        public string StationId
        {
            get { return this.stationId; }
            set { this.stationId = value; }
        }

        public string StationedAddress
        {
            get { return this.stationedAddress; }
            set { this.stationedAddress = value; }
        }

        public string StationedCity
        {
            get { return this.stationedCity; }
            set { this.stationedCity = value; }
        }

        public string StationedCountry
        {
            get { return this.stationedCountry; }
            set { this.stationedCountry = value; }
        }

        public string ActiveStatus
        {
            get { return this.activeStatus; }
            set { this.activeStatus = value; }
        }

        public DateTime ServiceStartDate
        {
            get { return this.serviceStartDate; }
            set { this.serviceStartDate = value; }
        }

        public DateTime ServiceEndDate
        {
            get { return this.serviceEndDate; }
            set { this.serviceEndDate = value; }
        }
    }
}
