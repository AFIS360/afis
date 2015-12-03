using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simpl


namespace AFIS360
{
    // Inherit from Person in order to add Name field
    [Serializable]
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

        public void setId(long id)
        {
            this.id = id;
        }

        public long getId()
        {
            return id;
        }

        public void setPersonId(string personId)
        {
            this.personId = personId;
        }

        public string getPersonId()
        {
            return this.personId;
        }

        public void setFirstName(string fname)
        {
            this.fname = fname;
        }

        public string getFirstName()
        {
            return this.fname;
        }
        public void setLastName(string lname)
        {
            this.lname = lname;
        }

        public string getLastName()
        {
            return this.lname;
        }
        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getUsername()
        {
            return this.username;
        }
        public void setPassword(string password)
        {
            this.password = password;
        }

        public string getPassword()
        {
            return this.password;
        }
        public void setUserRole(string userRole)
        {
            this.userRole = userRole;
        }

        public string getUserRole()
        {
            return this.userRole;
        }

        public void setStationId(string stationId)
        {
            this.stationId = stationId;
        }

        public string getStationId()
        {
            return this.stationId;
        }

        public void setStationedAddress(string stationedAddress)
        {
            this.stationedAddress = stationedAddress;
        }

        public string getStationedAddress()
        {
            return this.stationedAddress;
        }

        public void setStationedCity(string stationedCity)
        {
            this.stationedCity = stationedCity;
        }

        public string getStationedCity()
        {
            return this.stationedCity;
        }

        public void setStationedCountry(string stationedCountry)
        {
            this.stationedCountry = stationedCountry;
        }

        public string getStationedCountry()
        {
            return this.stationedCountry;
        }

        public void setActiveStatus(string activeStatus)
        {
            this.activeStatus = activeStatus;
        }

        public string getActiveStatus()
        {
            return this.activeStatus;
        }

        public void setServiceStartDate(DateTime serviceStartDate)
        {
            this.serviceStartDate = serviceStartDate;
        }

        public DateTime getServiceStartDate()
        {
            return this.serviceStartDate;
        }

        public void setServiceEndDate(DateTime serviceEndDate)
        {
            this.serviceEndDate = serviceEndDate;
        }

        public DateTime getServiceEndDate()
        {
            return this.serviceEndDate;
        }
    }
}
