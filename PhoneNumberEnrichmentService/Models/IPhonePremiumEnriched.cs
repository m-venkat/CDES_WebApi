using System;

namespace PhoneNumberEnrichmentService.Models
{
    public class Coordinates
    {
        public Double? Latitude {
            get;set;
        }
        public Double? Longitude {
            get;set;
        }
    }
    public interface IPhonePremiumEnriched : IPhoneEnriched
    {

         bool DoNotDisturbRegistered { get; set; }
         Coordinates ServiceCoordinates { get; set; }
    }

        
}