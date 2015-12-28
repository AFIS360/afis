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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertToFromWSQ));
            this.lblWSQConvInputFileFormat = new System.Windows.Forms.Label();
            this.listBoxWSQConvInputFileFormatList = new System.Windows.Forms.ListBox();
            this.lblWSQCInputFileLocation = new System.Windows.Forms.Label();
            this.txtBoxWSQCInputFileLocation = new System.Windows.Forms.TextBox();
            this.btnWSQCInputFileLocation = new System.Windows.Forms.Button();
            this.grpBoxWSQConverter = new System.Windows.Forms.GroupBox();
            this.btnWSQCClose = new System.Windows.Forms.Button();
            this.btnWSQCConvert = new System.Windows.Forms.Button();
            this.btnWSQCOutputFileLocation = new System.Windows.Forms.Button();
            this.txtBoxWSQCOutputFileLocation = new System.Windows.Forms.TextBox();
            this.lblWSQCOutputFileLocation = new System.Windows.Forms.Label();
            this.listBoxWSQCOutputFileFormatList = new System.Windows.Forms.ListBox();
            this.lblWSQCOutputfileFormat = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxWSQConverter.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWSQConvInputFileFormat
            // 
            this.lblWSQConvInputFileFormat.AutoSize = true;
            this.lblWSQConvInputFileFormat.Location = new System.Drawing.Point(18, 24);
            this.lblWSQConvInputFileFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWSQConvInputFileFormat.Name = "lblWSQConvInputFileFormat";
            this.lblWSQConvInputFileFormat.Size = new System.Drawing.Size(88, 13);
            this.lblWSQConvInputFileFormat.TabIndex = 0;
            this.lblWSQConvInputFileFormat.Text = "Input File Format:";
            // 
            // listBoxWSQConvInputFileFormatList
            // 
            this.listBoxWSQConvInputFileFormatList.FormattingEnabled = true;
            this.listBoxWSQConvInputFileFormatList.Items.AddRange(new object[] {
            "WSQ - FBI Wavelet Scalar Quantization",
            "BMP - Windows Bitmap Graphics"});
            this.listBoxWSQConvInputFileFormatList.Location = new System.Drawing.Point(20, 40);
            this.listBoxWSQConvInputFileFormatList.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxWSQConvInputFileFormatList.Name = "listBoxWSQConvInputFileFormatList";
            this.listBoxWSQConvInputFileFormatList.Size = new System.Drawing.Size(330, 95);
            this.listBoxWSQConvInputFileFormatList.TabIndex = 2;
            // 
            // lblWSQCInputFileLocation
            // 
            this.lblWSQCInputFileLocation.AutoSize = true;
            this.lblWSQCInputFileLocation.Location = new System.Drawing.Point(18, 150);
            this.lblWSQCInputFileLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWSQCInputFileLocation.Name = "lblWSQCInputFileLocation";
            this.lblWSQCInputFileLocation.Size = new System.Drawing.Size(100, 13);
            this.lblWSQCInputFileLocation.TabIndex = 3;
            this.lblWSQCInputFileLocation.Text = "Input File/Directory:";
            // 
            // txtBoxWSQCInputFileLocation
            // 
            this.txtBoxWSQCInputFileLocation.Location = new System.Drawing.Point(20, 164);
            this.txtBoxWSQCInputFileLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxWSQCInputFileLocation.Name = "txtBoxWSQCInputFileLocation";
            this.txtBoxWSQCInputFileLocation.Size = new System.Drawing.Size(330, 20);
            this.txtBoxWSQCInputFileLocation.TabIndex = 4;
            // 
            // btnWSQCInputFileLocation
            // 
            this.btnWSQCInputFileLocation.Location = new System.Drawing.Point(364, 157);
            this.btnWSQCInputFileLocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnWSQCInputFileLocation.Name = "btnWSQCInputFileLocation";
            this.btnWSQCInputFileLocation.Size = new System.Drawing.Size(74, 26);
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
            this.grpBoxWSQConverter.Location = new System.Drawing.Point(14, 34);
            this.grpBoxWSQConverter.Margin = new System.Windows.Forms.Padding(2);
            this.grpBoxWSQConverter.Name = "grpBoxWSQConverter";
            this.grpBoxWSQConverter.Padding = new System.Windows.Forms.Padding(2);
            this.grpBoxWSQConverter.Size = new System.Drawing.Size(500, 462);
            this.grpBoxWSQConverter.TabIndex = 6;
            this.grpBoxWSQConverter.TabStop = false;
            this.grpBoxWSQConverter.Text = "WSQ Converter";
            // 
            // btnWSQCClose
            // 
            this.btnWSQCClose.Location = new System.Drawing.Point(248, 410);
            this.btnWSQCClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnWSQCClose.Name = "btnWSQCClose";
            this.btnWSQCClose.Size = new System.Drawing.Size(65, 27);
            this.btnWSQCClose.TabIndex = 12;
            this.btnWSQCClose.Text = "Close";
            this.btnWSQCClose.UseVisualStyleBackColor = true;
            this.btnWSQCClose.Click += new System.EventHandler(this.btnWSQCClose_Click);
            // 
            // btnWSQCConvert
            // 
            this.btnWSQCConvert.Location = new System.Drawing.Point(171, 410);
            this.btnWSQCConvert.Margin = new System.Windows.Forms.Padding(2);
            this.btnWSQCConvert.Name = "btnWSQCConvert";
            this.btnWSQCConvert.Size = new System.Drawing.Size(65, 27);
            this.btnWSQCConvert.TabIndex = 11;
            this.btnWSQCConvert.Text = "Convert";
            this.btnWSQCConvert.UseVisualStyleBackColor = true;
            this.btnWSQCConvert.Click += new System.EventHandler(this.btnWSQCConvert_Click);
            // 
            // btnWSQCOutputFileLocation
            // 
            this.btnWSQCOutputFileLocation.Location = new System.Drawing.Point(364, 350);
            this.btnWSQCOutputFileLocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnWSQCOutputFileLocation.Name = "btnWSQCOutputFileLocation";
            this.btnWSQCOutputFileLocation.Size = new System.Drawing.Size(74, 28);
            this.btnWSQCOutputFileLocation.TabIndex = 10;
            this.btnWSQCOutputFileLocation.Text = "Browse";
            this.btnWSQCOutputFileLocation.UseVisualStyleBackColor = true;
            this.btnWSQCOutputFileLocation.Click += new System.EventHandler(this.btnWSQCOutputFileLocation_Click);
            // 
            // txtBoxWSQCOutputFileLocation
            // 
            this.txtBoxWSQCOutputFileLocation.Location = new System.Drawing.Point(20, 358);
            this.txtBoxWSQCOutputFileLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxWSQCOutputFileLocation.Name = "txtBoxWSQCOutputFileLocation";
            this.txtBoxWSQCOutputFileLocation.Size = new System.Drawing.Size(330, 20);
            this.txtBoxWSQCOutputFileLocation.TabIndex = 9;
            // 
            // lblWSQCOutputFileLocation
            // 
            this.lblWSQCOutputFileLocation.AutoSize = true;
            this.lblWSQCOutputFileLocation.Location = new System.Drawing.Point(18, 344);
            this.lblWSQCOutputFileLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWSQCOutputFileLocation.Name = "lblWSQCOutputFileLocation";
            this.lblWSQCOutputFileLocation.Size = new System.Drawing.Size(108, 13);
            this.lblWSQCOutputFileLocation.TabIndex = 8;
            this.lblWSQCOutputFileLocation.Text = "Output File/Directory:";
            // 
            // listBoxWSQCOutputFileFormatList
            // 
            this.listBoxWSQCOutputFileFormatList.FormattingEnabled = true;
            this.listBoxWSQCOutputFileFormatList.Items.AddRange(new object[] {
            "WSQ - FBI Wavelet Scalar Quantization",
            "BMP - Windows Bitmap Graphics"});
            this.listBoxWSQCOutputFileFormatList.Location = new System.Drawing.Point(20, 224);
            this.listBoxWSQCOutputFileFormatList.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxWSQCOutputFileFormatList.Name = "listBoxWSQCOutputFileFormatList";
            this.listBoxWSQCOutputFileFormatList.Size = new System.Drawing.Size(330, 95);
            this.listBoxWSQCOutputFileFormatList.TabIndex = 7;
            // 
            // lblWSQCOutputfileFormat
            // 
            this.lblWSQCOutputfileFormat.AutoSize = true;
            this.lblWSQCOutputfileFormat.Location = new System.Drawing.Point(18, 207);
            this.lblWSQCOutputfileFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWSQCOutputfileFormat.Name = "lblWSQCOutputfileFormat";
            this.lblWSQCOutputfileFormat.Size = new System.Drawing.Size(96, 13);
            this.lblWSQCOutputfileFormat.TabIndex = 6;
            this.lblWSQCOutputfileFormat.Text = "Output File Format:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(530, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ConvertToFromWSQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 519);
            this.Controls.Add(this.grpBoxWSQConverter);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ConvertToFromWSQ";
            this.Text = "WSQ Converter";
            this.grpBoxWSQConverter.ResumeLayout(false);
            this.grpBoxWSQConverter.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}