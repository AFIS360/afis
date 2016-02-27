using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace AFIS360Common
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
        private Image passportPhoto;


        public void setPersonId(string personId)
        {
            this.personId = personId;
        }
        public string getPersonId()
        {
            return this.personId;
        }
        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }
        public string getFirstName()
        {
            return this.firstName;
        }
        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }
        public string getLastName()
        {
            return this.lastName;
        }
        public void setMiddleName(string middleName)
        {
            this.middleName = middleName;
        }
        public string getMiddleName()
        {
            return this.middleName;
        }
        public void setPrefix(string prefix)
        {
            this.prefix = prefix;
        }
        public string getPrefix()
        {
            return this.prefix;
        }
        public void setSuffix(string suffix)
        {
            this.suffix = suffix;
        }
        public string getSuffix()
        {
            return this.suffix;
        }
        public void setDOB(DateTime? DOB)
        {
            this.DOB = DOB;
        }
        public DateTime? getDOB()
        {
            return this.DOB;
        }

        public void setDOBText(string DOBText)
        {
            this.DOBText = DOBText;
        }
        public string getDOBText()
        {
            return this.DOBText;
        }
        public void setStreetAddress(string streetAddress)
        {
            this.streetAddress = streetAddress;
        }
        public string getStreetAddress()
        {
            return this.streetAddress;
        }
        public void setCity(string city)
        {
            this.city = city;
        }
        public string getCity()
        {
            return this.city;
        }
        public void setPostalCode(string postalCode)
        {
            this.postalCode = postalCode;
        }
        public string getPostalCode()
        {
            return this.postalCode;
        }
        public void setState(string state)
        {
            this.state = state;
        }
        public string getState()
        {
            return this.state;
        }
        public void setCountry(string country)
        {
            this.country = country;
        }
        public string getCountry()
        {
            return this.country;
        }
        public void setProfession(string profession)
        {
            this.profession = profession;
        }
        public string getProfession()
        {
            return this.profession;
        }
        public void setFatherName(string fatherName)
        {
            this.fatherName = fatherName;
        }
        public string getFatherName()
        {
            return this.fatherName;
        }
        public void setcellNbr(string cellNbr)
        {
            this.cellNbr = cellNbr;
        }
        public string getCellNbr()
        {
            return this.cellNbr;
        }
        public void setWorkPhoneNbr(string workPhoneNbr)
        {
            this.workPhoneNbr = workPhoneNbr;
        }
        public string getWorkPhoneNbr()
        {
            return this.workPhoneNbr;
        }
        public void setHomwPhoneNbr(string homePhoneNbr)
        {
            this.homePhoneNbr = homePhoneNbr;
        }
        public string getHomePhoneNbr()
        {
            return this.homePhoneNbr;
        }
        public void setEmail(string email)
        {
            this.email = email;
        }
        public string getEmail()
        {
            return this.email;
        }
        public void setPassportPhoto(Image passportPhoto)
        {
            this.passportPhoto = passportPhoto;
        }
        public Image getPassportPhoto()
        {
            return this.passportPhoto;
        }
    }
}
