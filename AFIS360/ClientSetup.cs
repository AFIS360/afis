using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class ClientSetup : DataObject
    {
        private string clientId;
        private string legalName;
        private string addressLine;
        private string city;
        private string state;
        private string postalCode;
        private string country;

        public string ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        public string LegalName
        {
            get { return legalName; }
            set { legalName = value; }
        }

        public string AddressLine
        {
            get { return addressLine; }
            set { addressLine= value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
    }
}
