using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumberEnrichmentService.Models;
using PhoneNumbers;
using PhoneNumberEnrichmentService.Utils;

namespace PhoneNumberEnrichmentService.Services.Implementation
{
    public class PremiumPhoneEnrichmentService : IPhoneEnrichment
    {
        private PhoneNumbers.PhoneNumberUtil _util = null;
        private PhoneNumberOfflineGeocoder _geocoder = null;
        

        public PremiumPhoneEnrichmentService()
        {
            _util = PhoneNumberUtil.GetInstance();
            _geocoder = PhoneNumberOfflineGeocoder.GetInstance();
        }

                

        public PhoneEnriched EnrichPhoneNumber(PhoneInputToEnrich inputPhoneToEnrich)
        {
            PhoneNumber phonenumberObject = null;
            PhoneEnriched enriched = new PhonePremiumEnriched();           
            try
            {

                string isoCountryCodeFromInput = ISOCountryCodeList.GetISOCountryCode(inputPhoneToEnrich.CountryName);
                if (!string.IsNullOrEmpty(inputPhoneToEnrich.PhoneNumber))
                {
                     phonenumberObject = _util.Parse(inputPhoneToEnrich.PhoneNumber, isoCountryCodeFromInput); 
                }
                
                if (phonenumberObject != null)
                {
                    string regionCoundFromPhoneNumber = _util.GetRegionCodeForCountryCode(phonenumberObject.CountryCode);
                    enriched.E164Format = _util.Format(phonenumberObject, PhoneNumberFormat.E164);
                    enriched.RawInputPhone = inputPhoneToEnrich.PhoneNumber;
                    enriched.RFC3966Format =  _util.Format(phonenumberObject, PhoneNumberFormat.RFC3966);
                    enriched.PhoneLocation =  _geocoder.GetDescriptionForNumber(phonenumberObject, Locale.ENGLISH);
                    enriched.IsValidPhone = regionCoundFromPhoneNumber == isoCountryCodeFromInput && _util.IsValidNumber(phonenumberObject);
                    enriched.InternationalFormat = _util.Format(phonenumberObject, PhoneNumberFormat.INTERNATIONAL);
                    enriched.NationalFormat = _util.Format(phonenumberObject, PhoneNumberFormat.NATIONAL);
                    (enriched as PhonePremiumEnriched).ServiceCoordinates.Latitude = enriched.IsValidPhone ? new Random().Next(100, 500) : Double.NaN;
                    (enriched as PhonePremiumEnriched).ServiceCoordinates.Longitude = enriched.IsValidPhone ? new Random().Next(600, 1000) : Double.NaN;
                   
               
                }

            }
            catch (Exception ex)
            {
                /*
                 Gracefully handle exception, If any exception occurs
                 default object will be returned where as the IsValid will be false                 
                */

            }
            
            return enriched;
        }

        public List<PhoneEnriched> EnrichPhoneNumber(List<PhoneInputToEnrich> inputPhoneListToEnrich)
        {            
            List<Task<PhoneEnriched>> list = new List<Task<PhoneEnriched>>();
            foreach (PhoneInputToEnrich record in inputPhoneListToEnrich)
            {
                list.Add(Task.Run(() => EnrichPhoneNumber(record)));
            }
             Task.WhenAll(list).Wait();
            return list.Select(t=> t.Result).ToList<PhoneEnriched>();
        }
    }
}
