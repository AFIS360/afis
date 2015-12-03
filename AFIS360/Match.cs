using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AFIS360
{
    class Match
    {
        private MyPerson matchedPerson = null;
        private MyPerson probe = null;
        private float score = 0.0F;
        private bool status = false;


        public void setMatchedPerson(MyPerson matchedPerson)
        {
            this.matchedPerson = matchedPerson;
        }

        public MyPerson getMatchedPerson()
        {
            return matchedPerson;
        }

        public void setProbe(MyPerson probe)
        {
            this.probe = probe;
        }

        public MyPerson getprobe()
        {
            return probe;
        }

        public void setScore(float score)
        {
            this.score = score;
        }

        public float getScore()
        {
            return score;
        }

        public void setStatus(bool status)
        {
            this.status = status;
        }

        public bool getStatus()
        {
            return status;
        }
    }
}
