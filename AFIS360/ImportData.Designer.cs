namespace AFIS360
{
    partial class ImportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportData));
            this.grpBoxImportData = new System.Windows.Forms.GroupBox();
            this.tlpTmportDataImportStat = new System.Windows.Forms.TableLayoutPanel();
            this.lblImportDataFailedCount = new System.Windows.Forms.Label();
            this.lblDataImportSuccessCount = new System.Windows.Forms.Label();
            this.lblImportDataTotalNoRecs = new System.Windows.Forms.Label();
            this.lblImportDataImportedRecs = new System.Windows.Forms.Label();
            this.lblImportDataFailedToImportRecs = new System.Windows.Forms.Label();
            this.lblImportDataTotalRecCount = new System.Windows.Forms.Label();
            this.tlpImportData = new System.Windows.Forms.TableLayoutPanel();
            this.lblImportDataBrowseFile = new System.Windows.Forms.Label();
            this.txtBoxImportDataInputFile = new System.Windows.Forms.TextBox();
            this.btnImportDataBrowse = new System.Windows.Forms.Button();
            this.btnImportDataImport = new System.Windows.Forms.Button();
            this.progBarImportDataImportProgress = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxImportData.SuspendLayout();
            this.tlpTmportDataImportStat.SuspendLayout();
            this.tlpImportData.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxImportData
            // 
            this.grpBoxImportData.Controls.Add(this.tlpTmportDataImportStat);
            this.grpBoxImportData.Controls.Add(this.tlpImportData);
            this.grpBoxImportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxImportData.Location = new System.Drawing.Point(57, 67);
            this.grpBoxImportData.Name = "grpBoxImportData";
            this.grpBoxImportData.Size = new System.Drawing.Size(1564, 950);
            this.grpBoxImportData.TabIndex = 0;
            this.grpBoxImportData.TabStop = false;
            this.grpBoxImportData.Text = "Import Data";
            // 
            // tlpTmportDataImportStat
            // 
            this.tlpTmportDataImportStat.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpTmportDataImportStat.ColumnCount = 3;
            this.tlpTmportDataImportStat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.35666F));
            this.tlpTmportDataImportStat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.64334F));
            this.tlpTmportDataImportStat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 541F));
            this.tlpTmportDataImportStat.Controls.Add(this.lblImportDataFailedCount, 2, 1);
            this.tlpTmportDataImportStat.Controls.Add(this.lblDataImportSuccessCount, 1, 1);
            this.tlpTmportDataImportStat.Controls.Add(this.lblImportDataTotalNoRecs, 0, 0);
            this.tlpTmportDataImportStat.Controls.Add(this.lblImportDataImportedRecs, 1, 0);
            this.tlpTmportDataImportStat.Controls.Add(this.lblImportDataFailedToImportRecs, 2, 0);
            this.tlpTmportDataImportStat.Controls.Add(this.lblImportDataTotalRecCount, 0, 1);
            this.tlpTmportDataImportStat.Location = new System.Drawing.Point(35, 241);
            this.tlpTmportDataImportStat.Name = "tlpTmportDataImportStat";
            this.tlpTmportDataImportStat.RowCount = 2;
            this.tlpTmportDataImportStat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTmportDataImportStat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tlpTmportDataImportStat.Size = new System.Drawing.Size(1426, 134);
            this.tlpTmportDataImportStat.TabIndex = 2;
            // 
            // lblImportDataFailedCount
            // 
            this.lblImportDataFailedCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblImportDataFailedCount.AutoSize = true;
            this.lblImportDataFailedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataFailedCount.Location = new System.Drawing.Point(1154, 88);
            this.lblImportDataFailedCount.Name = "lblImportDataFailedCount";
            this.lblImportDataFailedCount.Size = new System.Drawing.Size(0, 31);
            this.lblImportDataFailedCount.TabIndex = 5;
            // 
            // lblDataImportSuccessCount
            // 
            this.lblDataImportSuccessCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDataImportSuccessCount.AutoSize = true;
            this.lblDataImportSuccessCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataImportSuccessCount.Location = new System.Drawing.Point(637, 88);
            this.lblDataImportSuccessCount.Name = "lblDataImportSuccessCount";
            this.lblDataImportSuccessCount.Size = new System.Drawing.Size(0, 31);
            this.lblDataImportSuccessCount.TabIndex = 4;
            // 
            // lblImportDataTotalNoRecs
            // 
            this.lblImportDataTotalNoRecs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblImportDataTotalNoRecs.AutoSize = true;
            this.lblImportDataTotalNoRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataTotalNoRecs.Location = new System.Drawing.Point(21, 21);
            this.lblImportDataTotalNoRecs.Name = "lblImportDataTotalNoRecs";
            this.lblImportDataTotalNoRecs.Size = new System.Drawing.Size(350, 31);
            this.lblImportDataTotalNoRecs.TabIndex = 0;
            this.lblImportDataTotalNoRecs.Text = "Total # of Records on File";
            // 
            // lblImportDataImportedRecs
            // 
            this.lblImportDataImportedRecs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblImportDataImportedRecs.AutoSize = true;
            this.lblImportDataImportedRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataImportedRecs.Location = new System.Drawing.Point(399, 21);
            this.lblImportDataImportedRecs.Name = "lblImportDataImportedRecs";
            this.lblImportDataImportedRecs.Size = new System.Drawing.Size(476, 31);
            this.lblImportDataImportedRecs.TabIndex = 1;
            this.lblImportDataImportedRecs.Text = "# of Records Imported Successfully";
            // 
            // lblImportDataFailedToImportRecs
            // 
            this.lblImportDataFailedToImportRecs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblImportDataFailedToImportRecs.AutoSize = true;
            this.lblImportDataFailedToImportRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataFailedToImportRecs.Location = new System.Drawing.Point(958, 21);
            this.lblImportDataFailedToImportRecs.Name = "lblImportDataFailedToImportRecs";
            this.lblImportDataFailedToImportRecs.Size = new System.Drawing.Size(392, 31);
            this.lblImportDataFailedToImportRecs.TabIndex = 2;
            this.lblImportDataFailedToImportRecs.Text = "# of Records Failed to Import";
            // 
            // lblImportDataTotalRecCount
            // 
            this.lblImportDataTotalRecCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblImportDataTotalRecCount.AutoSize = true;
            this.lblImportDataTotalRecCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataTotalRecCount.Location = new System.Drawing.Point(196, 88);
            this.lblImportDataTotalRecCount.Name = "lblImportDataTotalRecCount";
            this.lblImportDataTotalRecCount.Size = new System.Drawing.Size(0, 31);
            this.lblImportDataTotalRecCount.TabIndex = 3;
            // 
            // tlpImportData
            // 
            this.tlpImportData.ColumnCount = 3;
            this.tlpImportData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.07959F));
            this.tlpImportData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.92041F));
            this.tlpImportData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tlpImportData.Controls.Add(this.lblImportDataBrowseFile, 0, 0);
            this.tlpImportData.Controls.Add(this.txtBoxImportDataInputFile, 1, 0);
            this.tlpImportData.Controls.Add(this.btnImportDataBrowse, 2, 0);
            this.tlpImportData.Controls.Add(this.btnImportDataImport, 0, 1);
            this.tlpImportData.Controls.Add(this.progBarImportDataImportProgress, 1, 1);
            this.tlpImportData.Location = new System.Drawing.Point(32, 80);
            this.tlpImportData.Name = "tlpImportData";
            this.tlpImportData.RowCount = 2;
            this.tlpImportData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.5873F));
            this.tlpImportData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.4127F));
            this.tlpImportData.Size = new System.Drawing.Size(1429, 126);
            this.tlpImportData.TabIndex = 1;
            // 
            // lblImportDataBrowseFile
            // 
            this.lblImportDataBrowseFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImportDataBrowseFile.AutoSize = true;
            this.lblImportDataBrowseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataBrowseFile.Location = new System.Drawing.Point(3, 16);
            this.lblImportDataBrowseFile.Name = "lblImportDataBrowseFile";
            this.lblImportDataBrowseFile.Size = new System.Drawing.Size(277, 31);
            this.lblImportDataBrowseFile.TabIndex = 0;
            this.lblImportDataBrowseFile.Text = "Import from file (.csv):";
            // 
            // txtBoxImportDataInputFile
            // 
            this.txtBoxImportDataInputFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBoxImportDataInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxImportDataInputFile.Location = new System.Drawing.Point(311, 13);
            this.txtBoxImportDataInputFile.Name = "txtBoxImportDataInputFile";
            this.txtBoxImportDataInputFile.Size = new System.Drawing.Size(846, 38);
            this.txtBoxImportDataInputFile.TabIndex = 1;
            // 
            // btnImportDataBrowse
            // 
            this.btnImportDataBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnImportDataBrowse.Location = new System.Drawing.Point(1185, 3);
            this.btnImportDataBrowse.Name = "btnImportDataBrowse";
            this.btnImportDataBrowse.Size = new System.Drawing.Size(203, 57);
            this.btnImportDataBrowse.TabIndex = 2;
            this.btnImportDataBrowse.Text = "Browse";
            this.btnImportDataBrowse.UseVisualStyleBackColor = true;
            this.btnImportDataBrowse.Click += new System.EventHandler(this.btnImportDataBrowse_Click);
            // 
            // btnImportDataImport
            // 
            this.btnImportDataImport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnImportDataImport.Location = new System.Drawing.Point(3, 67);
            this.btnImportDataImport.Name = "btnImportDataImport";
            this.btnImportDataImport.Size = new System.Drawing.Size(187, 55);
            this.btnImportDataImport.TabIndex = 3;
            this.btnImportDataImport.Text = "Import";
            this.btnImportDataImport.UseVisualStyleBackColor = true;
            this.btnImportDataImport.Click += new System.EventHandler(this.btnImportDataImport_Click);
            // 
            // progBarImportDataImportProgress
            // 
            this.progBarImportDataImportProgress.Location = new System.Drawing.Point(311, 67);
            this.progBarImportDataImportProgress.Name = "progBarImportDataImportProgress";
            this.progBarImportDataImportProgress.Size = new System.Drawing.Size(846, 55);
            this.progBarImportDataImportProgress.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1699, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(90, 36);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1699, 1094);
            this.Controls.Add(this.grpBoxImportData);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImportData";
            this.Text = " Import Data";
            this.grpBoxImportData.ResumeLayout(false);
            this.tlpTmportDataImportStat.ResumeLayout(false);
            this.tlpTmportDataImportStat.PerformLayout();
            this.tlpImportData.ResumeLayout(false);
            this.tlpImportData.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxImportData;
        private System.Windows.Forms.Label lblImportDataBrowseFile;
        private System.Windows.Forms.TableLayoutPanel tlpImportData;
        private System.Windows.Forms.TextBox txtBoxImportDataInputFile;
        private System.Windows.Forms.Button btnImportDataBrowse;
        private System.Windows.Forms.Button btnImportDataImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progBarImportDataImportProgress;
        private System.Windows.Forms.TableLayoutPanel tlpTmportDataImportStat;
        private System.Windows.Forms.Label lblImportDataFailedCount;
        private System.Windows.Forms.Label lblDataImportSuccessCount;
        private System.Windows.Forms.Label lblImportDataTotalNoRecs;
        private System.Windows.Forms.Label lblImportDataImportedRecs;
        private System.Windows.Forms.Label lblImportDataFailedToImportRecs;
        private System.Windows.Forms.Label lblImportDataTotalRecCount;
    }
}