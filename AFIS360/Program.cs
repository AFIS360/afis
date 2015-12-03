using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using System.Collections;


namespace AFIS360
{
    class Program
    {
    

        // Initialize path to images
        static readonly string ImagePath = Path.Combine(Path.Combine("..", ".."), "images");

        // Shared AfisEngine instance (cannot be shared between different threads though)
        static AfisEngine Afis;

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
        }

        // Take fingerprint image file and create Person object from the image
        public static MyPerson Enroll(ICollection<KeyValuePair<String, String>> imgFilePaths, string name, string id)
        {

            // Initialize empty person object and set its properties
            MyPerson person = new MyPerson();
            person.Name = name;
            person.PersonId = id;
            MyFingerprint fp = null;

            foreach (KeyValuePair<string, string> element in imgFilePaths)
            {
                string fingerImagePathKey = element.Key;
                string fingerImangePath = element.Value;

                Console.WriteLine("{0}, {1}", fingerImagePathKey, fingerImangePath);

                Console.WriteLine("Enrolling {0}...", name);

                // Initialize empty fingerprint object and set properties
                fp = new MyFingerprint();
                fp.Filename = fingerImangePath;
                setFingername(fp, fingerImagePathKey);

                // Load image from the file
                Console.WriteLine(" Loading image from {0}...", fingerImangePath);
                BitmapImage image = new BitmapImage(new Uri(fingerImangePath, UriKind.RelativeOrAbsolute));
                fp.AsBitmapSource = image;
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
        }

        private static void setFingername(MyFingerprint fp, string fpLocationPath)
        {
            if (fpLocationPath.Equals("fpRTPath"))
            {
                fp.Fingername = MyFingerprint.RightThumb;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpRIPath"))
            {
                fp.Fingername = MyFingerprint.RightIndex;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpRMPath"))
            {
                fp.Fingername = MyFingerprint.RightMiddle;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpRRPath"))
            {
                fp.Fingername = MyFingerprint.RightRing;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpRLPath"))
            {
                fp.Fingername = MyFingerprint.RightLittle;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpLTPath"))
            {
                fp.Fingername = MyFingerprint.LeftThumb;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpLIPath"))
            {
                fp.Fingername = MyFingerprint.LeftIndex;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpLMPath"))
            {
                fp.Fingername = MyFingerprint.LeftMiddle;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpLRPath"))
            {
                fp.Fingername = MyFingerprint.LeftRing;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
            else if (fpLocationPath.Equals("fpLLPath"))
            {
                fp.Fingername = MyFingerprint.LeftLittle;
                Console.WriteLine("Finger = " + fp.Fingername);
            }
        }//end setFingername


        public static void enrollPerson(MyPerson myPerson)
        {
            DataAccess dataAccess = new DataAccess();
            dataAccess.storeFingerprints(myPerson);
            Console.WriteLine("Enrollment successful...");
        }

        static List<MyPerson> retrieveFromDB()
        {

            DataAccess dataAccess = new DataAccess();
            List<MyPerson> persons = dataAccess.retrievePersonFingerprints();
            return persons;
        }


        //Match probe with people in the database
        public static Match getMatch(string fpPath, string visitorId, Int32 threshold)
        {
            Match match = new Match();

            // Match visitor with unknown identity
            MyPerson probe = getProbe(fpPath, visitorId);

            // Load all people fron database 
            List<MyPerson> persons = retrieveFromDB();

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
        }

        [STAThread]
        static void Main(string[] args)
        {
            Afis = new AfisEngine();
            AFISMain afis = new AFISMain();
            afis.ShowDialog();
        }// end Main

    }//end Program
}
