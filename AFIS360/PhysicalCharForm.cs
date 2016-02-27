using AFIS360Common;
using AFIS360ommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFIS360
{
    public partial class PhysicalCharForm : Form
    {
        private ActivityLog activityLog;
        private PersonDetail personDetail;

        public PhysicalCharForm(ActivityLog activityLog, PersonDetail personDetail)
        {
            InitializeComponent();
            this.activityLog = activityLog;
            this.personDetail = personDetail;
        }


        private void btnPhysicalCharEnter_Click(object sender, EventArgs e)
        {
            Status status = null;

            try
            {
                PersonPhysicalChar personPhysicalChar = new PersonPhysicalChar();
                personPhysicalChar.PersonId = lblPhysicalCharPersonIdValue.Text;
                personPhysicalChar.Height = !string.IsNullOrEmpty(txtPhysicalCharHeight.Text) ? Convert.ToDouble(txtPhysicalCharHeight.Text) : 0.0;
                personPhysicalChar.Weight = !string.IsNullOrEmpty(txtBoxPhysicalCharWeight.Text) ? Convert.ToDouble(txtBoxPhysicalCharWeight.Text) : 0.0;
                personPhysicalChar.EyeColor = !string.IsNullOrEmpty((string)comboBoxPhysicalCharEyeColor.SelectedItem) ? (string)comboBoxPhysicalCharEyeColor.SelectedItem : "";
                personPhysicalChar.HairColor = !string.IsNullOrEmpty((string)comboBoxPhysicalCharHairColor.SelectedItem) ? (string)comboBoxPhysicalCharHairColor.SelectedItem : "";
                personPhysicalChar.Complexion = !string.IsNullOrEmpty((string)comboBoxPhysicalCharComplexion.SelectedItem) ? (string)comboBoxPhysicalCharComplexion.SelectedItem : "";
                personPhysicalChar.BuildType = !string.IsNullOrEmpty((string)comboBoxPhysicalCharBuildType.SelectedItem) ? (string)comboBoxPhysicalCharBuildType.SelectedItem : "";
                personPhysicalChar.BirthMark = !string.IsNullOrEmpty(txtBoxPhysicalCharBirthMark.Text) ? txtBoxPhysicalCharBirthMark.Text : "";
                personPhysicalChar.IdMark = !string.IsNullOrEmpty(txtBoxPhycicalCharIdMark.Text) ? txtBoxPhycicalCharIdMark.Text : "";
                personPhysicalChar.Gender = !string.IsNullOrEmpty((string)comboBoxPhycicalCharGender.SelectedItem) ? (string)comboBoxPhycicalCharGender.SelectedItem : "";
                personPhysicalChar.DOD = Convert.ToDateTime(dtpPhysicalCharDeathDate.Value.ToString("MM/dd/yyyy"));
                personPhysicalChar.CreatedBy = AFISMain.user.getPersonId();
                personPhysicalChar.CreationDateTime = DateTime.Now;
                personPhysicalChar.UpdatedBy = null;
                personPhysicalChar.UpdateDateTime = null;

                if (string.IsNullOrWhiteSpace(personPhysicalChar.PersonId))
                {
                    MessageBox.Show("Person ID field is required, cannot be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Console.WriteLine("###-->> detail: Height =  " + personPhysicalChar.Height + ", Weight = " + personPhysicalChar.Weight + ", Eye Color = " + personPhysicalChar.EyeColor);
                Console.WriteLine("###-->> detail: Hair Color =  " + personPhysicalChar.HairColor + ", Complexion = " + personPhysicalChar.Complexion + ", Build Type = " + personPhysicalChar.BuildType);
                Console.WriteLine("###-->> detail: Birth Mark =  " + personPhysicalChar.BirthMark + ", ID mark = " + personPhysicalChar.IdMark + ", Gender = " + personPhysicalChar.Gender + ", DOD = " + personPhysicalChar.DOD);
                Console.WriteLine("###-->> detail: Created By =  " + personPhysicalChar.CreatedBy + ", Creation Date = " + personPhysicalChar.CreationDateTime + ", Updated By = " + personPhysicalChar.UpdatedBy + ", Updated Date = " + personPhysicalChar.UpdateDateTime);

                status = new DataAccess().storePersonPhysicalCharacteristics(personPhysicalChar);
                Console.WriteLine("###-->> status = " + status.getStatusDesc());

                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                }
                else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exp)
            {
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine(exp.StackTrace);
            }

            lblPhysicalCharStatusMsg.Text = status.getStatusDesc();

        }//end btnPhysicalCharEnter_Click


        private void btnPhysicalCharUpdate_Click(object sender, EventArgs e)
        {
            Status status = null;

            try
            {
                PersonPhysicalChar personPhysicalChar = new PersonPhysicalChar();
                personPhysicalChar.PersonId = lblPhysicalCharPersonIdValue.Text;
                personPhysicalChar.Height = !string.IsNullOrEmpty(txtPhysicalCharHeight.Text) ? Convert.ToDouble(txtPhysicalCharHeight.Text) : 0.0;
                personPhysicalChar.Weight = !string.IsNullOrEmpty(txtBoxPhysicalCharWeight.Text) ? Convert.ToDouble(txtBoxPhysicalCharWeight.Text) : 0.0;
                personPhysicalChar.EyeColor = !string.IsNullOrEmpty((string)comboBoxPhysicalCharEyeColor.SelectedItem) ? (string)comboBoxPhysicalCharEyeColor.SelectedItem : "";
                personPhysicalChar.HairColor = !string.IsNullOrEmpty((string)comboBoxPhysicalCharHairColor.SelectedItem) ? (string)comboBoxPhysicalCharHairColor.SelectedItem : "";
                personPhysicalChar.Complexion = !string.IsNullOrEmpty((string)comboBoxPhysicalCharComplexion.SelectedItem) ? (string)comboBoxPhysicalCharComplexion.SelectedItem : "";
                personPhysicalChar.BuildType = !string.IsNullOrEmpty((string)comboBoxPhysicalCharBuildType.SelectedItem) ? (string)comboBoxPhysicalCharBuildType.SelectedItem : "";
                personPhysicalChar.BirthMark = !string.IsNullOrEmpty(txtBoxPhysicalCharBirthMark.Text) ? txtBoxPhysicalCharBirthMark.Text : "";
                personPhysicalChar.IdMark = !string.IsNullOrEmpty(txtBoxPhycicalCharIdMark.Text) ? txtBoxPhycicalCharIdMark.Text : "";
                personPhysicalChar.Gender = !string.IsNullOrEmpty((string)comboBoxPhycicalCharGender.SelectedItem) ? (string)comboBoxPhycicalCharGender.SelectedItem : "" ;
                personPhysicalChar.DOD = Convert.ToDateTime(dtpPhysicalCharDeathDate.Value.ToString("MM/dd/yyyy"));
                personPhysicalChar.CreatedBy = null;
                personPhysicalChar.CreationDateTime = null;
                personPhysicalChar.UpdatedBy = AFISMain.user.getPersonId();
                personPhysicalChar.UpdateDateTime = DateTime.Now;

                if (string.IsNullOrWhiteSpace(personPhysicalChar.PersonId))
                {
                    MessageBox.Show("Person ID field is required, cannot be empty.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Console.WriteLine("###-->> detail: Height =  " + personPhysicalChar.Height + ", Weight = " + personPhysicalChar.Weight + ", Eye Color = " + personPhysicalChar.EyeColor);
                Console.WriteLine("###-->> detail: Hair Color =  " + personPhysicalChar.HairColor + ", Complexion = " + personPhysicalChar.Complexion + ", Build Type = " + personPhysicalChar.BuildType);
                Console.WriteLine("###-->> detail: Birth Mark =  " + personPhysicalChar.BirthMark + ", ID mark = " + personPhysicalChar.IdMark + ", Gender = " + personPhysicalChar.Gender + ", DOD = " + personPhysicalChar.DOD);
                Console.WriteLine("###-->> detail: Created By =  " + personPhysicalChar.CreatedBy + ", Creation Date = " + personPhysicalChar.CreationDateTime + ", Updated By = " + personPhysicalChar.UpdatedBy + ", Updated Date = " + personPhysicalChar.UpdateDateTime);

                status = new DataAccess().updatePersonPhysicalCharacteristics(personPhysicalChar);
                Console.WriteLine("###-->> status = " + status.getStatusDesc());

                if (status.getStatusCode().Equals(Status.STATUS_SUCCESSFUL))
                {
                    lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Green;
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                }
                else
                {
                    activityLog.setActivity(status.getStatusDesc() + "\n");
                    lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exp)
            {
                activityLog.setActivity(status.getStatusDesc() + "\n");
                lblPhysicalCharStatusMsg.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine(exp.StackTrace);
            }

            lblPhysicalCharStatusMsg.Text = status.getStatusDesc();
        }

        private void btnPhysicalCharClear_Click(object sender, EventArgs e)
        {
            txtPhysicalCharHeight.Text = null;
            txtBoxPhysicalCharWeight.Text = null;
            comboBoxPhysicalCharEyeColor.SelectedItem = null;
            comboBoxPhysicalCharHairColor.SelectedItem = null;
            comboBoxPhysicalCharComplexion.SelectedItem = null;
            comboBoxPhysicalCharBuildType.SelectedItem = null;
            txtBoxPhysicalCharBirthMark.Text = null;
            txtBoxPhycicalCharIdMark.Text = null;
            comboBoxPhycicalCharGender.SelectedItem = null;
            dtpPhysicalCharDeathDate.Value = DateTime.Now;
            lblPhysicalCharStatusMsg.Text = null;
        }

        private void btnPhysicalCharClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhysicalChar_Load(object sender, EventArgs e)
        {
            //retrieve Person Physical Char if exists
            PersonPhysicalChar personPhysicalChar = new DataAccess().retrievePersonPhysicalCharacteristics(personDetail.getPersonId());
            Console.WriteLine("###-->> personPhysicalChar = " + personPhysicalChar);

            lblPhysicalCharPersonIdValue.Text = personDetail.getPersonId();
            lblPhysicalCharFName.Text = personDetail.getFirstName();
            lblPhysicalCharLName.Text = personDetail.getLastName();


            if (personPhysicalChar != null)
            {
                txtPhysicalCharHeight.Text = Convert.ToString(personPhysicalChar.Height);
                txtBoxPhysicalCharWeight.Text = Convert.ToString(personPhysicalChar.Weight);
                comboBoxPhysicalCharEyeColor.SelectedItem = !string.IsNullOrEmpty(personPhysicalChar.EyeColor) ? personPhysicalChar.EyeColor : "";
                comboBoxPhysicalCharHairColor.SelectedItem = !string.IsNullOrEmpty(personPhysicalChar.HairColor) ? personPhysicalChar.HairColor : "";
                comboBoxPhysicalCharComplexion.SelectedItem = !string.IsNullOrEmpty(personPhysicalChar.Complexion) ? personPhysicalChar.Complexion : "";
                comboBoxPhysicalCharBuildType.SelectedItem = !string.IsNullOrEmpty(personPhysicalChar.BuildType) ? personPhysicalChar.BuildType : "";
                txtBoxPhysicalCharBirthMark.Text = !string.IsNullOrEmpty(personPhysicalChar.BirthMark) ? personPhysicalChar.BirthMark : "";
                txtBoxPhycicalCharIdMark.Text = !string.IsNullOrEmpty(personPhysicalChar.IdMark) ? personPhysicalChar.IdMark : "";
                comboBoxPhycicalCharGender.SelectedItem = !string.IsNullOrEmpty(personPhysicalChar.Gender) ? personPhysicalChar.Gender : "";
                dtpPhysicalCharDeathDate.Value = personPhysicalChar.DOD;
                btnPhysicalCharEnter.Enabled = false;
                btnPhysicalCharUpdate.Enabled = true;
            }
            else
            {
                btnPhysicalCharEnter.Enabled = true;
                btnPhysicalCharUpdate.Enabled = false;
            }
        }//end PhysicalChar_Load

        private void txtPhysicalCharHeight_TextChanged(object sender, EventArgs e)
        {
            //            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhysicalCharHeight.Text, "[^0-9]"))
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhysicalCharHeight.Text, "^[0-9]{0,3}([.][0-9]{0,2})?$"))
            {
                MessageBox.Show("Please enter a valid decimal number (i.e. xxx.xx) only.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhysicalCharHeight.Text.Remove(txtPhysicalCharHeight.Text.Length - 1);
            }
        }

        private void txtBoxPhysicalCharWeight_TextChanged(object sender, EventArgs e)
        {
            //            if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhysicalCharWeight.Text, "[^0-9]"))
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhysicalCharWeight.Text, "^[0-9]{0,3}([.][0-9]{0,2})?$"))
            {
                MessageBox.Show("Please enter a valid decimal number (i.e. xxx.xx) only.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxPhysicalCharWeight.Text.Remove(txtBoxPhysicalCharWeight.Text.Length - 1);
            }
        }

        private void txtPhysicalCharHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void txtBoxPhysicalCharWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
