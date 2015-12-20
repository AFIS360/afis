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
using Wsqm;

namespace AFIS360
{
    public partial class AFISMain : Form
    {
        ICollection<KeyValuePair<String, String>> imgFilePaths = new Dictionary<String, String>();

        string picRTImagePath = null;
        string picRIImagePath = null;
        string picRMImagePath = null;
        string picRRImagePath = null;
        string picRLImagePath = null;
        string picLTImagePath = null;
        string picLIImagePath = null;
        string picLMImagePath = null;
        string picLRImagePath = null;
        string picLLImagePath = null;
        string picMatchImagePath = null;
        ToolTip tTip = null;
        User cachedUser = null;
        User user = null;
        ActivityLog activityLog = null;


        public AFISMain()
        {
            Console.WriteLine("Init components...");
            InitializeComponent();
            tTip = new ToolTip();
            tabControlAFIS.TabPages.Remove(tabEnroll);
            tabControlAFIS.TabPages.Remove(tabMatch);
            tabControlAFIS.TabPages.Remove(tabUserMgmt);
            tabControlAFIS.TabPages.Remove(tabAuditReport);
            menuStrip.Visible = true;
            btnUserMgmtUpdate.Enabled = false;
            //set the default image for match picBox
            if (picMatch.Image == null) picMatch.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
      }

/*
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
            string cellNbr = txtEnrollCellNbr.Text;
            string workPhoneNbr = txtEnrollWorkPNbr.Text;
            string homePhoneNbr = txtEnrollHomePNbr.Text;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
            string status = null;

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


                if (picRTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRTPath", picRTImagePath));
                if (picRIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRIPath", picRIImagePath));
                if (picRMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRMPath", picRMImagePath));
                if (picRRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRRPath", picRRImagePath));
                if (picRLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRLPath", picRLImagePath));
                if (picLTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLTPath", picLTImagePath));
                if (picLIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLIPath", picLIImagePath));
                if (picLMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLMPath", picLMImagePath));
                if (picLRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLRPath", picLRImagePath));
                if (picLLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLLPath", picLLImagePath));


                //store person's demograpgy
                DataAccess dataAccess = new DataAccess();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    dataAccess.storePersonDetail(personDetail);
                } else
                {
                    MessageBox.Show("Person ID field is required.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MyPerson person;
                if (imgFilePaths.Count > 0)
                {
                    //store person's finger prints
                    person = Program.Enroll(imgFilePaths, fname, id);
                } else
                {
                    person = new MyPerson();
                    person.Name = fname;
                    person.PersonId = id;
                }
                dataAccess.storeFingerprints(person);
                
                status = "Enrollment of " + fname + " (Id = " + id + ") completed successfully.";
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity(status);
            }
            catch (Exception exp)
            {
                status = "Enrollment of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status);
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
//                throw exp;
            }

            lblEnrollStatusMsg.Text = status;
        }
*/

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
            string cellNbr = txtEnrollCellNbr.Text;
            string workPhoneNbr = txtEnrollWorkPNbr.Text;
            string homePhoneNbr = txtEnrollHomePNbr.Text;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
            string status = null;

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
/*

                if (picRTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRTPath", picRTImagePath));
                if (picRIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRIPath", picRIImagePath));
                if (picRMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRMPath", picRMImagePath));
                if (picRRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRRPath", picRRImagePath));
                if (picRLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRLPath", picRLImagePath));
                if (picLTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLTPath", picLTImagePath));
                if (picLIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLIPath", picLIImagePath));
                if (picLMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLMPath", picLMImagePath));
                if (picLRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLRPath", picLRImagePath));
                if (picLLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLLPath", picLLImagePath));

*/
                //store person's demograpgy
                DataAccess dataAccess = new DataAccess();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    dataAccess.storePersonDetail(personDetail);
                }
                else
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
                    //store person's finger prints
                    person = Program.Enroll(imgsFromPicBox, fname, id);
                }
                else
                {
                    person = new MyPerson();
                    person.Name = fname;
                    person.PersonId = id;
                }
                dataAccess.storeFingerprints(person);

                status = "Enrollment of " + fname + " (Id = " + id + ") completed successfully.";
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity(status);
            }
            catch (Exception exp)
            {
                status = "Enrollment of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status);
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                //                throw exp;
            }

            lblEnrollStatusMsg.Text = status;
        }


        private void btnMatch_Click(object sender, EventArgs e)
        {
            Int32 matchingThreshold = Convert.ToInt32(ConfigurationManager.AppSettings["InitialThresholdScore"]);
            string fpPath = picMatchImagePath;
            string message = null;

            if (!string.IsNullOrWhiteSpace(txtMatchThreshold.Text)) matchingThreshold = Convert.ToInt32(txtMatchThreshold.Text);
            
            //If fpPath = null, show the error message
            if (fpPath == null)
            {
                MessageBox.Show("Must select a finger print to match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


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
                List<Fingerprint> fps = matchedPerson.Fingerprints;
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
                message = "Match found. Matching Score:" + match.getScore();
                lblMatchResTxt.ForeColor = Color.Green;
                //adding the activity log
                activityLog.setActivity("Match Activity: " + message + " .");
            }
            else
            {
                message = "Match not found.";
                lblMatchResTxt.ForeColor = System.Drawing.Color.Red;
                activityLog.setActivity("Match Activity: " + message);
            }
            lblMatchResTxt.Text = message;
        }


        private void picRT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRTImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRTImagePath = Program.convertWSQtoBMP(picRTImagePath);
                }

                picEnrollRT.Image = System.Drawing.Image.FromFile(picRTImagePath);
                picEnrollRT.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRIImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRIImagePath = Program.convertWSQtoBMP(picRIImagePath);
                }
                picEnrollRI.Image = System.Drawing.Image.FromFile(picRIImagePath);
                picEnrollRI.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRMImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRMImagePath = Program.convertWSQtoBMP(picRMImagePath);
                }
                picEnrollRM.Image = System.Drawing.Image.FromFile(picRMImagePath);
                picEnrollRM.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRRImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRRImagePath = Program.convertWSQtoBMP(picRRImagePath);
                }
                picEnrollRR.Image = System.Drawing.Image.FromFile(picRRImagePath);
                picEnrollRR.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRLImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picRLImagePath = Program.convertWSQtoBMP(picRLImagePath);
                }
                picEnrollRL.Image = System.Drawing.Image.FromFile(picRLImagePath);
                picEnrollRL.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLTImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLTImagePath = Program.convertWSQtoBMP(picLTImagePath);
                }
                picEnrollLT.Image = System.Drawing.Image.FromFile(picLTImagePath);
                picEnrollLT.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void picLI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLIImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLIImagePath = Program.convertWSQtoBMP(picLIImagePath);
                }
                picEnrollLI.Image = System.Drawing.Image.FromFile(picLIImagePath);
                picEnrollLI.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLMImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLMImagePath = Program.convertWSQtoBMP(picLMImagePath);
                }
                picEnrollLM.Image = System.Drawing.Image.FromFile(picLMImagePath);
                picEnrollLM.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLRImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLRImagePath = Program.convertWSQtoBMP(picLRImagePath);
                }
                picEnrollLR.Image = System.Drawing.Image.FromFile(picLRImagePath);
                picEnrollLR.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.wsq;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLLImagePath = ofd.FileName;
                //if image is in WSQ format convert it to BMP
                if (Path.GetExtension(ofd.FileName).ToUpper().Replace(".", "") == "WSQ")
                {
                    picLLImagePath = Program.convertWSQtoBMP(picLLImagePath);
                }
                picEnrollLL.Image = System.Drawing.Image.FromFile(picLLImagePath);
                picEnrollLL.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void picRT_MouseHover(object sender, EventArgs e)
        {

            tTip.SetToolTip(picEnrollRT, "Click to select the Right Thumb image");
        }

        private void picRI_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollRI, "Click to select the Right Index finger image");
        }

        private void picRM_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollRM, "Click to select the Right Middle finger image");
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
                picMatch.Image = System.Drawing.Image.FromFile(picMatchImagePath);
                picMatch.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void picRR_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollRR, "Click to select the Right Ring finger image");
        }

        private void picRL_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollRL, "Click to select the Right Little finger image");
        }

        private void picLT_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollLT, "Click to select the Left Thumb image");
        }

        private void picLI_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollLI, "Click to select the Left Index finger image");
        }

        private void picLM_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollLM, "Click to select the Left Middle finger image");
        }

        private void picLR_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollLR, "Click to select the Left Ring finger image");
        }

        private void picLL_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picEnrollLL, "Click to select the Left Little finger image");
        }

        private void btnCRL_Click(object sender, EventArgs e)
        {
            clearEnrollTab();

        }

        private void clearEnrollTab()
        {
            if (imgFilePaths != null) imgFilePaths.Clear();
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
                picRTImagePath = null;
            }
            if (picEnrollRI.Image != null)
            {
                picEnrollRI.Image.Dispose();
                picEnrollRI.Image = null;
                picRIImagePath = null;
            }
            if (picEnrollRM.Image != null)
            {
                picEnrollRM.Image.Dispose();
                picEnrollRM.Image = null;
                picRMImagePath = null;
            }
            if (picEnrollRR.Image != null)
            {
                picEnrollRR.Image.Dispose();
                picEnrollRR.Image = null;
                picRRImagePath = null;
            }
            if (picEnrollRL.Image != null)
            {
                picEnrollRL.Image.Dispose();
                picEnrollRL.Image = null;
                picRLImagePath = null;
            }
            if (picEnrollLT.Image != null)
            {
                picEnrollLT.Image.Dispose();
                picEnrollLT.Image = null;
                picLTImagePath = null;
            }
            if (picEnrollLI.Image != null)
            {
                picEnrollLI.Image.Dispose();
                picEnrollLI.Image = null;
                picLIImagePath = null;
            }
            if (picEnrollLM.Image != null)
            {
                picEnrollLM.Image.Dispose();
                picEnrollLM.Image = null;
                picLMImagePath = null;
            }
            if (picEnrollLR.Image != null)
            {
                picEnrollLR.Image.Dispose();
                picEnrollLR.Image = null;
                picLRImagePath = null;
            }
            if (picEnrollLL.Image != null)
            {
                picEnrollLL.Image.Dispose();
                picEnrollLL.Image = null;
                picLLImagePath = null;
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
            string btnMatchName = ((Button)sender).Name;
            Console.WriteLine("###-->> Button clicked = " + btnMatchName);

            if (picMatch.Image != null && btnMatchName.Equals("lblMatchCLR"))
            {
                picMatch.Image.Dispose();
                picMatch.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
                picMatchImagePath = null;
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

//            picMatchImagePath = null;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginId.Text;
            string password = txtLoginPass.Text;
            DataAccess dataAccess = new DataAccess();
            user = dataAccess.getValidUser(username, password);

            if (user != null)
            {
                //set the login user - "Login as"
                lblLoginPerson.Text = user.getFirstName() + " " + user.getLastName() + " (" + user.getPersonId() + ") - " + user.getUserRole();
                //if same user login back, then no need to clear the tabs
                if (!isCachedUser(user))
                {
                    clearEnrollTab();
                    clearMatchTab(sender);
                    clearUserMgmtTab();
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

                //Start the audit log for the logged in user
                Status status = dataAccess.createUserAuditLog(user, DateTime.Now);
                Console.WriteLine("####-->> Login Status: " + status.getStatusCode());
                long auditLogId = status.getAuditLogId();
                user.setId(auditLogId);

                //Create the activity log
                activityLog.setActivity("Successful login.");
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

        }//end applyRolebasedAccessCntrl


        private bool isCachedUser(User user)
        {
            if (cachedUser != null)
            {
                if (cachedUser.getId().Equals(user.getId()) && cachedUser.getPassword().Equals(user.getPassword()))
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

            txtLoginId.Clear();
            txtLoginPass.Clear();
            lblLoginFailureStatus.Text = null;
            menuStrip.Visible = true;
            cachedUser = user;

            //Audit message for properly Logging out
            activityLog.setActivity("Gracefully logged out."); 
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
                Console.WriteLine("###-->> NOT Through Required field validation");
            }

            Status status = new DataAccess().createAFISUser(user);

            if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
            {
                lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity("User (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has been created successfully.");
            }
            else
            {
                lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Red;
                activityLog.setActivity("Creation of user (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has failed.");

            }
            lblUserMgmtStatusMsg.Text = status.getStatusDesc();

        }


        private void picMatch_MouseHover(object sender, EventArgs e)
        {
            tTip.SetToolTip(picMatch, "Click to select the fingerprint to be matched");
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
            Console.WriteLine("####-->> Lost focust from the txtEnrollId");

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
//                    txtEnrollIdTemp = txtEnrollId.Text;
//                   clearEnrollTab();
//                    txtEnrollId.Text = txtEnrollIdTemp;
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
                    activityLog.setActivity("User (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has been updated successfully.");
                }
                else
                {
                    lblUserMgmtStatusMsg.ForeColor = System.Drawing.Color.Red;
                    lblUserMgmtStatusMsg.Text = status.getStatusDesc();
                    activityLog.setActivity("Update of user (" + user.getFirstName() + " " + user.getLastName() + " - " + user.getPersonId() + ") has failed.");
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
            Console.WriteLine("####-->> Loading the Window");
            activityLog = new ActivityLog();
        }


        private void AFISMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            activityLog.setActivity("Gacefully closed the AFIS.");
            //Audit log the logout time
            DataAccess dataAccess = new DataAccess();
            Status status = dataAccess.updateUserAuditLog(user, DateTime.Now, 0, activityLog);
            Console.WriteLine("####-->> Status code = " + status.getStatusCode());
        }


        private void btnAuditReportCustReport_Click(object sender, EventArgs e)
        {
            activityLog.setActivity("Login Access Report Created.");

            string userId = txtAuditReportUserId.Text;
            DateTime startDate = dtpAuditReportStartDate.Value;
            DateTime endDate = dtpAuditReportEndDate.Value;

            List<AuditLog> auditLogs = new DataAccess().getAuditLogs(userId, startDate, endDate);
            Console.WriteLine("# of AuditLog = " + auditLogs.Count());

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            string datetimePref = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string pdfPath = ConfigurationManager.AppSettings["CustUserReportPath"] + "-" + datetimePref + ".pdf";
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
            doc.Open();

            //add title
            doc.AddTitle("User Access Report");
            doc.AddHeader("Daily Report", "User Access Report");

            Paragraph paragraphCompanyInfo = new Paragraph("RAB (Rapid Action Battalion)\n");
            paragraphCompanyInfo.Add("Station: " + user.getStationId() + ", " + user.getStationedCity() + "\n");
            paragraphCompanyInfo.Add(user.getStationedCountry() + "\n");
            paragraphCompanyInfo.Alignment = Element.ALIGN_LEFT;

            iTextSharp.text.Font contentFont = iTextSharp.text.FontFactory.GetFont("Webdings", 20, iTextSharp.text.Font.BOLD);
            Paragraph paragraphReportTitle = new Paragraph("Login Access Report\n", contentFont);
            paragraphReportTitle.Alignment = Element.ALIGN_CENTER;

            Paragraph paragraphReportSubTitle = new Paragraph();
            paragraphReportSubTitle.Add("By: " + user.getFirstName() + " " + user.getLastName() + ", ID: " + user.getPersonId() + "\n");
            paragraphReportSubTitle.Add("At: " + DateTime.Now.ToString() + "\n\n");
            paragraphReportSubTitle.Alignment = Element.ALIGN_CENTER;

            doc.Add(paragraphCompanyInfo);
            doc.Add(paragraphReportTitle);
            doc.Add(paragraphReportSubTitle);

            PdfPTable accessReportTable = new PdfPTable(6);
            float[] widths = new float[] {25f, 40f, 40f, 40f, 40f, 100f};
            accessReportTable.SetWidths(widths);

            PdfPCell headerCellRecNo = new PdfPCell(new Phrase("RecNo"));
            PdfPCell headerCellID = new PdfPCell(new Phrase("ID"));
            PdfPCell headerCellName = new PdfPCell(new Phrase("Name"));
            PdfPCell headerCellLoginDateTime = new PdfPCell(new Phrase("Login Date time"));
            PdfPCell headerCellLogoutDateTime = new PdfPCell(new Phrase("Logout Date time"));
            PdfPCell headerCellActivityLog = new PdfPCell(new Phrase("Login Activity"));


            //Add Headers to the table
            accessReportTable.AddCell(headerCellRecNo);
            accessReportTable.AddCell(headerCellID);
            accessReportTable.AddCell(headerCellName);
            accessReportTable.AddCell(headerCellLoginDateTime);
            accessReportTable.AddCell(headerCellLogoutDateTime);
            accessReportTable.AddCell(headerCellActivityLog);


            for (int i = 0; i < auditLogs.Count(); i++)
            {
                AuditLog auditLog = (AuditLog)auditLogs.ElementAt(i);
                PdfPCell userIdRecNo = new PdfPCell(new Phrase(Convert.ToString(i+1)));
                PdfPCell userIdCell = new PdfPCell(new Phrase(auditLog.getUserId()));
                PdfPCell usernameIdCell = new PdfPCell(new Phrase(auditLog.getUsername()));
                PdfPCell loginDateTimeCell = new PdfPCell(new Phrase(auditLog.getLoginDateTime().ToString()));

                //Add logout DateTime to the cell
                DateTime? logoutDateTime = auditLog.getLogoutDateTime();
                string logoutDateTimeStr;
                if (logoutDateTime == null)
                {
                    logoutDateTimeStr = "N/A";
                }
                else
                {
                    logoutDateTimeStr = logoutDateTime.ToString();
                }
                PdfPCell logoutDateTimeCell = new PdfPCell(new Phrase(logoutDateTimeStr));

                //Add ActivityLog to the cell
                ActivityLog activityLog = auditLog.getActivityLog();
                Console.WriteLine("####-->>Activity Log = " + activityLog);
                StringBuilder strBuilder = new StringBuilder();

                if (activityLog != null)
                {
                    List<string> actvities = activityLog.getActivity();
                    List<string>.Enumerator activitiesEnum = actvities.GetEnumerator();

                    while (activitiesEnum.MoveNext())
                    {
                        string activity = activitiesEnum.Current;
                        Console.WriteLine("####-->> Activity = " + activity);
                        strBuilder.AppendLine(activity);
                    }
                }
                PdfPCell activityLogCell = new PdfPCell(new Phrase(strBuilder.ToString()));

                //Adding cells to the table
                accessReportTable.AddCell(userIdRecNo);
                accessReportTable.AddCell(userIdCell);
                accessReportTable.AddCell(usernameIdCell);
                accessReportTable.AddCell(loginDateTimeCell);
                accessReportTable.AddCell(logoutDateTimeCell);
                accessReportTable.AddCell(activityLogCell);
            }

            doc.Add(accessReportTable);

            doc.Close();
            Console.WriteLine("PDF Generated successfully...");
            System.Diagnostics.Process.Start(pdfPath);
        }

        private void btnAuditReportPersonDetailReport_Click(object sender, EventArgs e)
        {
            activityLog.setActivity("Person Detailed Report Created.");

            string personId = txtAuditReportPersonId.Text;

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
                            if(imageRT != null)
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
                            if(imageRI != null)
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
                            if(imageLT != null)
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
                }else
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

        private void timerCurrentDateTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
        }

/*
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
            string cellNbr = txtEnrollCellNbr.Text;
            string workPhoneNbr = txtEnrollWorkPNbr.Text;
            string homePhoneNbr = txtEnrollHomePNbr.Text;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
            string status = null;

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


                if (picRTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRTPath", picRTImagePath));
                if (picRIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRIPath", picRIImagePath));
                if (picRMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRMPath", picRMImagePath));
                if (picRRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRRPath", picRRImagePath));
                if (picRLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpRLPath", picRLImagePath));
                if (picLTImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLTPath", picLTImagePath));
                if (picLIImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLIPath", picLIImagePath));
                if (picLMImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLMPath", picLMImagePath));
                if (picLRImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLRPath", picLRImagePath));
                if (picLLImagePath != null) imgFilePaths.Add(new KeyValuePair<String, String>("fpLLPath", picLLImagePath));

                DataAccess dataAccess = new DataAccess();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    //store person's demograpgy
                    dataAccess.updatePersonDetail(personDetail);
                } else
                {
                    MessageBox.Show("Person ID field is required.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (imgFilePaths.Count > 0)
                {
                    Console.WriteLine("####-->> # of Fp to update = " + imgFilePaths.Count);
                    //store person's finger prints
                    MyPerson person = Program.Enroll(imgFilePaths, fname, id);
                    Console.WriteLine("####-->> person.name = " + person.Name);
                    MyPerson person1 = Program.Enroll1(picEnrollRT.Image, fname, id);
                    Console.WriteLine("####-->> person1.name = " + person.Name);
                    dataAccess.updateFingerprints(person);
                }
                status = "Enrollment update of " + fname + " (Id = " + id + ") completed successfully.";
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity(status);   
            }
            catch (Exception exp)
            {
                status = "Enrollment update of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status);
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine("###--->> exp.StackTrace = " + exp.StackTrace);
//                throw exp;
            }
            lblEnrollStatusMsg.Text = status;
        }//btnEnrollUpdate_Click
*/

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
            string cellNbr = txtEnrollCellNbr.Text;
            string workPhoneNbr = txtEnrollWorkPNbr.Text;
            string homePhoneNbr = txtEnrollHomePNbr.Text;
            string email = txtEnrollEmail.Text;
            System.Drawing.Image passportPhoto = picEnrollPassportPhoto.Image;
            string status = null;

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

                if (!string.IsNullOrWhiteSpace(id))
                {
                    //store person's demograpgy
                    dataAccess.updatePersonDetail(personDetail);
                }
                else
                {
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
                    Console.WriteLine("####-->> # of Fp to update = " + imgsFromPicBox.Count);
                    //store person's finger prints
                    MyPerson person = Program.Enroll(imgsFromPicBox, fname, id);
                    Console.WriteLine("####-->> person.name = " + person.Name);
                    dataAccess.updateFingerprints(person);
                }
                status = "Enrollment update of " + fname + " (Id = " + id + ") completed successfully.";
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                activityLog.setActivity(status);
            }
            catch (Exception exp)
            {
                status = "Enrollment update of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status);
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine("###--->> exp.StackTrace = " + exp.StackTrace);
                //                throw exp;
            }
            lblEnrollStatusMsg.Text = status;
        }//btnEnrollUpdate_Click

        private void convertWSQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToFromWSQ convrertWSQ = new ConvertToFromWSQ(activityLog);
            convrertWSQ.ShowDialog();
        }
    }
}
