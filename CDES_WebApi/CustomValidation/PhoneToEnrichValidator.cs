using CDES_WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDES_WebApi.CustomValidation
{
    /// <summary>
    /// Custom ModelValidation to validate one of the attribute is valid
    /// </summary>
    public class AtleastOnePhoneRequired : ValidationAttribute
    {
        /// <summary>
        /// When Model.IsValid is called this method will be invoked.
        /// </summary>
        /// <param name="value">model expected to be validated</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            Tuple<ValidationResult, bool> res = _IsValid(value);
            return res.Item2;
        }

        /// <summary>
        /// This method is required to set this as decoration attribute.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Tuple<ValidationResult, bool> res = _IsValid(value);
            return res.Item1;
        }
        
       
        /// <summary>
        /// Private internal method called by two different methods defined above.
        /// </summary>
        /// <param name="value">model expected to be validated</param>
        /// <returns></returns>
        private Tuple<ValidationResult, bool> _IsValid(object value)
        {
            PhoneInputToEnrich _inputToEnrich = (PhoneInputToEnrich)value;
            if (string.IsNullOrEmpty(_inputToEnrich.RegularPhone) && string.IsNullOrEmpty(_inputToEnrich.BusinessPhone))
            {               
                return Tuple.Create<ValidationResult, bool>(new ValidationResult(this.ErrorMessage), false);               
            }
            else
            {
                return Tuple.Create<ValidationResult, bool>(null, true);                
            }
        }
        //override 
    }
}
