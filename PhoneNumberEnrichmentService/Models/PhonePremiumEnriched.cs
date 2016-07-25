using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneNumberEnrichmentService.Models
{
    
    /// <summary>
    /// Container that holds the Enriched Attributes
    /// </summary>
    public class PhonePremiumEnriched : PhoneEnriched, IPhonePremiumEnriched 
    {
        private bool _doNotDisturbRegistered = false;
        private Coordinates _Coordinates = new Coordinates();

        public bool DoNotDisturbRegistered { get { return false; } set { _doNotDisturbRegistered = value; } }
        public Coordinates ServiceCoordinates { get { return _Coordinates; } set { _Coordinates = value; } }

    }
    

    
    
}