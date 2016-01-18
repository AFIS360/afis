using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class ExportData : Form
    {
        private ActivityLog activityLog;

        public ExportData(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnExportDataFind_Click(object sender, EventArgs e)
        {
            Thread searchFindThread = new Thread(processFindToExportData);
            searchFindThread.Name = "FindExportDataThread";
            searchFindThread.Start();
        }

        private void processFindToExportData()
        {
            try
            {
                lblExportDataStatus.Text = "Searching....";
                string personId = txtBoxExportDataPersonId.Text;
                string fname = txtBoxExportDataFName.Text;
                string lname = txtBoxExportDataLName.Text;
                string mnane = txtBoxExportDataMName.Text;
                string prefix = txtBoxExportDataPrefix.Text;
                string dobText = dtpExportDataDOB.Text;
                string street = txtBoxExportDataAddrLine.Text;
                string city = txtBoxExportDataCity.Text;
                string state = txtBoxExportDataState.Text;
                string postalCode = txtBoxExportDataPostalCode.Text;
                string country = txtBoxExportDataCountry.Text;
                string cellNbr = Regex.Replace(txtBoxExportDataCellNbr.Text, @"\D", "");
                string workNbr = Regex.Replace(txtBoxExportDataWorkNbr.Text, @"\D", "");
                string homeNbr = Regex.Replace(txtBoxExportDataHomeNbr.Text, @"\D", "");
                string email = txtBoxExportDataEmail.Text;
                string profession = txtBoxExportDataProfession.Text;
                string fatherName = txtBoxExportDataFatherName.Text;

                PersonDetail pDeatil = new PersonDetail();
                pDeatil.setPersonId(personId);
                pDeatil.setFirstName(fname);
                pDeatil.setLastName(lname);
                pDeatil.setMiddleName(mnane);
                pDeatil.setPrefix(prefix);
                pDeatil.setDOBText(dobText);
                pDeatil.setStreetAddress(street);
                pDeatil.setCity(city);
                pDeatil.setState(state);
                pDeatil.setPostalCode(postalCode);
                pDeatil.setCountry(country);
                pDeatil.setcellNbr(cellNbr);
                pDeatil.setHomwPhoneNbr(homeNbr);
                pDeatil.setWorkPhoneNbr(workNbr);
                pDeatil.setEmail(email);
                pDeatil.setProfession(profession);
                pDeatil.setFatherName(fatherName);

                activityLog.setActivity("Activity: Advanced Find/Search. \n");

                DataAccess dataAccess = new DataAccess();
                List<PersonDetail> matchedPersons = dataAccess.findPersonsToExport(pDeatil);

                this.Invoke((MethodInvoker)delegate
                {
                    //First Clear previous controlls on button click
                    this.tlpExportDataResult.Controls.Clear();

                    //Add the Table Header
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderPersonId, 1, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderFName, 2, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderLName, 3, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderState, 4, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderCountry, 5, 0);

                    Console.WriteLine("###-->>tlpFindResult.RowCount = " + tlpExportDataResult.RowCount);
                    //Build new controlls based on find ressults. Max results = 10 rows
                    for (int i = 0; i < this.tlpExportDataResult.RowCount - 1; i++)
                    {
                        if (matchedPersons.Count > i)
                        {
                            Console.WriteLine("Id = " + matchedPersons[i].getPersonId() + ", FirstName = " + matchedPersons[i].getFirstName() + ", LastName = " + matchedPersons[i].getLastName());

                            switch (i)
                            {
                                case 0:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes1, 0, i + 1);
                                    lnklblExportDataPersonId_1.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_1, 1, i + 1);
                                    break;
                                case 1:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes2, 0, i + 1);
                                    lnklblExportDataPersonId_2.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_2, 1, i + 1);
                                    break;
                                case 2:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes3, 0, i + 1);
                                    lnklblExportDataPersonId_3.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_3, 1, i + 1);
                                    break;
                                case 3:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes4, 0, i + 1);
                                    lnklblExportDataPersonId_4.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_4, 1, i + 1);
                                    break;
                                case 4:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes5, 0, i + 1);
                                    lnklblExportDataPersonId_5.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_5, 1, i + 1);
                                    break;
                                case 5:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes6, 0, i + 1);
                                    lnklblExportDataPersonId_6.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_6, 1, i + 1);
                                    break;
                                case 6:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes7, 0, i + 1);
                                    lnklblExportDataPersonId_7.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_7, 1, i + 1);
                                    break;
                                case 7:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes8, 0, i + 1);
                                    lnklblExportDataPersonId_8.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_8, 1, i + 1);
                                    break;
                                case 8:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes9, 0, i + 1);
                                    lnklblExportDataPersonId_9.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_9, 1, i + 1);
                                    break;
                                case 9:
                                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataRes10, 0, i + 1);
                                    lnklblExportDataPersonId_10.Text = matchedPersons[i].getPersonId();
                                    this.tlpExportDataResult.Controls.Add(lnklblExportDataPersonId_10, 1, i + 1);
                                    break;
                            }//end switch

                            //Label - First Nmae
                            Label lblExportDataResFirstName = new Label() { Text = matchedPersons[i].getFirstName() };
                            lblExportDataResFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResFirstName, 2, i + 1);
                            //Label - Last Name
                            Label lblExportDataResLastName = new Label() { Text = matchedPersons[i].getLastName() };
                            lblExportDataResLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResLastName, 3, i + 1);
                            //Label - State
                            Label lblExportDataResState = new Label() { Text = matchedPersons[i].getState()};
                            lblExportDataResState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResState, 4, i + 1);
                            //Label - Country
                            Label lblExportDataResCountry = new Label() { Text = matchedPersons[i].getCountry()};
                            lblExportDataResCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResCountry, 5, i + 1);
                        }
                    }
                    lblExportDataStatus.Text = "# of Match found = " + matchedPersons.Count();
                });
            } catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }//end processFindToExportData

        private void lnklblExportDataPersonId_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnklbl = (LinkLabel)sender;
            Console.WriteLine("###-->> Clicked lnklblPersonId ..." + lnklbl.Text);
            Reporter.generatePersonDetailReport(AFISMain.user, lnklbl.Text);
            activityLog.setActivity("Detail Person report created for Person Id = " + lnklbl.Text + "\n");
        }

        private void chkBoxExportDataDOB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxExportDataDOB.Checked == true)
            {
                dtpExportDataDOB.Enabled = false;
                dtpExportDataDOB.CustomFormat = " ";
                dtpExportDataDOB.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dtpExportDataDOB.Enabled = true;
                dtpExportDataDOB.Format = DateTimePickerFormat.Long;
            }
        }

        private void ExportData_Load(object sender, EventArgs e)
        {
            chkBoxExportDataDOB.Checked = true;
            dtpExportDataDOB.Enabled = false;
            dtpExportDataDOB.CustomFormat = " ";
            dtpExportDataDOB.Format = DateTimePickerFormat.Custom;
        }

        private void btnExportDataClear_Click(object sender, EventArgs e)
        {
            this.tlpExportDataResult.Controls.Clear();
            lblExportDataStatus.Text = null;
        }
    }
}
