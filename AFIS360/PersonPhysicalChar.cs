using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class PersonPhysicalChar : DataObject
    {
        private string personId;
        private double height;
        private double weight;
        private string eyeColor;
        private string hairColor;
        private string complexion;
        private string buildType;
        private string birthMark;
        private string idMark;
        private string gender;
        private DateTime dod;

        public string PersonId
        {
            get { return personId; }
            set { personId = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string EyeColor
        {
            get { return eyeColor; }
            set { eyeColor = value; }
        }

        public string HairColor
        {
            get { return hairColor; }
            set { hairColor = value; }
        }

        public string Complexion
        {
            get { return complexion; }
            set { complexion = value; }
        }

        public string BuindType
        {
            get { return buildType; }
            set { buildType = value; }
        }

        public string BirthMark
        {
            get { return birthMark; }
            set { birthMark = value; }
        }

        public string IdMark
        {
            get { return idMark; }
            set { idMark = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime DOD 
        {
            get { return dod; }
            set { dod = value; }
        }
    }
}

