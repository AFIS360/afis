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
                    MessageBox.Show("Must select a finger print to match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Try minimal threshold score(s) to find matches
                List<Match> matches = Program.getMatches(picMatchImagePath, "[Unknown Identity]", matchingThreshold);
                Console.WriteLine("###-->> Multi-Match performed...# of matches found = " + matches.Count);

                //Set Serach status
                lblAdvMatcherStatus.Text = "Search completed.";

                if (matches.Count == 0)
                {
                    MessageBox.Show("No Match found for threshold = " + matchingThreshold + ", adjust the threshold and try again.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                    MyPerson person = matches[i].getMatchedPerson();
                    float score = matches[i].getScore();
                    PersonDetail pDetail = dataAccess.retrievePersonDetail(person.PersonId).FirstOrDefault();
                    Console.WriteLine("Id = " + person.PersonId + ", FirstName = " + pDetail.getFirstName() + ", LastName = " + pDetail.getLastName());

                    if (i == 0)
                    {
                        lnllblAdvMatchId_1.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_1, 0, i + 1);
                    }
                    else if (i == 1)
                    {
                        lnllblAdvMatchId_2.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_2, 0, i + 1);
                    }
                    else if (i == 2)
                    {
                        lnllblAdvMatchId_3.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_3, 0, i + 1);
                    }
                    else if (i == 3)
                    {
                        lnllblAdvMatchId_4.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_4, 0, i + 1);
                    }
                    else if (i == 4)
                    {
                        lnllblAdvMatchId_5.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_5, 0, i + 1);
                    }
                    else if (i == 5)
                    {
                        lnllblAdvMatchId_6.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_6, 0, i + 1);
                    }
                    else if (i == 6)
                    {
                        lnllblAdvMatchId_7.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_7, 0, i + 1);
                    }
                    else if (i == 7)
                    {
                        lnllblAdvMatchId_8.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_8, 0, i + 1);
                    }
                    else if (i == 8)
                    {
                        lnllblAdvMatchId_9.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_9, 0, i + 1);
                    }
                    else if (i == 9)
                    {
                        lnllblAdvMatchId_10.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_10, 0, i + 1);
                    }
                    else if (i == 10)
                    {
                        lnllblAdvMatchId_11.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_11, 0, i + 1);
                    }
                    else if (i == 11)
                    {
                        lnllblAdvMatchId_12.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_12, 0, i + 1);
                    }
                    else if (i == 12)
                    {
                        lnllblAdvMatchId_13.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_13, 0, i + 1);
                    }
                    else if (i == 13)
                    {
                        lnllblAdvMatchId_14.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_14, 0, i + 1);
                    }
                    else if (i == 14)
                    {
                        lnllblAdvMatchId_15.Text = person.PersonId;
                        this.tlpAdvMatcherResult.Controls.Add(lnllblAdvMatchId_15, 0, i + 1);
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


                    if (matches.Count < i) break;

                }//end for
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.StackTrace);
            }


        }//end btnAdvMatcherMatch_Click


        private void AdvancedMatcher_Load(object sender, EventArgs e)
        {
            if (picBoxAdvMatcherFp.Image == null) picBoxAdvMatcherFp.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
            txtBoxAdvMatcherThreshold.Text = ConfigurationManager.AppSettings["InitialThresholdScore"];
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
            Console.WriteLine("###-->> Clicked lnklblPersonId ..." + lnklbl.Text);
//            generateAuditReportPersonDetailReport(lnklbl.Text);
        }

/*
        private void generateAuditReportPersonDetailReport(string personId)
        {
            activityLog.setActivity("Person Detailed Report Created.");

            List<PersonDetail> personDetailList = new DataAccess().retrievePersonDetail(personId);
            Console.WriteLine("# of Persons found = " + personDetailList.Count());
            PersonDetail personDetail = personDetailList.FirstOrDefault();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            string datetimePref = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string pdfPath = ConfigurationManager.AppSettings["PersonReportPath"] + "-" + datetimePref + ".pdf";
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
            doc.Open();

            //add title
            doc.AddTitle("Person Detail Report");
            doc.AddHeader("Person Detail Report", "Person Detail Report");

            Paragraph paragraphCompanyInfo = new Paragraph("RAB (Rapid Action Battalion)\n");
            paragraphCompanyInfo.Add("Station: " + user.getStationId() + ", " + user.getStationedCity() + "\n");
            paragraphCompanyInfo.Add(user.getStationedCountry() + "\n");
            paragraphCompanyInfo.Alignment = Element.ALIGN_LEFT;

            iTextSharp.text.Font contentFont = iTextSharp.text.FontFactory.GetFont("Webdings", 20, iTextSharp.text.Font.BOLD);
            Paragraph paragraphReportTitle = new Paragraph("Person Detail Report\n", contentFont);
            paragraphReportTitle.Alignment = Element.ALIGN_CENTER;

            Paragraph paragraphReportSubTitle = new Paragraph();
            paragraphReportSubTitle.Add("By: " + user.getFirstName() + " " + user.getLastName() + ", ID: " + user.getPersonId() + "\n");
            paragraphReportSubTitle.Add("At: " + DateTime.Now.ToString() + "\n\n");
            paragraphReportSubTitle.Alignment = Element.ALIGN_CENTER;

            doc.Add(paragraphCompanyInfo);
            doc.Add(paragraphReportTitle);
            doc.Add(paragraphReportSubTitle);

            if (personDetail != null)
            {
                //Adding the Passport size photo
                System.Drawing.Image passportPhoto = personDetail.getPassportPhoto();
                if (passportPhoto != null)
                {
                    iTextSharp.text.Image passportPic = iTextSharp.text.Image.GetInstance(passportPhoto, System.Drawing.Imaging.ImageFormat.Bmp);
                    passportPic.ScaleAbsolute(120f, 120f);
                    doc.Add(passportPic);
                }

                //Adding the person detail
                Paragraph paragraphReportBody = new Paragraph();
                paragraphReportBody.Add("ID: " + personDetail.getPersonId() + "\n");
                paragraphReportBody.Add("Name: " + " " + personDetail.getPrefix() + " " + personDetail.getFirstName() + " " + personDetail.getMiddleName() + " " + personDetail.getLastName() + " " + personDetail.getSuffix() + "\n");
                paragraphReportBody.Add("Date of Birth (DOB): " + ((DateTime)personDetail.getDOB()).ToString("yyyy-MM-dd") + "\n");
                paragraphReportBody.Add("Father's Name: " + personDetail.getFatherName() + "\n");
                paragraphReportBody.Add("Address: " + personDetail.getStreetAddress() + ", " + personDetail.getCity() + ", " + personDetail.getState() + " " + personDetail.getPostalCode() + ", " + personDetail.getCountry() + "\n");
                paragraphReportBody.Add("Profession: " + personDetail.getProfession() + "\n");
                paragraphReportBody.Add("Cell#: " + personDetail.getCellNbr() + ", Home Phone#: " + personDetail.getHomePhoneNbr() + ", Work Phone#: " + personDetail.getWorkPhoneNbr() + "\n");
                paragraphReportBody.Add("Email: " + personDetail.getEmail() + "\n\n");
                doc.Add(paragraphReportBody);
            }

            //add table for fingerprints
            Paragraph paragraphFingerprints = new Paragraph();
            paragraphFingerprints.Add("Fingerprint(s):\n\n");
            doc.Add(paragraphFingerprints);

            PdfPTable fingerprintsTable = new PdfPTable(5);
            float[] widths = new float[] { 40f, 40f, 40f, 40f, 40f };
            fingerprintsTable.SetWidths(widths);

            //Add Headers to the table
            fingerprintsTable.AddCell(new PdfPCell(new Phrase("RT")));
            fingerprintsTable.AddCell(new PdfPCell(new Phrase("RI")));
            fingerprintsTable.AddCell(new PdfPCell(new Phrase("RM")));
            fingerprintsTable.AddCell(new PdfPCell(new Phrase("RR")));
            fingerprintsTable.AddCell(new PdfPCell(new Phrase("RL")));

            PdfPCell imageRTCell = null;
            PdfPCell imageRICell = null;
            PdfPCell imageRMCell = null;
            PdfPCell imageRRCell = null;
            PdfPCell imageRLCell = null;
            PdfPCell imageLTCell = null;
            PdfPCell imageLICell = null;
            PdfPCell imageLMCell = null;
            PdfPCell imageLRCell = null;
            PdfPCell imageLLCell = null;

            //Default image in case, image is not available
            iTextSharp.text.Image iTextDefaultFpImage = iTextSharp.text.Image.GetInstance(ConfigurationManager.AppSettings["DefaultFpImagePath"]);

            List<MyPerson> persons = new DataAccess().retrievePersonFingerprintsById(personId);
            Console.WriteLine("####-->> # of persons retrived = " + persons.Count);

            if (persons.Count > 0)
            {
                MyPerson person = persons.FirstOrDefault();
                //Get all the fingerprints of the matched person 
                List<Fingerprint> fps = person.Fingerprints;
                Console.WriteLine("###-->> # of Fps retrived = " + fps.Count);

                for (int i = 0; i < fps.Count; i++)
                {
                    MyFingerprint fp = (MyFingerprint)fps.ElementAt(i);

                    if (fp.Fingername != null)
                    {
                        if (fp.Fingername.Equals(MyFingerprint.RightThumb))
                        {
                            System.Drawing.Image imageRT = fp.AsBitmap;
                            if (imageRT != null)
                            {
                                Console.WriteLine("###-->> RT");
                                iTextSharp.text.Image iTextImgRT = iTextSharp.text.Image.GetInstance(imageRT, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgRT.ScaleAbsolute(60f, 60f);
                                imageRTCell = new PdfPCell(iTextImgRT);
                                imageRTCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageRTCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightIndex))
                        {
                            System.Drawing.Image imageRI = fp.AsBitmap;
                            if (imageRI != null)
                            {
                                Console.WriteLine("###-->> RI");
                                iTextSharp.text.Image iTextImgRI = iTextSharp.text.Image.GetInstance(imageRI, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgRI.ScaleAbsolute(60f, 60f);
                                imageRICell = new PdfPCell(iTextImgRI);
                                imageRICell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageRICell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightMiddle))
                        {
                            System.Drawing.Image imageRM = fp.AsBitmap;
                            if (imageRM != null)
                            {
                                Console.WriteLine("###-->> RM");
                                iTextSharp.text.Image iTextImgRM = iTextSharp.text.Image.GetInstance(imageRM, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgRM.ScaleAbsolute(60f, 60f);
                                imageRMCell = new PdfPCell(iTextImgRM);
                                imageRMCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageRMCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightRing))
                        {
                            System.Drawing.Image imageRR = fp.AsBitmap;
                            if (imageRR != null)
                            {
                                Console.WriteLine("###-->> RR");
                                iTextSharp.text.Image iTextImgRR = iTextSharp.text.Image.GetInstance(imageRR, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgRR.ScaleAbsolute(60f, 60f);
                                imageRRCell = new PdfPCell(iTextImgRR);
                                imageRRCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageRRCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightLittle))
                        {
                            System.Drawing.Image imageRL = fp.AsBitmap;
                            if (imageRL != null)
                            {
                                Console.WriteLine("###-->> RL");
                                iTextSharp.text.Image iTextImgRL = iTextSharp.text.Image.GetInstance(imageRL, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgRL.ScaleAbsolute(60f, 60f);
                                imageRLCell = new PdfPCell(iTextImgRL);
                                imageRLCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageRLCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }

                        else if (fp.Fingername.Equals(MyFingerprint.LeftThumb))
                        {
                            System.Drawing.Image imageLT = fp.AsBitmap;
                            if (imageLT != null)
                            {
                                Console.WriteLine("###-->> LT");
                                iTextSharp.text.Image iTextImgLT = iTextSharp.text.Image.GetInstance(imageLT, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgLT.ScaleAbsolute(60f, 60f);
                                imageLTCell = new PdfPCell(iTextImgLT);
                                imageLTCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageLTCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftIndex))
                        {
                            System.Drawing.Image imageLI = fp.AsBitmap;
                            if (imageLI != null)
                            {
                                Console.WriteLine("###-->> LI");
                                iTextSharp.text.Image iTextImgLI = iTextSharp.text.Image.GetInstance(imageLI, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgLI.ScaleAbsolute(60f, 60f);
                                imageLICell = new PdfPCell(iTextImgLI);
                                imageLICell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageLICell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftMiddle))
                        {
                            System.Drawing.Image imageLM = fp.AsBitmap;
                            if (imageLM != null)
                            {
                                Console.WriteLine("###-->> LM");
                                iTextSharp.text.Image iTextImgLM = iTextSharp.text.Image.GetInstance(imageLM, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgLM.ScaleAbsolute(60f, 60f);
                                imageLMCell = new PdfPCell(iTextImgLM);
                                imageLMCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageLMCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftRing))
                        {
                            System.Drawing.Image imageLR = fp.AsBitmap;
                            if (imageLR != null)
                            {
                                Console.WriteLine("###-->> LR");
                                iTextSharp.text.Image iTextImgLR = iTextSharp.text.Image.GetInstance(imageLR, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgLR.ScaleAbsolute(60f, 60f);
                                imageLRCell = new PdfPCell(iTextImgLR);
                                imageLRCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageLRCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftLittle))
                        {
                            System.Drawing.Image imageLL = fp.AsBitmap;
                            if (imageLL != null)
                            {
                                Console.WriteLine("###-->> LL");
                                iTextSharp.text.Image iTextImgLL = iTextSharp.text.Image.GetInstance(imageLL, System.Drawing.Imaging.ImageFormat.Bmp);
                                iTextImgLL.ScaleAbsolute(60f, 60f);
                                imageLLCell = new PdfPCell(iTextImgLL);
                                imageLLCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                imageLLCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                        }
                    }
                }

                //add the Right-Hand fingerprints
                if (imageRTCell != null)
                {
                    fingerprintsTable.AddCell(imageRTCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }

                if (imageRICell != null)
                {
                    fingerprintsTable.AddCell(imageRICell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageRMCell != null)
                {
                    fingerprintsTable.AddCell(imageRMCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageRRCell != null)
                {
                    fingerprintsTable.AddCell(imageRRCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageRLCell != null)
                {
                    fingerprintsTable.AddCell(imageRLCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }

                //add 2nd row on the table
                fingerprintsTable.AddCell(new PdfPCell(new Phrase("LT")));
                fingerprintsTable.AddCell(new PdfPCell(new Phrase("LI")));
                fingerprintsTable.AddCell(new PdfPCell(new Phrase("LM")));
                fingerprintsTable.AddCell(new PdfPCell(new Phrase("LR")));
                fingerprintsTable.AddCell(new PdfPCell(new Phrase("LL")));

                //add the Left-Hand fingerprints
                if (imageLTCell != null)
                {
                    fingerprintsTable.AddCell(imageLTCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }

                if (imageLICell != null)
                {
                    fingerprintsTable.AddCell(imageLICell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageLMCell != null)
                {
                    fingerprintsTable.AddCell(imageLMCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageLRCell != null)
                {
                    fingerprintsTable.AddCell(imageLRCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }
                if (imageLLCell != null)
                {
                    fingerprintsTable.AddCell(imageLLCell);
                }
                else
                {
                    fingerprintsTable.AddCell(new PdfPCell(iTextDefaultFpImage, true));
                }


            }//end-if - persons

            //add Table for fingerprints

            doc.Add(fingerprintsTable);

            doc.Close();
            Console.WriteLine("PDF Generated successfully...");
            System.Diagnostics.Process.Start(pdfPath);
        }
*/
    }
}
