using System.Web.Http;
using CDES_WebApi.Models;
using System.Web.Http.Description;
using PhoneNumbers;
using CDES_WebApi.Utils;
using System;

namespace CDES_WebApi.Controllers
{
    /// <summary>
    /// PhoneValidationController
    /// </summary>
    public class PhoneValidationController : BaseController
    {
        private PhoneNumberUtil _util;
        private PhoneNumberOfflineGeocoder _geoCoder;
        

        public PhoneValidationController() { }
        /// <summary>
        /// PhoneValidationController
        /// </summary>
        /// <param name="util">PhoneNumberUtil</param>
        /// <param name="geoCoder">PhoneNumberOfflineGeocoder</param>
        public PhoneValidationController(PhoneNumberUtil util, PhoneNumberOfflineGeocoder geoCoder)
        {
            _util = util;
            _geoCoder = geoCoder;
        }
        
        
        // GET api/values
        /// <summary>
        /// REST Service EndPoint to Take Phone Number and Validate/Enrich Information
        /// </summary>
        /// <param name="inputPhone">Input Object with Regular and Business Phone Number to Enrich</param>
        /// <returns></returns>
        [Route("EnrichPhone")]
        [ResponseType(typeof(PhoneEnrichedResult))]
        [HttpPost]//Just retreiving the data, Not updating/creating resource Hence Get
        public IHttpActionResult Validate_And_Enrich_Phone(PhoneInputToEnrich inputPhone)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PhoneNumberUtil util = PhoneNumberUtil.GetInstance();
                PhoneNumberOfflineGeocoder geocoder = PhoneNumberOfflineGeocoder.GetInstance();
                PhoneValidationUtil validationUtil = new PhoneValidationUtil(util, geocoder);
                PhoneEnrichedResult result = validationUtil.ValidateEnrich(inputPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

    }
}
