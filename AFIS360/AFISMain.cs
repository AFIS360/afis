using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SourceAFIS.Simple;
using System.Windows.Media.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using CsvFile;

namespace AFIS360
{
    public partial class AFISMain : Form
    {
        string picMatchImagePath = null;
        User cachedUser = null;
        public static User user = null;
        public static ActivityLog activityLog = null;
        public static AppConfig appConfig = null;


        public AFISMain()
        {
            Console.WriteLine("Init components...");
            InitializeComponent();
            tabControlAFIS.TabPages.Remove(tabEnroll);
            tabControlAFIS.TabPages.Remove(tabMatch);
            tabControlAFIS.TabPages.Remove(tabUserMgmt);
            tabControlAFIS.TabPages.Remove(tabAuditReport);
            tabControlAFIS.TabPages.Remove(tabFind);
            menuStrip.Visible = true;
            btnUserMgmtUpdate.Enabled = false;
            //set the default image for match picBox
            if (picMatch.Image == null) picMatch.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
      }


        private void btnEnroll_Click(object sender, EventArgs e)
        {
            string id = txtEnrollId.Text;
            string fname = txtEnrollFName.Text;
            string lname = txtEnrollLName.Text;
            string mname = txtEnrollMName.Text;
            string prefix = txtEnrollPrefix.Text;
            string suffix = txtEnrollSuffix.Text;
            DateTime dobTemp = dtpEnrollDOB.Value;
            DateTime dob = Convert.ToDateTime(dobTemp.ToString("MM/dd/yyy"));
            string streeAddr = txtEnrollAddrLine.Text;
            string city = txtEnrollCity.Text;
            string postalCode = txtEnrollPostalCode.Text;
            string state = txtEnrollState.Text;
            string country = txtEnrollCountry.Text;
            string profession = txtEnrollProfession.Text;
            string fatherName = txtEnrollFatherName.Text;
            string cellNbr = txtEnrollCellNbr.Text != null ? Regex.Replace(txtEnrollCellNbr.Text, @"\D", "") : null;
            string workPhoneNbr = txtEnrollWorkPNbr.Text;
            string homePhoneNbr = txtEnrollHomePNbr.Text;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
//            string statusStr = null;
            Status status = null;

            try
            {
                PersonDetail personDetail = new PersonDetail();
                personDetail.setPersonId(id);
                personDetail.setFirstName(fname);
                personDetail.setLastName(lname);
                personDetail.setMiddleName(mname);
                personDetail.setPrefix(prefix);
                personDetail.setSuffix(suffix);
                personDetail.setDOB(dob);
                personDetail.setStreetAddress(streeAddr);
                personDetail.setCity(city);
                personDetail.setPostalCode(postalCode);
                personDetail.setState(state);
                personDetail.setCountry(country);
                personDetail.setProfession(profession);
                personDetail.setFatherName(fatherName);
                personDetail.setcellNbr(cellNbr);
                personDetail.setWorkPhoneNbr(workPhoneNbr);
                personDetail.setHomwPhoneNbr(homePhoneNbr);
                personDetail.setEmail(email);
                personDetail.setPassportPhoto(passportPhoto);

                //store person's demograpgy
                DataAccess dataAccess = new DataAccess();

                if (string.IsNullOrWhiteSpace(id))
                {
                    MessageBox.Show("Person ID field is required.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //store person's fingerprints
                ICollection<KeyValuePair<String, System.Drawing.Image>> imgsFromPicBox = new Dictionary<String, System.Drawing.Image>();
                if (picEnrollRT.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightThumb, picEnrollRT.Image));
                if (picEnrollRI.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightIndex, picEnrollRI.Image));
                if (picEnrollRM.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightMiddle, picEnrollRM.Image));
                if (picEnrollRR.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightRing, picEnrollRR.Image));
                if (picEnrollRL.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightLittle, picEnrollRL.Image));
                if (picEnrollLT.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftThumb, picEnrollLT.Image));
                if (picEnrollLI.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftIndex, picEnrollLI.Image));
                if (picEnrollLM.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftMiddle, picEnrollLM.Image));
                if (picEnrollLR.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftRing, picEnrollLR.Image));
                if (picEnrollLL.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftLittle, picEnrollLL.Image));

                MyPerson person;
                if (imgsFromPicBox.Count > 0)
                {
                        //get person with fingerprints
                        person = Program.Enroll(imgsFromPicBox, fname, id);
                }
                else
                {
                    person = new MyPerson();
                    person.Name = fname;
                    person.PersonId = id;
                }
                //store person with fingerprint images
                //                dataAccess.storeFingerprints(person);
                status = dataAccess.storePersonDetailwithFingerprints(person, personDetail);

                //store person without fingerprint images but fingerptint templates. Match will be performed against the template
                //                dataAccess.storeFingerprintTemplates(person);

                //                status = "Enrollment of " + fname + " (Id = " + id + ") completed successfully.";
                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                } else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exp)
            {
//                statusStr = "Enrollment of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Enrollment of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".");
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
            }

            lblEnrollStatusMsg.Text = status.getStatusDesc();
        }


        private void btnMatch_Click(object sender, EventArgs e)
        {
            Thread fingerprintMatchThread = new Thread(processFingerprintMatch);
            fingerprintMatchThread.Name = "FpMatchThread";
            fingerprintMatchThread.Start(sender);
        }

        private void processFingerprintMatch(object sender)
        {
            Int32 matchingThreshold = Convert.ToInt32(ConfigurationManager.AppSettings["InitialThresholdScore"]);
            string fpPath = picMatchImagePath;
            string message = null;

            if (!string.IsNullOrWhiteSpace(txtMatchThreshold.Text)) matchingThreshold = Convert.ToInt32(txtMatchThreshold.Text);

            //If fpPath = null, show the error message
            if (fpPath == null)
            {
                MessageBox.Show("Must select a fingerprint to match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Set Serach status
            lblMatchProgressStatus.Text = "Searching....";

            Match match = Program.getMatch(fpPath, "[Unknown Identity]", matchingThreshold);
            MyPerson matchedPerson = match.getMatchedPerson();

            //clear the Matched Result section before populating
            clearMatchTab(sender);

            if (matchedPerson != null)
            {
                string personId = matchedPerson.PersonId;
                DataAccess dataAccess = new DataAccess();
                List<PersonDetail> personsDetail = dataAccess.retrievePersonDetail(personId);
                PersonDetail pDetail = (PersonDetail)personsDetail.FirstOrDefault();
                lblMatchResIDTxt.Text = pDetail.getPersonId();
                lblMatchResFNameTxt.Text = pDetail.getFirstName();
                lblMatchResLNameTxt.Text = pDetail.getLastName();
                richTxtMatchResAdds.Text = pDetail.getStreetAddress() + "\n" + pDetail.getCity() + ", " + pDetail.getState() + " " + pDetail.getPostalCode() + "\n" + pDetail.getCountry();
                picBoxMatchResPPhoto.Image = pDetail.getPassportPhoto();

                //Get all the fingerprints of the matched person 
                MyPerson person = dataAccess.retrievePersonFingerprintsById(pDetail.getPersonId()).FirstOrDefault();
                List<Fingerprint> fps = person.Fingerprints;

                for (int i = 0; i < fps.Count; i++)
                {
                    MyFingerprint fp = (MyFingerprint)fps.ElementAt(i);

                    if (fp.Fingername != null)
                    {
                        if (fp.Fingername.Equals(MyFingerprint.RightThumb))
                        {
                            picMatchRT.Image = fp.AsBitmap;
                            picMatchRT.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightIndex))
                        {
                            picMatchRI.Image = fp.AsBitmap;
                            picMatchRI.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightMiddle))
                        {
                            picMatchRM.Image = fp.AsBitmap;
                            picMatchRM.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightRing))
                        {
                            picMatchRR.Image = fp.AsBitmap;
                            picMatchRR.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.RightLittle))
                        {
                            picMatchRL.Image = fp.AsBitmap;
                            picMatchRL.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftThumb))
                        {
                            picMatchLT.Image = fp.AsBitmap;
                            picMatchLT.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftIndex))
                        {
                            picMatchLI.Image = fp.AsBitmap;
                            picMatchLI.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftMiddle))
                        {
                            picMatchLM.Image = fp.AsBitmap;
                            picMatchLM.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftRing))
                        {
                            picMatchLR.Image = fp.AsBitmap;
                            picMatchLR.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else if (fp.Fingername.Equals(MyFingerprint.LeftLittle))
                        {
                            picMatchLL.Image = fp.AsBitmap;
                            picMatchLL.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        Console.WriteLine("####-->>>> Finger Name is not assigned");
                    }
                }
                message = "Match found. Matching Score: " + match.getScore();
                lblMatchResTxt.ForeColor = Color.Green;
                //adding the activity log
                string activityMsg = "Match found. Person Id = " + match.getMatchedPerson().PersonId + ", Matching Score: " + match.getScore();
                activityLog.setActivity("Match Activity: " + activityMsg + ". \n");
            }
            else
            {
                message = "Match not found.";
                lblMatchResTxt.ForeColor = System.Drawing.Color.Red;
                activityLog.setActivity("Match Activity: " + message + "\n");
            }
            lblMatchResTxt.Text = message;
            //Set Serach status
            lblMatchProgressStatus.Text = "Search Completed...";
        }


        private void doDuplicateCheck(string fingerName, System.Drawing.Image fingerImage, PictureBox picBox)
        {
            Match match = Program.getMatch(fingerName, fingerImage, "[Unknown]", 60);
            if (match.getStatus())
            {
                DialogResult dr = MessageBox.Show("Duplicate fingerprint found that belongs to a Person with Person Id = " + match.getMatchedPerson().PersonId + ". Do you want to continue?", "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    picBox.Image = fingerImage;
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            } else
            {
                picBox.Image = fingerImage;
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }


        private void picRT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picRTImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRTImagePath = Program.convertWSQtoBMP(picRTImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picRTImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picRTImagePath, fingerImage, picEnrollRT);
                }else
                {
                    picEnrollRT.Image = fingerImage;
                    picEnrollRT.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picRI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picRIImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRIImagePath = Program.convertWSQtoBMP(picRIImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picRIImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picRIImagePath, fingerImage, picEnrollRI);
                }
                else
                {
                    picEnrollRI.Image = fingerImage;
                    picEnrollRI.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picRM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picRMImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRMImagePath = Program.convertWSQtoBMP(picRMImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picRMImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picRMImagePath, fingerImage, picEnrollRM);
                }
                else
                {
                    picEnrollRM.Image = fingerImage;
                    picEnrollRM.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picRR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picRRImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRRImagePath = Program.convertWSQtoBMP(picRRImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picRRImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picRRImagePath, fingerImage, picEnrollRR);
                }
                else
                {
                    picEnrollRR.Image = fingerImage;
                    picEnrollRR.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picRL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picRLImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRLImagePath = Program.convertWSQtoBMP(picRLImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picRLImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picRLImagePath, fingerImage, picEnrollRL);
                }
                else
                {
                    picEnrollRL.Image = fingerImage;
                    picEnrollRL.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picLT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picLTImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLTImagePath = Program.convertWSQtoBMP(picLTImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picLTImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picLTImagePath, fingerImage, picEnrollLT);
                }
                else
                {
                    picEnrollLT.Image = fingerImage;
                    picEnrollLT.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picLI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picLIImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLIImagePath = Program.convertWSQtoBMP(picLIImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picLIImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picLIImagePath, fingerImage, picEnrollLI);
                }
                else
                {
                    picEnrollLI.Image = fingerImage;
                    picEnrollLI.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picLM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picLMImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLMImagePath = Program.convertWSQtoBMP(picLMImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picLMImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picLMImagePath, fingerImage, picEnrollLM);
                }
                else
                {
                    picEnrollLM.Image = fingerImage;
                    picEnrollLM.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picLR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picLRImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLRImagePath = Program.convertWSQtoBMP(picLRImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picLRImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picLRImagePath, fingerImage, picEnrollLR);
                }
                else
                {
                    picEnrollLR.Image = fingerImage;
                    picEnrollLR.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picLL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picLLImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLLImagePath = Program.convertWSQtoBMP(picLLImagePath);
                }

                System.Drawing.Image fingerImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picLLImagePath)));

                if (appConfig.isDupCheck())
                {
                    doDuplicateCheck(picLLImagePath, fingerImage, picEnrollLL);
                }
                else
                {
                    picEnrollLL.Image = fingerImage;
                    picEnrollLL.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void picRT_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollRT, "Click to select the Right Thumb image");
        }

        private void picRI_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollRI, "Click to select the Right Index finger image");
        }

        private void picRM_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollRM, "Click to select the Right Middle finger image");
        }

        private void picMatch_Click(object sender, EventArgs e)
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
                picMatch.Image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(picMatchImagePath)));
                picMatch.SizeMode = PictureBoxSizeMode.StretchImage;

                String selectedImageName = Path.GetFileName(picMatchImagePath);
                lblMatchSelectedFp.Text = selectedImageName;
            }
        }

        private void picRR_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollRR, "Click to select the Right Ring finger image");
        }

        private void picRL_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollRL, "Click to select the Right Little finger image");
        }

        private void picLT_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollLT, "Click to select the Left Thumb image");
        }

        private void picLI_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollLI, "Click to select the Left Index finger image");
        }

        private void picLM_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollLM, "Click to select the Left Middle finger image");
        }

        private void picLR_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollLR, "Click to select the Left Ring finger image");
        }

        private void picLL_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picEnrollLL, "Click to select the Left Little finger image");
        }

        private void btnCRL_Click(object sender, EventArgs e)
        {
            clearEnrollTab();

        }

        private void clearEnrollTab()
        {
            txtEnrollId.Clear();
            txtEnrollFName.Clear();
            txtEnrollLName.Clear();
            txtEnrollMName.Clear();
            txtEnrollPrefix.Clear();
            txtEnrollSuffix.Clear();
            dtpEnrollDOB.Value = dtpEnrollDOB.MinDate;
            txtEnrollFatherName.Clear();
            txtEnrollAddrLine.Clear();
            txtEnrollCity.Clear();
            txtEnrollState.Clear();
            txtEnrollPostalCode.Clear();
            txtEnrollCountry.Clear();
            txtEnrollCellNbr.Clear();
            txtEnrollProfession.Clear();
            txtEnrollHomePNbr.Clear();
            txtEnrollWorkPNbr.Clear();
            txtEnrollEmail.Clear();
            lblEnrollStatusMsg.Text = null;

            if (picEnrollRT.Image != null)
            {
                picEnrollRT.Image.Dispose();
                picEnrollRT.Image = null;
            }
            if (picEnrollRI.Image != null)
            {
                picEnrollRI.Image.Dispose();
                picEnrollRI.Image = null;
            }
            if (picEnrollRM.Image != null)
            {
                picEnrollRM.Image.Dispose();
                picEnrollRM.Image = null;
            }
            if (picEnrollRR.Image != null)
            {
                picEnrollRR.Image.Dispose();
                picEnrollRR.Image = null;
            }
            if (picEnrollRL.Image != null)
            {
                picEnrollRL.Image.Dispose();
                picEnrollRL.Image = null;
            }
            if (picEnrollLT.Image != null)
            {
                picEnrollLT.Image.Dispose();
                picEnrollLT.Image = null;
            }
            if (picEnrollLI.Image != null)
            {
                picEnrollLI.Image.Dispose();
                picEnrollLI.Image = null;
            }
            if (picEnrollLM.Image != null)
            {
                picEnrollLM.Image.Dispose();
                picEnrollLM.Image = null;
            }
            if (picEnrollLR.Image != null)
            {
                picEnrollLR.Image.Dispose();
                picEnrollLR.Image = null;
            }
            if (picEnrollLL.Image != null)
            {
                picEnrollLL.Image.Dispose();
                picEnrollLL.Image = null;
            }
            if (picEnrollPassportPhoto.Image != null)
            {
                picEnrollPassportPhoto.Image.Dispose();
                picEnrollPassportPhoto.Image = null;
            }

        }

        private void lblMatchCLR_Click(object sender, EventArgs e)
        {
            clearMatchTab(sender);
        }

        private void clearMatchTab(object sender)
        {
            string btnClickedName = ((System.Windows.Forms.Button)sender).Name;
            Console.WriteLine("###-->> Button clicked = " + btnClickedName);

            if ((picMatch.Image != null && btnClickedName.Equals("lblMatchCLR")) || (picMatch.Image != null && btnClickedName.Equals("btnLogin")))
            {
                picMatch.Image.Dispose();
                picMatch.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
                picMatchImagePath = null;
                lblMatchSelectedFp.Text = null;
            }

            if (picMatchRT.Image != null)
            {
                picMatchRT.Image.Dispose();
                picMatchRT.Image = null;
            }
            if (picMatchRI.Image != null)
            {
                picMatchRI.Image.Dispose();
                picMatchRI.Image = null;
            }
            if (picMatchRM.Image != null)
            {
                picMatchRM.Image.Dispose();
                picMatchRM.Image = null;
            }
            if (picMatchRR.Image != null)
            {
                picMatchRR.Image.Dispose();
                picMatchRR.Image = null;
            }
            if (picMatchRL.Image != null)
            {
                picMatchRL.Image.Dispose();
                picMatchRL.Image = null;
            }
            if (picMatchLT.Image != null)
            {
                picMatchLT.Image.Dispose();
                picMatchLT.Image = null;
            }
            if (picMatchLI.Image != null)
            {
                picMatchLI.Image.Dispose();
                picMatchLI.Image = null;
            }
            if (picMatchLM.Image != null)
            {
                picMatchLM.Image.Dispose();
                picMatchLM.Image = null;
            }
            if (picMatchLR.Image != null)
            {
                picMatchLR.Image.Dispose();
                picMatchLR.Image = null;
            }
            if (picMatchLL.Image != null)
            {
                picMatchLL.Image.Dispose();
                picMatchLL.Image = null;
            }

            richTxtMatchResAdds.Text = null;
            lblMatchResTxt.Text = null;
            lblMatchResFNameTxt.Text = null;
            lblMatchResLNameTxt.Text = null;
            lblMatchResIDTxt.Text = null;
            if (picBoxMatchResPPhoto.Image != null)
            {
                picBoxMatchResPPhoto.Image.Dispose();
                picBoxMatchResPPhoto.Image = null; ;
            }
            lblMatchProgressStatus.Text = null;
        }

        private void clearUserMgmtTab()
        {
            txtUserMgmtFName.Clear();
            txtUserMgmtId.Clear();
            txtUserMgmtLName.Clear();
            txtUserMgmtPass.Clear();
            txtUserMgmtUsername.Clear();
            listUserMgmtRole.ClearSelected();
            txtUserMgmtStationId.Clear();
            txtUserMgmtStationedAddr.Clear();
            txtUserMgmtStationedCity.Clear();
            txtUserMgmtStationedCountry.Clear();
            listUserMgmtActiveStatus.ClearSelected();

            lblUserMgmtStatusMsg.Text = null;
        }

        private void clearReportTab()
        {
            txtAuditReportPersonId.Clear();
            txtAuditReportUserId.Clear();
        }

        private void clearFindTab()
        {
            txtBoxFindFirstName.Clear();
            txtBoxFindLastName.Clear();
            txtBoxFindCellNbr.Clear();
            txtBoxFindCity.Clear();
            txtBoxFindCountry.Clear();
            txtBoxFindEmail.Clear();
            txtBoxFindHomePhoneNbr.Clear();
            txtBoxFindMiddleName.Clear();
            txtBoxFindPostalCode.Clear();
            txtBoxFindPrefix.Clear();
            txtBoxFindProfession.Clear();
            txtBoxFindState.Clear();
            txtBoxFindStreet.Clear();
            txtBoxFindWorkPhoneNbr.Clear();
            lblFindStatus.Text = null;
            tlpFindResult.Controls.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginId.Text;
            string password = txtLoginPass.Text;
            DataAccess dataAccess = new DataAccess();
            user = dataAccess.getValidUser(username, password);

            if (user != null)
            {
                //After successful login, create the ActivityLog
                activityLog = new ActivityLog();

                //set the login user - "Login as"
                lblLoginPerson.Text = user.getFirstName() + " " + user.getLastName() + " (" + user.getPersonId() + ") - " + user.getUserRole();
                //if same user login back, then no need to clear the tabs
                if (!isCachedUser(user))
                {
                    clearEnrollTab();
                    clearMatchTab(sender);
                    clearUserMgmtTab();
                    clearReportTab();
                    clearFindTab();
                }

                Console.WriteLine("User.Id = " + user.getPersonId());
                Console.WriteLine("User.fname = " + user.getFirstName());
                Console.WriteLine("User.lname = " + user.getLastName());
                Console.WriteLine("User.username = " + user.getUsername());
                Console.WriteLine("User.UserRole = " + user.getUserRole());
                Console.WriteLine("User.Station Id = " + user.getStationId());
                Console.WriteLine("User.Stationed Address = " + user.getStationedAddress());
                Console.WriteLine("User.Stationed City = " + user.getStationedCity());
                Console.WriteLine("User.Stationed Country = " + user.getStationedCountry());
                Console.WriteLine("User.ActiveStatus = " + user.getActiveStatus());
                Console.WriteLine("User.Service Start Date = " + user.getServiceStartDate());
                Console.WriteLine("User.Service End Date = " + user.getServiceEndDate());

                //apply user access control
                applyRolebasedAccessCntrl(user.getUserRole());

                //apply AppConfig
                applyAppConfig(user.getPersonId());

                //Enable the WSQ converter MenuItem
                convertToFromWSQToolStripMenuItem.Enabled = true;

                //Enable the Password Change
                changePasswordToolStripMenuItem.Enabled = true;

                //Enable the Logout MenuItem
                logOutToolStripMenuItem.Enabled = true;

                //Enable the userPreferenceToolstripMenuItem
                userPreferenceToolStripMenuItem.Enabled = true;

                //Start the audit log for the logged in user
                Status status = dataAccess.createUserAuditLog(user, DateTime.Now);
                Console.WriteLine("####-->> Login Status: " + status.getStatusCode());
                long auditLogId = status.getAuditLogId();
                user.setId(auditLogId);

                //Create the activity log
                activityLog.setActivity("Successful login. \n");
            }
            else
            {
                lblLoginFailureStatus.Text = "Status: Invalid credentials. Please try again.";
            }
        }

        private void applyRolebasedAccessCntrl(string userRole)
        {

            DataAccess dataAccess = new DataAccess();
            AccessControl accessCntrl = dataAccess.getAcessCntrl(userRole);

            if (accessCntrl.hasAccessToEnrollTab())
            {
                tabControlAFIS.TabPages.Add(tabEnroll);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabEnroll);
            }
            if (accessCntrl.hasAccessToMatchTab())
            {
                tabControlAFIS.TabPages.Add(tabMatch);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabMatch);
            }
            if (accessCntrl.hasAccessToUserMgmtTab())
            {
                tabControlAFIS.TabPages.Add(tabUserMgmt);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabUserMgmt);
            }
            if (accessCntrl.hasAccessToAuditTab())
            {
                tabControlAFIS.TabPages.Add(tabAuditReport);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabAuditReport);
            }
            if (accessCntrl.hasAccessToLoginTab())
            {
                tabControlAFIS.TabPages.Remove(tabLogin);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabLogin);
            }
            if (accessCntrl.hasAccessToFindTab())
            {
                tabControlAFIS.TabPages.Add(tabFind);
            }
            else
            {
                tabControlAFIS.TabPages.Remove(tabFind);
            }
            if (accessCntrl.hasAccessToDataImport())
            {
                importDataToolStripMenuItem.Enabled = true;
            }
            else
            {
                importDataToolStripMenuItem.Enabled = false;
            }
            if (accessCntrl.hasAccessToDataExport())
            {
                exportDataToolStripMenuItem.Enabled = true;
            }
            else
            {
                exportDataToolStripMenuItem.Enabled = false;
            }
            if (accessCntrl.hasAccessToMultiMatch())
            {
                advancedMatchToolStripMenuItem.Enabled = true;
            }
            else
            {
                advancedMatchToolStripMenuItem.Enabled = false;
            }

        }//end applyRolebasedAccessCntrl

        //apply User specific application configuration
        private void applyAppConfig(string personId)
        {
            DataAccess dataAccess = new DataAccess();
            appConfig = dataAccess.getAppConfig(personId);
            Console.WriteLine("####-->> AppConfig - PersonId = " + personId + ", dupCheck = " + appConfig.isDupCheck());
        }

        private bool isCachedUser(User user)
        {
            if (cachedUser != null)
            {
                Console.WriteLine("###-->> Cached  UserId = [" + cachedUser.getId() + "]");
                Console.WriteLine("###-->> Current UserId = [" + user.getId() + "]");
                Console.WriteLine("###-->> Cached  User Pass = [" + cachedUser.getPassword() + "]");
                Console.WriteLine("###-->> Current User Pass = [" + user.getPassword() + "]");

                if (cachedUser.getUsername().Equals(user.getUsername()) && cachedUser.getPassword().Equals(user.getPassword()))
                {
                    return true;
                }
            }

            return false;
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblLoginPerson.Text = "N/A";
            tabControlAFIS.TabPages.Add(tabLogin);
            tabControlAFIS.TabPages.Remove(tabEnroll);
            tabControlAFIS.TabPages.Remove(tabMatch);
            tabControlAFIS.TabPages.Remove(tabUserMgmt);
            tabControlAFIS.TabPages.Remove(tabAuditReport);
            tabControlAFIS.TabPages.Remove(tabFind);

            txtLoginId.Clear();
            txtLoginPass.Clear();
            lblLoginFailureStatus.Text = null;
            menuStrip.Visible = true;
            cachedUser = user;

            //Disable the WSQ converter MenuItem
            convertToFromWSQToolStripMenuItem.Enabled = false;

            //Disable the Password Change
            changePasswordToolStripMenuItem.Enabled = false;

            //Disable the Logout MenuItem
            logOutToolStripMenuItem.Enabled = false;

            //Disable the User Preference menu item
            userPreferenceToolStripMenuItem.Enabled = false;

            //Disable the Export Data
            exportDataToolStripMenuItem.Enabled = false;

            //Disable the Import Data
            importDataToolStripMenuItem.Enabled = false;

            //Disable Multi-Match
            advancedMatchToolStripMenuItem.Enabled = false;

            //Audit message for properly Logging out
            activityLog.setActivity("Gracefully logged out. \n"); 
            //Audit log the logout time
            DataAccess dataAccess = new DataAccess();
            Status status = dataAccess.updateUserAuditLog(user, DateTime.Now, 0, activityLog);
            Console.WriteLine("####-->> Status code = " + status.getStatusCode());
        }

        private void btnUserMgmtCreate_Click(object sender, EventArgs e)
        {
            string id = txtUserMgmtId.Text;
            string fname = txtUserMgmtFName.Text;
            string lname = txtUserMgmtLName.Text;
            string username = txtUserMgmtUsername.Text;
            string password = txtUserMgmtPass.Text;
            string userRole = (listUserMgmtRole.SelectedItem != null) ? listUserMgmtRole.SelectedItem.ToString() : null;
            string stationId = txtUserMgmtStationId.Text;
            string stationedAddr = txtUserMgmtStationedAddr.Text;
            string stationedCity = txtUserMgmtStationedCity.Text;
            string stationedCountry = txtUserMgmtStationedCountry.Text;
            string activeStatus = listUserMgmtActiveStatus.Text;
            DateTime serviceStartDate = dtpUserMgmtServiceStartDate.Value.Date;
            DateTime serviceEndDate = dtpUserMgmtServiceStartDate.Value.Date;

            User user = new User();
            user.setPersonId(id);
            user.setFirstName(fname);
            user.setLastName(lname);
            user.setUsername(username);
            user.setPassword(password);
            user.setUserRole(userRole);
            user.setStationId(stationId);
            user.setStationedAddress(stationedAddr);
            user.setStationedCity(stationedCity);
            user.setStationedCountry(stationedCountry);
            user.setActiveStatus(activeStatus);
            user.setServiceStartDate(serviceStartDate);
            user.setServiceEndDate(serviceEndDate);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userRole) || string.IsNullOrWhiteSpace(activeStatus))
            {
                Console.WriteLine("###-->> Through Required field validation");
                MessageBox.Show("Required fields (*) cannot be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                Console.WriteLine("###-->> Through Required field validation");
            }
            //create new User
            Status status = new DataAccess().createAFISUser(user);

            //create Default AppConfig for this user 
            //            Status statusAppConfig = new DataAccess().createUserDefaultAppConfig(user);
            //            Console.WriteLine("###-->> statusConfig = " + statusAppConfig.getStatusDesc());

            if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
            {
                lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity("User (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has been created successfully. \n");
            }
            else
            {
                lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Red;
                activityLog.setActivity("Creation of user (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has failed. \n");

            }
            lblUserMgmtStatusMsg.Text = status.getStatusDesc();

        }


        private void picMatch_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(picMatch, "Click to select the fingerprint to be matched");
        }

        private void btnUserMgmtCLR_Click(object sender, EventArgs e)
        {
            clearUserMgmtTab();
        }

        private void tabControlAFIS_Selected(object sender, TabControlEventArgs e)
        {
            Console.WriteLine("####--> Tab Changed. Current Tab = " + e.TabPage.Text);

            if (e.TabPage.Text.Equals("Enroll"))
            {
                this.ActiveControl = txtEnrollId;
                txtEnrollId.Select();
            }
            else if (e.TabPage.Text.Equals("Match"))
            {
                Console.WriteLine("####--> Setting focus on txtMatchVisitorNbr");
                picMatch.Select();
            }
            else if (e.TabPage.Text.Equals("User Mgmt"))
            {
                Console.WriteLine("####--> Setting focus on txtUserMgmtId");
                txtUserMgmtId.Select();
            }
            else if (e.TabPage.Text.Equals("Login"))
            {
                Console.WriteLine("####--> Setting focus on txtLoginId");
                txtLoginId.Select();
            }
        }

        private void tabControlAFIS_VisibleChanged(object sender, EventArgs e)
        {
            //set the cursor to the username field
            txtLoginId.Select();
        }

        private void picEnrollPassportPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picEnrollPassportPhoto.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollPassportPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void txtEnrollId_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("####-->> Lost focus from the txtEnrollId");

            //clear the tab before populating 
            string txtEnrollIdTemp = txtEnrollId.Text;
            clearEnrollTab();
            txtEnrollId.Text = txtEnrollIdTemp;


            if (txtEnrollId.Text.Length > 0)
            {
                DataAccess dataAccess = new DataAccess();
                string personId = txtEnrollId.Text;
                List<PersonDetail> personsDetail = dataAccess.retrievePersonDetail(personId);

                if (personsDetail.Count > 0)
                {
                    //disable the Enroll button
                    btnEnrollEnroll.Enabled = false;

                    PersonDetail pDetail = (PersonDetail)personsDetail.FirstOrDefault();
                    txtEnrollFName.Text = pDetail.getFirstName();
                    txtEnrollLName.Text = pDetail.getLastName();
                    txtEnrollMName.Text = pDetail.getMiddleName();
                    txtEnrollPrefix.Text = pDetail.getPrefix();
                    txtEnrollSuffix.Text = pDetail.getSuffix();
                    dtpEnrollDOB.Value = (DateTime)pDetail.getDOB();
                    txtEnrollFatherName.Text = pDetail.getFatherName();
                    txtEnrollAddrLine.Text = pDetail.getStreetAddress();
                    txtEnrollCity.Text = pDetail.getCity();
                    txtEnrollState.Text = pDetail.getState();
                    txtEnrollPostalCode.Text = pDetail.getPostalCode();
                    txtEnrollCountry.Text = pDetail.getCountry();
                    txtEnrollProfession.Text = pDetail.getProfession();
                    txtEnrollCellNbr.Text = pDetail.getCellNbr();
                    txtEnrollWorkPNbr.Text = pDetail.getWorkPhoneNbr();
                    txtEnrollHomePNbr.Text = pDetail.getHomePhoneNbr();
                    txtEnrollEmail.Text = pDetail.getEmail();
                    picEnrollPassportPhoto.Image = pDetail.getPassportPhoto();
                    picEnrollPassportPhoto.SizeMode = PictureBoxSizeMode.StretchImage;


                    List<MyPerson> persons = dataAccess.retrievePersonFingerprintsById(personId);

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

                            Console.WriteLine("###-->> fp.Fingername = " + fp.Fingername);

                            if (fp.Fingername != null)
                            {
                                if (fp.Fingername.Equals(MyFingerprint.RightThumb))
                                {
                                    picEnrollRT.Image = fp.AsBitmap;
                                    picEnrollRT.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.RightIndex))
                                {
                                    picEnrollRI.Image = fp.AsBitmap;
                                    picEnrollRI.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.RightMiddle))
                                {
                                    picEnrollRM.Image = fp.AsBitmap;
                                    picEnrollRM.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.RightRing))
                                {
                                    picEnrollRR.Image = fp.AsBitmap;
                                    picEnrollRR.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.RightLittle))
                                {
                                    picEnrollRL.Image = fp.AsBitmap;
                                    picEnrollRL.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.LeftThumb))
                                {
                                    picEnrollLT.Image = fp.AsBitmap;
                                    picEnrollLT.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.LeftIndex))
                                {
                                    picEnrollLI.Image = fp.AsBitmap;
                                    picEnrollLI.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.LeftMiddle))
                                {
                                    picEnrollLM.Image = fp.AsBitmap;
                                    picEnrollLM.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.LeftRing))
                                {
                                    picEnrollLR.Image = fp.AsBitmap;
                                    picEnrollLR.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else if (fp.Fingername.Equals(MyFingerprint.LeftLittle))
                                {
                                    picEnrollLL.Image = fp.AsBitmap;
                                    picEnrollLL.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                            }
                        }
                    }//end-if - persons
                }//end-if - personsDetail
                else
                {
                    btnEnrollEnroll.Enabled = true;
                }
            }

        }//end txtEnrollId_Leave

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUserMgmtId_Leave(object sender, EventArgs e)
        {
            if (txtUserMgmtId.Text.Length > 0)
            {
                DataAccess dataAccess = new DataAccess();
                string personId = txtUserMgmtId.Text;
                User user = dataAccess.getUser(personId);

                if (user != null)
                {
                    txtUserMgmtFName.Text = user.getFirstName();
                    txtUserMgmtLName.Text = user.getLastName();
                    txtUserMgmtUsername.Text = user.getUsername();
                    txtUserMgmtPass.Text = user.getPassword();
                    listUserMgmtRole.Text = user.getUserRole();
                    txtUserMgmtStationId.Text = user.getStationId();
                    txtUserMgmtStationedAddr.Text = user.getStationedAddress();
                    txtUserMgmtStationedCity.Text = user.getStationedCity();
                    txtUserMgmtStationedCountry.Text = user.getStationedCountry();
                    listUserMgmtActiveStatus.Text = user.getActiveStatus();
                    dtpUserMgmtServiceStartDate.Value = user.getServiceStartDate();
                    dtpUserMgmtServiceEndDate.Value = user.getServiceEndDate();
                    btnUserMgmtUpdate.Enabled = true;
                    //Disable the Create button - this option is for update only with existing user
                    btnUserMgmtCreate.Enabled = false;
                    //Disable password TextBox
                    txtUserMgmtPass.Enabled = false;
                }
                else
                {
                    //User does not exists in database, so create one
                    string txtUserMgmtIdTemp = txtUserMgmtId.Text;
                    clearUserMgmtTab();
                    txtUserMgmtId.Text = txtUserMgmtIdTemp;
                    btnUserMgmtUpdate.Enabled = false;
                    btnUserMgmtCreate.Enabled = true;
                    txtUserMgmtPass.Enabled = true;
                }
            }
        }// end txtUserMgmtId_Leav

        private void btnUserMgmtUpdate_Click(object sender, EventArgs e)
        {
            string personId = txtUserMgmtId.Text;
            //check user already exists to update
            DataAccess dataAccess = new DataAccess();
            User user = dataAccess.getUser(personId);
            if (user != null)
            {
                User updatedUser = new User();
                updatedUser.setPersonId(user.getPersonId());
                updatedUser.setFirstName(txtUserMgmtFName.Text);
                updatedUser.setLastName(txtUserMgmtLName.Text);
                updatedUser.setUsername(txtUserMgmtUsername.Text);
                updatedUser.setPassword(txtUserMgmtPass.Text);
                updatedUser.setUserRole(listUserMgmtRole.Text);
                updatedUser.setStationId(txtUserMgmtStationId.Text);
                updatedUser.setStationedAddress(txtUserMgmtStationedAddr.Text);
                updatedUser.setStationedCity(txtUserMgmtStationedCity.Text);
                updatedUser.setStationedCountry(txtUserMgmtStationedCountry.Text);
                updatedUser.setActiveStatus(listUserMgmtActiveStatus.Text);
                updatedUser.setServiceStartDate(dtpUserMgmtServiceStartDate.Value.Date);
                updatedUser.setServiceEndDate(dtpUserMgmtServiceEndDate.Value.Date);

                Status status = dataAccess.updateAFISUser(updatedUser);
                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Green;
                    lblUserMgmtStatusMsg.Text = status.getStatusDesc();
                    activityLog.setActivity("User (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has been updated successfully.\n");
                }
                else
                {
                    lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Red;
                    lblUserMgmtStatusMsg.Text = status.getStatusDesc();
                    activityLog.setActivity("Update of user (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has failed. \n");
                }
            }
            else
            {
                lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Red;
                lblUserMgmtStatusMsg.Text = "User with Id (" + personId + ") does not exists. \nPlease choose an existing user to update, otherwise create a new user.";
            }

        }//end btnUserMgmtUpdate_Click

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordChange passChange = new PasswordChange(activityLog);
            passChange.ShowDialog();

        }//end changePasswordToolStripMenuItem_Click

        private void btnUserMgmtResetPass_Click(object sender, EventArgs e)
        {
            PasswordReset passReset = new PasswordReset(activityLog);
            passReset.ShowDialog();

        }//end btnUserMgmtResetPass_Cl

        private void AFISMain_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "Main Thread";

            Console.WriteLine("####-->> Loading the Window");
            //Disable the WSQ Converter MenuItem
            convertToFromWSQToolStripMenuItem.Enabled = false;
            //Disable the Password Change
            changePasswordToolStripMenuItem.Enabled = false;
            //Disable Advanced Matcher
            advancedMatchToolStripMenuItem.Enabled = false;
            //Disable the Logout MenuItem
            logOutToolStripMenuItem.Enabled = false;
            //Disable the User Preference Menu Item
            userPreferenceToolStripMenuItem.Enabled = false;
            //Disble the DateTimePicker on Find Tab
            dtpFindDOB.Enabled = false;
            dtpFindDOB.CustomFormat = " ";
            dtpFindDOB.Format = DateTimePickerFormat.Custom;
            checkBoxFindEmptyDOB.Checked = true;
            //Disable Export Data
            exportDataToolStripMenuItem.Enabled = false;
            //Disable Import Data
            importDataToolStripMenuItem.Enabled = false;
        }



        private void AFISMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            if(activityLog != null)
            {
                activityLog.setActivity("Gracefully closed the AFIS. \n");
                //Audit log the logout time
                DataAccess dataAccess = new DataAccess();
                Status status = dataAccess.updateUserAuditLog(user, DateTime.Now, 0, activityLog);
                Console.WriteLine("####-->> Status code = " + status.getStatusCode());
            }
        }


        private void btnAuditReportCustReport_Click(object sender, EventArgs e)
        {
            Thread userAccessReportThread = new Thread(processUserAccessReport);
            userAccessReportThread.Name = "UserAccessReportThread";
            userAccessReportThread.Start();
        }

        private void processUserAccessReport()
        {
            lblAuditReportAccessReportStatus.Text = "Processing..";
            string userId = txtAuditReportUserId.Text;
            DateTime startDate = dtpAuditReportStartDate.Value;
            DateTime endDate = dtpAuditReportEndDate.Value;
            Reporter.generateUserAccessReport(user, userId, startDate, endDate);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                activityLog.setActivity("Login Access Report Created, User Id = " + userId + ".\n");
            }
            else
            {
                activityLog.setActivity("Login Access Report Created for all users.\n");
            }
            lblAuditReportAccessReportStatus.Text = "Complete..";
        }

        private void btnAuditReportPersonDetailReport_Click(object sender, EventArgs e)
        {
            string personId = txtAuditReportPersonId.Text;

            if(string.IsNullOrWhiteSpace(personId))
            {
                MessageBox.Show("Person Id is a required field.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Reporter.generatePersonDetailReport(user, personId);
            activityLog.setActivity("Detail Person report created for Person Id = " + personId + "\n");

        }


        private void timerCurrentDateTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
        }


        private void btnEnrollUpdate_Click(object sender, EventArgs e)
        {
            string id = txtEnrollId.Text;
            string fname = txtEnrollFName.Text;
            string lname = txtEnrollLName.Text;
            string mname = txtEnrollMName.Text;
            string prefix = txtEnrollPrefix.Text;
            string suffix = txtEnrollSuffix.Text;
            DateTime dobTemp = dtpEnrollDOB.Value;
            DateTime dob = Convert.ToDateTime(dobTemp.ToString("MM/dd/yyy"));
            string streeAddr = txtEnrollAddrLine.Text;
            string city = txtEnrollCity.Text;
            string postalCode = txtEnrollPostalCode.Text;
            string state = txtEnrollState.Text;
            string country = txtEnrollCountry.Text;
            string profession = txtEnrollProfession.Text;
            string fatherName = txtEnrollFatherName.Text;
            string cellNbr = txtEnrollCellNbr.Text != null ? Regex.Replace(txtEnrollCellNbr.Text, @"\D", "") : null;
            string workPhoneNbr = txtEnrollWorkPNbr.Text != null ? Regex.Replace(txtEnrollWorkPNbr.Text, @"\D", "") : null;
            string homePhoneNbr = txtEnrollHomePNbr.Text != null ? Regex.Replace(txtEnrollHomePNbr.Text, @"\D", "") : null;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
            //            string status = null;
            Status status = null;
            MyPerson person = null;

            try
            {
                PersonDetail personDetail = new PersonDetail();
                personDetail.setPersonId(id);
                personDetail.setFirstName(fname);
                personDetail.setLastName(lname);
                personDetail.setMiddleName(mname);
                personDetail.setPrefix(prefix);
                personDetail.setSuffix(suffix);
                personDetail.setDOB(dob);
                personDetail.setStreetAddress(streeAddr);
                personDetail.setCity(city);
                personDetail.setPostalCode(postalCode);
                personDetail.setState(state);
                personDetail.setCountry(country);
                personDetail.setProfession(profession);
                personDetail.setFatherName(fatherName);
                personDetail.setcellNbr(cellNbr);
                personDetail.setWorkPhoneNbr(workPhoneNbr);
                personDetail.setHomwPhoneNbr(homePhoneNbr);
                personDetail.setEmail(email);
                personDetail.setPassportPhoto(passportPhoto);
                DataAccess dataAccess = new DataAccess();

//                if (!string.IsNullOrWhiteSpace(id))
                if (string.IsNullOrWhiteSpace(id))
                {
                    //store person's demograpgy
//                    dataAccess.updatePersonDetail(personDetail);
//                }
//                else
//                {
                    MessageBox.Show("Person ID field is required.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ICollection<KeyValuePair<String, System.Drawing.Image>> imgsFromPicBox = new Dictionary<String, System.Drawing.Image>();
                if (picEnrollRT.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightThumb, picEnrollRT.Image));
                if (picEnrollRI.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightIndex, picEnrollRI.Image));
                if (picEnrollRM.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightMiddle, picEnrollRM.Image));
                if (picEnrollRR.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightRing, picEnrollRR.Image));
                if (picEnrollRL.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.RightLittle, picEnrollRL.Image));
                if (picEnrollLT.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftThumb, picEnrollLT.Image));
                if (picEnrollLI.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftIndex, picEnrollLI.Image));
                if (picEnrollLM.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftMiddle, picEnrollLM.Image));
                if (picEnrollLR.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftRing, picEnrollLR.Image));
                if (picEnrollLL.Image != null) imgsFromPicBox.Add(new KeyValuePair<string, System.Drawing.Image>(MyFingerprint.LeftLittle, picEnrollLL.Image));

                if (imgsFromPicBox.Count > 0)
                {
                   Console.WriteLine("####-->> # of Fp to update1 = " + imgsFromPicBox.Count);
                   //person's fingeprints
                    person = Program.Enroll(imgsFromPicBox, fname, id);
                    Console.WriteLine("####-->> person = " + person);
                    Console.WriteLine("####-->> person.name = " + person.Name);

                //update fingerprint image
                //                    dataAccess.updateFingerprints(person);
                //update fingerprint templates
                //                    dataAccess.updateFingerprintTemplates(person);
                }else
                {
                    person = new MyPerson();
                    person.Name = personDetail.getFirstName();
                    person.PersonId = personDetail.getPersonId();
                }
                status = dataAccess.updatePersonDetailWithFingerprints(person, personDetail);
//                status = "Enrollment update of " + fname + " (Id = " + id + ") completed successfully.";
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity(status.getStatusDesc() + "\n");
            }
            catch (Exception exp)
            {
//                status = "Enrollment update of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine("###--->> exp.StackTrace = " + exp.StackTrace);
                //                throw exp;
            }
            lblEnrollStatusMsg.Text = status.getStatusDesc();

        }//btnEnrollUpdate_Click

        private void convertToFromWSQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToFromWSQ convrertWSQ = new ConvertToFromWSQ(activityLog);
            convrertWSQ.Show();
        }

        private void btnFindFind_Click(object sender, EventArgs e)
        {
            Thread searchFindThread = new Thread(processSearchFind);
            searchFindThread.Name = "SearchFindThread";
            searchFindThread.Start();
        }

        private void processSearchFind()
        {
            try
            {
                progressBarSearchFind.Visible = true;
                progressBarSearchFind.Minimum = 1;
                progressBarSearchFind.Maximum = tlpFindResult.RowCount;
                progressBarSearchFind.Value = 1;
                progressBarSearchFind.Step = 1;

                lblFindStatus.Text = "Searching....";
                string fname = txtBoxFindFirstName.Text;
                string lname = txtBoxFindLastName.Text;
                string dobText = dtpFindDOB.Text;
                string mnane = txtBoxFindMiddleName.Text;
                string prefix = txtBoxFindPrefix.Text;
                string street = txtBoxFindStreet.Text;
                string city = txtBoxFindCity.Text;
                string state = txtBoxFindState.Text;
                string postalCode = txtBoxFindPostalCode.Text;
                string country = txtBoxFindCountry.Text;
                string cellNbr = Regex.Replace(txtBoxFindCellNbr.Text, @"\D", "");
                string workNbr = Regex.Replace(txtBoxFindWorkPhoneNbr.Text, @"\D", "");
                string homeNbr = Regex.Replace(txtBoxFindHomePhoneNbr.Text, @"\D", "");
                string email = txtBoxFindEmail.Text;
                string profession = txtBoxFindProfession.Text;

                PersonDetail pDeatil = new PersonDetail();
                pDeatil.setFirstName(fname);
                pDeatil.setLastName(lname);
                pDeatil.setDOBText(dobText);
                pDeatil.setMiddleName(mnane);
                pDeatil.setPrefix(prefix);
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

                activityLog.setActivity("Activity: Advanced Find/Search. \n");

                DataAccess dataAccess = new DataAccess();
                List<PersonDetail> matchedPersons = dataAccess.findPersons(pDeatil);
                Console.WriteLine("###-->> Data access complete @ = " + DateTime.Now);

                this.Invoke((MethodInvoker)delegate
                {
                    //First Clear previous controlls on button click
                    this.tlpFindResult.Controls.Clear();

                    //Add the Table Header
                    this.tlpFindResult.Controls.Add(lblFindResID, 0, 0);
                    this.tlpFindResult.Controls.Add(lblFindResFirstName, 1, 0);
                    this.tlpFindResult.Controls.Add(lblFindResLastName, 2, 0);

                    Console.WriteLine("###-->>tlpFindResult.RowCount = " + tlpFindResult.RowCount);
                    //Build new controlls based on find ressults. Max results = 10 rows
                    for (int i = 0; i < this.tlpFindResult.RowCount - 1; i++)
                    {
                        progressBarSearchFind.PerformStep();

                        if (matchedPersons.Count > i)
                        {
                            Console.WriteLine("Id = " + matchedPersons[i].getPersonId() + ", FirstName = " + matchedPersons[i].getFirstName() + ", LastName = " + matchedPersons[i].getLastName());
                            switch (i)
                            {
                                case 0:
                                    lnklblPersonId_1.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_1, 0, i + 1);
                                    break;
                                case 1:
                                    lnklblPersonId_2.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_2, 0, i + 1);
                                    break;
                                case 2:
                                    lnklblPersonId_3.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_3, 0, i + 1);
                                    break;
                                case 3:
                                    lnklblPersonId_4.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_4, 0, i + 1);
                                    break;
                                case 4:
                                    lnklblPersonId_5.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_5, 0, i + 1);
                                    break;
                                case 5:
                                    lnklblPersonId_6.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_6, 0, i + 1);
                                    break;
                                case 6:
                                    lnklblPersonId_7.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_7, 0, i + 1);
                                    break;
                                case 7:
                                    lnklblPersonId_8.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_8, 0, i + 1);
                                    break;
                                case 8:
                                    lnklblPersonId_9.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_9, 0, i + 1);
                                    break;
                                case 9:
                                    lnklblPersonId_10.Text = matchedPersons[i].getPersonId();
                                    this.tlpFindResult.Controls.Add(lnklblPersonId_10, 0, i + 1);
                                    break;
                            }//end switch

                            //Label - First Nmae
                            Label lblFindFirstName = new Label() { Text = matchedPersons[i].getFirstName() };
                            lblFindFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpFindResult.Controls.Add(lblFindFirstName, 1, i + 1);
                            //Label - Last Name
                            Label lblFindLastName = new Label() { Text = matchedPersons[i].getLastName() };
                            lblFindLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.tlpFindResult.Controls.Add(lblFindLastName, 2, i + 1);
                        }
                    }
                    lblFindStatus.Text = "# of Match found = " + matchedPersons.Count();
                });
                Console.WriteLine("###-->> Table creation complete @ = " + DateTime.Now);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }

        private void checkBoxFindEmptyDOB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFindEmptyDOB.Checked == true)
            {
                dtpFindDOB.Enabled = false;
                dtpFindDOB.CustomFormat = " ";
                dtpFindDOB.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dtpFindDOB.Enabled = true;
                dtpFindDOB.Format = DateTimePickerFormat.Long;
            }
        }

        private void lnklblPersonId_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnklbl = (LinkLabel)sender;
            Console.WriteLine("###-->> Clicked lnklblPersonId ..." + lnklbl.Text);
            Reporter.generatePersonDetailReport(user, lnklbl.Text);
            activityLog.setActivity("Detail Person report created for Person Id = " + lnklbl.Text + "\n");
        }

        private void advancedMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdvancedMatcher advMatcher = new AdvancedMatcher(activityLog);
            advMatcher.Show(); //non-modal dialog
        }

        private void lnklblFooterLKT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.lnklblFooterLKT.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.lakerstekusa.com");
        }

        private void userPreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserPref userPref = new UserPref(activityLog);
            userPref.ShowDialog();
        }

        private void btnAuditReportDuplicatefpReport_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you really want to continue with duplicate check report?", "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Thread dupFpMatchReportthread = new Thread(this.processDupFingerprintMatchReport);
                dupFpMatchReportthread.Name = "DupMatchReportThread";
                dupFpMatchReportthread.Start();
            }
        }

        private void processDupFingerprintMatchReport()
        {
            lblAuditReportDupReportStatus.Text = "Processing..";
            Console.WriteLine("###-->> Performing duplicate check against Fingerprint table for entire population.");
            ICollection<KeyValuePair<String, MyPerson>> dupMatches = Program.getDuplicateFingerprintRecords();
            Reporter.generateDuplicateFingerprintReport(user, dupMatches);
            Console.WriteLine("###-->> Duplicate check complete...");
            lblAuditReportDupReportStatus.Text = "Complete..";
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData importData = new ImportData(activityLog);
            importData.Show(); //non-modal dialog
        }

        private void btnFindClear_Click(object sender, EventArgs e)
        {
            //Clear all the controlls of the table with past result
            this.tlpFindResult.Controls.Clear();
            this.lblFindStatus.Text = null;
        }

        private void selectExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData exportData = new ExportData(activityLog);
            exportData.Show(); //non-modal dialog
        }

        private void batchExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchExport batchExport = new BatchExport(activityLog);
            batchExport.Show(); //non-modal dialog
        }
    }
}

