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

                if (imgFilePaths.Count > 0)
                {

                    //store person's demograpgy
                    DataAccess dataAccess = new DataAccess();
                    dataAccess.storePersonDetail(personDetail);

                    //store person's finger prints
                    MyPerson person = Program.Enroll(imgFilePaths, fname, id);
                    dataAccess.storeFingerprints(person);

                    status = "Enrollment of " + fname + " (Id = " + id + ") completed successfully.";
                    lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status);
                }
            }
            catch (Exception exp)
            {
                status = "Enrollment of " + fname + " (Id = " + id + ") is unsuccessful. Reason is - " + exp.Message + ".";
                activityLog.setActivity(status);
                lblEnrollStatusMsg.ForeColor = System.Drawing.Color.Red;
                throw exp;
            }

            lblEnrollStatusMsg.Text = status;
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            Int32 matchingThreshold = Convert.ToInt32(ConfigurationManager.AppSettings["InitialThresholdScore"]);
//            string visitorNbr = txtMatchVisitorNbr.Text;
            string fpPath = picMatchImagePath;
            string message = null;

            if (!string.IsNullOrWhiteSpace(txtMatchThreshold.Text)) matchingThreshold = Convert.ToInt32(txtMatchThreshold.Text);
            
            //If fpPath = null, show the error message
            if (fpPath == null)
            {
                MessageBox.Show("Must select a finger print to match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Match match = Program.getMatch(fpPath, "1234", matchingThreshold);
            MyPerson matchedPerson = match.getMatchedPerson();
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
                message = "(Match found. Matching Score:" + match.getScore() +".)";
                grpBoxMatchResult.ForeColor = Color.Green;
                //adding the activity log
                activityLog.setActivity("Match Activity: " + message);
            }
            else
            {
                message = "(Match not found.)";
                grpBoxMatchResult.ForeColor = System.Drawing.Color.Red;
                activityLog.setActivity("Match Activity: " + message);
            }

                grpBoxMatchResult.Text = message;
        }


        private void picRT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRTImagePath = ofd.FileName;
                picEnrollRT.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollRT.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRIImagePath = ofd.FileName;
                picEnrollRI.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollRI.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRMImagePath = ofd.FileName;
                picEnrollRM.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollRM.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRRImagePath = ofd.FileName;
                picEnrollRR.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollRR.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picRL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picRLImagePath = ofd.FileName;
                picEnrollRL.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollRL.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLTImagePath = ofd.FileName;
                picEnrollLT.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollLT.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void picLI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLIImagePath = ofd.FileName;
                picEnrollLI.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollLI.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLM_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLMImagePath = ofd.FileName;
                picEnrollLM.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollLM.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLRImagePath = ofd.FileName;
                picEnrollLR.Image = System.Drawing.Image.FromFile(ofd.FileName);
                picEnrollLR.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void picLL_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLLImagePath = ofd.FileName;
                picEnrollLL.Image = System.Drawing.Image.FromFile(ofd.FileName);
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
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picMatchImagePath = ofd.FileName;
                picMatch.Image = System.Drawing.Image.FromFile(ofd.FileName);
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
            clearMatchTab();
        }

        private void clearMatchTab()
        {
            if (picMatch.Image != null)
            {
                picMatch.Image.Dispose();
                picMatch.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["defaultImageForMatch"]);
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

            picMatchImagePath = null;
//            txtMatchVisitorNbr.Clear();
            richTxtMatchResAdds.Text = null;
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
                    clearMatchTab();
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
            string userRole = listUserMgmtRole.SelectedItem.ToString();
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

            if (txtEnrollId.Text.Length > 0)
            {
                DataAccess dataAccess = new DataAccess();
                string personId = txtEnrollId.Text;
                List<PersonDetail> personsDetail = dataAccess.retrievePersonDetail(personId);

                if (personsDetail.Count > 0)
                {

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

                    if (persons.Count > 0)
                    {
                        MyPerson person = persons.FirstOrDefault();
                        //Get all the fingerprints of the matched person 
                        List<Fingerprint> fps = person.Fingerprints;
                        for (int i = 0; i < fps.Count; i++)
                        {
                            MyFingerprint fp = (MyFingerprint)fps.ElementAt(i);

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
                    string txtEnrollIdTemp = txtEnrollId.Text;
                    clearEnrollTab();
                    txtEnrollId.Text = txtEnrollIdTemp;
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
                iTextSharp.text.Image passportPic = iTextSharp.text.Image.GetInstance(passportPhoto, System.Drawing.Imaging.ImageFormat.Bmp);
                passportPic.ScaleAbsolute(120f, 120f);
                doc.Add(passportPic);

                //Adding the person detail
                Paragraph paragraphReportBody = new Paragraph();
                paragraphReportBody.Add("ID: " + personDetail.getPersonId() + "\n");
                paragraphReportBody.Add("Name: " + " " + personDetail.getPrefix() + " " + personDetail.getFirstName() + " " + personDetail.getMiddleName() + " " + personDetail.getLastName() + " " + personDetail.getSuffix() + "\n");
                paragraphReportBody.Add("Date of Birth (DOB): " + ((DateTime)personDetail.getDOB()).ToString("yyyy-MM-dd") + "\n");
                paragraphReportBody.Add("Father's Name: " + personDetail.getFatherName() + "\n");
                paragraphReportBody.Add("Address: " + personDetail.getStreetAddress() + ", " + personDetail.getCity() + ", " + personDetail.getState() + " " + personDetail.getPostalCode() + ", " + personDetail.getCountry() + "\n");
                paragraphReportBody.Add("Profession: " + personDetail.getProfession() + "\n");
                paragraphReportBody.Add("Cell#: " + personDetail.getCellNbr() + ", Home Phone#: " + personDetail.getHomePhoneNbr() + ", Work Phone#: " + personDetail.getWorkPhoneNbr() + "\n");
                paragraphReportBody.Add("Email: " + personDetail.getEmail() + "\n");
                doc.Add(paragraphReportBody);                 
            }

            doc.Close();
            Console.WriteLine("PDF Generated successfully...");
            System.Diagnostics.Process.Start(pdfPath);
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
                dataAccess.updatePersonDetail(personDetail);

                if (imgFilePaths.Count > 0)
                {
                    //store person's finger prints
                    MyPerson person = Program.Enroll(imgFilePaths, fname, id);
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
                throw exp;
            }
            lblEnrollStatusMsg.Text = status;
        }//btnEnrollUpdate_Click


    }
}
