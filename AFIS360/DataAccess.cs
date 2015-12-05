﻿using System;
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


namespace AFIS360
{
    public class DataAccess
    {

        public void storeMyPerson(MyPerson person)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            byte[] oSerializedPerson;
            conn.Open();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, person);
                    oSerializedPerson = stream.ToArray();
                }

                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO person(id,person,name) VALUES(@id,@person,@name)";
                cmd.Parameters.AddWithValue("@id", person.Id);
                cmd.Parameters.AddWithValue("@person", oSerializedPerson);
                cmd.Parameters.AddWithValue("@name", person.Name);
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
        }//storeMyPerson

        public void storeFingerprints(MyPerson person)
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
                cmd.CommandText = "INSERT INTO fingerprint(person_id,image,name) VALUES(@person_id,@image,@name)";
                cmd.Parameters.AddWithValue("@person_id", person.PersonId);
                cmd.Parameters.AddWithValue("@image", oSerializedFpImages);
                cmd.Parameters.AddWithValue("@name", person.Name);
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
        }//end storeFingerprints


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
                                  "addr_street, addr_city, addr_postal_code, addr_state, addr_country, profession," +
                                  "father_name, cell_nbr, home_phone, office_phone, email_addr, photo)" +
                                  "VALUES(@person_id, @fname, @lname, @mname, @name_prefix, @name_suffix," +
                                  "@addr_street, @addr_city, @addr_postal_code, @addr_state, @addr_country, @profession," +
                                  "@father_name, @cell_nbr, @home_phone, @office_phone, @email_addr, @photo)";

                cmd.Parameters.AddWithValue("@person_id", personDetail.getPersonId());
                cmd.Parameters.AddWithValue("@fname", personDetail.getFirstName());
                cmd.Parameters.AddWithValue("@lname", personDetail.getLastName());
                cmd.Parameters.AddWithValue("@mname", personDetail.getMiddleName());
                cmd.Parameters.AddWithValue("@name_Prefix", personDetail.getPrefix());
                cmd.Parameters.AddWithValue("@name_suffix", personDetail.getSuffix());
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

                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter oBFormatter = new BinaryFormatter();
                    oBFormatter.Serialize(stream, personDetail.getPassportPhoto());
                    oSerializedPassportPhoto = stream.ToArray();
                }

                cmd.Parameters.AddWithValue("@photo", oSerializedPassportPhoto);

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

                    using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["photo"]))
                    {
                        BinaryFormatter oBFormatter = new BinaryFormatter();
                        oStr.Position = 0;
                        photo = (System.Drawing.Image)oBFormatter.Deserialize(oStr);
                    }

                    pDetail.setPersonId(pId);
                    pDetail.setFirstName(fname);
                    pDetail.setLastName(lname);
                    pDetail.setMiddleName(mname);
                    pDetail.setPrefix(name_prefix);
                    pDetail.setSuffix(name_suffix);
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


        public List<MyPerson> retrieveMyPersons()
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
                cmd.CommandText = "SELECT * FROM afis.person";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                IEnumerator rows = ds.Rows.GetEnumerator();
                Int32 i = 0;

                while (rows.MoveNext())
                {

                    int id = (int)ds.Rows[i]["id"];
                    using (MemoryStream oStr = new MemoryStream((byte[])ds.Rows[i]["person"]))
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
            catch (Exception)
            {
                throw;
            }

            return person;
        }


        public Status createAFISUser(User user)
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
        }


        private bool isValidUser(string username, string password)
        {
            string connStr = getConnectionStringByName("MySQL_AFIS_conn");
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            bool validUser = false;
            DataTable ds;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM afis.users WHERE username = @username and password = @password";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Encrypt(password));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                ds = new DataTable();
                da.Fill(ds);
                Int32 rows = ds.Rows.Count;
                if (rows > 0) validUser = true;
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
            return validUser;
        }


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
                cmd.CommandText = "UPDATE users set fname = @fname, lname = @lname, username = @username, user_role = @user_role, " +
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
                    cmd.CommandText = "UPDATE users set password = @password WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", Encrypt(new_password));

                    cmd.ExecuteNonQuery();

                    status = new Status();
                    status.setStatusCode(Status.STATUS_SUCCESSFUL);
                    status.setStatusDesc("Password updated successfully.");
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
                status.setStatusDesc("Password update unsuccessful. \nReason is - " + e.Message);
                //                throw e;
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
                cmd.CommandText = "UPDATE users set password = @password WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Encrypt(temp_password));

                cmd.ExecuteNonQuery();

                status = new Status();
                status.setStatusCode(Status.STATUS_SUCCESSFUL);
                status.setStatusDesc("Password reseted successfully.");
            }
            catch (Exception e)
            {

                status = new Status();
                status.setStatusCode(Status.STATUS_FAILURE);
                status.setStatusDesc("Password reset unsuccessful. \nReason is - " + e.Message);
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
                cmd.CommandText = "UPDATE audit_log set logout_date_time = @logout_date_time, login_duration = @login_duration, login_activity = @login_activity WHERE user_id = @user_id and id = @id";

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

                    accessCntrl = new AccessControl();
                    accessCntrl.setAccessLoginTab(access_login_tab);
                    accessCntrl.setAccessEnrollTab(access_enroll_tab);
                    accessCntrl.setAccessMatchTab(access_match_tab);
                    accessCntrl.setAccessUserMgmtTab(access_usermgmt_tab);
                    accessCntrl.setAccessAuditTab(access_audit_tab);

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


        public List<AuditLog> getAuditLogs()
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
                cmd.CommandText = "SELECT * FROM afis.audit_log WHERE DATE(login_date_time) = CURDATE()";

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
/*
                    if(ds.Rows[i]["logout_date_time"] != null)
                    {
                        logoutDateTime = (DateTime)ds.Rows[i]["logout_date_time"];
                    }
*/
                    AuditLog auditLog = new AuditLog();
                    auditLog.setUserId(userId);
                    auditLog.setUsername(username);
                    auditLog.setLoginDateTime(loginDateTime);
//                    auditLog.setLogoutDateTime(logoutDateTime);
                    auditLogs.Add(auditLog);

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
            return auditLogs;
        }


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

        private string ImageToBase64(System.Drawing.Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }//end ImagetoBase64

        public System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }//end Base64ToImage

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
    }
}
