using CsvFile;
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
        private List<PersonDetail> matchedPersons = null;

        public ExportData(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnExportDataFind_Click(object sender, EventArgs e)
        {
            Thread searchFindToExportThread = new Thread(processFindToExportData);
            searchFindToExportThread.Name = "FindExportDataThread";
            searchFindToExportThread.Start();
        }

        private void processFindToExportData()
        {
            try
            {
                //First clear the previous population
                clearExportDataFields();

                progBarExportData.Visible = true;
                progBarExportData.Minimum = 1;
                progBarExportData.Maximum = tlpExportDataResult.RowCount;
                progBarExportData.Value = 1;
                progBarExportData.Step = 1;


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
                matchedPersons = dataAccess.findPersonsToExport(pDeatil);
                Console.WriteLine("###-->> Data Access complete @ = " + DateTime.Now);

                this.Invoke((MethodInvoker)delegate
                {
                    //First Clear previous controlls on button click
                    this.tlpExportDataResult.Controls.Clear();

                    //Add the Table Header
                    this.tlpExportDataResult.Controls.Add(chkBoxExportDataSelectAll, 0, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderPersonId, 1, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderFName, 2, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderLName, 3, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderState, 4, 0);
                    this.tlpExportDataResult.Controls.Add(lblExportDataResHeaderCountry, 5, 0);

                    Console.WriteLine("###-->>tlpFindResult.RowCount = " + tlpExportDataResult.RowCount);
                    //Build new controlls based on find ressults. Max results = 10 rows
                    for (int i = 0; i < this.tlpExportDataResult.RowCount - 1; i++)
                    {
                        progBarExportData.PerformStep();

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
                            Label lblExportDataResState = new Label() { Text = matchedPersons[i].getState() };
                            lblExportDataResState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResState, 4, i + 1);
                            //Label - Country
                            Label lblExportDataResCountry = new Label() { Text = matchedPersons[i].getCountry() };
                            lblExportDataResCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpExportDataResult.Controls.Add(lblExportDataResCountry, 5, i + 1);
                        }
                    }
                    lblExportDataStatus.Text = "# of Match found = " + matchedPersons.Count();
                });
                Console.WriteLine("###-->> Table creation complete @ = " + DateTime.Now);
            }
            catch (Exception exp)
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
            clearExportDataFields();
        }
        private void clearExportDataFields()
        {
            this.tlpExportDataResult.Controls.Clear();
            lblExportDataStatus.Text = null;
            chkBoxExportDataSelectAll.Checked = false;
            chkBoxExportDataRes1.Checked = false;
            chkBoxExportDataRes2.Checked = false;
            chkBoxExportDataRes3.Checked = false;
            chkBoxExportDataRes4.Checked = false;
            chkBoxExportDataRes5.Checked = false;
            chkBoxExportDataRes6.Checked = false;
            chkBoxExportDataRes7.Checked = false;
            chkBoxExportDataRes8.Checked = false;
            chkBoxExportDataRes9.Checked = false;
            chkBoxExportDataRes10.Checked = false;
        }

        private void btnExportDataExport_Click(object sender, EventArgs e)
        {
            Thread exportDataThread = new Thread(processToExportData);
            exportDataThread.Name = "processExportDataThread";
            exportDataThread.Start();
        }

        private void processToExportData()
        {
            //check if a valid export file has been selected
            if(string.IsNullOrEmpty(txtBoxExportDataExportFolder.Text))
            {
                MessageBox.Show("Must select an export file.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filename = txtBoxExportDataExportFolder.Text;
            progBarExportData.Visible = true;
            progBarExportData.Minimum = 1;
            progBarExportData.Value = 1;
            progBarExportData.Step = 1;


            try
            {
                ICollection<KeyValuePair<String, bool>> selectedRows = new Dictionary<String, bool>();
                if (chkBoxExportDataRes1.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_1.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_1.Text, chkBoxExportDataRes1.Checked));
                if (chkBoxExportDataRes2.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_2.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_2.Text, chkBoxExportDataRes2.Checked));
                if (chkBoxExportDataRes3.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_3.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_3.Text, chkBoxExportDataRes3.Checked));
                if (chkBoxExportDataRes4.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_4.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_4.Text, chkBoxExportDataRes4.Checked));
                if (chkBoxExportDataRes5.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_5.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_5.Text, chkBoxExportDataRes5.Checked));
                if (chkBoxExportDataRes6.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_6.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_6.Text, chkBoxExportDataRes6.Checked));
                if (chkBoxExportDataRes7.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_7.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_7.Text, chkBoxExportDataRes7.Checked));
                if (chkBoxExportDataRes8.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_8.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_8.Text, chkBoxExportDataRes8.Checked));
                if (chkBoxExportDataRes9.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_9.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_9.Text, chkBoxExportDataRes9.Checked));
                if (chkBoxExportDataRes10.Checked && !string.IsNullOrEmpty(lnklblExportDataPersonId_10.Text)) selectedRows.Add(new KeyValuePair<string, bool>(lnklblExportDataPersonId_10.Text, chkBoxExportDataRes10.Checked));

                if(selectedRows.Count < 1)
                {
                    MessageBox.Show("Must select one or more record(s) from the table below.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(chkBoxExportDataSelectAll.Checked)
                {
                    DialogResult dr = MessageBox.Show("All records will will be exported. Do you want to continue?", "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }

                progBarExportData.Maximum = selectedRows.Count;

                using (var writer = new CsvFileWriter(filename))
                {
                    progBarExportData.PerformStep();

                    //add the headers
                    List<string> columns = new List<string>();
                    columns.Add("Person Id");
                    columns.Add("First Name");
                    columns.Add("Last Name");
                    columns.Add("Middle Name");
                    columns.Add("Prefix");
                    columns.Add("Suffix");
                    columns.Add("DOB");
                    columns.Add("Street");
                    columns.Add("City");
                    columns.Add("State");
                    columns.Add("Postal Code");
                    columns.Add("Country");
                    columns.Add("Profession");
                    columns.Add("Father Name");
                    columns.Add("Cell Nbr");
                    columns.Add("Home Nbr");
                    columns.Add("Work Nbr");
                    columns.Add("Email");
                    columns.Add("Photo");
                    writer.WriteRow(columns);

                    foreach (KeyValuePair<string, bool> selectedRow in selectedRows)
                    {
                        progBarExportData.PerformStep();

                        string personId = selectedRow.Key;
                        bool rowSelected = selectedRow.Value;
                        PersonDetail personDetail = getPersonDetail(personId);

                        columns = new List<string>();
                        columns.Add(personDetail.getPersonId() ?? String.Empty);
                        columns.Add(personDetail.getFirstName() ?? String.Empty);
                        columns.Add(personDetail.getLastName() ?? String.Empty);
                        columns.Add(personDetail.getMiddleName() ?? String.Empty);
                        columns.Add(personDetail.getPrefix() ?? String.Empty);
                        columns.Add(personDetail.getSuffix() ?? String.Empty);
                        columns.Add(personDetail.getDOB().ToString() ?? String.Empty);
                        columns.Add(personDetail.getStreetAddress() ?? String.Empty);
                        columns.Add(personDetail.getCity() ?? String.Empty);
                        columns.Add(personDetail.getState() ?? String.Empty);
                        columns.Add(personDetail.getPostalCode() ?? String.Empty);
                        columns.Add(personDetail.getCountry() ?? String.Empty);
                        columns.Add(personDetail.getProfession() ?? String.Empty);
                        columns.Add(personDetail.getFatherName() ?? String.Empty);
                        columns.Add(personDetail.getCellNbr() ?? String.Empty);
                        columns.Add(personDetail.getHomePhoneNbr() ?? String.Empty);
                        columns.Add(personDetail.getWorkPhoneNbr() ?? String.Empty);
                        columns.Add(personDetail.getEmail() ?? String.Empty);
                        System.Drawing.Image photo = personDetail.getPassportPhoto();
                        string photoStr = (photo != null) ? Program.ImageToBase64(personDetail.getPassportPhoto(), System.Drawing.Imaging.ImageFormat.Bmp) : null;
                        columns.Add(photoStr ?? String.Empty);
                        writer.WriteRow(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("###-->> Exception = " + ex.StackTrace);
                MessageBox.Show(String.Format("Error writing to {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }//end processToExportData

        private PersonDetail getPersonDetail(string personId)
        {
            PersonDetail pDetail = null;

            foreach (PersonDetail personDetail in matchedPersons)
            {
                if (personDetail.getPersonId() == personId)
                {
                    pDetail = personDetail;
                    Console.WriteLine("###-->> Matched against list of person = " + personId);
                    break;
                }
            }
            return pDetail;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkBoxExportDataSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkBoxExportDataSelectAll.Checked)
            {
                chkBoxExportDataRes1.Checked = true;
                chkBoxExportDataRes2.Checked = true;
                chkBoxExportDataRes3.Checked = true;
                chkBoxExportDataRes4.Checked = true;
                chkBoxExportDataRes5.Checked = true;
                chkBoxExportDataRes6.Checked = true;
                chkBoxExportDataRes7.Checked = true;
                chkBoxExportDataRes8.Checked = true;
                chkBoxExportDataRes9.Checked = true;
                chkBoxExportDataRes10.Checked = true;
            } else
            {
                chkBoxExportDataRes1.Checked = false;
                chkBoxExportDataRes2.Checked = false;
                chkBoxExportDataRes3.Checked = false;
                chkBoxExportDataRes4.Checked = false;
                chkBoxExportDataRes5.Checked = false;
                chkBoxExportDataRes6.Checked = false;
                chkBoxExportDataRes7.Checked = false;
                chkBoxExportDataRes8.Checked = false;
                chkBoxExportDataRes9.Checked = false;
                chkBoxExportDataRes10.Checked = false;
            }
        }

        private void btnExportDataExportFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files|*.csv;...";
            DialogResult result = ofd.ShowDialog();
            txtBoxExportDataExportFolder.Text = ofd.FileName;
        }

        private void chkBoxExportDataRes1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            Console.WriteLine("###-->> chkBox = " + chkBox.Checked);
            if (!chkBox.Checked) validateSelectAllCheckBox();
        }

        private void validateSelectAllCheckBox()
        {
            if (chkBoxExportDataSelectAll.Checked) chkBoxExportDataSelectAll.Checked = false;
        }
    }
}
