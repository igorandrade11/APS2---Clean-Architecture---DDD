using System.ComponentModel.DataAnnotations;
using ProductManagement.Domain.Validation;

namespace ProductManagement.Application.DTOs
{
    public class CategoryCreateUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [NoDigits]
        public string Name { get; set; } = "";

        [StringLength(500)]
        [MaxWords(50)]
        public string? Description { get; set; }
    }
}
