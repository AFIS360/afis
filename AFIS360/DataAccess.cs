using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Net.Mime.MediaTypeNames;
using SourceAFIS.Simple;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;
using AFIS360Common;
using AFIS360ommon;

namespace AFIS360
{
    public class DataAccess
    {
        public Status storePersonDetailwithFingerprints(MyPerson person, PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            Status status = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //store person detail
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedPassportPhoto;
                            cmd.CommandText = "INSERT INTO person(person_id, fname, lname, mname, name_prefix, name_suffix, " +
                                              "DOB, addr_street, addr_city, addr_postal_code, addr_state, addr_country, profession, " +
                                              "father_name, cell_nbr, home_phone, office_phone, email_addr, photo) " +
                                              "VALUES(@person_id, @fname, @lname, @mname, @name_prefix, @name_suffix, " +
                                              "@DOB, @addr_street, @addr_city, @addr_postal_code, @addr_state, @addr_country, @profession, " +
                                              "@father_name, @cell_nbr, @home_phone, @office_phone, @email_addr, @photo)";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                            cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                            cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                            cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                            cmd.Parameters.AddWithValue("@name_Prefix", personDetail.getPrefix());
                            cmd.Parameters.AddWithValue("@name_suffix", personDetail.getSuffix());
                            cmd.Parameters.AddWithValue("@DOB", personDetail.getDOB());
                            cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                            cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                            cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                            cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                            cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                            cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());
                            cmd.Parameters.AddWithValue("@father_name", personDetail.getFatherName());
                            cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                            cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                            cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                            cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());

                            if (personDetail.getPassportPhoto() != null)
                            {
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    BinaryFormatter oBFormatter = new BinaryFormatter();
                                    oBFormatter.Serialize(stream, personDetail.getPassportPhoto());
                                    oSerializedPassportPhoto = stream.ToArray();
                                }
                                cmd.Parameters.AddWithValue("@photo", oSerializedPassportPhoto);
                            }
                            else {
                                cmd.Parameters.AddWithValue("@photo", null);
                            }

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        //store person's finperprints
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedFpImages;
                            using (MemoryStream stream = new MemoryStream())
                            {
                                BinaryFormatter oBFormatter = new BinaryFormatter();
                                oBFormatter.Serialize(stream, person);
                                oSerializedFpImages = stream.ToArray();
                            }

                            cmd.CommandText = "INSERT INTO fingerprint(person_id,image,name) VALUES(@person_id,@image,@name)";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                            cmd.Parameters.AddWithValue("@image", oSerializedFpImages);
                            cmd.Parameters.AddWithValue("@name", person.Name);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        //store fingerptint templates
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedFpImages;
                            //first remove the fingerprint images from MyPerson object
                            removeFingerptintImages(person);

                            using (MemoryStream stream = new MemoryStream())
                            {
                                BinaryFormatter oBFormatter = new BinaryFormatter();
                                oBFormatter.Serialize(stream, person);
                                oSerializedFpImages = stream.ToArray();
                            }
                            
                            cmd.CommandText = "INSERT INTO fp_template(person_id,template,name) VALUES(@person_id,@template,@name)";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                            cmd.Parameters.AddWithValue("@template", oSerializedFpImages);
                            cmd.Parameters.AddWithValue("@name", person.Name);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();

                        //Successful status
                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("Enrollment of " + person.Name + " (Id = " + person.PersonId + ") completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("Enrollment of " + person.Name + " (Id = " + person.PersonId + ") is unsuccessful. Reason is - " + ex.Message + ".");
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }//end transaction
            }//end Connection

            return status;
        }//end storePersonDetailwithFingerprints

        public Status storeAccessControl(AccessControl accessCntrl)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO access_cntrl(role, access_login_tab, access_enroll_tab, access_match_tab, access_usermgmt_tab, " +
                                  "access_audit_tab, access_find_tab, access_data_import, access_data_export, access_multi_match, access_client_setup) VALUES " + 
                                  "(@role,@access_login_tab,@access_enroll_tab,@access_match_tab,@access_usermgmt_tab,@access_audit_tab, " +
                                  "@access_find_tab,@access_data_import,@access_data_export,@access_multi_match,@access_client_setup)";

                cmd.Parameters.AddWithValue("@role", accessCntrl.getRole());
                cmd.Parameters.AddWithValue("@access_login_tab", accessCntrl.getAccessLoginTab());
                cmd.Parameters.AddWithValue("@access_enroll_tab", accessCntrl.getAccessEnrollTab());
                cmd.Parameters.AddWithValue("@access_match_tab", accessCntrl.getAccessMatchTab());
                cmd.Parameters.AddWithValue("@access_usermgmt_tab", accessCntrl.getAccessUserMgmtTab());
                cmd.Parameters.AddWithValue("@access_audit_tab", accessCntrl.getAccessAuditTab());
                cmd.Parameters.AddWithValue("@access_find_tab", accessCntrl.getAccessFindTab());
                cmd.Parameters.AddWithValue("@access_data_import", accessCntrl.getAccessDataImport());
                cmd.Parameters.AddWithValue("@access_data_export", accessCntrl.getAccessDataExport());
                cmd.Parameters.AddWithValue("@access_multi_match", accessCntrl.getAccessMultiMatch());
                cmd.Parameters.AddWithValue("@access_client_setup", accessCntrl.getAccessClientSetup());

                cmd.ExecuteNonQuery();
                Console.WriteLine("###-->> Access Contrl created successfully.....");

                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Access Control (" + accessCntrl.getRole() + ") is created successfully.");
            }
            catch (Exception exp)
            {
                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Failed to create Access Control (" + accessCntrl.getRole() + "). Reason is - " + exp.Message + ".");
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }//end storeFingerprints

        public Status storeCriminalRecord(CriminalRecord criminalRec)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            byte[] oSerializedCrimeDetail;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO criminal_rec(person_id, crime_detail, crime_date, crime_location, court, " +
                                  "statute, court_addr, case_id, sentenced_date, release_date, arrest_date, arrest_agency, " +
                                  "status, parole_date, criminal_alert_level, criminal_alert_msg, ref_doc_loc, created_by, creation_date, " +
                                  "updated_by, update_date) VALUES " +
                                  "(@person_id, @crime_detail, @crime_date, @crime_location, @court, " +
                                  "@statute, @court_addr, @case_id, @sentenced_date, @release_date, @arrest_date, @arrest_agency, " +
                                  "@status, @parole_date, @criminal_alert_level, @criminal_alert_msg, @ref_doc_loc, @created_by, @creation_date, " +
                                  "@updated_by, @update_date)";

                Console.WriteLine("###-->> criminalRec.PersonId = " + criminalRec.PersonId);
                Console.WriteLine("###-->> criminalRec.CrimeDate = " + criminalRec.CrimeDate);
                Console.WriteLine("###-->> criminalRec.CrimeLocation = " + criminalRec.CrimeLocation);
                Console.WriteLine("###-->> criminalRec.Court = " + criminalRec.Court);
                Console.WriteLine("###-->> criminalRec.Statute = " + criminalRec.Statute);
                Console.WriteLine("###-->> criminalRec.CourtyAddress = " + criminalRec.CourtAddress);
                Console.WriteLine("###-->> criminalRec.CaseNbr = " + criminalRec.CaseId);
                Console.WriteLine("###-->> criminalRec.SentencedDate = " + criminalRec.SentencedDate);
                Console.WriteLine("###-->> criminalRec.ReleaseDate = " + criminalRec.ReleaseDate);
                Console.WriteLine("###-->> criminalRec.ArrestDate = " + criminalRec.ArrestDate);
                Console.WriteLine("###-->> criminalRec.ArrestAgency = " + criminalRec.ArrestAgency);
                Console.WriteLine("###-->> criminalRec.Status = " + criminalRec.Status);
                Console.WriteLine("###-->> criminalRec.ParoleDate = " + criminalRec.ParoleDate);
                Console.WriteLine("###-->> criminalRec.CriminalAlertLevel = " + criminalRec.CriminalAlertLevel);
                Console.WriteLine("###-->> criminalRec.CriminalAlertMsg = " + criminalRec.CriminalAlertMsg);
                Console.WriteLine("###-->> criminalRec.RefDocLocation = " + criminalRec.RefDocLocation);
                Console.WriteLine("###-->> criminalRec.CreatedBy = " + criminalRec.CreatedBy);
                Console.WriteLine("###-->> criminalRec.CreationDateTime = " + criminalRec.CreationDateTime);
                Console.WriteLine("###-->> criminalRec.UpdatedBy = " + criminalRec.UpdatedBy);
                Console.WriteLine("###-->> criminalRec.UpdateDateTime = " + criminalRec.UpdateDateTime);
                Console.WriteLine("###-->> criminalRec.CrimeDetail = " + criminalRec.CrimeDetail);


                cmd.Parameters.AddWithValue("@person_id", criminalRec.PersonId);
                cmd.Parameters.AddWithValue("@crime_date", criminalRec.CrimeDate);
                cmd.Parameters.AddWithValue("@crime_location", criminalRec.CrimeLocation);
                cmd.Parameters.AddWithValue("@court", criminalRec.Court);
                cmd.Parameters.AddWithValue("@statute", criminalRec.Statute);
                cmd.Parameters.AddWithValue("@court_addr", criminalRec.CourtAddress);
                cmd.Parameters.AddWithValue("@case_id", criminalRec.CaseId);
                cmd.Parameters.AddWithValue("@sentenced_date", criminalRec.SentencedDate);
                cmd.Parameters.AddWithValue("@release_date", criminalRec.ReleaseDate);
                cmd.Parameters.AddWithValue("@arrest_date", criminalRec.ArrestDate);
                cmd.Parameters.AddWithValue("@arrest_agency", criminalRec.ArrestAgency);
                cmd.Parameters.AddWithValue("@status", criminalRec.Status);
                cmd.Parameters.AddWithValue("@parole_date", criminalRec.ParoleDate);
                cmd.Parameters.AddWithValue("@criminal_alert_level", criminalRec.CriminalAlertLevel);
                cmd.Parameters.AddWithValue("@criminal_alert_msg", criminalRec.CriminalAlertMsg);
                cmd.Parameters.AddWithValue("@ref_doc_loc", criminalRec.RefDocLocation);
                cmd.Parameters.AddWithValue("@created_by", criminalRec.CreatedBy);
                cmd.Parameters.AddWithValue("@creation_date", criminalRec.CreationDateTime);
                cmd.Parameters.AddWithValue("@updated_by", criminalRec.UpdatedBy);
                cmd.Parameters.AddWithValue("@update_date", criminalRec.UpdateDateTime);

                if (criminalRec.CrimeDetail != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, criminalRec.CrimeDetail);
                        oSerializedCrimeDetail = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@crime_detail", oSerializedCrimeDetail);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@crime_detail", null);
                }
                cmd.ExecuteNonQuery();

                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Criminal Record (Case Id = " + criminalRec.CaseId + ") is created successfully.");
            }
            catch (Exception exp)
            {
                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Failed to create Criminal Record (Case Id = " + criminalRec.CaseId + "). Reason is - " + exp.Message + ".");
                Console.WriteLine("###--->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }//end storeCriminalRecord

        public Status storeClientSetup(ClientSetup clientSetup)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            byte[] oSerializedCompanyLogo;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO client_setup(client_id, legal_name, addr_line, city, state, postal_code, country, refresh_intrvl, company_logo, created_by, creation_date, updated_by, update_date) VALUES " +
                                  "(@client_id, @legal_name, @addr_line, @city, @state, @postal_code, @country, @refresh_intrvl, @company_logo, @created_by, @creation_date, @updated_by, @update_date)";

                cmd.Parameters.AddWithValue("@client_id", clientSetup.ClientId);
                cmd.Parameters.AddWithValue("@legal_name", clientSetup.LegalName);
                cmd.Parameters.AddWithValue("@addr_line", clientSetup.AddressLine);
                cmd.Parameters.AddWithValue("@city", clientSetup.City);
                cmd.Parameters.AddWithValue("@state", clientSetup.State);
                cmd.Parameters.AddWithValue("@postal_code", clientSetup.PostalCode);
                cmd.Parameters.AddWithValue("@country", clientSetup.Country);
                cmd.Parameters.AddWithValue("@refresh_intrvl", clientSetup.DataRefreshInterval);

                if (clientSetup.CompanyLogo != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, clientSetup.CompanyLogo);
                        oSerializedCompanyLogo = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@company_logo", oSerializedCompanyLogo);
                }
                else {
                    cmd.Parameters.AddWithValue("@company_logo", null);
                }

                cmd.Parameters.AddWithValue("@created_by", clientSetup.CreatedBy);
                cmd.Parameters.AddWithValue("@creation_date", clientSetup.CreationDateTime);
                cmd.Parameters.AddWithValue("@updated_by", clientSetup.UpdatedBy);
                cmd.Parameters.AddWithValue("@update_date", clientSetup.UpdateDateTime);

                cmd.ExecuteNonQuery();

                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Client Setup Record (Client Id = " + clientSetup.ClientId + ") is created successfully.");
            }
            catch (Exception exp)
            {
                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Failed to create Client Setup Record (Case Id = " + clientSetup.ClientId + "). Reason is - " + exp.Message + ".");
                Console.WriteLine("###--->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }//end storeCriminalRecord


        public List<AccessControl> getAccessControls()
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<AccessControl> accessCntrls;
            DataTable ds;
            conn.Open();
            try
            {
                accessCntrls = new List<AccessControl>();
                cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT * FROM afis.access_cntrl";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    AccessControl accessCntrl = new AccessControl();
                    string role = (string)ds.Rows[i]["role"];
                    Console.WriteLine("###-->># of matched role = " + role);
                    string access_login_tab = (string)ds.Rows[i]["access_login_tab"];
                    string access_enroll_tab = (string)ds.Rows[i]["access_enroll_tab"];
                    string access_match_tab = (string)ds.Rows[i]["access_match_tab"];
                    string access_usermgmt_tab = (string)ds.Rows[i]["access_usermgmt_tab"];
                    string access_audit_tab = (string)ds.Rows[i]["access_audit_tab"];
                    string access_find_tab = (string)ds.Rows[i]["access_find_tab"];
                    string access_data_import = (string)ds.Rows[i]["access_data_import"];
                    string access_data_export = (string)ds.Rows[i]["access_data_export"];
                    string access_multi_match = (string)ds.Rows[i]["access_multi_match"];

                    accessCntrl.setRole(role);
                    accessCntrl.setAccessLoginTab(access_login_tab);
                    accessCntrl.setAccessEnrollTab(access_enroll_tab);
                    accessCntrl.setAccessMatchTab(access_match_tab);
                    accessCntrl.setAccessUserMgmtTab(access_usermgmt_tab);
                    accessCntrl.setAccessAuditTab(access_audit_tab);
                    accessCntrl.setAccessFindTab(access_find_tab);
                    accessCntrl.setAccessDataImport(access_data_import);
                    accessCntrl.setAccessDataExport(access_data_export);
                    accessCntrl.setAccessMultiMatch(access_multi_match);
                    accessCntrls.Add(accessCntrl);

                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return accessCntrls;
        }//end getAccessControls

        public ClientSetup getClientSetup()
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            ClientSetup clientSetup = null;
            DataTable ds;
            System.Drawing.Image companyLogo = null;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT * FROM afis.client_setup";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);

                if(ds.Rows.Count > 0)
                {
                    IEnumerator rows = ds.Rows.GetEnumerator();
                    string client_id = (string)ds.Rows[0]["client_id"];
                    string legal_name = (string)ds.Rows[0]["legal_name"];
                    string addr_line = (string)ds.Rows[0]["addr_line"];
                    string city = (string)ds.Rows[0]["city"];
                    string state = (string)ds.Rows[0]["state"];
                    string postal_code = (string)ds.Rows[0]["postal_code"];
                    string country = (string)ds.Rows[0]["country"];
                    int refresh_intrvl = (int)ds.Rows[0]["refresh_intrvl"];

                    using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[0]["company_logo"]))
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oStr.Position = 0;
                        companyLogo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                    }


                    clientSetup = new ClientSetup();
                    clientSetup.ClientId = client_id;
                    clientSetup.LegalName = legal_name;
                    clientSetup.AddressLine = addr_line;
                    clientSetup.City = city;
                    clientSetup.State = state;
                    clientSetup.PostalCode = postal_code;
                    clientSetup.Country = country;
                    clientSetup.DataRefreshInterval = refresh_intrvl;
                    clientSetup.CompanyLogo = companyLogo;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return clientSetup;
        }//end getClientSetup

        public CriminalRecord getCriminalRecord(string personId, string caseId)
        {
            CriminalRecord criminalRecord = null;

            List <CriminalRecord> criminalRecs = getCriminalRecords(personId);
            if(criminalRecs != null)
            {
                foreach (CriminalRecord criminalRec in criminalRecs)
                {
                    if (criminalRec.CaseId == caseId)
                    {
                        criminalRecord = criminalRec;
                    }
                }
            }

            return criminalRecord;
        }


        public List<CriminalRecord> getCriminalRecords(string personId)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<CriminalRecord> criminalRecs = null;
            DataTable ds;
            conn.Open();
            try
            {
                criminalRecs = new List<CriminalRecord>();
                cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT * FROM afis.criminal_rec WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    CriminalRecord criminalRec = new CriminalRecord();
                    Console.WriteLine("###-->> person_id = " + ds.Rows[i]["person_id"]);
                    criminalRec.PersonId = (string)ds.Rows[i]["person_id"];
                    Console.WriteLine("###-->> crime_date = " + ds.Rows[i]["crime_date"]);
                    criminalRec.CrimeDate = (DateTime)ds.Rows[i]["crime_date"];
                    Console.WriteLine("###-->> crime_location = " + ds.Rows[i]["crime_location"]);
                    criminalRec.CrimeLocation = (string)ds.Rows[i]["crime_location"];
                    Console.WriteLine("###-->> case_id = " + ds.Rows[i]["case_id"]);
                    criminalRec.CaseId = (string)ds.Rows[i]["case_id"];
                    Console.WriteLine("###-->> court = " + ds.Rows[i]["court"]);
                    criminalRec.Court = ds.Rows[i]["court"] != null ? (string)ds.Rows[i]["court"] : null;
                    Console.WriteLine("###-->> court_addr = " + ds.Rows[i]["court_addr"]);
                    criminalRec.CourtAddress = (string)ds.Rows[i]["court_addr"];
                    Console.WriteLine("###-->> statute = " + ds.Rows[i]["statute"]);
                    criminalRec.Statute = (string)ds.Rows[i]["statute"];
                    Console.WriteLine("###-->> court_addr = " + ds.Rows[i]["court_addr"]);
                    criminalRec.CourtAddress = (string)ds.Rows[i]["court_addr"];
                    Console.WriteLine("###-->> arrest_date = " + ds.Rows[i]["arrest_date"]);
                    criminalRec.ArrestDate = (DateTime)ds.Rows[i]["arrest_date"];
                    Console.WriteLine("###-->> arrest_agency = " + ds.Rows[i]["arrest_agency"]);
                    criminalRec.ArrestAgency = (string)ds.Rows[i]["arrest_agency"];
                    Console.WriteLine("###-->> sentenced_date = " + ds.Rows[i]["sentenced_date"]);
                    criminalRec.SentencedDate = (DateTime)ds.Rows[i]["sentenced_date"];
                    Console.WriteLine("###-->> release_date = " + ds.Rows[i]["release_date"]);
                    criminalRec.ReleaseDate = (DateTime)ds.Rows[i]["release_date"];
                    Console.WriteLine("###-->> parole_date = " + ds.Rows[i]["parole_date"]);
                    criminalRec.ParoleDate = (DateTime)ds.Rows[i]["parole_date"];
                    Console.WriteLine("###-->> status = " + ds.Rows[i]["status"]);
                    criminalRec.Status = (string)ds.Rows[i]["status"];
                    Console.WriteLine("###-->> criminal_alert_level = " + ds.Rows[i]["criminal_alert_level"]);
                    criminalRec.CriminalAlertLevel = (string)ds.Rows[i]["criminal_alert_level"];
                    Console.WriteLine("###-->> criminal_alert_msg = " + ds.Rows[i]["criminal_alert_msg"]);
                    criminalRec.CriminalAlertMsg = (string)ds.Rows[i]["criminal_alert_msg"];
                    criminalRec.RefDocLocation = !string.IsNullOrEmpty((string)ds.Rows[i]["ref_doc_loc"]) ? (string)ds.Rows[i]["ref_doc_loc"] : "";

                    if (ds.Rows[i]["crime_detail"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["crime_detail"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            string crime_detail = (string)oBFormatter.Deserialize(oStr);
                            criminalRec.CrimeDetail = crime_detail;
                        }
                    }


                    criminalRecs.Add(criminalRec);

                    i = i + 1;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return criminalRecs;
        }//end getCriminalRecords


        public Status storePersonPhysicalCharacteristics(PersonPhysicalChar personPhysicalChar)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO physical_char(person_id, height, weight, eye_color, hair_color, complexion, " +
                                  "birth_mark, id_mark, build_type, gender, death_date, created_by, creation_date, updated_by, updated_date) VALUES " +
                                  "(@person_id, @height, @weight, @eye_color, @hair_color, @complexion, " +
                                  "@birth_mark, @id_mark, @build_type, @gender, @death_date, @created_by, @creation_date, @updated_by, @updated_date)";

                cmd.Parameters.AddWithValue("@person_id", personPhysicalChar.PersonId);
                cmd.Parameters.AddWithValue("@height", personPhysicalChar.Height);
                cmd.Parameters.AddWithValue("@weight", personPhysicalChar.Weight);
                cmd.Parameters.AddWithValue("@eye_color", personPhysicalChar.EyeColor);
                cmd.Parameters.AddWithValue("@hair_color", personPhysicalChar.HairColor); 
                cmd.Parameters.AddWithValue("@complexion", personPhysicalChar.Complexion);
                cmd.Parameters.AddWithValue("@birth_mark", personPhysicalChar.BirthMark);
                cmd.Parameters.AddWithValue("@id_mark", personPhysicalChar.IdMark);
                cmd.Parameters.AddWithValue("@build_type", personPhysicalChar.BuildType);
                cmd.Parameters.AddWithValue("@gender", personPhysicalChar.Gender);
                cmd.Parameters.AddWithValue("@death_date", personPhysicalChar.DOD);
                cmd.Parameters.AddWithValue("@created_by", personPhysicalChar.CreatedBy);
                cmd.Parameters.AddWithValue("@creation_date", personPhysicalChar.CreationDateTime);
                cmd.Parameters.AddWithValue("@updated_by", personPhysicalChar.UpdatedBy);
                cmd.Parameters.AddWithValue("@updated_date", personPhysicalChar.UpdateDateTime);                
 
                cmd.ExecuteNonQuery();
                Console.WriteLine("###-->> Physical Characteristics created successfully.....");

                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Physical Characteristics (Person Id = " + personPhysicalChar.PersonId + ") is created successfully.");
            }
            catch (MySqlException exp)
            {
                Console.WriteLine("###-->> Exception = " + exp.StackTrace);
                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Failed to store Physical Characteristics (Person Id = " + personPhysicalChar.PersonId + "). Reason is - " + exp.Message + ".");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }//end storeFingerprints


        //Remove fingerprint images from Person
        private static void removeFingerptintImages(MyPerson person)
        {
            for (int i = 0; i < person.Fingerprints.Count; i++)
            {
                MyFingerprint fp = (MyFingerprint)person.Fingerprints.ElementAt(i);
                fp.Image = null;
            }
        }//end removeFingerptintImages


        public Status updatePersonDetailWithFingerprints(MyPerson person, PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            Status status = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //update person detail
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedPassportPhoto;
                            cmd.CommandText = "UPDATE person SET fname = @fname, lname = @lname, mname = @mname, name_prefix = @name_prefix, name_suffix = @name_suffix ," +
                                              "DOB = @DOB, addr_street = @addr_street, addr_city = @addr_city, addr_postal_code = @addr_postal_code, addr_state = @addr_state, addr_country = @addr_country, profession = @profession," +
                                              "father_name = @father_name, cell_nbr = @cell_nbr, home_phone = @home_phone, office_phone = @office_phone, email_addr = @email_addr, photo = @photo WHERE person_id = @person_id";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                            cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                            cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                            cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                            cmd.Parameters.AddWithValue("@name_Prefix", personDetail.getPrefix());
                            cmd.Parameters.AddWithValue("@name_suffix", personDetail.getSuffix());
                            cmd.Parameters.AddWithValue("@DOB", personDetail.getDOB());
                            cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                            cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                            cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                            cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                            cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                            cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());
                            cmd.Parameters.AddWithValue("@father_name", personDetail.getFatherName());
                            cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                            cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                            cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                            cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());

                            if (personDetail.getPassportPhoto() != null)
                            {
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    BinaryFormatter oBFormatter = new BinaryFormatter();
                                    oBFormatter.Serialize(stream, personDetail.getPassportPhoto());
                                    oSerializedPassportPhoto = stream.ToArray();
                                }
                                cmd.Parameters.AddWithValue("@photo", oSerializedPassportPhoto);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@photo", null);
                            }

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        //update person's finperprints
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedFpImages;
                            using (MemoryStream stream = new MemoryStream())
                            {
                                BinaryFormatter oBFormatter = new BinaryFormatter();
                                oBFormatter.Serialize(stream, person);
                                oSerializedFpImages = stream.ToArray();
                            }

                            cmd.CommandText = "UPDATE fingerprint SET image = @image, name = @name WHERE person_id = @person_id";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                            cmd.Parameters.AddWithValue("@image", oSerializedFpImages);
                            cmd.Parameters.AddWithValue("@name", person.Name);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        //update fingerptint templates
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            byte[] oSerializedFpImages;
                            //first remove the fingerprint images from MyPerson object
                            removeFingerptintImages(person);

                            using (MemoryStream stream = new MemoryStream())
                            {
                                BinaryFormatter oBFormatter = new BinaryFormatter();
                                oBFormatter.Serialize(stream, person);
                                oSerializedFpImages = stream.ToArray();
                            }
                        
                            cmd.CommandText = "UPDATE fp_template SET template = @template, name = @name WHERE person_id = @person_id";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                            cmd.Parameters.AddWithValue("@template", oSerializedFpImages);
                            cmd.Parameters.AddWithValue("@name", person.Name);

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();

                        //Successful status
                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("Enrollment update of " + person.Name + " (Id = " + person.PersonId + ") completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("Enrollment update of " + person.Name + " (Id = " + person.PersonId + ") is unsuccessful. Reason is - " + ex.Message + ".");
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }//end transaction
            }//end Connection

            return status;

        }//end updatePersonDetailWithFingerprints

/*
        public void updateFingerprints(MyPerson person)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            byte[] oSerializedFpImages;
            conn.Open();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, person);
                    oSerializedFpImages = stream.ToArray();
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE fingerprint SET image = @image, name = @name WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                cmd.Parameters.AddWithValue("@image", oSerializedFpImages);
                cmd.Parameters.AddWithValue("@name", person.Name);
                cmd.ExecuteNonQuery();
                Console.WriteLine("####-->> PersonId = " + person.PersonId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }//end updateFingerprints


        public void updateFingerprintTemplates(MyPerson person)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            byte[] oSerializedFpImages;
            conn.Open();
            try
            {
                ////first remove the fingerprint images from MyPerson object
                removeFingerptintImages(person);

                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, person);
                    oSerializedFpImages = stream.ToArray();
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE fp_template SET template = @template, name = @name WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                cmd.Parameters.AddWithValue("@template", oSerializedFpImages);
                cmd.Parameters.AddWithValue("@name", person.Name);
                cmd.ExecuteNonQuery();
                Console.WriteLine("####-->> PersonId = " + person.PersonId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }//end updateFingerprintTemplates
*/

/*
        public void storePersonDetail(PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            byte[] oSerializedPassportPhoto;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO person(person_id, fname, lname, mname, name_prefix, name_suffix," +
                                  "DOB, addr_street, addr_city, addr_postal_code, addr_state, addr_country, profession," +
                                  "father_name, cell_nbr, home_phone, office_phone, email_addr, photo)" +
                                  "VALUES(@person_id, @fname, @lname, @mname, @name_prefix, @name_suffix," +
                                  "@DOB, @addr_street, @addr_city, @addr_postal_code, @addr_state, @addr_country, @profession," +
                                  "@father_name, @cell_nbr, @home_phone, @office_phone, @email_addr, @photo)";

                cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                cmd.Parameters.AddWithValue("@name_Prefix", personDetail.getPrefix());
                cmd.Parameters.AddWithValue("@name_suffix", personDetail.getSuffix());
                cmd.Parameters.AddWithValue("@DOB", personDetail.getDOB());
                cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());
                cmd.Parameters.AddWithValue("@father_name", personDetail.getFatherName());
                cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());

                if (personDetail.getPassportPhoto() != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, personDetail.getPassportPhoto());
                        oSerializedPassportPhoto = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@photo", oSerializedPassportPhoto);
                } else {
                    cmd.Parameters.AddWithValue("@photo", null);
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }//end storePersonDetail
*/
/*
        public void updatePersonDetail(PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            byte[] oSerializedPassportPhoto;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE person SET fname = @fname, lname = @lname, mname = @mname, name_prefix = @name_prefix, name_suffix = @name_suffix ," +
                                  "DOB = @DOB, addr_street = @addr_street, addr_city = @addr_city, addr_postal_code = @addr_postal_code, addr_state = @addr_state, addr_country = @addr_country, profession = @profession," +
                                  "father_name = @father_name, cell_nbr = @cell_nbr, home_phone = @home_phone, office_phone = @office_phone, email_addr = @email_addr, photo = @photo WHERE person_id = @person_id";
    
                cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                cmd.Parameters.AddWithValue("@name_Prefix", personDetail.getPrefix());
                cmd.Parameters.AddWithValue("@name_suffix", personDetail.getSuffix());
                cmd.Parameters.AddWithValue("@DOB", personDetail.getDOB());
                cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());
                cmd.Parameters.AddWithValue("@father_name", personDetail.getFatherName());
                cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());

                if(personDetail.getPassportPhoto() != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, personDetail.getPassportPhoto());
                        oSerializedPassportPhoto = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@photo", oSerializedPassportPhoto);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@photo", null);
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }//end storePersonDetail
*/

        public List<PersonDetail> retrievePersonDetail(string personId)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<PersonDetail> personsDetail;
            DataTable ds;
            conn.Open();
            try
            {
                personsDetail = new List<PersonDetail>();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.person p where p.person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    PersonDetail pDetail = new PersonDetail();
                    string pId = (string)ds.Rows[i]["person_id"];
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string mname = (string)ds.Rows[i]["mname"];
                    string name_prefix = (string)ds.Rows[i]["name_prefix"];
                    string name_suffix = (string)ds.Rows[i]["name_suffix"];


                    //Handle nullable DateTime
                    DateTime? dob;

                    if (ds.Rows[i]["DOB"] == DBNull.Value)
                    {
                        dob = new DateTime(1753, 01, 01);
                    }
                    else
                    {
                        dob = (DateTime?)ds.Rows[i]["DOB"];
                    }

                    string addr_street = (string)ds.Rows[i]["addr_street"];
                    string addr_city = (string)ds.Rows[i]["addr_city"];
                    string addr_postal_code = (string)ds.Rows[i]["addr_postal_code"];
                    string addr_state = (string)ds.Rows[i]["addr_state"];
                    string addr_country = (string)ds.Rows[i]["addr_country"];
                    string profession = (string)ds.Rows[i]["profession"];
                    string father_name = (string)ds.Rows[i]["father_name"];
                    string cell_nbr = (string)ds.Rows[i]["cell_nbr"];
                    string home_phone = (string)ds.Rows[i]["home_phone"];
                    string office_phone = (string)ds.Rows[i]["office_phone"];
                    string email_addr = (string)ds.Rows[i]["email_addr"];

                    System.Drawing.Image photo = null;

                    if(ds.Rows[i]["photo"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["photo"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            photo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                        }
                    }

                    pDetail.setPersonId(pId);
                    pDetail.setFirstName(fname);
                    pDetail.setLastName(lname);
                    pDetail.setMiddleName(mname);
                    pDetail.setPrefix(name_prefix);
                    pDetail.setSuffix(name_suffix);
                    pDetail.setDOB(dob);
                    pDetail.setStreetAddress(addr_street);
                    pDetail.setCity(addr_city);
                    pDetail.setPostalCode(addr_postal_code);
                    pDetail.setState(addr_state);
                    pDetail.setCountry(addr_country);
                    pDetail.setProfession(profession);
                    pDetail.setFatherName(father_name);
                    pDetail.setcellNbr(cell_nbr);
                    pDetail.setHomwPhoneNbr(home_phone);
                    pDetail.setWorkPhoneNbr(office_phone);
                    pDetail.setEmail(email_addr);
                    pDetail.setPassportPhoto(photo);
                    personsDetail.Add(pDetail);

                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return personsDetail;
        }

        public PersonPhysicalChar retrievePersonPhysicalCharacteristics(string personId)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            PersonPhysicalChar personsPhysicalChar = null;
            DataTable ds;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.physical_char p where p.person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);

                IEnumerator rows = ds.Rows.GetEnumerator();
                
                if(rows.MoveNext())
                {
                    personsPhysicalChar = new PersonPhysicalChar();
                    personsPhysicalChar.PersonId = (string)ds.Rows[0]["person_id"];
                    personsPhysicalChar.Height = (double)ds.Rows[0]["height"];
                    personsPhysicalChar.Weight = (double)ds.Rows[0]["weight"];
                    personsPhysicalChar.EyeColor = (string)ds.Rows[0]["eye_color"];
                    personsPhysicalChar.HairColor = (string)ds.Rows[0]["hair_color"];
                    personsPhysicalChar.Complexion = (string)ds.Rows[0]["complexion"];
                    personsPhysicalChar.BirthMark = (string)ds.Rows[0]["birth_mark"];
                    personsPhysicalChar.IdMark = (string)ds.Rows[0]["id_mark"];
                    personsPhysicalChar.BuildType = (string)ds.Rows[0]["build_type"];
                    personsPhysicalChar.Gender = (string)ds.Rows[0]["gender"];
                    personsPhysicalChar.DOD = (DateTime)ds.Rows[0]["death_date"];
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.StackTrace);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return personsPhysicalChar;
        }//end retrievePersonPhysicalCharacteristics


        public List<PersonDetail> findPersons(PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<PersonDetail> personsDetail;
            DataTable ds;
            conn.Open();
            try
            {
                personsDetail = new List<PersonDetail>();
                cmd = conn.CreateCommand();


                cmd.CommandText = "SELECT * FROM afis.person p WHERE " +
                                  "p.fname like '%@fname%' AND " +
                                  "p.lname like '%@lname%' AND " +
                                  "p.mname like '%@mname%' AND " +
                                  "p.name_prefix like '%@name_prefix%' AND " +
                                  "p.addr_street like '%@addr_street%' AND " +
                                  "p.addr_city like '%@addr_city%' AND " +
                                  "p.addr_state like '%@addr_state%' AND " +
                                  "p.addr_postal_code like '%@addr_postal_code%' AND " +
                                  "p.addr_country like '%@addr_country%' AND " +
                                  "p.cell_nbr like '%@cell_nbr%' AND " +
                                  "p.home_phone like '%@home_phone%' AND " +
                                  "p.office_phone like '%@office_phone%' AND " +
                                  "p.email_addr like '%@email_addr%' AND " +
                                  "p.profession like '%@profession%' AND " +
                                  "p.DOB like '%@DOB%' limit 100";

                
                cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                cmd.Parameters.AddWithValue("@name_prefix", personDetail.getPrefix());
                cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());
                cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());


                string dobStr = "";
                if (!string.IsNullOrWhiteSpace(personDetail.getDOBText()))
                {
                    DateTime dateTime = DateTime.Parse(personDetail.getDOBText());
                    dobStr = dateTime.ToString("yyyy-MM-dd");
                }
                cmd.Parameters.AddWithValue("@DOB", dobStr);


                string query = cmd.CommandText;
                foreach (MySql.Data.MySqlClient.MySqlParameter p in cmd.Parameters)
                {
                    Console.WriteLine("###-->> ParameterName = [" + p.ParameterName + "]");

                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }

                cmd.CommandText = query;
                Console.WriteLine("###-->>cmd.CommandText = " + cmd.CommandText);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    PersonDetail pDetail = new PersonDetail();
                    string pId = (string)ds.Rows[i]["person_id"];
                    Console.WriteLine("###-->># of matched person Id = " + pId);
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string mname = (string)ds.Rows[i]["mname"];
                    string name_prefix = (string)ds.Rows[i]["name_prefix"];
                    string name_suffix = (string)ds.Rows[i]["name_suffix"];


                    //Handle nullable DateTime
                    DateTime? dob;

                    if (ds.Rows[i]["DOB"] == DBNull.Value)
                    {
                        dob = new DateTime(1753, 01, 01);
                    }
                    else
                    {
                        dob = (DateTime?)ds.Rows[i]["DOB"];
                    }

                    string addr_street = (string)ds.Rows[i]["addr_street"];
                    string addr_city = (string)ds.Rows[i]["addr_city"];
                    string addr_postal_code = (string)ds.Rows[i]["addr_postal_code"];
                    string addr_state = (string)ds.Rows[i]["addr_state"];
                    string addr_country = (string)ds.Rows[i]["addr_country"];
                    string profession = (string)ds.Rows[i]["profession"];
                    string father_name = (string)ds.Rows[i]["father_name"];
                    string cell_nbr = (string)ds.Rows[i]["cell_nbr"];
                    string home_phone = (string)ds.Rows[i]["home_phone"];
                    string office_phone = (string)ds.Rows[i]["office_phone"];
                    string email_addr = (string)ds.Rows[i]["email_addr"];

                    System.Drawing.Image photo = null;

                    if (ds.Rows[i]["photo"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["photo"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            photo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                        }
                    }

                    pDetail.setPersonId(pId);
                    pDetail.setFirstName(fname);
                    pDetail.setLastName(lname);
                    pDetail.setMiddleName(mname);
                    pDetail.setPrefix(name_prefix);
                    pDetail.setSuffix(name_suffix);
                    pDetail.setDOB(dob);
                    pDetail.setStreetAddress(addr_street);
                    pDetail.setCity(addr_city);
                    pDetail.setPostalCode(addr_postal_code);
                    pDetail.setState(addr_state);
                    pDetail.setCountry(addr_country);
                    pDetail.setProfession(profession);
                    pDetail.setFatherName(father_name);
                    pDetail.setcellNbr(cell_nbr);
                    pDetail.setHomwPhoneNbr(home_phone);
                    pDetail.setWorkPhoneNbr(office_phone);
                    pDetail.setEmail(email_addr);
                    pDetail.setPassportPhoto(photo);
                    personsDetail.Add(pDetail);

                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return personsDetail;
        }//end findPersons

        public List<PersonDetail> findPersonsToExport(PersonDetail personDetail)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<PersonDetail> personsDetail;
            DataTable ds;
            conn.Open();
            try
            {
                personsDetail = new List<PersonDetail>();
                cmd = conn.CreateCommand();


                cmd.CommandText = "SELECT * FROM afis.person p WHERE " +
                                  "p.person_id like '%@person_id%' AND " +
                                  "p.fname like '%@fname%' AND " +
                                  "p.lname like '%@lname%' AND " +
                                  "p.mname like '%@mname%' AND " +
                                  "p.name_prefix like '%@name_prefix%' AND " +
                                  "p.addr_street like '%@addr_street%' AND " +
                                  "p.addr_city like '%@addr_city%' AND " +
                                  "p.addr_state like '%@addr_state%' AND " +
                                  "p.addr_postal_code like '%@addr_postal_code%' AND " +
                                  "p.addr_country like '%@addr_country%' AND " +
                                  "p.cell_nbr like '%@cell_nbr%' AND " +
                                  "p.home_phone like '%@home_phone%' AND " +
                                  "p.office_phone like '%@office_phone%' AND " +
                                  "p.email_addr like '%@email_addr%' AND " +
                                  "p.profession like '%@profession%' AND " +
                                  "p.father_name like '%@father_name%' AND " +
                                  "p.DOB like '%@DOB%' limit 100";

                cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                cmd.Parameters.AddWithValue("@name_prefix", personDetail.getPrefix());
                cmd.Parameters.AddWithValue("@addr_street", personDetail.getStreetAddress());
                cmd.Parameters.AddWithValue("@addr_city", personDetail.getCity());
                cmd.Parameters.AddWithValue("@addr_state", personDetail.getState());
                cmd.Parameters.AddWithValue("@addr_postal_code", personDetail.getPostalCode());
                cmd.Parameters.AddWithValue("@addr_country", personDetail.getCountry());
                cmd.Parameters.AddWithValue("@cell_nbr", personDetail.getCellNbr());
                cmd.Parameters.AddWithValue("@home_phone", personDetail.getHomePhoneNbr());
                cmd.Parameters.AddWithValue("@office_phone", personDetail.getWorkPhoneNbr());
                cmd.Parameters.AddWithValue("@email_addr", personDetail.getEmail());
                cmd.Parameters.AddWithValue("@profession", personDetail.getProfession());
                cmd.Parameters.AddWithValue("@father_name", personDetail.getFatherName());


                string dobStr = "";
                if (!string.IsNullOrWhiteSpace(personDetail.getDOBText()))
                {
                    DateTime dateTime = DateTime.Parse(personDetail.getDOBText());
                    dobStr = dateTime.ToString("yyyy-MM-dd");
                }
                cmd.Parameters.AddWithValue("@DOB", dobStr);


                string query = cmd.CommandText;
                foreach (MySql.Data.MySqlClient.MySqlParameter p in cmd.Parameters)
                {
                    Console.WriteLine("###-->> ParameterName = [" + p.ParameterName + "]");

                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }

                cmd.CommandText = query;
                Console.WriteLine("###-->>cmd.CommandText = " + cmd.CommandText);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    PersonDetail pDetail = new PersonDetail();
                    string pId = (string)ds.Rows[i]["person_id"];
                    Console.WriteLine("###-->># of matched person Id = " + pId);
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string mname = (string)ds.Rows[i]["mname"];
                    string name_prefix = (string)ds.Rows[i]["name_prefix"];
                    string name_suffix = (string)ds.Rows[i]["name_suffix"];


                    //Handle nullable DateTime
                    DateTime? dob;

                    if (ds.Rows[i]["DOB"] == DBNull.Value)
                    {
                        dob = new DateTime(1753, 01, 01);
                    }
                    else
                    {
                        dob = (DateTime?)ds.Rows[i]["DOB"];
                    }

                    string addr_street = (string)ds.Rows[i]["addr_street"];
                    string addr_city = (string)ds.Rows[i]["addr_city"];
                    string addr_postal_code = (string)ds.Rows[i]["addr_postal_code"];
                    string addr_state = (string)ds.Rows[i]["addr_state"];
                    string addr_country = (string)ds.Rows[i]["addr_country"];
                    string profession = (string)ds.Rows[i]["profession"];
                    string father_name = (string)ds.Rows[i]["father_name"];
                    string cell_nbr = (string)ds.Rows[i]["cell_nbr"];
                    string home_phone = (string)ds.Rows[i]["home_phone"];
                    string office_phone = (string)ds.Rows[i]["office_phone"];
                    string email_addr = (string)ds.Rows[i]["email_addr"];

                    System.Drawing.Image photo = null;

                    if (ds.Rows[i]["photo"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["photo"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            photo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                        }
                    }

                    pDetail.setPersonId(pId);
                    pDetail.setFirstName(fname);
                    pDetail.setLastName(lname);
                    pDetail.setMiddleName(mname);
                    pDetail.setPrefix(name_prefix);
                    pDetail.setSuffix(name_suffix);
                    pDetail.setDOB(dob);
                    pDetail.setStreetAddress(addr_street);
                    pDetail.setCity(addr_city);
                    pDetail.setPostalCode(addr_postal_code);
                    pDetail.setState(addr_state);
                    pDetail.setCountry(addr_country);
                    pDetail.setProfession(profession);
                    pDetail.setFatherName(father_name);
                    pDetail.setcellNbr(cell_nbr);
                    pDetail.setHomwPhoneNbr(home_phone);
                    pDetail.setWorkPhoneNbr(office_phone);
                    pDetail.setEmail(email_addr);
                    pDetail.setPassportPhoto(photo);
                    personsDetail.Add(pDetail);

                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return personsDetail;
        }//end findPersonsToExport

        /*
                public List<MyPerson> retrievePersonFingerprints()
                {
                    string connStr = getConnectionStringByName("MySQL_AFIS_conn");
                    MySqlConnection conn = new MySqlConnection(connStr);
                    MySqlCommand cmd;
                    MyPerson person = null;
                    List<MyPerson> persons;
                    DataTable ds;
                    conn.Open();
                    try
                    {
                        persons = new List<MyPerson>();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT * FROM afis.fingerprint";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        ds = new DataTable();
                        da.Fill(ds);
                        IEnumerator rows = ds.Rows.GetEnumerator();
                        Int32 i = 0;

                        while (rows.MoveNext())
                        {

                            int id = (int)ds.Rows[i]["id"];
                            using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["image"]))
                            {
                                BinaryFormatter oBFormatter = new BinaryFormatter();
                                oStr.Position = 0;
                                person = (MyPerson)oBFormatter.Deserialize(oStr);
                                persons.Add(person);
                            }
                            i = i + 1;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                    return persons;
                }
        */
        public List<MyPerson> retrievePersonFingerprintTemplates()
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            MyPerson person = null;
            List<MyPerson> persons;
            DataTable ds;
            conn.Open();
            try
            {
                persons = new List<MyPerson>();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.fp_template";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;
                Console.WriteLine("###-->> DataAccess:retrievePersonFingerprintTemplates() - Loading fingerprint telplates..");

                while (rows.MoveNext())
                {

                    int id = (int)ds.Rows[i]["id"];
                    using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["template"]))
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oStr.Position = 0;
                        person = (MyPerson)oBFormatter.Deserialize(oStr);
                        persons.Add(person);
                    }
                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return persons;
        }


        public List<MyPerson> retrievePersonFingerprintsById(string personId)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            MyPerson person = null;
            List<MyPerson> persons;
            DataTable ds;
            conn.Open();
            try
            {
                persons = new List<MyPerson>();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.fingerprint p where p.person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {

                    int id = (int)ds.Rows[i]["id"];
                    if(ds.Rows[i]["image"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["image"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            person = (MyPerson)oBFormatter.Deserialize(oStr);
                            persons.Add(person);
                        }
                    }
                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return persons;
        }


        public Status createAFISUser(User user)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            Status status = null;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //Create new User
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO users(person_id,fname,lname, username, password, user_role, station_id, stationed_address, stationed_city, stationed_country, active_status, service_start, service_end) " +
                                              "VALUES(@person_id, @fname, @lname, @username, @password, @user_role, @station_id, @stationed_address, @stationed_city, @stationed_country, @active_status, @service_start, @service_end)";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", user.getPersonId());
                            cmd.Parameters.AddWithValue("@fname", user.getFirstName());
                            cmd.Parameters.AddWithValue("@lname", user.getLastName());
                            cmd.Parameters.AddWithValue("@username", user.getUsername());
                            cmd.Parameters.AddWithValue("@password", Encrypt(user.getPassword()));
                            cmd.Parameters.AddWithValue("@user_role", user.getUserRole());
                            cmd.Parameters.AddWithValue("@station_id", user.getStationId());
                            cmd.Parameters.AddWithValue("@stationed_address", user.getStationedAddress());
                            cmd.Parameters.AddWithValue("@stationed_city", user.getStationedCity());
                            cmd.Parameters.AddWithValue("@stationed_Country", user.getStationedCountry());
                            cmd.Parameters.AddWithValue("@active_status", user.getActiveStatus());
                            cmd.Parameters.AddWithValue("@service_start", user.getServiceStartDate());
                            cmd.Parameters.AddWithValue("@service_end", user.getServiceEndDate());

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        //Create Default Application Config (user preference) for this User
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO app_conf(person_id,dup_check) " +
                                              "VALUES(@person_id, @dup_check)";
                            cmd.Transaction = trans;

                            cmd.Parameters.AddWithValue("@person_id", user.getPersonId());
                            cmd.Parameters.AddWithValue("@dup_check", "N");

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();

                        //Successful status
                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("User created successfully.");

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("User creation unsuccessful. Reason is - " + ex.Message + ".");
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }//end transaction
            }//end Connection

            return status;

        }//end createAFISUser

        /*
                public Status createAFISUserOld(User user)
                {
                    string connStr = getConnectionStringByName("MySQL_AFIS_conn");
                    MySqlConnection conn = new MySqlConnection(connStr);
                    MySqlCommand cmd;
                    Status status = null;
                    conn.Open();
                    try
                    {
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO users(person_id,fname,lname, username, password, user_role, station_id, stationed_address, stationed_city, stationed_country, active_status, service_start, service_end) " +
                                          "VALUES(@person_id, @fname, @lname, @username, @password, @user_role, @station_id, @stationed_address, @stationed_city, @stationed_country, @active_status, @service_start, @service_end)";
                        cmd.Parameters.AddWithValue("@person_id", user.getPersonId());
                        cmd.Parameters.AddWithValue("@fname", user.getFirstName());
                        cmd.Parameters.AddWithValue("@lname", user.getLastName());
                        cmd.Parameters.AddWithValue("@username", user.getUsername());
                        cmd.Parameters.AddWithValue("@password", Encrypt(user.getPassword()));
                        cmd.Parameters.AddWithValue("@user_role", user.getUserRole());
                        cmd.Parameters.AddWithValue("@station_id", user.getStationId());
                        cmd.Parameters.AddWithValue("@stationed_address", user.getStationedAddress());
                        cmd.Parameters.AddWithValue("@stationed_city", user.getStationedCity());
                        cmd.Parameters.AddWithValue("@stationed_Country", user.getStationedCountry());
                        cmd.Parameters.AddWithValue("@active_status", user.getActiveStatus());
                        cmd.Parameters.AddWithValue("@service_start", user.getServiceStartDate());
                        cmd.Parameters.AddWithValue("@service_end", user.getServiceEndDate());

                        cmd.ExecuteNonQuery();

                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("User created successfully.");
                    }
                    catch (Exception e)
                    {
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("User creation unsuccessful. Reason is - " + e.Message + ".");
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    return status;
                }//end createAFISUser
        */

        /*
                public Status createUserDefaultAppConfigOld(User user)
                {
                    string connStr = getConnectionStringByName("MySQL_AFIS_conn");
                    MySqlConnection conn = new MySqlConnection(connStr);
                    MySqlCommand cmd;
                    Status status = null;
                    conn.Open();
                    try
                    {
                        Console.WriteLine("###-->> Creating default AppConfig....");
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO app_conf(person_id,dup_check) " +
                                          "VALUES(@person_id, @dup_check)";
                        cmd.Parameters.AddWithValue("@person_id", user.getPersonId());
                        cmd.Parameters.AddWithValue("@dup_check", "N");

                        cmd.ExecuteNonQuery();

                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("User Default AppConfig created successfully.");
                    }
                    catch (Exception e)
                    {
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("User Default AppConfig unsuccessful. Reason is - " + e.Message + ".");
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    return status;
                }//end createUserDefaultAppConfig
        */

        public User getValidUser(string username, string password)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            User validUser = null;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.users WHERE username = @username and password = @password and active_status = @active_status";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Encrypt(password));
                cmd.Parameters.AddWithValue("@active_status", "Active");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    string personId = (string)ds.Rows[i]["person_id"];
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string userName = (string)ds.Rows[i]["username"];
                    string passWord = (string)ds.Rows[i]["password"];
                    string userRole = (string)ds.Rows[i]["user_role"];
                    string stationId = (string)ds.Rows[i]["station_id"];
                    string stationedAddress = (string)ds.Rows[i]["stationed_address"];
                    string stationedCity = (string)ds.Rows[i]["stationed_city"];
                    string stationedCountry = (string)ds.Rows[i]["stationed_country"];
                    string activeStatus = (string)ds.Rows[i]["active_status"];
                    DateTime serviceStartDate = (DateTime)ds.Rows[i]["service_start"];
                    DateTime serviceEndDate = (DateTime)ds.Rows[i]["service_end"];

                    validUser = new User();
                    validUser.setPersonId(personId);
                    validUser.setFirstName(fname);
                    validUser.setLastName(lname);
                    validUser.setUsername(userName);
                    validUser.setPassword(passWord);
                    validUser.setUserRole(userRole);
                    validUser.setStationId(stationId);
                    validUser.setStationedAddress(stationedAddress);
                    validUser.setStationedCity(stationedCity);
                    validUser.setStationedCountry(stationedCountry);
                    validUser.setActiveStatus(activeStatus);
                    validUser.setServiceStartDate(serviceStartDate);
                    validUser.setServiceEndDate(serviceEndDate);

                    i = i + 1;
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return validUser;
        }


        public User getUser(string person_id)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            User user = null;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.users WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", person_id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    string personId = (string)ds.Rows[i]["person_id"];
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string userName = (string)ds.Rows[i]["username"];
                    string passWord = (string)ds.Rows[i]["password"];
                    string userRole = (string)ds.Rows[i]["user_role"];
                    string stationId = (string)ds.Rows[i]["station_id"];
                    string stationedAddress = (string)ds.Rows[i]["stationed_address"];
                    string stationedCity = (string)ds.Rows[i]["stationed_city"];
                    string stationedCountry = (string)ds.Rows[i]["stationed_country"];
                    string activeStatus = (string)ds.Rows[i]["active_status"];
                    DateTime serviceStartDate = (DateTime)ds.Rows[i]["service_start"];
                    DateTime serviceEndDate = (DateTime)ds.Rows[i]["service_end"];

                    user = new User();
                    user.setPersonId(personId);
                    user.setFirstName(fname);
                    user.setLastName(lname);
                    user.setUsername(userName);
                    user.setPassword(passWord);
                    user.setUserRole(userRole);
                    user.setStationId(stationId);
                    user.setStationedAddress(stationedAddress);
                    user.setStationedCity(stationedCity);
                    user.setStationedCountry(stationedCountry);
                    user.setActiveStatus(activeStatus);
                    user.setServiceStartDate(serviceStartDate);
                    user.setServiceEndDate(serviceEndDate);

                    i = i + 1;
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return user;
        }


        public Status updateAFISUser(User user)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                string person_id = user.getPersonId();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE users SET fname = @fname, lname = @lname, username = @username, user_role = @user_role, " +
                                  "station_id = @station_id, stationed_address = @stationed_address, stationed_city = @stationed_city, stationed_country = @stationed_country, " +
                                  "active_status = @active_status, service_start = @service_start, service_end = @service_end WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", user.getPersonId());
                cmd.Parameters.AddWithValue("@fname", user.getFirstName());
                cmd.Parameters.AddWithValue("@lname", user.getLastName());
                cmd.Parameters.AddWithValue("@username", user.getUsername());
                cmd.Parameters.AddWithValue("@user_role", user.getUserRole());
                cmd.Parameters.AddWithValue("@station_id", user.getStationId());
                cmd.Parameters.AddWithValue("@stationed_address", user.getStationedAddress());
                cmd.Parameters.AddWithValue("@stationed_city", user.getStationedCity());
                cmd.Parameters.AddWithValue("@stationed_country", user.getStationedCountry());
                cmd.Parameters.AddWithValue("@active_status", user.getActiveStatus());
                cmd.Parameters.AddWithValue("@service_start", user.getServiceStartDate());
                cmd.Parameters.AddWithValue("@service_end", user.getServiceEndDate());

                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("User information updated successfully.");
            }
            catch (Exception e)
            {

                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("User creation unsuccessful. Reason is - " + e.Message + ".");
                throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public Status updatePersonPhysicalCharacteristics(PersonPhysicalChar personPhysicalChar)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE physical_char SET height = @height, weight = @weight, eye_color = @eye_color, " +
                                  "hair_color = @hair_color, complexion = @complexion, birth_mark = @birth_mark, id_mark = @id_mark, " +
                                  "build_type = @build_type, gender = @gender, death_date = @death_date, updated_by = @updated_by, updated_date = @updated_date WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personPhysicalChar.PersonId);
                cmd.Parameters.AddWithValue("@height", personPhysicalChar.Height);
                cmd.Parameters.AddWithValue("@weight", personPhysicalChar.Weight);
                cmd.Parameters.AddWithValue("@eye_color", personPhysicalChar.EyeColor);
                cmd.Parameters.AddWithValue("@hair_color", personPhysicalChar.HairColor);
                cmd.Parameters.AddWithValue("@complexion", personPhysicalChar.Complexion);
                cmd.Parameters.AddWithValue("@birth_mark", personPhysicalChar.BirthMark);
                cmd.Parameters.AddWithValue("@id_mark", personPhysicalChar.IdMark);
                cmd.Parameters.AddWithValue("@build_type", personPhysicalChar.BuildType);
                cmd.Parameters.AddWithValue("@gender", personPhysicalChar.Gender);
                cmd.Parameters.AddWithValue("@death_date", personPhysicalChar.DOD);
                cmd.Parameters.AddWithValue("@updated_by", personPhysicalChar.UpdatedBy);
                cmd.Parameters.AddWithValue("@updated_date", personPhysicalChar.UpdateDateTime);


                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Person's (PersonId = " + personPhysicalChar.PersonId + ") Physical Characteristics updated successfully.");
            }
            catch (Exception exp)
            {
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Update of Person's (PersonId = " + personPhysicalChar.PersonId + ") Physical Characteristics is not successful. Reason is - " + exp.Message + ".");
                Console.WriteLine(exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public Status updatePersonCriminalRecord(CriminalRecord criminalRec)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            byte[] oSerializedCrimeDetail;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE criminal_rec SET person_id = @person_id, crime_detail = @crime_detail, crime_date = @crime_date, " +
                                  "crime_location = @crime_location, court = @court, statute = @statute, court_addr = @court_addr, " +
                                  "case_id = @case_id, sentenced_date = @sentenced_date, release_date = @release_date, arrest_date = @arrest_date, " +
                                  "arrest_agency = @arrest_agency, status = @status, parole_date = @parole_date, criminal_alert_level = @criminal_alert_level, " + 
                                  "criminal_alert_msg = @criminal_alert_msg, ref_doc_loc = @ref_doc_loc, updated_by = @updated_by, update_date = @update_date " +
                                  "WHERE person_id = @person_id AND case_id = @case_id";

                cmd.Parameters.AddWithValue("@person_id", criminalRec.PersonId);
                cmd.Parameters.AddWithValue("@crime_date", criminalRec.CrimeDate);
                cmd.Parameters.AddWithValue("@crime_location", criminalRec.CrimeLocation);
                cmd.Parameters.AddWithValue("@court", criminalRec.Court);
                cmd.Parameters.AddWithValue("@statute", criminalRec.Statute);
                cmd.Parameters.AddWithValue("@court_addr", criminalRec.CourtAddress);
                cmd.Parameters.AddWithValue("@case_id", criminalRec.CaseId);
                cmd.Parameters.AddWithValue("@sentenced_date", criminalRec.SentencedDate);
                cmd.Parameters.AddWithValue("@release_date", criminalRec.ReleaseDate);
                cmd.Parameters.AddWithValue("@arrest_date", criminalRec.ArrestDate);
                cmd.Parameters.AddWithValue("@arrest_agency", criminalRec.ArrestAgency);
                cmd.Parameters.AddWithValue("@status", criminalRec.Status);
                cmd.Parameters.AddWithValue("@parole_date", criminalRec.ParoleDate);
                cmd.Parameters.AddWithValue("@criminal_alert_level", criminalRec.CriminalAlertLevel);
                cmd.Parameters.AddWithValue("@criminal_alert_msg", criminalRec.CriminalAlertMsg);
                cmd.Parameters.AddWithValue("@ref_doc_loc", criminalRec.RefDocLocation);
                cmd.Parameters.AddWithValue("@updated_by", criminalRec.UpdatedBy);
                cmd.Parameters.AddWithValue("@update_date", criminalRec.UpdateDateTime);

                if (criminalRec.CrimeDetail != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, criminalRec.CrimeDetail);
                        oSerializedCrimeDetail = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@crime_detail", oSerializedCrimeDetail);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@crime_detail", null);
                }


                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Person's (PersonId = " + criminalRec.PersonId + ") Criminal record (Case Id = " + criminalRec.CaseId + ") is updated successfully.");
            }
            catch (Exception exp)
            {
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Update of Person's (PersonId = " + criminalRec.PersonId + ") Criminal record (Case Id = " + criminalRec.CaseId + ") is not successful. Reason is - " + exp.Message + ".");
                Console.WriteLine(exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }


        public Status updateUserPref(AppConfig appConfig)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                Console.WriteLine("###-->> Update User Preference....");
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE app_conf SET person_id = @person_id, dup_check = @dup_check WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", appConfig.PersonId);
                cmd.Parameters.AddWithValue("@dup_check", appConfig.DupCheck);
                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("User Preference updated successfully.");
            }
            catch (Exception e)
            {

                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Update of User Preference is unsuccessful. Reason is - " + e.Message + ".");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public Status updateAccessControl(AccessControl accessCntrl)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                Console.WriteLine("###-->> Update Access Control....");
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE access_cntrl SET access_login_tab = @access_login_tab, access_enroll_tab = @access_enroll_tab, " +
                                  "access_match_tab = @access_match_tab, access_usermgmt_tab = @access_usermgmt_tab, access_audit_tab = @access_audit_tab, " +
                                  "access_find_tab = @access_find_tab, access_data_import = @access_data_import, access_data_export = @access_data_export, " +
                                  "access_multi_match = @access_multi_match, access_client_setup = @access_client_setup WHERE role = @role";
                cmd.Parameters.AddWithValue("@role", accessCntrl.getRole());
                cmd.Parameters.AddWithValue("@access_login_tab", accessCntrl.getAccessLoginTab());
                cmd.Parameters.AddWithValue("@access_enroll_tab", accessCntrl.getAccessEnrollTab());
                cmd.Parameters.AddWithValue("@access_match_tab", accessCntrl.getAccessMatchTab());
                cmd.Parameters.AddWithValue("@access_usermgmt_tab", accessCntrl.getAccessUserMgmtTab());
                cmd.Parameters.AddWithValue("@access_audit_tab", accessCntrl.getAccessAuditTab());
                cmd.Parameters.AddWithValue("@access_find_tab", accessCntrl.getAccessFindTab());
                cmd.Parameters.AddWithValue("@access_data_import", accessCntrl.getAccessDataImport());
                cmd.Parameters.AddWithValue("@access_data_export", accessCntrl.getAccessDataExport());
                cmd.Parameters.AddWithValue("@access_multi_match", accessCntrl.getAccessMultiMatch());
                cmd.Parameters.AddWithValue("@access_client_setup", accessCntrl.getAccessClientSetup());

                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Access Control (" + accessCntrl.getRole() + ") updated successfully.");
            }
            catch (Exception e)
            {
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Update of Access Control (" + accessCntrl.getRole() + ") is not successful. Reason is - " + e.Message + ".");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public Status updateClientSetup(ClientSetup clientSetup)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            byte[] oSerializedCompanyLogo;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE client_setup SET client_id = @client_id, legal_name = @legal_name, addr_line = @addr_line, city = @city, " +
                                  "state = @state, postal_code = @postal_code, country = @country, refresh_intrvl = @refresh_intrvl, company_logo = @company_logo, updated_by = @updated_by, update_date = @update_date";

                cmd.Parameters.AddWithValue("@client_id", clientSetup.ClientId);
                cmd.Parameters.AddWithValue("@legal_name", clientSetup.LegalName);
                cmd.Parameters.AddWithValue("@addr_line", clientSetup.AddressLine);
                cmd.Parameters.AddWithValue("@city", clientSetup.City);
                cmd.Parameters.AddWithValue("@state", clientSetup.State);
                cmd.Parameters.AddWithValue("@postal_code", clientSetup.PostalCode);
                cmd.Parameters.AddWithValue("@country", clientSetup.Country);
                cmd.Parameters.AddWithValue("@refresh_intrvl", clientSetup.DataRefreshInterval);

                if (clientSetup.CompanyLogo != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oBFormatter.Serialize(stream, clientSetup.CompanyLogo);
                        oSerializedCompanyLogo = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@company_logo", oSerializedCompanyLogo);
                }
                else {
                    cmd.Parameters.AddWithValue("@company_logo", null);
                }
                cmd.Parameters.AddWithValue("@updated_by", clientSetup.UpdatedBy);
                cmd.Parameters.AddWithValue("@update_date", clientSetup.UpdateDateTime);

                cmd.ExecuteNonQuery();

                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Client Setup Record (Client Id = " + clientSetup.ClientId + ") is updated successfully.");
            }
            catch (Exception exp)
            {
                //Successful status
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Failed to update Client Setup Record (Case Id = " + clientSetup.ClientId + "). Reason is - " + exp.Message + ".");
                Console.WriteLine("###--->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }//end updateClientSetup



        public Status updateAFISUserPassword(string username, string current_password, string new_password)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                DataAccess dataAccess = new DataAccess();
                User user = dataAccess.getValidUser(username, current_password);
                if (user != null)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE users SET password = @password WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", Encrypt(new_password));

                    int resp = cmd.ExecuteNonQuery();

                    if( resp == 1)
                    {
                        status = new Status();
                        status.setStatusCode(Status.STATUS_SUCCESSFUL);
                        status.setStatusDesc("Password updated successfully.");
                    } else
                    {
                        status = new Status();
                        status.setStatusCode(Status.STATUS_FAILURE);
                        status.setStatusDesc("Password update is not successful. \nUsername (" + username + ") does not exists.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Username and/or password.");
                }
            }
            catch (Exception e)
            {

                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Password update is not successful. \nReason is - " + e.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }//end updateAFISUserPassword


        public Status resetAFISUserPassword(string username, string temp_password)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE users SET password = @password WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Encrypt(temp_password));

                int resp = cmd.ExecuteNonQuery();

                Console.WriteLine("###-->> Password RESET response = " + resp);
                if (resp == 1)
                {
                    status = new Status();
                    status.setStatusCode(Status.STATUS_SUCCESSFUL);
                    status.setStatusDesc("Password reseted successfully.");
                } else
                {
                    status = new Status();
                    status.setStatusCode(Status.STATUS_FAILURE);
                    status.setStatusDesc("Password reset is not successful. \nUsername (" + username + ") does not exist.");
                }
            }
            catch (Exception e)
            {

                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Password reset is not successful. \nReason is - " + e.Message);
                throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }//end updateAFISUserPassword


        public Status createUserAuditLog(User user, DateTime login_date_time)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            conn.Open();
            try
            {

                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO audit_log(user_id, user_name, user_role, station_id, stationed_address, stationed_city, stationed_country, login_date_time, logout_date_time, login_duration, login_activity, current_date_time) " +
                                  "VALUES(@user_id, @user_name, @user_role, @station_id, @stationed_address, @stationed_city, @stationed_country, @login_date_time, @logout_date_time, @login_duration, @login_activity, @current_date_time)";
                cmd.Parameters.AddWithValue("@user_id", user.getPersonId());
                cmd.Parameters.AddWithValue("@user_name", user.getUsername());
                cmd.Parameters.AddWithValue("@user_role", user.getUserRole());
                cmd.Parameters.AddWithValue("@station_id", user.getStationId());
                cmd.Parameters.AddWithValue("@stationed_address", user.getStationedAddress());
                cmd.Parameters.AddWithValue("@stationed_city", user.getStationedCity());
                cmd.Parameters.AddWithValue("@stationed_Country", user.getStationedCountry());
                cmd.Parameters.AddWithValue("@login_date_time", login_date_time);
                cmd.Parameters.AddWithValue("@logout_date_time", null);
                cmd.Parameters.AddWithValue("@login_duration", null);
                cmd.Parameters.AddWithValue("@login_activity", null);
                cmd.Parameters.AddWithValue("@current_date_time", DateTime.Now);

                cmd.ExecuteNonQuery();

                long lastInsertedId = cmd.LastInsertedId;

                Console.WriteLine("####-->> Last Inserted Id = " + lastInsertedId);

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Audit log created successfully.");
                status.setAuditLogId(lastInsertedId);
            }
            catch (Exception e)
            {
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Audit log creation unsuccessful. Reason is - " + e.Message + ".");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public Status updateUserAuditLog(User user, DateTime logout_date_time, Int32 login_duration, ActivityLog login_activity)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Status status = null;
            byte[] oSerializedLoginActivity;
            conn.Open();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, login_activity);
                    oSerializedLoginActivity = stream.ToArray();
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE audit_log SET logout_date_time = @logout_date_time, login_duration = @login_duration, login_activity = @login_activity WHERE user_id = @user_id and id = @id";

                cmd.Parameters.AddWithValue("@user_id", user.getPersonId());
                cmd.Parameters.AddWithValue("@id", user.getId());
                cmd.Parameters.AddWithValue("@logout_date_time", logout_date_time);
                cmd.Parameters.AddWithValue("@login_duration", login_duration);
                cmd.Parameters.AddWithValue("@login_activity", oSerializedLoginActivity);
                cmd.Parameters.AddWithValue("@current_date_time", DateTime.Now);

                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Audit log created successfully.");
            }
            catch (Exception e)
            {
                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Audit log creation unsuccessful. Reason is - " + e.Message + ".");
            }
            finally
            {
                if (conn.State  == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public AccessControl getAcessCntrl(string userRole)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            AccessControl accessCntrl = null;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.access_cntrl WHERE role = @role";
                cmd.Parameters.AddWithValue("@role", userRole);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    string access_login_tab = (string)ds.Rows[i]["access_login_tab"];
                    string access_enroll_tab = (string)ds.Rows[i]["access_enroll_tab"];
                    string access_match_tab = (string)ds.Rows[i]["access_match_tab"];
                    string access_usermgmt_tab = (string)ds.Rows[i]["access_usermgmt_tab"];
                    string access_audit_tab = (string)ds.Rows[i]["access_audit_tab"];
                    string access_find_tab = (string)ds.Rows[i]["access_find_tab"];
                    string access_data_import = (string)ds.Rows[i]["access_data_import"];
                    string access_data_export = (string)ds.Rows[i]["access_data_export"];
                    string access_multi_match = (string)ds.Rows[i]["access_multi_match"];
                    string access_client_setup = (string)ds.Rows[i]["access_client_setup"];

                    accessCntrl = new AccessControl();
                    accessCntrl.setAccessLoginTab(access_login_tab);
                    accessCntrl.setAccessEnrollTab(access_enroll_tab);
                    accessCntrl.setAccessMatchTab(access_match_tab);
                    accessCntrl.setAccessUserMgmtTab(access_usermgmt_tab);
                    accessCntrl.setAccessAuditTab(access_audit_tab);
                    accessCntrl.setAccessFindTab(access_find_tab);
                    accessCntrl.setAccessDataImport(access_data_import);
                    accessCntrl.setAccessDataExport(access_data_export);
                    accessCntrl.setAccessMultiMatch(access_multi_match);
                    accessCntrl.setAccessClientSetup(access_client_setup);

                    i = i + 1;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return accessCntrl;
        }

        //get AppConfig
        public AppConfig getAppConfig(string personId)
        { 
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            AppConfig appConfig = null;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.app_conf WHERE person_id = @person_id";
                cmd.Parameters.AddWithValue("@person_id", personId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    Int32 id = (Int32)ds.Rows[i]["id"];
                    string dupCheck = (string)ds.Rows[i]["dup_check"];

                    appConfig = new AppConfig();
                    appConfig.PersonId = personId;
                    appConfig.DupCheck = dupCheck;

                    i = i + 1;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return appConfig;
        }//end getAppConfig


        //Get the AuditLogs between start & end date
        public List<AuditLog> getAuditLogs(string id, DateTime startDate, DateTime endDate)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<AuditLog> auditLogs;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                string startDateStr = startDate.ToString("yyyy-MM-dd");
                string endDateStr = endDate.ToString("yyyy-MM-dd");

                if (id == null || id.Length == 0)  //get audit logs for all users
                {
                    cmd.CommandText = "SELECT * FROM afis.audit_log WHERE " +
                                       "DATE(login_date_time) BETWEEN @startDateStr AND  @endDateStr ORDER BY login_date_time DESC";
                    cmd.Parameters.AddWithValue("@startDateStr", startDateStr);
                    cmd.Parameters.AddWithValue("@endDateStr", endDateStr);
                } else //get the audit logs for certail users
                {
                    cmd.CommandText = "SELECT * FROM afis.audit_log WHERE " +
                                      "user_id = @user_id and " +
                                      "DATE(login_date_time) BETWEEN @startDateStr AND  @endDateStr ORDER BY login_date_time DESC";
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.Parameters.AddWithValue("@startDateStr", startDateStr);
                    cmd.Parameters.AddWithValue("@endDateStr", endDateStr);
                }

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;
                auditLogs = new List<AuditLog>();

                while (rows.MoveNext())
                {

                    string userId = (string)ds.Rows[i]["user_id"];
                    string username = (string)ds.Rows[i]["user_name"];
                    DateTime loginDateTime = (DateTime)ds.Rows[i]["login_date_time"];

                    //Handle nullable DateTime
                    DateTime? logoutDateTime;

                    if (ds.Rows[i]["logout_date_time"] == DBNull.Value)
                    {
                        logoutDateTime = null;
                    }
                    else
                    {
                        logoutDateTime = (DateTime?)ds.Rows[i]["logout_date_time"];
                    }

                    //Get login activities
                    ActivityLog activityLog = null;
                    if (ds.Rows[i]["login_activity"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["login_activity"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            activityLog = (ActivityLog)oBFormatter.Deserialize(oStr);
                        }
                    }

                    AuditLog auditLog = new AuditLog();
                    auditLog.setUserId(userId);
                    auditLog.setUsername(username);
                    auditLog.setLoginDateTime(loginDateTime);
                    auditLog.setLogoutDateTime(logoutDateTime);
                    auditLog.setActivityLog(activityLog);
                    auditLogs.Add(auditLog);

                    i = i + 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return auditLogs;
        }

        //Get the total number of MyPerson records
        public Int32 getPersonCount()
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            Int32 personCount = 0;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT count(*) FROM afis.person";
                personCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return personCount;

        }//end getPersonCount


        public List<PersonDetail> getPersons()
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            List<PersonDetail> personsDetail;
            DataTable ds;
            conn.Open();
            try
            {
                personsDetail = new List<PersonDetail>();
                cmd = conn.CreateCommand();


                cmd.CommandText = "SELECT * FROM afis.person";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {
                    PersonDetail pDetail = new PersonDetail();
                    string pId = (string)ds.Rows[i]["person_id"];
                    Console.WriteLine("###-->># of matched person Id = " + pId);
                    string fname = (string)ds.Rows[i]["fname"];
                    string lname = (string)ds.Rows[i]["lname"];
                    string mname = (string)ds.Rows[i]["mname"];
                    string name_prefix = (string)ds.Rows[i]["name_prefix"];
                    string name_suffix = (string)ds.Rows[i]["name_suffix"];


                    //Handle nullable DateTime
                    DateTime? dob;

                    if (ds.Rows[i]["DOB"] == DBNull.Value)
                    {
                        dob = new DateTime(1753, 01, 01);
                    }
                    else
                    {
                        dob = (DateTime?)ds.Rows[i]["DOB"];
                    }

                    string addr_street = (string)ds.Rows[i]["addr_street"];
                    string addr_city = (string)ds.Rows[i]["addr_city"];
                    string addr_postal_code = (string)ds.Rows[i]["addr_postal_code"];
                    string addr_state = (string)ds.Rows[i]["addr_state"];
                    string addr_country = (string)ds.Rows[i]["addr_country"];
                    string profession = (string)ds.Rows[i]["profession"];
                    string father_name = (string)ds.Rows[i]["father_name"];
                    string cell_nbr = (string)ds.Rows[i]["cell_nbr"];
                    string home_phone = (string)ds.Rows[i]["home_phone"];
                    string office_phone = (string)ds.Rows[i]["office_phone"];
                    string email_addr = (string)ds.Rows[i]["email_addr"];

                    System.Drawing.Image photo = null;

                    if (ds.Rows[i]["photo"] != DBNull.Value)
                    {
                        using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["photo"]))
                        {
                            BinaryFormatter oBFormatter = new BinaryFormatter();
                            oStr.Position = 0;
                            photo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                        }
                    }

                    pDetail.setPersonId(pId);
                    pDetail.setFirstName(fname);
                    pDetail.setLastName(lname);
                    pDetail.setMiddleName(mname);
                    pDetail.setPrefix(name_prefix);
                    pDetail.setSuffix(name_suffix);
                    pDetail.setDOB(dob);
                    pDetail.setStreetAddress(addr_street);
                    pDetail.setCity(addr_city);
                    pDetail.setPostalCode(addr_postal_code);
                    pDetail.setState(addr_state);
                    pDetail.setCountry(addr_country);
                    pDetail.setProfession(profession);
                    pDetail.setFatherName(father_name);
                    pDetail.setcellNbr(cell_nbr);
                    pDetail.setHomwPhoneNbr(home_phone);
                    pDetail.setWorkPhoneNbr(office_phone);
                    pDetail.setEmail(email_addr);
                    pDetail.setPassportPhoto(photo);
                    personsDetail.Add(pDetail);

                    i = i + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return personsDetail;
        }//end getPersons


        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }//end Ebcrypt


        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }// Decrypt


        // Retrieves a connection string by name.
        // Returns null if the name is not found.
        static string getConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        private MyPerson retrieveMyPersonFromFile()
        {
            MyPerson person = null;
            try
            {
                using (FileStream stream = File.OpenRead("C:\\software\\SourceAFIS\\Sample\\bin\\Release\\person.dat"))
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    stream.Position = 0;
                    person = (MyPerson)oBFormatter.Deserialize(stream);
                }

                Console.WriteLine("###--->> Id from File = " + person.Id);
            }
            catch (Exception exp)
            {
                Console.WriteLine("###-->> Exception = " + exp);
            }

            return person;
        }

        private void storeMyPersonToFile(MyPerson person)
        {
            try
            {
                using (Stream stream = File.Open("C:\\software\\SourceAFIS\\Sample\\bin\\Release\\person.dat", FileMode.Create))
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, person);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User getUser()
        {
            User user = new User();
            user.setFirstName("Mohammad");
            user.setLastName("Mohsin");
            return user;
        }
    }
}
