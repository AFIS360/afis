using CsvFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class ImportData : Form
    {
        private ActivityLog activityLog;

        public ImportData(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnImportDataBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files|*.csv;...";
            DialogResult result = ofd.ShowDialog();
            txtBoxImportDataInputFile.Text = ofd.FileName;
        }
      
        private void btnImportDataImport_Click(object sender, EventArgs e)
        {
            Thread importDataThread = new Thread(this.processImportData);
            importDataThread.Name = "ImportDataThread";
            importDataThread.Start();
        }

        private void processImportData()
        {
            string filename = txtBoxImportDataInputFile.Text;
            Console.WriteLine("###-->> filename = " + filename);
            progBarImportDataImportProgress.Visible = true;
            progBarImportDataImportProgress.Minimum = 1;
            progBarImportDataImportProgress.Maximum = 1000000;
            progBarImportDataImportProgress.Value = 1;
            progBarImportDataImportProgress.Step = 1;

            try
            {

                //check if a valid export file has been selected
                if (string.IsNullOrEmpty(txtBoxImportDataInputFile.Text))
                {
                    MessageBox.Show("Must select an import file.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<string> columns = new List<string>();
                DataAccess dataAccess = new DataAccess();
                using (var reader = new CsvFileReader(filename))
                {
                    int headerRow = 0;

                    while (reader.ReadRow(columns))
                    {
                        if (headerRow > 0) //skip the Header Row
                        {
                            progBarImportDataImportProgress.PerformStep();
                            PersonDetail pDetail = new PersonDetail();
                            MyPerson person = new MyPerson();

                            System.Drawing.Image photo = null;

                            string personId = columns[0] != null ? columns[0] : "";
                            string firstName = columns[1] != null ? columns[1] : "";
                            string lastName = columns[2] != null ? columns[2] : "";
                            string middleName = columns[3] != null ? columns[3] : "";
                            string prefix = columns[4] != null ? columns[4] : "";
                            string suffix = columns[5] != null ? columns[5] : "";
                            Console.WriteLine("###-->> DateStr = " + columns[6]);
                            DateTime dob = columns[6] != null ? Convert.ToDateTime(columns[6]) : new DateTime(1753, 1, 1);
                            string streetAddr = columns[7] != null ? columns[7] : "";
                            string city = columns[8] != null ? columns[8] : "";
                            string state = columns[9] != null ? columns[9] : "";
                            string postalCode = columns[10] != null ? columns[10] : "";
                            string country = columns[11] != null ? columns[11] : "";
                            string profession = columns[12] != null ? columns[12] : "";
                            string fatherName = columns[13] != null ? columns[13] : "";
                            string cellNbr = columns[14] != null ? columns[14] : "";
                            string homeNbr = columns[15] != null ? columns[15] : "";
                            string workNbr = columns[16] != null ? columns[16] : "";
                            string email = columns[17] != null ? columns[17] : "";
                            string photoStr = columns[18] != null ? columns[18] : "";
                            if (!string.IsNullOrEmpty(photoStr))
                            {
                                photo = Program.Base64ToImage(photoStr);
                            }

                            pDetail.setPersonId(personId);
                            pDetail.setFirstName(firstName);
                            pDetail.setLastName(lastName);
                            pDetail.setMiddleName(middleName);
                            pDetail.setPrefix(prefix);
                            pDetail.setSuffix(suffix);
                            pDetail.setDOB(dob);
                            pDetail.setStreetAddress(streetAddr);
                            pDetail.setCity(city);
                            pDetail.setState(state);
                            pDetail.setPostalCode(postalCode);
                            pDetail.setCountry(country);
                            pDetail.setProfession(profession);
                            pDetail.setFatherName(fatherName);
                            pDetail.setcellNbr(cellNbr);
                            pDetail.setWorkPhoneNbr(workNbr);
                            pDetail.setHomwPhoneNbr(homeNbr);
                            pDetail.setEmail(email);
                            pDetail.setPassportPhoto(photo);

                            person.PersonId = personId;
                            person.Name = firstName;

                            Console.WriteLine("###-->> PersonDetail = " + pDetail.getPersonId() + ", " + pDetail.getFirstName() + ", " + pDetail.getLastName());
                            Status status = dataAccess.storePersonDetailwithFingerprints(person, pDetail);
                            Console.WriteLine("Status = " + status.getStatusDesc());
                        }//end if
                        headerRow++;
                    }//end while
                    progBarImportDataImportProgress.Maximum = headerRow;
                    progBarImportDataImportProgress.PerformStep();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(String.Format("Error reading from {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
