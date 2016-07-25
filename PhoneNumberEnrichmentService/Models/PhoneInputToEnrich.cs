using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberEnrichmentService.Models
{
    /// <summary>
    /// Input Phone Number Collection
    /// </summary>
    public class PhoneInputToEnrich
    {
        /// <summary>
        /// Leads's Regular Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Leads Country Name associated with Regular Phone Number
        /// </summary>
        public string CountryName { get; set; }

    }

}
