namespace AFIS360
{
    partial class BatchExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchExport));
            this.grpBoxBatchExport = new System.Windows.Forms.GroupBox();
            this.tlpBatchExport = new System.Windows.Forms.TableLayoutPanel();
            this.lblBatchExportFile = new System.Windows.Forms.Label();
            this.txtBoxBatchExportFile = new System.Windows.Forms.TextBox();
            this.btnBatchExportBrowse = new System.Windows.Forms.Button();
            this.btnBatchExportExport = new System.Windows.Forms.Button();
            this.progressBarBatchExport = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBatchExportTotalRec = new System.Windows.Forms.Label();
            this.grpBoxBatchExport.SuspendLayout();
            this.tlpBatchExport.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxBatchExport
            // 
            this.grpBoxBatchExport.Controls.Add(this.tlpBatchExport);
            this.grpBoxBatchExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxBatchExport.Location = new System.Drawing.Point(68, 74);
            this.grpBoxBatchExport.Name = "grpBoxBatchExport";
            this.grpBoxBatchExport.Size = new System.Drawing.Size(1795, 893);
            this.grpBoxBatchExport.TabIndex = 0;
            this.grpBoxBatchExport.TabStop = false;
            this.grpBoxBatchExport.Text = "Batch Export";
            // 
            // tlpBatchExport
            // 
            this.tlpBatchExport.ColumnCount = 3;
            this.tlpBatchExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.04333F));
            this.tlpBatchExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.95667F));
            this.tlpBatchExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 453F));
            this.tlpBatchExport.Controls.Add(this.lblBatchExportFile, 0, 0);
            this.tlpBatchExport.Controls.Add(this.txtBoxBatchExportFile, 1, 0);
            this.tlpBatchExport.Controls.Add(this.btnBatchExportBrowse, 2, 0);
            this.tlpBatchExport.Controls.Add(this.btnBatchExportExport, 0, 1);
            this.tlpBatchExport.Controls.Add(this.progressBarBatchExport, 1, 1);
            this.tlpBatchExport.Controls.Add(this.lblBatchExportTotalRec, 2, 1);
            this.tlpBatchExport.Location = new System.Drawing.Point(62, 109);
            this.tlpBatchExport.Name = "tlpBatchExport";
            this.tlpBatchExport.RowCount = 2;
            this.tlpBatchExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.75758F));
            this.tlpBatchExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.24242F));
            this.tlpBatchExport.Size = new System.Drawing.Size(1601, 132);
            this.tlpBatchExport.TabIndex = 0;
            // 
            // lblBatchExportFile
            // 
            this.lblBatchExportFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBatchExportFile.AutoSize = true;
            this.lblBatchExportFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchExportFile.Location = new System.Drawing.Point(3, 18);
            this.lblBatchExportFile.Name = "lblBatchExportFile";
            this.lblBatchExportFile.Size = new System.Drawing.Size(269, 31);
            this.lblBatchExportFile.TabIndex = 0;
            this.lblBatchExportFile.Text = "Export to a file (.csv):";
            // 
            // txtBoxBatchExportFile
            // 
            this.txtBoxBatchExportFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBoxBatchExportFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBatchExportFile.Location = new System.Drawing.Point(290, 14);
            this.txtBoxBatchExportFile.Name = "txtBoxBatchExportFile";
            this.txtBoxBatchExportFile.Size = new System.Drawing.Size(834, 38);
            this.txtBoxBatchExportFile.TabIndex = 1;
            // 
            // btnBatchExportBrowse
            // 
            this.btnBatchExportBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBatchExportBrowse.Location = new System.Drawing.Point(1150, 3);
            this.btnBatchExportBrowse.Name = "btnBatchExportBrowse";
            this.btnBatchExportBrowse.Size = new System.Drawing.Size(163, 61);
            this.btnBatchExportBrowse.TabIndex = 2;
            this.btnBatchExportBrowse.Text = "Browse";
            this.btnBatchExportBrowse.UseVisualStyleBackColor = true;
            this.btnBatchExportBrowse.Click += new System.EventHandler(this.btnBatchExportBrowse_Click);
            // 
            // btnBatchExportExport
            // 
            this.btnBatchExportExport.Location = new System.Drawing.Point(3, 70);
            this.btnBatchExportExport.Name = "btnBatchExportExport";
            this.btnBatchExportExport.Size = new System.Drawing.Size(234, 59);
            this.btnBatchExportExport.TabIndex = 3;
            this.btnBatchExportExport.Text = "Export";
            this.btnBatchExportExport.UseVisualStyleBackColor = true;
            this.btnBatchExportExport.Click += new System.EventHandler(this.btnBatchExportExport_Click);
            // 
            // progressBarBatchExport
            // 
            this.progressBarBatchExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressBarBatchExport.Location = new System.Drawing.Point(290, 70);
            this.progressBarBatchExport.Name = "progressBarBatchExport";
            this.progressBarBatchExport.Size = new System.Drawing.Size(834, 59);
            this.progressBarBatchExport.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1929, 40);
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
            // lblBatchExportTotalRec
            // 
            this.lblBatchExportTotalRec.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBatchExportTotalRec.AutoSize = true;
            this.lblBatchExportTotalRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchExportTotalRec.Location = new System.Drawing.Point(1150, 84);
            this.lblBatchExportTotalRec.Name = "lblBatchExportTotalRec";
            this.lblBatchExportTotalRec.Size = new System.Drawing.Size(0, 31);
            this.lblBatchExportTotalRec.TabIndex = 5;
            // 
            // BatchExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1929, 1111);
            this.Controls.Add(this.grpBoxBatchExport);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BatchExport";
            this.Text = " Batch Export";
            this.grpBoxBatchExport.ResumeLayout(false);
            this.tlpBatchExport.ResumeLayout(false);
            this.tlpBatchExport.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxBatchExport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpBatchExport;
        private System.Windows.Forms.Label lblBatchExportFile;
        private System.Windows.Forms.TextBox txtBoxBatchExportFile;
        private System.Windows.Forms.Button btnBatchExportBrowse;
        private System.Windows.Forms.Button btnBatchExportExport;
        private System.Windows.Forms.ProgressBar progressBarBatchExport;
        private System.Windows.Forms.Label lblBatchExportTotalRec;
    }
}