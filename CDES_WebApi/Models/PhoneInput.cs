using CDES_WebApi.CustomValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDES_WebApi.Models
{
    /// <summary>
    /// Input Phone Number Collection
    /// </summary>
    [AtleastOnePhoneRequired(ErrorMessage ="Atleast one Input Phone Number is required")]
    public class PhoneInputToEnrich
    {
        /// <summary>
        /// Leads's Regular Phone Number
        /// </summary>
        public string RegularPhone { get; set; }
        /// <summary>
        /// Leads Country Name associated with Regular Phone Number
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Lead's Business Phone
        /// </summary>
        public string BusinessPhone { get; set; }
       
    }

    

    public class PhoneEnriched
    {
        private bool _isValidPhone = false;
        /// <summary>
        /// Input Phone number passed to Validate/Enrich
        /// </summary>
        public string RawInputPhone { get; set; }
        /// <summary>
        /// E164 Format Phone number
        /// </summary>
        public string E164Format { get; set; }
        /// <summary>
        /// IsValid Phone
        /// </summary>
        public bool IsValidPhone {
                get
                {
                    return _isValidPhone;
                }
                set
                {
                     _isValidPhone = value;
                }
        }
        public string PhoneType { get; set; }
        /// <summary>
        /// City, Town based on the Phone Number's National, Region Code & Phone Carier
        /// </summary>
        public string PhoneLocation { get; set; }
        public string InternationalFormat { get; set; }
        public string NationalFormat { get; set; }
        public string RFC3966Format { get; set; }
    }
    public class PhoneEnrichedResult
    {
        public PhoneEnriched RegularPhone { get; set; }
        public PhoneEnriched BusinessPhone { get; set; }
    }
}