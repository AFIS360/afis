using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple

namespace AFIS360Common
{
    // Inherit from Person in order to add Name field
    [Serializable]
    public class MyFingerprint : Fingerprint
    {
        public string Filename;
        public string Fingername;
        public static string RightThumb = "RightThumb";
        public static string RightIndex = "RightIndex";
        public static string RightMiddle = "RightMiddle";
        public static string RightRing = "RightRing";
        public static string RightLittle = "RightLittle";
        public static string LeftThumb = "LeftThumb";
        public static string LeftIndex = "LeftIndex";
        public static string LeftMiddle = "LeftMiddle";
        public static string LeftRing = "LeftRing";
        public static string LeftLittle = "LeftLittle";
    }
}
