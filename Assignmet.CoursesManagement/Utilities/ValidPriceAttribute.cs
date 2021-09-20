using Assignment.CoursesManagement.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Utilities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ValidPriceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var courseType = validationContext.ObjectType.GetProperty("CourseType").GetValue(validationContext.ObjectInstance);

            if((int)courseType == ((int)CourseType.Free) && (decimal)value != 0)
            {
                return new ValidationResult("Price must be 0");
            }
            else if((int)courseType == (int)CourseType.Paid && (decimal)value <= 0)
            {
                return new ValidationResult("Price must be more than 0");
            }
            
            return ValidationResult.Success;
        }
    }
}
