using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UserRAPI.DTO
{
    public class AllowedTypesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedTypesAttribute(string[] allowedValues)
        {
            _allowedValues = allowedValues.Select(v => v.ToLowerInvariant()).ToArray();
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is string strValue)
            {
                if (_allowedValues.Contains(strValue.ToLowerInvariant()))
                    return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Invalid value.");
        }
    }

}
