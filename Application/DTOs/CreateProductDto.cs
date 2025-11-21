using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
