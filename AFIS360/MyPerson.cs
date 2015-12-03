using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple


namespace AFIS360
{
    // Inherit from Person in order to add Name field
    [Serializable]
    public class MyPerson : Person
    {
        public string Name;
        public string PersonId;
    }
}
