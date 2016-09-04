using PhoneNumberEnrichmentService.Models;
using PhoneNumberEnrichmentService.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDES_WebApi.ServiceExtension
{
    public class PhoneEnrichmentWithCallCounter : PremiumPhoneEnrichmentService
    {
        private static int serviceCallCounter = 0;
        private static object _locker = new object();
        public static int ServiceCallCounter {
            get
            {
                return serviceCallCounter;
            }
            
        }
        public static void IncrementServiceCallCounter()
        {
            serviceCallCounter++;
        }
        public PhoneEnrichmentWithCallCounter() :base()
        {
           
        }
        public override PhoneEnriched EnrichPhoneNumber(PhoneInputToEnrich inputPhoneToEnrich)
        {
            lock (_locker) {
                IncrementServiceCallCounter();
            return base.EnrichPhoneNumber(inputPhoneToEnrich);
            }
        }
    }
}
