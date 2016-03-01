using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AFIS360Webservice.webobj
{
    public class PersonDetail
    {
        private string personId;
        private string firstName;
        private string lastName;
        private string middleName;
        private string prefix;
        private string suffix;
        private DateTime? DOB;
        private string DOBText;
        private string streetAddress;
        private string city;
        private string postalCode;
        private string state;
        private string country;
        private string profession;
        private string fatherName;
        private string cellNbr;
        private string homePhoneNbr;
        private string workPhoneNbr;
        private string email;
        private string passportPhoto;

        public string PersonId
        {
            get { return this.personId; }
            set { this.personId = value; }
        }

        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        public string MiddleName
        {
            get { return this.middleName; }
            set { this.middleName = value; }
        }

        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value;  }
        }

        public string Suffix
        {
            get { return this.suffix; }
            set { this.suffix = value; }
        }

        public DateTime? DateOfBirth
        {
            get { return this.DOB; }
            set { this.DOB = value; }
        }

        public string DateOfBirthText
        {
            get { return this.DOBText; }
            set { this.DOBText = value; }
        }

        public string StreetAddress
        {
            get { return this.streetAddress; }
            set { this.streetAddress = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public string Country
        {
            get { return this.country; }
            set { this.country = value; }
        }

        public string Profession
        {
            get { return this.profession; }
            set { this.profession = value; }
        }

        public string FatherName
        {
            get { return this.fatherName; }
            set { this.fatherName = value; }
        }

        public string CellNbr
        {
            get { return this.cellNbr; }
            set { this.cellNbr = value; }
        }

        public string WorkPhoneNbr
        {
            get { return this.workPhoneNbr; }
            set { this.workPhoneNbr = value; }
        }

        public string HomePhoneNbr
        {
            get { return this.homePhoneNbr; }
            set { this.homePhoneNbr = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string PassportPhoto
        {
            get { return this.passportPhoto; }
            set { this.passportPhoto = value; }
        }
    }
}