using AFIS360Common;
using AFIS360Common.dao;
using CsvFile;
using SourceAFIS.Simple;
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
            progBarImportDataImportProgress.Maximum = 1000;
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
                Int32 recordsInfile = 0;
                Int32 recordsInportedSuccessfully = 0;
                Int32 recordsFailedToImport = 0;

                using (var reader = new CsvFileReader(filename))
                {
                    while (reader.ReadRow(columns))
                    {
                        if (recordsInfile > 0) //skip the Header Row
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

                            List<Fingerprint> fingerprints = new List<Fingerprint>();
                            //RT Fingerptint
                            string rTStr = columns[19] != null ? columns[19] : "";
                            System.Drawing.Image rTImg = !string.IsNullOrEmpty(rTStr) ? Program.Base64ToImage(rTStr) : null;
                            MyFingerprint rTFp = new MyFingerprint();
                            rTFp.Fingername = MyFingerprint.RightThumb;
                            rTFp.AsBitmap = rTImg != null ? new System.Drawing.Bitmap(rTImg) : null;
                            fingerprints.Add(rTFp);
                            //RI Fingerprint
                            string rIStr = columns[20] != null ? columns[20] : "";
                            System.Drawing.Image rIImg = !string.IsNullOrEmpty(rIStr) ? Program.Base64ToImage(rIStr) : null;
                            MyFingerprint rIFp = new MyFingerprint();
                            rIFp.Fingername = MyFingerprint.RightIndex;
                            rIFp.AsBitmap = rIImg != null ? new System.Drawing.Bitmap(rIImg) : null;
                            fingerprints.Add(rIFp);
                            //RM Fingerprint
                            string rMStr = columns[21] != null ? columns[21] : "";
                            System.Drawing.Image rMImg = !string.IsNullOrEmpty(rMStr) ? Program.Base64ToImage(rMStr) : null;
                            MyFingerprint rMFp = new MyFingerprint();
                            rMFp.Fingername = MyFingerprint.RightMiddle;
                            rMFp.AsBitmap = rMImg != null ? new System.Drawing.Bitmap(rMImg) : null;
                            fingerprints.Add(rMFp);
                            //RR Fingerprint
                            string rRStr = columns[22] != null ? columns[22] : "";
                            System.Drawing.Image rRImg = !string.IsNullOrEmpty(rMStr) ? Program.Base64ToImage(rRStr) : null;
                            MyFingerprint rRFp = new MyFingerprint();
                            rRFp.Fingername = MyFingerprint.RightRing;
                            rRFp.AsBitmap = rRImg != null ? new System.Drawing.Bitmap(rRImg) : null;
                            fingerprints.Add(rRFp);
                            //RL Fingerprint
                            string rLStr = columns[23] != null ? columns[23] : "";
                            System.Drawing.Image rLImg = !string.IsNullOrEmpty(rLStr) ? Program.Base64ToImage(rLStr) : null;
                            MyFingerprint rLFp = new MyFingerprint();
                            rLFp.Fingername = MyFingerprint.RightLittle;
                            rLFp.AsBitmap = rLImg != null ? new System.Drawing.Bitmap(rLImg) : null;
                            fingerprints.Add(rLFp);
                            //LT Fingerprint
                            string lTStr = columns[24] != null ? columns[24] : "";
                            System.Drawing.Image lTImg = !string.IsNullOrEmpty(lTStr) ? Program.Base64ToImage(lTStr) : null;
                            MyFingerprint lTFp = new MyFingerprint();
                            lTFp.Fingername = MyFingerprint.LeftThumb;
                            lTFp.AsBitmap = lTImg != null ? new System.Drawing.Bitmap(lTImg) : null;
                            fingerprints.Add(lTFp);
                            //LI Fingerprint
                            string lIStr = columns[25] != null ? columns[25] : "";
                            System.Drawing.Image lIImg = !string.IsNullOrEmpty(lIStr) ? Program.Base64ToImage(lIStr) : null;
                            MyFingerprint lIFp = new MyFingerprint();
                            lIFp.Fingername = MyFingerprint.LeftIndex;
                            lIFp.AsBitmap = lIImg != null ? new System.Drawing.Bitmap(lIImg) : null;
                            fingerprints.Add(lIFp);
                            //LM Fingerprint
                            string lMStr = columns[26] != null ? columns[26] : "";
                            System.Drawing.Image lMImg = !string.IsNullOrEmpty(lMStr) ? Program.Base64ToImage(lMStr) : null;
                            MyFingerprint lMFp = new MyFingerprint();
                            lMFp.Fingername = MyFingerprint.LeftMiddle;
                            lMFp.AsBitmap = lMImg != null ? new System.Drawing.Bitmap(lMImg) : null;
                            fingerprints.Add(lMFp);
                            //LR Fingerprint
                            string lRStr = columns[27] != null ? columns[27] : "";
                            System.Drawing.Image lRImg = !string.IsNullOrEmpty(lRStr) ? Program.Base64ToImage(lRStr) : null;
                            MyFingerprint lRFp = new MyFingerprint();
                            lRFp.Fingername = MyFingerprint.LeftRing;
                            lRFp.AsBitmap = lRImg != null ? new System.Drawing.Bitmap(lRImg) : null;
                            fingerprints.Add(lRFp);
                            //LL Fingerprint
                            string lLStr = columns[28] != null ? columns[28] : "";
                            System.Drawing.Image lLImg = !string.IsNullOrEmpty(lLStr) ? Program.Base64ToImage(lLStr) : null;
                            MyFingerprint lLFp = new MyFingerprint();
                            lLFp.Fingername = MyFingerprint.LeftLittle;
                            lLFp.AsBitmap = lLImg != null ? new System.Drawing.Bitmap(lLImg) : null;
                            fingerprints.Add(lLFp);

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

                            //store person's fingerprints
                            ICollection<KeyValuePair<String, System.Drawing.Image>> fps = new Dictionary<String, System.Drawing.Image>();
                            if (rTImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightThumb, rTImg));
                            if (rIImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightIndex, rIImg));
                            if (rMImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightMiddle, rMImg));
                            if (rRImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightRing, rRImg));
                            if (rLImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightLittle, rLImg));
                            if (lTImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftThumb, lTImg));
                            if (lIImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftIndex, lIImg));
                            if (lMImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftMiddle, lMImg));
                            if (lRImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftRing, lRImg));
                            if (lLImg != null) fps.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftLittle, lLImg));

                            if (fps.Count > 0)
                            {
                                //get person with fingerprints
                                person = Program.Enroll(fps, firstName, personId);
                            }
                            else
                            {
                                person = new MyPerson();
                                person.Name = firstName;
                                person.PersonId = personId;
                            }

                            Console.WriteLine("###-->> PersonDetail = " + pDetail.getPersonId() + ", " + pDetail.getFirstName() + ", " + pDetail.getLastName());
                            Status status = dataAccess.storePersonDetailwithFingerprints(person, pDetail);
                            if (status.getStatusCode() == Status.STATUS_SUCCESSFUL)
                            {
                                recordsInportedSuccessfully++;
                            } else
                            {
                                recordsFailedToImport++;
                            }
                            Console.WriteLine("###-->> Status = " + status.getStatusDesc() + ". # of Successful import = " + recordsInportedSuccessfully + ", Failed import = " + recordsFailedToImport);
                        }//end if
                        recordsInfile++;
                    }//end while
                    progBarImportDataImportProgress.Maximum = recordsInfile;
                    progBarImportDataImportProgress.PerformStep();
                }
                Int32 totalImportedRecords = progBarImportDataImportProgress.Maximum - 1; //subtracting header
                activityLog.setActivity("Batch Import from a file - " + filename + ". Total # of records imported = " + totalImportedRecords + "\n");
                lblImportDataTotalRecCount.Text = Convert.ToString(recordsInportedSuccessfully + recordsFailedToImport);
                lblDataImportSuccessCount.Text = Convert.ToString(recordsInportedSuccessfully);
                lblImportDataFailedCount.Text = Convert.ToString(recordsFailedToImport);
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
