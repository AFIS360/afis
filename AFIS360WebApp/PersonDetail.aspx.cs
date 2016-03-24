using AFIS360WebApp.GetPersonServiceRef;
using AFIS360WebApp.PersonCriminalRecordServiceRef;
using AFIS360WebApp.PersonPhysicalCharServiceRef;
using AFIS360WebApp.UserAccessControlServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFIS360WebApp
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["CurrentUserRole"] != null)
            {
                AccessControl accessCntrl = (AccessControl)Session["CurrentUserRole"];
                if (accessCntrl.AccessEnrollment == "N") Response.Redirect("/AccessErrorPage.aspx");
                TablePersonDetail.Visible = false;
                TablePersonDetailPhysical.Visible = false;
                TableCriminalRecord.Visible = false;
                lblStatusMsg.Text = null;
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            GetPersonSoapClient getPerson = new GetPersonSoapClient();
            PersonDetail personDetail = getPerson.getPerson(txtBoxPersonId.Text);
            if(personDetail != null)
            {
                passportPhoto.ImageUrl = "data:image/png;base64," + personDetail.PassportPhoto;
                lblName.Text = personDetail.Prefix + " " + personDetail.FirstName + " " + personDetail.MiddleName + " " + personDetail.LastName + " " + personDetail.Suffix;
                lblStreet.Text = personDetail.StreetAddress;
                lblCity.Text = personDetail.City;
                lblState.Text = personDetail.State;
                lblCountry.Text = personDetail.Country;
                lblDOB.Text = personDetail.DateOfBirth != null ? personDetail.DateOfBirth.Value.ToString("MM/dd/yyyy") : "";
                lblCellNbr.Text = personDetail.CellNbr;
                lblWorkNbr.Text = personDetail.WorkPhoneNbr;
                lblHomeNbr.Text = personDetail.HomePhoneNbr;
                lblEmail.Text = personDetail.Email;

                //Get person's physical characteristics
                PersonPhysicalCharServiceSoapClient personPhysicalCharService = new PersonPhysicalCharServiceSoapClient();
                PersonPhysicalChar personPhysicalChar = personPhysicalCharService.GetPersonPhysicalChar(txtBoxPersonId.Text);
                if(personPhysicalChar != null)
                {
                    lblHeight.Text = lblHeight.Text != null ? personPhysicalChar.Height.ToString() :"";
                    lblWeight.Text = lblWeight.Text != null ? personPhysicalChar.Weight.ToString() : "";
                    lblEyeColor.Text = personPhysicalChar.EyeColor;
                    lblHairColor.Text = personPhysicalChar.HairColor;
                    lblComplexion.Text = personPhysicalChar.Complexion;
                    lblBuildType.Text = personPhysicalChar.BuildType;
                    lblBrithMark.Text = personPhysicalChar.BirthMark;
                    lblIDMark.Text = personPhysicalChar.IdMark;
                    lblGender.Text = personPhysicalChar.Gender;
                    lblDeathDate.Text = personPhysicalChar.DOD != null ? personPhysicalChar.DOD.ToString("MM/dd/yyyy") : "";

                    //make the table visible
                    TablePersonDetailPhysical.Visible = true;
                }

                //Get person's criminal record
                PersonCriminalRecordServiceSoapClient criminalRecordServiceSoapClient = new PersonCriminalRecordServiceSoapClient();
                CriminalRecord[] criminalRecords = criminalRecordServiceSoapClient.GetCriminalRecords(txtBoxPersonId.Text);
                //Add Criminal Records to the Session
                Session["CriminalRecords"] = criminalRecords;

                if(criminalRecords.Length > 0)
                {
                    //Make the table visible
                    TableCriminalRecord.Visible = true;

                    if (criminalRecords[0] != null)
                    {
                        lblCaseNo_1.Text = criminalRecords[0].CaseId;
                        CaseNo_1.NavigateUrl = "~/CriminalCaseDetail.aspx?CaseNo=" + lblCaseNo_1.Text;
                        lblCrimeLoc_1.Text = criminalRecords[0].CrimeLocation;
                        lblCrimeDate_1.Text = criminalRecords[0].CrimeDate != null ? criminalRecords[0].CrimeDate.ToString("MM/dd/yyyy") : "";
                        lblStatus_1.Text = criminalRecords[0].Status;
                        RowCriminalRec_1.Visible = true;
                    } else
                    {
                        RowCriminalRec_1.Visible = false;
                    }

                    if(criminalRecords.Length > 1)
                    {
                        if (criminalRecords[1] != null)
                        {
                            lblCaseNo_2.Text = criminalRecords[1].CaseId;
                            CaseNo_2.NavigateUrl = "~/CriminalCaseDetail.aspx?CaseNo=" + lblCaseNo_2.Text;
                            lblCrimeLoc_2.Text = criminalRecords[1].CrimeLocation;
                            lblCrimeDate_2.Text = criminalRecords[1].CrimeDate != null ? criminalRecords[1].CrimeDate.ToString("MM/dd/yyyy") : "";
                            lblStatus_2.Text = criminalRecords[1].Status;
                            RowCriminalRec_2.Visible = true;
                        }
                        else
                        {
                            RowCriminalRec_2.Visible = false;
                        }
                    }else
                    {
                        RowCriminalRec_2.Visible = false;
                    }

                    if (criminalRecords.Length > 2)
                    {
                        if (criminalRecords[2] != null)
                        {
                            lblCaseNo_3.Text = criminalRecords[2].CaseId;
                            CaseNo_3.NavigateUrl = "~/CriminalCaseDetail.aspx?CaseNo=" + lblCaseNo_3.Text;
                            lblCrimeLoc_3.Text = criminalRecords[2].CrimeLocation;
                            lblCrimeDate_3.Text = criminalRecords[2].CrimeDate != null ? criminalRecords[2].CrimeDate.ToString("MM/dd/yyyy") : "";
                            lblStatus_3.Text = criminalRecords[2].Status;
                            RowCriminalRec_3.Visible = true;
                        }
                        else
                        {
                            RowCriminalRec_3.Visible = false;
                        }
                    }
                    else
                    {
                        RowCriminalRec_3.Visible = false;
                    }

                }
                else
                {
                    TableCriminalRecord.Visible = false;
                }

                //make the table visible
                TablePersonDetail.Visible = true;

            }
            else
            {
                TablePersonDetail.Visible = false;
                TablePersonDetailPhysical.Visible = false;
                TableCriminalRecord.Visible = false;
                lblStatusMsg.Text = "No match found.";
            }
        }//end SubmitButton_Click
    }
}