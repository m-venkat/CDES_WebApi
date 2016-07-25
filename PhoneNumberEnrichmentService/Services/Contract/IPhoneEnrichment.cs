using PhoneNumberEnrichmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberEnrichmentService.Services
{
    public interface IPhoneEnrichment
    {

         List<PhoneEnriched> EnrichPhoneNumber(List<PhoneInputToEnrich> inputPhoneListToEnrich);
         PhoneEnriched EnrichPhoneNumber(PhoneInputToEnrich inputPhoneToEnrich);
    }
}
