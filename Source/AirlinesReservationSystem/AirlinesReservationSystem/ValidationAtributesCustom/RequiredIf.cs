using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.ValidationAtributesCustom
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private String PropertyName { get; set; }
        private new String ErrorMessage { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(string propertyName, object desiredvalue, string errormessage)
        {
            this.PropertyName = propertyName;
            this.DesiredValue = desiredvalue;
            this.ErrorMessage = errormessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == DesiredValue.ToString() && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}