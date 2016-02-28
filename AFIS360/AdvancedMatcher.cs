using AFIS360Common;
using AFIS360Common.dao;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SourceAFIS.Simple;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class AdvancedMatcher : Form
    {
        private ActivityLog activityLog;
        string picMatchImagePath = null;

        public AdvancedMatcher(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnAdvMatcherMatch_Click(object sender, EventArgs e)
        {
            Thread advMultiMatchThread = new Thread(processAdvancedMultiMatch);
            advMultiMatchThread.Name = "AdvMultiMatchThread";
            advMultiMatchThread.Start();

        }//end btnAdvMatcherMatch_Click

        private void processAdvancedMultiMatch()
        {
            try
            {
                //Set Serach status
                lblAdvMatcherStatus.Text = "Searching....";

                //Get the Threshold
                Int32 matchingThreshold = Convert.ToInt32(ConfigurationManager.AppSettings["InitialThresholdScore"]);
                if (!string.IsNullOrWhiteSpace(txtBoxAdvMatcherThreshold.Text)) matchingThreshold = Convert.ToInt32(txtBoxAdvMatcherThreshold.Text);


                //If fpPath = null, show the error message
                if (picMatchImagePath == null)
                {
                    MessageBox.Show("Must select a fingerprint to match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Try minimal threshold score(s) to find matches
                List<Match> matches = Program.getMatches(picMatchImagePath, "[Unknown Identity]", matchingThreshold);
                Console.WriteLine("###-->> Multi-Match performed...# of matches found = " + matches.Count);

                if (matches.Count == 0)
                {
                    MessageBox.Show("No Match found for threshold = " + matchingThreshold + ", adjust the threshold and try again.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Invoke((MethodInvoker)delegate
                {

                    //First Clear previous controlls on button click
                    this.tlpAdvMatcherResult.Controls.Clear();

                    //Add the Table Header
                    this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherPersonId, 0, 0);
                    this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherFName, 1, 0);
                    this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherLName, 2, 0);
                    this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherScore, 3, 0);

                    DataAccess dataAccess = new DataAccess();

                    for (int i = 0; i < this.tlpAdvMatcherResult.RowCount - 1; i++)
                    {

                        if(matches.Count > i)
                        {
                            MyPerson person = matches[i].getMatchedPerson();
                            float score = matches[i].getScore();
                            PersonDetail pDetail = dataAccess.retrievePersonDetail(person.PersonId).FirstOrDefault();

                            Console.WriteLine("Id = " + person.PersonId + ", FirstName = " + pDetail.getFirstName() + ", LastName = " + pDetail.getLastName());

                            switch(i)
                            {
                                case 0:
                                    lnllblAdvMatchId_1.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_1, 0, i + 1);
                                    break;
                                case 1:
                                    lnllblAdvMatchId_2.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_2, 0, i + 1);
                                    break;
                                case 2:
                                    lnllblAdvMatchId_3.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_3, 0, i + 1);
                                    break;
                                case 3:                                
                                    lnllblAdvMatchId_4.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_4, 0, i + 1);
                                    break;
                                case 4:
                                    lnllblAdvMatchId_5.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_5, 0, i + 1);
                                    break;
                                case 5:
                                    lnllblAdvMatchId_6.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_6, 0, i + 1);
                                    break;
                                case 6:
                                    lnllblAdvMatchId_7.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_7, 0, i + 1);
                                    break;
                                case 7:
                                    lnllblAdvMatchId_8.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_8, 0, i + 1);
                                    break;
                                case 8:
                                    lnllblAdvMatchId_9.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_9, 0, i + 1);
                                    break;
                                case 9:
                                    lnllblAdvMatchId_10.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_10, 0, i + 1);
                                    break;
                                case 10:
                                    lnllblAdvMatchId_11.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_11, 0, i + 1);
                                    break;
                                case 11:
                                    lnllblAdvMatchId_12.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_12, 0, i + 1);
                                    break;
                                case 12:
                                    lnllblAdvMatchId_13.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_13, 0, i + 1);
                                    break;
                                case 13:
                                    lnllblAdvMatchId_14.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_14, 0, i + 1);
                                    break;
                                case 14:
                                    lnllblAdvMatchId_15.Text = person.PersonId;
                                    this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_15, 0, i + 1);
                                    break;
                            }

                            //Label - First Nmae
                            Label lblAdvMatcherFirstName = new Label() { Text = pDetail.getFirstName() };
                            lblAdvMatcherFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherFirstName, 1, i + 1);
                            //Label - Last Name
                            Label lblAdvMatcherLastName = new Label() { Text = pDetail.getLastName() };
                            lblAdvMatcherLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherLastName, 2, i + 1);
                            //Label - Score
                            Label lblAdvMatcherScore = new Label() { Text = matches[i].getScore().ToString() };
                            lblAdvMatcherScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpAdvMatcherResult.Controls.Add(lblAdvMatcherScore, 3, i + 1);
                        }//end if
                    }//end for
                    //Set Serach status
                    lblAdvMatcherStatus.Text = "Search Completed.";
                });
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }


        private void AdvancedMatcher_Load(object sender, EventArgs e)
        {
            if (picBoxAdvMatcherFp.Image == null) picBoxAdvMatcherFp.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
            txtBoxAdvMatcherThreshold.Text = ConfigurationManager.AppSettings["InitialThresholdScore"];
            activityLog.setActivity("Activity: Advanced Match \n");
        }

        private void picBoxAdvMatcherFp_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picMatchImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picMatchImagePath = Program.convertWSQtoBMP(picMatchImagePath);
                }
                picBoxAdvMatcherFp.Image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picMatchImagePath)));
                picBoxAdvMatcherFp.SizeMode = PictureBoxSizeMode.StretchImage;

                String selectedImageName = Path.GetFileName(picMatchImagePath);
                lblAdvMatcherSelectedFp.Text = selectedImageName;
            }
        }//end picBoxAdvMatcherFp_Click

        private void btnAdvMatcherClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnllblAdvMatchId_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnklbl = (LinkLabel)sender;
            Console.WriteLine("###-->> Clicked lnklblPersonId ..." + lnklbl.Text + ", Logged in user = " + AFISMain.user.getFirstName() + " " + AFISMain.user.getLastName());
            Reporter.generatePersonDetailReport(AFISMain.user, lnklbl.Text);
            activityLog.setActivity("Detail Person report created for Person Id = " + lnklbl.Text + "\n");
        }

        private void picBoxAdvMatcherFp_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picBoxAdvMatcherFp, "Click to select the fingerprint to be matched");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
