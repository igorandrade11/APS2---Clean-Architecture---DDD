using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Name { get; private set; } = string.Empty; 
        
        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string Description { get; private set; } = string.Empty;
        
        [Range(0.01, 9999999.99, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; private set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa")]
        public int StockQuantity { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }

        protected Product() { }

        public Product(string name, decimal price, int stockQuantity, Guid categoryId, string? description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
            Description = description ?? string.Empty;

            Validate();
        }

        public void Update(string name, decimal price, int stockQuantity, Guid categoryId, string? description = null)
        {
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
            Description = description ?? string.Empty;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("O nome do produto não pode ser vazio");
            
            if (Price <= 0)
                throw new InvalidOperationException("O preço deve ser maior que zero");
            
            if (StockQuantity < 0)
                throw new InvalidOperationException("A quantidade em estoque não pode ser negativa");
            
            if (CategoryId == Guid.Empty)
                throw new InvalidOperationException("Produto deve pertencer a uma categoria válida");
        }
    }
}
