using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wsqm;

namespace AFIS360
{
    public partial class ConvertToFromWSQ : Form
    {
        private ActivityLog activityLog;

        public ConvertToFromWSQ(ActivityLog activityLog)
        {
            InitializeComponent();
            this.activityLog = activityLog;
        }

        private void btnWSQCInputFileLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            txtBoxWSQCInputFileLocation.Text = fbd.SelectedPath;

        }//end btnWSQCInputFileLocation_Click

        private void btnWSQCOutputFileLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            txtBoxWSQCOutputFileLocation.Text = fbd.SelectedPath;
        }

        private void btnWSQCConvert_Click(object sender, EventArgs e)
        {
            string inputFileDir = txtBoxWSQCInputFileLocation.Text;
            string outputFileDir = txtBoxWSQCOutputFileLocation.Text;
            string searchPattern = null;
            string outputFileExtension = null;
            string inputFileFormat = null;
            string outputFileFormat = null;

            //set activity
            activityLog.setActivity("Activity: Image conversion \n");

            //Verify the Input & Output file directories are not empty
            if(string.IsNullOrWhiteSpace(txtBoxWSQCInputFileLocation.Text))
            {
                MessageBox.Show("Input file directory must be selected.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBoxWSQCOutputFileLocation.Text))
            {
                MessageBox.Show("Output file directory must be selected.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Find out Input file format
            if (!string.IsNullOrWhiteSpace(listBoxWSQConvInputFileFormatList.Text) && listBoxWSQConvInputFileFormatList.Text == "BMP - Windows Bitmap Graphics")
            {
                Console.WriteLine("###-->> Selected Input File Format = " + listBoxWSQConvInputFileFormatList.Text);
                searchPattern = "*.bmp";
                inputFileFormat = "bmp";
            }
            else if (!string.IsNullOrWhiteSpace(listBoxWSQConvInputFileFormatList.Text) && listBoxWSQConvInputFileFormatList.Text == "WSQ - FBI Wavelet Scalar Quantization")
            {
                Console.WriteLine("###-->> Selected Input File Format = " + listBoxWSQConvInputFileFormatList.Text);
                searchPattern = "*.wsq";
                inputFileFormat = "wsq";
            }
            else if (!string.IsNullOrWhiteSpace(listBoxWSQConvInputFileFormatList.Text) && listBoxWSQConvInputFileFormatList.Text == "TIF - PC Tagged Information File Format")
            {
                Console.WriteLine("###-->> Selected Input File Format = " + listBoxWSQConvInputFileFormatList.Text);
                searchPattern = "*.tif";
                inputFileFormat = "tif";
            }
            else
            {
                Console.WriteLine("###-->> Selected Input File Format = " + listBoxWSQConvInputFileFormatList.Text);
                MessageBox.Show("Input file format must be selected.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Find out Output file format
            if (!string.IsNullOrWhiteSpace(listBoxWSQCOutputFileFormatList.Text) && listBoxWSQCOutputFileFormatList.Text == "BMP - Windows Bitmap Graphics")
            {
                Console.WriteLine("###-->> Selected Output File Format = " + listBoxWSQCOutputFileFormatList.Text);
                outputFileExtension = ".bmp";
                outputFileFormat = "bmp";
            }
            else if (!string.IsNullOrWhiteSpace(listBoxWSQCOutputFileFormatList.Text) && listBoxWSQCOutputFileFormatList.Text == "WSQ - FBI Wavelet Scalar Quantization")
            {
                Console.WriteLine("###-->> Selected Output File Format = " + listBoxWSQCOutputFileFormatList.Text);
                outputFileExtension = ".wsq";
                outputFileFormat = "wsq";
            }
            else if (!string.IsNullOrWhiteSpace(listBoxWSQCOutputFileFormatList.Text) && listBoxWSQCOutputFileFormatList.Text == "TIF - PC Tagged Information File Format")
            {
                Console.WriteLine("###-->> Selected Output File Format = " + listBoxWSQCOutputFileFormatList.Text);
                outputFileExtension = ".tif";
                outputFileFormat = "tif";
            }
            else
            {
                Console.WriteLine("###-->> Selected Output File Format = " + listBoxWSQConvInputFileFormatList.Text);
                MessageBox.Show("Output file format must be selected.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Verify there are file(s) in the folder to convert
            string[] inputFiles = System.IO.Directory.GetFiles(inputFileDir, searchPattern);
            Console.WriteLine("###-->> # of Selected Format files = " + inputFiles.Length);
            if(inputFiles.Length == 0)
            {
                MessageBox.Show("No file(s) to convert.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Verify input & output formats are not same or empty
            if(inputFileFormat == "wsq" && outputFileFormat == "wsq")
            {
                MessageBox.Show(" Both Input and Output file cannot be WSQ format.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if(inputFileFormat == "bmp" && outputFileFormat == "bmp")
            {
                MessageBox.Show(" Both Input and Output file cannot be BMP format.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (inputFileFormat == "tif" && outputFileFormat == "tif")
            {
                MessageBox.Show(" Both Input and Output file cannot be TIF format.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (inputFileFormat == outputFileFormat)
            {
                MessageBox.Show(" Both Input and Output file cannot be same format.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(inputFileFormat) || string.IsNullOrWhiteSpace(outputFileFormat))
            {
                MessageBox.Show(" Input and Output file format must be selected.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                WSQ wsq = new WSQ();

                for (int i = 0; i < inputFiles.Length; i++)
                {
                    Console.WriteLine("###-->> # InputFiles [" + i + "] = " + inputFiles[i]);
                    string inputFileName = inputFiles[i];
                    string outputFileName = outputFileDir + "\\" + Path.GetFileNameWithoutExtension(inputFileName) + outputFileExtension;
                    Console.WriteLine("###-->> # OutputFiles [" + i + "] = " + outputFileName);

                    if (inputFileFormat == "bmp" && outputFileFormat == "wsq")
                    {
                        Console.WriteLine("###-->> Converting BMP to WSQ");
                        //Encode BMP to WSQ
                        String[] comentario = new String[2];
                        comentario[0] = "Mohammad Mohsin";
                        comentario[1] = "LKT";
                        wsq.EnconderFile(@inputFileName, @outputFileName, comentario, 0.75f);
                    }
                    else if (inputFileFormat == "wsq" && outputFileFormat == "bmp")
                    {
                        Console.WriteLine("###-->> Converting WSQ to BMP");
                        //Decode WSQ to BMP
                        wsq.DecoderFile(@inputFileName, @outputFileName);
                    }
                    else if (inputFileFormat == "wsq" && outputFileFormat == "tif")
                    {
                        Console.WriteLine("###-->> Converting WSQ to TIF");
                        //Decode WSQ to BMP
                        wsq.DecoderFile(@inputFileName, @outputFileName);
                    }
                    else if (inputFileFormat == "tif" && outputFileFormat == "wsq")
                    {
                        Console.WriteLine("###-->> Converting TIF to WSQ");
                        //Encode BMP to WSQ
                        String[] comentario = new String[2];
                        comentario[0] = "Mohammad Mohsin";
                        comentario[1] = "LKT";
                        wsq.EnconderFile(@inputFileName, @outputFileName, comentario, 0.75f);
                    }
                    else
                    {
                        MessageBox.Show(" Invalid conversion parameter. Converter only converts To/From WSQ.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }//end for
                MessageBox.Show(" Conversion completed successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }//end else

        }

        private void btnWSQCClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
