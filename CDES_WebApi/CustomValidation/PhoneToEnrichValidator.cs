using CDES_WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDES_WebApi.CustomValidation
{
    public class AtleastOnePhoneRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Tuple<ValidationResult, bool> res = _IsValid(value);
            return res.Item2;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Tuple<ValidationResult, bool> res = _IsValid(value);
            return res.Item1;
        }
        
       
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
