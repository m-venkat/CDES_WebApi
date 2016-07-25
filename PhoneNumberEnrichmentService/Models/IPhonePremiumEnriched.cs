using System;

namespace PhoneNumberEnrichmentService.Models
{
    public struct Coordinates
    {
        public Double Latitude { get { return -400; } }
        public Double Longitude { get { return -200; } }
    }
    public interface IPhonePremiumEnriched : IPhoneEnriched
    {

         bool DoNotDisturbRegistered { get; set; }
         Coordinates ServiceCoordinates { get; set; }
    }

        
}