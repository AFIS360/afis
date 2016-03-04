using AFIS360Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AFIS360Webservice.webobj
{
    public class Match
    {
        private MyPerson matchedPerson = null;
        private MyPerson probe = null;
        private float score = 0.0F;
        private bool status = false;

        public MyPerson MatchedPerson
        {
            get { return matchedPerson;  }
            set { matchedPerson = value; }
        }

        public void setProbe(MyPerson probe)
        {
            this.probe = probe;
        }

        public MyPerson getprobe()
        {
            return probe;
        }

        public MyPerson Probe
        {
            get { return probe; }
            set { probe = value; }
        }

        public float Score
        {
            get { return score; }
            set { score = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
