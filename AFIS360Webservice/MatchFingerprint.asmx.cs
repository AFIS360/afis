using AFIS360Common;
using AFIS360Common.util;
using AFIS360Webservice.controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AFIS360Webservice
{
    /// <summary>
    /// Summary description for MatchFingerprint
    /// </summary>
    [WebService(Namespace = "http://localhost/AFIS360Webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MatchFingerprint : System.Web.Services.WebService
    {

        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(AFIS360Common.MyFingerprint))]
        public AFIS360Webservice.webobj.Match GetMatch(string fingerName, string fingerprintBase64Str, string visitorId, Int32 threshold)
        {
            Image fingerprintImage = Converter.Base64ToImage(fingerprintBase64Str);
            Match match = FingerprintMatcher.getMatch(fingerName, fingerprintImage, "[Unknown]", 60);
            AFIS360Webservice.webobj.Match webMatch = null;

            if (match != null)
            {
                webMatch = new AFIS360Webservice.webobj.Match();
                webMatch.MatchedPerson = match.getMatchedPerson();
                webMatch.Probe = match.getprobe();
                webMatch.Score = match.getScore();
                webMatch.Status = match.getStatus();
            }
            return webMatch;
        }
    }
}
