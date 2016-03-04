using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using System.Configuration;
using System.Drawing.Imaging;
using AFIS360Common;
using AFIS360Common.dao;
using System.Windows.Media.Imaging;

namespace AFIS360Webservice.controller
{
    class FingerprintMatcher
    {
    
        // Shared AfisEngine instance (cannot be shared between different threads though)
        static AfisEngine Afis;

        //Load all persons from DB table on first time call persons
        public static List<MyPerson> persons = null;

        // Take fingerprint image file and create Person object from the image
        public static MyPerson getProbe(string filename, string visitorNbr)
        {
            Console.WriteLine("Matching {0}...", visitorNbr);

            // Initialize empty fingerprint object and set properties
            MyFingerprint fp = new MyFingerprint();
            fp.Filename = filename;
            // Load image from the file
            Console.WriteLine(" Loading image from {0}...", filename);
            BitmapImage image = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
            fp.AsBitmapSource = image;
            // Above update of fp.AsBitmapSource initialized also raw image in fp.Image
            // Check raw image dimensions, Y axis is first, X axis is second
            Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));

            // Initialize empty person object and set its properties
            MyPerson person = new MyPerson();
            person.Name = visitorNbr;
            //            person.Id = id;
            // Add fingerprint to the person
            person.Fingerprints.Add(fp);

            // Execute extraction in order to initialize fp.Template
            Afis.Extract(person);
            // Check template size
            Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

            return person;
        }//getProbe

        // Take fingerprint image file and create Person object from the image
        public static MyPerson getProbe(string fingerName, System.Drawing.Image fingerImage, string visitorNbr)
        {

            // Initialize empty person object and set its properties
            MyPerson person = new MyPerson();
            person.Name = visitorNbr;

            // Initialize empty fingerprint object and set properties
            MyFingerprint fp = new MyFingerprint();
            fp.Fingername = fingerName;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(fingerImage);
            fp.AsBitmap = image;

            // Above update of fp.AsBitmapSource initialized also raw image in fp.Image
            // Check raw image dimensions, Y axis is first, X axis is second
            Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));
            // Add fingerprint to the person
            person.Fingerprints.Add(fp);
            

            // Execute extraction in order to initialize fp.Template
            Afis.Extract(person);
            // Check template size
            Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

            return person;
        }//Enroll


        // Take fingerprint image file and create Person object from the image
        public static MyPerson Enroll(ICollection<KeyValuePair<String, System.Drawing.Image>> imgsFromPicBox, string name, string id)
        {
            // Initialize empty person object and set its properties
            MyPerson person = new MyPerson();
            person.Name = name;
            person.PersonId = id;
            MyFingerprint fp = null;

            foreach (KeyValuePair<string, System.Drawing.Image> element in imgsFromPicBox)
            {
                string fingerName = element.Key;
                System.Drawing.Image fingerImage = element.Value;

                // Initialize empty fingerprint object and set properties
                fp = new MyFingerprint();
                fp.Fingername = fingerName;

                System.Drawing.Bitmap image = new System.Drawing.Bitmap(fingerImage);
                fp.AsBitmap = image;

                // Above update of fp.AsBitmapSource initialized also raw image in fp.Image
                // Check raw image dimensions, Y axis is first, X axis is second
                Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));
                // Add fingerprint to the person
                person.Fingerprints.Add(fp);
            }

            // Execute extraction in order to initialize fp.Template
            Afis.Extract(person);
            // Check template size
            Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

            return person;
        }//Enroll


        //Match probe with people in the database
        public static Match getMatch(string fpPath, string visitorId, Int32 threshold)
        {
            Match match = new Match();

            // Match visitor with unknown identity
            MyPerson probe = getProbe(fpPath, visitorId);

            if(persons == null)
            {
                DataAccess dataAccess = new DataAccess();
                persons = dataAccess.retrievePersonFingerprintTemplates();
                Console.WriteLine("###-->> Loading persons from DB. Total persons = " + persons.Count());
            } else
            {
                Console.WriteLine("###-->> Loading persons from Cache. Total persons = " + persons.Count());
            }


            // Look up the probe using Threshold = 10
            Afis.Threshold = threshold;
            Console.WriteLine("Identifying {0} in database of {1} persons...", probe.Name, persons.Count);
            MyPerson matchedPerson = Afis.Identify(probe, persons).FirstOrDefault() as MyPerson;
            // Null result means that there is no candidate with similarity score above threshold
            if (matchedPerson == null)
            {
                match.setProbe(probe);
                match.setMatchedPerson(matchedPerson);
                match.setStatus(false);
                match.setScore(0.0F);
                return match;
            }

            // Compute similarity score
            float score = Afis.Verify(probe, matchedPerson);
            match.setProbe(probe);
            match.setMatchedPerson(matchedPerson);
            match.setStatus(true);
            match.setScore(score);

            Console.WriteLine("Similarity score between {0} and {1} = {2:F3}", probe.Name, matchedPerson.Name, score);
            Console.WriteLine("Visitor " + visitorId + " matches with registered person " + matchedPerson.Name + ". Match score = " + score);

            return match;
        }//getMatch


        //Match probe with people in the database
        public static Match getMatch(string fingerName, System.Drawing.Image fingerImage, string visitorId, Int32 threshold)
        {
            if (Afis == null) Afis = new AfisEngine();

            Match match = new Match();

            // Match visitor with unknown identity
            MyPerson probe = getProbe(fingerName, fingerImage, visitorId);

            if (persons == null)
            {
                // Load all people fron database
                DataAccess dataAccess = new DataAccess();
                persons = dataAccess.retrievePersonFingerprintTemplates();
                Console.WriteLine("###-->> Loading persons from DB. Total persons = " + persons.Count());
            }
            else
            {
                Console.WriteLine("###-->> Loading persons from Cache. Total persons = " + persons.Count());
            }


            // Look up the probe using Threshold = 10
            Afis.Threshold = threshold;
            Console.WriteLine("Identifying {0} in database of {1} persons...", probe.Name, persons.Count);
            MyPerson matchedPerson = Afis.Identify(probe, persons).FirstOrDefault() as MyPerson;
            // Null result means that there is no candidate with similarity score above threshold
            if (matchedPerson == null)
            {
                match.setProbe(probe);
                match.setMatchedPerson(matchedPerson);
                match.setStatus(false);
                match.setScore(0.0F);
                return match;
            }

            // Compute similarity score
            float score = Afis.Verify(probe, matchedPerson);
            match.setProbe(probe);
            match.setMatchedPerson(matchedPerson);
            match.setStatus(true);
            match.setScore(score);

            Console.WriteLine("Similarity score between {0} and {1} = {2:F3}", probe.Name, matchedPerson.Name, score);
            Console.WriteLine("Visitor " + visitorId + " matches with registered person " + matchedPerson.Name + ". Match score = " + score);

            return match;
        }//getMatch

        public static ICollection<KeyValuePair<String, MyPerson>> getDuplicateFingerprintRecords()
        {
            List<MyPerson> persons = new DataAccess().retrievePersonFingerprintTemplates();
            ICollection<KeyValuePair<String, MyPerson>> matchedPersons = new Dictionary<String, MyPerson>();

            foreach (MyPerson person in persons)
            {
                MyPerson probe = person;
                List<Match> matches = getMatches(probe, persons);
                int i = 0;
                foreach(Match match in matches)
                {
                    if (match.getStatus())
                    {
                        if (probe.PersonId != match.getMatchedPerson().PersonId)
                        {
                            string key = probe.PersonId + "[" + i + "]";
                            matchedPersons.Add(new KeyValuePair<string, MyPerson>(key, match.getMatchedPerson()));
                        }
                    }
                    i++;
                }
            }
            return matchedPersons;
        }


        private static List<Match> getMatches(MyPerson probe, List<MyPerson> persons)
        {
            List<Match> matches = new List<Match>();

            Console.WriteLine("###-->> Loading persons from DB. Total persons = " + persons.Count());

            // Look up the probe using Threshold = 10
            Afis.Threshold = Convert.ToInt32(ConfigurationManager.AppSettings["InitialThresholdScore"]);

            Console.WriteLine("Identifying {0} in database of {1} persons...", probe.Name, persons.Count);
            IEnumerable<Person> matchedPersons = Afis.Identify(probe, persons);
            IEnumerator<Person> matchedPersonsIterator = matchedPersons.GetEnumerator();

            while (matchedPersonsIterator.MoveNext())
            {
                MyPerson matchedPerson = (MyPerson)matchedPersonsIterator.Current;
                Match match = new Match();
                // Compute similarity score
                float score = Afis.Verify(probe, matchedPerson);
                match.setProbe(probe);
                match.setMatchedPerson(matchedPerson);
                match.setStatus(true);
                match.setScore(score);
                matches.Add(match);
                Console.WriteLine("Similarity score between {0} and {1} = {2:F3}", probe.Name, matchedPerson.Name, score);
                Console.WriteLine("Visitor " + probe.PersonId + " matches with registered person " + matchedPerson.Name + ". Match score = " + score);
            }

            return matches;
        }


        //Match probe with people in the database
        public static List<Match> getMatches(string fpPath, string visitorId, Int32 threshold)
        {
            List<Match> matches = new List<Match>();

            // Match visitor with unknown identity
            MyPerson probe = getProbe(fpPath, visitorId);

            Console.WriteLine("###-->> persons object = " + persons);
            if (persons == null)
            {
                // Load all people fron database
                DataAccess dataAccess = new DataAccess();
                persons = dataAccess.retrievePersonFingerprintTemplates();
                Console.WriteLine("###-->> Loading persons from DB. Total persons = " + persons.Count());
            }
            else
            {
                Console.WriteLine("###-->> Loading persons from Cache. Total persons = " + persons.Count());
            }

            // Look up the probe using Threshold = 10
            Afis.Threshold = threshold;
            Console.WriteLine("Identifying {0} in database of {1} persons...", probe.Name, persons.Count);
            IEnumerable<Person> matchedPersons = Afis.Identify(probe, persons);
            IEnumerator<Person> matchedPersonsIterator = matchedPersons.GetEnumerator();
            while (matchedPersonsIterator.MoveNext())
            {
                MyPerson matchedPerson = (MyPerson)matchedPersonsIterator.Current;
                Match match = new Match();
                // Compute similarity score
                float score = Afis.Verify(probe, matchedPerson);
                match.setProbe(probe);
                match.setMatchedPerson(matchedPerson);
                match.setStatus(true);
                match.setScore(score);
                matches.Add(match);
                Console.WriteLine("Similarity score between {0} and {1} = {2:F3}", probe.Name, matchedPerson.Name, score);
                Console.WriteLine("Visitor " + visitorId + " matches with registered person " + matchedPerson.Name + ". Match score = " + score);
            }
            return matches;

        }//getMatches

        public static PersonDetail getPerson(string personId)
        {
            DataAccess dataAccess = new DataAccess();
            PersonDetail personDetail = dataAccess.retrievePersonDetail(personId).FirstOrDefault();
            return personDetail;
        }

        public static void loadFingerptintTemplates()
        {
            DataAccess dataAccess = new DataAccess();
            persons = dataAccess.retrievePersonFingerprintTemplates();
            Console.WriteLine("###-->> # of Fingerprints loaded = " + persons.Count);
        }

    }//end Program
}
