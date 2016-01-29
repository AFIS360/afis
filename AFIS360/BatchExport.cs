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
    public partial class BatchExport : Form
    {
        private ActivityLog activityLog;

        public BatchExport(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBatchExportBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files|*.csv;...";
            DialogResult result = ofd.ShowDialog();
            txtBoxBatchExportFile.Text = ofd.FileName;
        }

        private void btnBatchExportExport_Click(object sender, EventArgs e)
        {
            Thread importDataThread = new Thread(this.processBatchExport);
            importDataThread.Name = "ImportDataThread";
            importDataThread.Start();
        }

        private void processBatchExport()
        {
            string fileName = txtBoxBatchExportFile.Text;

            try
            {
                //check if a valid export file has been selected
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Must select an export file.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Progress bar
                DataAccess dataAccess = new DataAccess();
                Int32 personCount = dataAccess.getPersonCount();
                Console.WriteLine("###-->>Total number of person records = " + personCount);
                activityLog.setActivity("Batch Export to a file - " + fileName + ". Totall record exported = " + personCount + "\n");

                progressBarBatchExport.Visible = true;
                progressBarBatchExport.Minimum = 1;
                progressBarBatchExport.Maximum = personCount;
                progressBarBatchExport.Value = 1;
                progressBarBatchExport.Step = 1;

                using (var writer = new CsvFileWriter(fileName))
                {
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

                    List<PersonDetail> persons = dataAccess.getPersons();
                    foreach(PersonDetail personDetail in persons)
                    {
                        progressBarBatchExport.PerformStep();

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
                    }//end foreach
                    lblBatchExportTotalRec.Text = "Total # of exported records = " + persons.Count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(String.Format("Error reading from {0}.\r\n\r\n{1}", fileName, ex.Message));
            }
        }
    }
}
