using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProductManagement.Domain.Validation
{
    public class NoDigitsAttribute : ValidationAttribute
    {
        public NoDigitsAttribute() : base("O campo {0} não pode conter números.") { }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var str = value.ToString();
            if (Regex.IsMatch(str!, @"\d"))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            return ValidationResult.Success;
        }
    }
}
