using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneNumberEnrichmentService.Models
{

   
    /// <summary>
    /// Container that holds the Enriched Attributes
    /// </summary>
    public class PhoneEnriched : IPhoneEnriched
    {

        public PhoneInputToEnrich InputPhone { get; set; }

        private bool _isValidPhone = false;
        public bool IsValidPhone
        {
            get { return _isValidPhone; }
            set { _isValidPhone = value; }
        }

        public string RawInputPhone { get; set; }
        public string RFC3966Format { get; set; }
        public string PhoneLocation { get; set; }
        public string NationalFormat { get; set; }
        public string InternationalFormat { get; set; }
        public string E164Format { get; set; }


    }

}