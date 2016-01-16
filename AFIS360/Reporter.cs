using iTextSharp.text;
using iTextSharp.text.pdf;
using SourceAFIS.Simple;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public static class Reporter
    {
        public static void generatePersonDetailReport(User user, string personId)
        {
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
        }//end generatePersonDetailReport


        public static void generateUserAccessReport(User user, string userId, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("###-->> Current Thread = " + System.Threading.Thread.CurrentThread.Name);

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
            float[] widths = new float[] { 25f, 40f, 40f, 40f, 40f, 100f };
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
                PdfPCell userIdRecNo = new PdfPCell(new Phrase(Convert.ToString(i + 1)));
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


        public static void generateDuplicateFingerprintReport(User user, ICollection<KeyValuePair<String, MyPerson>> dupMatches)
        {
            Console.WriteLine("###-->> Current Thread = " + System.Threading.Thread.CurrentThread.Name);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            string datetimePref = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string pdfPath = ConfigurationManager.AppSettings["DupFingerprintReportPath"] + "-" + datetimePref + ".pdf";
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
            doc.Open();

            //add title
            doc.AddTitle("Duplicate Fingerprint Report");
            doc.AddHeader("Monthly Report", "Duplicate Fingerprint Report");

            Paragraph paragraphCompanyInfo = new Paragraph("RAB (Rapid Action Battalion)\n");
            paragraphCompanyInfo.Add("Station: " + user.getStationId() + ", " + user.getStationedCity() + "\n");
            paragraphCompanyInfo.Add(user.getStationedCountry() + "\n");
            paragraphCompanyInfo.Alignment = Element.ALIGN_LEFT;

            iTextSharp.text.Font contentFont = iTextSharp.text.FontFactory.GetFont("Webdings", 20, iTextSharp.text.Font.BOLD);
            Paragraph paragraphReportTitle = new Paragraph("Duplicate Fingerprint Report\n", contentFont);
            paragraphReportTitle.Alignment = Element.ALIGN_CENTER;

            Paragraph paragraphReportSubTitle = new Paragraph();
            paragraphReportSubTitle.Add("By: " + user.getFirstName() + " " + user.getLastName() + ", ID: " + user.getPersonId() + "\n");
            paragraphReportSubTitle.Add("At: " + DateTime.Now.ToString() + "\n\n");
            paragraphReportSubTitle.Alignment = Element.ALIGN_CENTER;

            doc.Add(paragraphCompanyInfo);
            doc.Add(paragraphReportTitle);
            doc.Add(paragraphReportSubTitle);

            PdfPTable dupFingerprintReportTable = new PdfPTable(3);
            float[] widths = new float[] { 25f, 40f, 40f };
            dupFingerprintReportTable.SetWidths(widths);

            PdfPCell headerCellRecNo = new PdfPCell(new Phrase("RecNo"));
            PdfPCell headerCellID = new PdfPCell(new Phrase("Person Id"));
            PdfPCell headerCellDup = new PdfPCell(new Phrase("Person Id with Duplicate fingerprint"));


            //Add Headers to the table
            dupFingerprintReportTable.AddCell(headerCellRecNo);
            dupFingerprintReportTable.AddCell(headerCellID);
            dupFingerprintReportTable.AddCell(headerCellDup);

            int i = 0;

            foreach (KeyValuePair<string, MyPerson> dupMatch in dupMatches)
            {                
                string probePersonId = dupMatch.Key;
                MyPerson dupPerson = dupMatch.Value;
                Console.WriteLine("###-->> Fingerprints of Person[PersonId = " + probePersonId + "] has duplicate with Person[PersonId = " + dupPerson.PersonId + "]");
                PdfPCell recNo = new PdfPCell(new Phrase(Convert.ToString(i + 1)));
                PdfPCell probePersonIdCell = new PdfPCell(new Phrase(probePersonId));
                PdfPCell dupPersonIdCell = new PdfPCell(new Phrase(dupPerson.PersonId));

                //Adding cells to the table
                dupFingerprintReportTable.AddCell(recNo);
                dupFingerprintReportTable.AddCell(probePersonIdCell);
                dupFingerprintReportTable.AddCell(dupPersonIdCell);

                i++;
            }

            doc.Add(dupFingerprintReportTable);

            doc.Close();
            Console.WriteLine("PDF Generated successfully...");
            System.Diagnostics.Process.Start(pdfPath);
        }
    }
}

