using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ShortestWayFinder.Web.ValidationAttributes
{
    public class PointsNameNotEqualAttribute : ValidationAttribute
    {
        private readonly string _firstPoint;

        public PointsNameNotEqualAttribute(string firstPoint)
        {
            _firstPoint = firstPoint;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var firstPointProperty = validationContext.ObjectType.GetProperty(_firstPoint);

            if (firstPointProperty == null)
                return new ValidationResult($"Unkown property: {_firstPoint}");

            var firstPointName = firstPointProperty.GetValue(validationContext.ObjectInstance, null);

            if (string.Equals(value.ToString(), firstPointName.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
