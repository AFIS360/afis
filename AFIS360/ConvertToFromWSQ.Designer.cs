namespace AFIS360
{
    partial class ConvertToFromWSQ
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWSQConvInputFileFormat = new System.Windows.Forms.Label();
            this.listBoxWSQConvInputFileFormatList = new System.Windows.Forms.ListBox();
            this.lblWSQCInputFileLocation = new System.Windows.Forms.Label();
            this.txtBoxWSQCInputFileLocation = new System.Windows.Forms.TextBox();
            this.btnWSQCInputFileLocation = new System.Windows.Forms.Button();
            this.grpBoxWSQConverter = new System.Windows.Forms.GroupBox();
            this.lblWSQCOutputfileFormat = new System.Windows.Forms.Label();
            this.listBoxWSQCOutputFileFormatList = new System.Windows.Forms.ListBox();
            this.lblWSQCOutputFileLocation = new System.Windows.Forms.Label();
            this.txtBoxWSQCOutputFileLocation = new System.Windows.Forms.TextBox();
            this.btnWSQCOutputFileLocation = new System.Windows.Forms.Button();
            this.btnWSQCConvert = new System.Windows.Forms.Button();
            this.btnWSQCClose = new System.Windows.Forms.Button();
            this.grpBoxWSQConverter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWSQConvInputFileFormat
            // 
            this.lblWSQConvInputFileFormat.AutoSize = true;
            this.lblWSQConvInputFileFormat.Location = new System.Drawing.Point(35, 46);
            this.lblWSQConvInputFileFormat.Name = "lblWSQConvInputFileFormat";
            this.lblWSQConvInputFileFormat.Size = new System.Drawing.Size(179, 25);
            this.lblWSQConvInputFileFormat.TabIndex = 0;
            this.lblWSQConvInputFileFormat.Text = "Input File Format:";
            // 
            // listBoxWSQConvInputFileFormatList
            // 
            this.listBoxWSQConvInputFileFormatList.FormattingEnabled = true;
            this.listBoxWSQConvInputFileFormatList.ItemHeight = 25;
            this.listBoxWSQConvInputFileFormatList.Items.AddRange(new object[] {
            "WSQ - FBI Wavelet Scalar Quantization",
            "BMP - Windows Bitmap Graphics"});
            this.listBoxWSQConvInputFileFormatList.Location = new System.Drawing.Point(40, 74);
            this.listBoxWSQConvInputFileFormatList.Name = "listBoxWSQConvInputFileFormatList";
            this.listBoxWSQConvInputFileFormatList.Size = new System.Drawing.Size(657, 179);
            this.listBoxWSQConvInputFileFormatList.TabIndex = 2;
            // 
            // lblWSQCInputFileLocation
            // 
            this.lblWSQCInputFileLocation.AutoSize = true;
            this.lblWSQCInputFileLocation.Location = new System.Drawing.Point(35, 288);
            this.lblWSQCInputFileLocation.Name = "lblWSQCInputFileLocation";
            this.lblWSQCInputFileLocation.Size = new System.Drawing.Size(198, 25);
            this.lblWSQCInputFileLocation.TabIndex = 3;
            this.lblWSQCInputFileLocation.Text = "Input File/Directory:";
            // 
            // txtBoxWSQCInputFileLocation
            // 
            this.txtBoxWSQCInputFileLocation.Location = new System.Drawing.Point(40, 316);
            this.txtBoxWSQCInputFileLocation.Name = "txtBoxWSQCInputFileLocation";
            this.txtBoxWSQCInputFileLocation.Size = new System.Drawing.Size(657, 31);
            this.txtBoxWSQCInputFileLocation.TabIndex = 4;
            // 
            // btnWSQCInputFileLocation
            // 
            this.btnWSQCInputFileLocation.Location = new System.Drawing.Point(727, 316);
            this.btnWSQCInputFileLocation.Name = "btnWSQCInputFileLocation";
            this.btnWSQCInputFileLocation.Size = new System.Drawing.Size(147, 36);
            this.btnWSQCInputFileLocation.TabIndex = 5;
            this.btnWSQCInputFileLocation.Text = "Browse";
            this.btnWSQCInputFileLocation.UseVisualStyleBackColor = true;
            this.btnWSQCInputFileLocation.Click += new System.EventHandler(this.btnWSQCInputFileLocation_Click);
            // 
            // grpBoxWSQConverter
            // 
            this.grpBoxWSQConverter.Controls.Add(this.btnWSQCClose);
            this.grpBoxWSQConverter.Controls.Add(this.btnWSQCConvert);
            this.grpBoxWSQConverter.Controls.Add(this.btnWSQCOutputFileLocation);
            this.grpBoxWSQConverter.Controls.Add(this.txtBoxWSQCOutputFileLocation);
            this.grpBoxWSQConverter.Controls.Add(this.lblWSQCOutputFileLocation);
            this.grpBoxWSQConverter.Controls.Add(this.listBoxWSQCOutputFileFormatList);
            this.grpBoxWSQConverter.Controls.Add(this.lblWSQCOutputfileFormat);
            this.grpBoxWSQConverter.Controls.Add(this.lblWSQConvInputFileFormat);
            this.grpBoxWSQConverter.Controls.Add(this.btnWSQCInputFileLocation);
            this.grpBoxWSQConverter.Controls.Add(this.listBoxWSQConvInputFileFormatList);
            this.grpBoxWSQConverter.Controls.Add(this.txtBoxWSQCInputFileLocation);
            this.grpBoxWSQConverter.Controls.Add(this.lblWSQCInputFileLocation);
            this.grpBoxWSQConverter.Location = new System.Drawing.Point(27, 28);
            this.grpBoxWSQConverter.Name = "grpBoxWSQConverter";
            this.grpBoxWSQConverter.Size = new System.Drawing.Size(1001, 888);
            this.grpBoxWSQConverter.TabIndex = 6;
            this.grpBoxWSQConverter.TabStop = false;
            this.grpBoxWSQConverter.Text = "WSQ Converter";
            // 
            // lblWSQCOutputfileFormat
            // 
            this.lblWSQCOutputfileFormat.AutoSize = true;
            this.lblWSQCOutputfileFormat.Location = new System.Drawing.Point(35, 398);
            this.lblWSQCOutputfileFormat.Name = "lblWSQCOutputfileFormat";
            this.lblWSQCOutputfileFormat.Size = new System.Drawing.Size(196, 25);
            this.lblWSQCOutputfileFormat.TabIndex = 6;
            this.lblWSQCOutputfileFormat.Text = "Output File Format:";
            // 
            // listBoxWSQCOutputFileFormatList
            // 
            this.listBoxWSQCOutputFileFormatList.FormattingEnabled = true;
            this.listBoxWSQCOutputFileFormatList.ItemHeight = 25;
            this.listBoxWSQCOutputFileFormatList.Items.AddRange(new object[] {
            "WSQ - FBI Wavelet Scalar Quantization",
            "BMP - Windows Bitmap Graphics"});
            this.listBoxWSQCOutputFileFormatList.Location = new System.Drawing.Point(40, 431);
            this.listBoxWSQCOutputFileFormatList.Name = "listBoxWSQCOutputFileFormatList";
            this.listBoxWSQCOutputFileFormatList.Size = new System.Drawing.Size(657, 179);
            this.listBoxWSQCOutputFileFormatList.TabIndex = 7;
            // 
            // lblWSQCOutputFileLocation
            // 
            this.lblWSQCOutputFileLocation.AutoSize = true;
            this.lblWSQCOutputFileLocation.Location = new System.Drawing.Point(35, 661);
            this.lblWSQCOutputFileLocation.Name = "lblWSQCOutputFileLocation";
            this.lblWSQCOutputFileLocation.Size = new System.Drawing.Size(215, 25);
            this.lblWSQCOutputFileLocation.TabIndex = 8;
            this.lblWSQCOutputFileLocation.Text = "Output File/Directory:";
            // 
            // txtBoxWSQCOutputFileLocation
            // 
            this.txtBoxWSQCOutputFileLocation.Location = new System.Drawing.Point(40, 689);
            this.txtBoxWSQCOutputFileLocation.Name = "txtBoxWSQCOutputFileLocation";
            this.txtBoxWSQCOutputFileLocation.Size = new System.Drawing.Size(657, 31);
            this.txtBoxWSQCOutputFileLocation.TabIndex = 9;
            // 
            // btnWSQCOutputFileLocation
            // 
            this.btnWSQCOutputFileLocation.Location = new System.Drawing.Point(727, 684);
            this.btnWSQCOutputFileLocation.Name = "btnWSQCOutputFileLocation";
            this.btnWSQCOutputFileLocation.Size = new System.Drawing.Size(147, 36);
            this.btnWSQCOutputFileLocation.TabIndex = 10;
            this.btnWSQCOutputFileLocation.Text = "Browse";
            this.btnWSQCOutputFileLocation.UseVisualStyleBackColor = true;
            this.btnWSQCOutputFileLocation.Click += new System.EventHandler(this.btnWSQCOutputFileLocation_Click);
            // 
            // btnWSQCConvert
            // 
            this.btnWSQCConvert.Location = new System.Drawing.Point(342, 789);
            this.btnWSQCConvert.Name = "btnWSQCConvert";
            this.btnWSQCConvert.Size = new System.Drawing.Size(130, 33);
            this.btnWSQCConvert.TabIndex = 11;
            this.btnWSQCConvert.Text = "Convert";
            this.btnWSQCConvert.UseVisualStyleBackColor = true;
            this.btnWSQCConvert.Click += new System.EventHandler(this.btnWSQCConvert_Click);
            // 
            // btnWSQCClose
            // 
            this.btnWSQCClose.Location = new System.Drawing.Point(495, 789);
            this.btnWSQCClose.Name = "btnWSQCClose";
            this.btnWSQCClose.Size = new System.Drawing.Size(130, 33);
            this.btnWSQCClose.TabIndex = 12;
            this.btnWSQCClose.Text = "Close";
            this.btnWSQCClose.UseVisualStyleBackColor = true;
            this.btnWSQCClose.Click += new System.EventHandler(this.btnWSQCClose_Click);
            // 
            // ConvertToFromWSQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 960);
            this.Controls.Add(this.grpBoxWSQConverter);
            this.Name = "ConvertToFromWSQ";
            this.Text = "WSQ Converter";
            this.grpBoxWSQConverter.ResumeLayout(false);
            this.grpBoxWSQConverter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWSQConvInputFileFormat;
        private System.Windows.Forms.ListBox listBoxWSQConvInputFileFormatList;
        private System.Windows.Forms.Label lblWSQCInputFileLocation;
        private System.Windows.Forms.TextBox txtBoxWSQCInputFileLocation;
        private System.Windows.Forms.Button btnWSQCInputFileLocation;
        private System.Windows.Forms.GroupBox grpBoxWSQConverter;
        private System.Windows.Forms.ListBox listBoxWSQCOutputFileFormatList;
        private System.Windows.Forms.Label lblWSQCOutputfileFormat;
        private System.Windows.Forms.Button btnWSQCOutputFileLocation;
        private System.Windows.Forms.TextBox txtBoxWSQCOutputFileLocation;
        private System.Windows.Forms.Label lblWSQCOutputFileLocation;
        private System.Windows.Forms.Button btnWSQCClose;
        private System.Windows.Forms.Button btnWSQCConvert;
    }
}