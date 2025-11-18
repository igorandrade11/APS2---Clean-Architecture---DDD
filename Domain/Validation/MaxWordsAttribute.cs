using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Validation
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private readonly int _maxWords;
        public MaxWordsAttribute(int maxWords) : base($"O campo {{0}} deve ter no máximo {maxWords} palavras.")
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var words = value.ToString()!.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (words > _maxWords)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            return ValidationResult.Success;
        }
    }
}
