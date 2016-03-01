using AFIS360Common;
using AFIS360Common.dao;
using AFIS360Common.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Services;

namespace AFIS360Webservice
{
    /// <summary>
    /// Summary description for GetPerson
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetPerson : System.Web.Services.WebService
    {

        [WebMethod]
        public webobj.PersonDetail getPerson(string personId)
        {
            DataAccess dataAccess = new DataAccess();
            List<PersonDetail> persons = dataAccess.retrievePersonDetail(personId);
            PersonDetail personDetail = persons.FirstOrDefault();
            webobj.PersonDetail webPersonDetail = new webobj.PersonDetail();
            webPersonDetail.PersonId = personDetail.getPersonId();
            webPersonDetail.FirstName = personDetail.getFirstName();
            webPersonDetail.LastName = personDetail.getLastName();
            webPersonDetail.MiddleName = personDetail.getMiddleName();
            webPersonDetail.Prefix = personDetail.getPrefix();
            webPersonDetail.Suffix = personDetail.getSuffix();
            webPersonDetail.LastName = personDetail.getLastName();
            webPersonDetail.DateOfBirth = personDetail.getDOB();
            webPersonDetail.DateOfBirthText = personDetail.getDOBText();
            webPersonDetail.StreetAddress = personDetail.getStreetAddress();
            webPersonDetail.City = personDetail.getCity();
            webPersonDetail.State = personDetail.getState();
            webPersonDetail.PostalCode = personDetail.getPostalCode();
            webPersonDetail.Country = personDetail.getCountry();
            webPersonDetail.Profession = personDetail.getProfession();
            webPersonDetail.FatherName = personDetail.getFatherName();
            webPersonDetail.CellNbr = personDetail.getCellNbr();
            webPersonDetail.HomePhoneNbr = personDetail.getHomePhoneNbr();
            webPersonDetail.WorkPhoneNbr = personDetail.getWorkPhoneNbr();
            webPersonDetail.Email = personDetail.getEmail();
            webPersonDetail.PassportPhoto = personDetail.getPassportPhoto() != null ? Converter.ImageToBase64(personDetail.getPassportPhoto(), System.Drawing.Imaging.ImageFormat.Bmp) : "";

            return webPersonDetail;
        }

    }
}
