using System.Web.Http;
using System.Web.Http.Description;
using System;
using System.Collections.Generic;
using PhoneNumberEnrichmentService.Services;
using PhoneNumberEnrichmentService.Models;
using PhoneNumberEnrichmentService.Services.Implementation;

namespace CDES_WebApi.Controllers
{
    /// <summary>
    /// PhoneValidationController
    /// </summary>
    public class PhoneValidationController : BaseController
    {

        private IPhoneEnrichment _enrichment = null;
       
       /// <summary>
       /// Constructor that takes abstraction of IphoneEnrichment Service
       /// </summary>
       /// <param name="enrichmentService"></param>
        public PhoneValidationController(IPhoneEnrichment enrichmentService)
        {
            
            _enrichment = enrichmentService;
        }
        
        
        // GET api/values
        /// <summary>
        /// REST Service EndPoint to Take Phone Number and Validate/Enrich Information
        /// </summary>
        /// <param name="inputPhone">Input Object with Regular and Business Phone Number to Enrich</param>
        /// <returns></returns>
        [Route("EnrichPhone")]
        [ResponseType(typeof(IPhoneEnriched))]
        [HttpPost]//Just retreiving the data, Not updating/creating resource Hence Get
        public IHttpActionResult Validate_And_Enrich_Phone(PhoneInputToEnrich inputPhone)
        {
           
            try
            {
                PhoneEnriched result = _enrichment.EnrichPhoneNumber(inputPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        // GET api/values
        /// <summary>
        /// REST Service EndPoint to Take Phone Number and Validate/Enrich Information
        /// </summary>
        /// <param name="inputPhone">Input Object with Regular and Business Phone Number to Enrich</param>
        /// <returns></returns>
        [Route("EnrichPhoneList")]
        [ResponseType(typeof(List<IPhoneEnriched>))]
        [HttpPost]//Just retreiving the data, Not updating/creating resource Hence Get
        public IHttpActionResult Validate_And_Enrich_Phone(List<PhoneInputToEnrich> inputPhone)
        {

            try
            {
                List<PhoneEnriched> result = _enrichment.EnrichPhoneNumber(inputPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
