using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        // ✅ CORREÇÃO CS8618: Inicializado para string.Empty
        public string Name { get; private set; } = string.Empty; 
        
        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        // ✅ CORREÇÃO CS8618: Inicializado para string.Empty
        public string Description { get; private set; } = string.Empty;
        
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; private set; }
        
        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
        public int StockQuantity { get; private set; }
        
        [Required(ErrorMessage = "A categoria é obrigatória")]
        [StringLength(50, ErrorMessage = "A categoria não pode exceder 50 caracteres")]
        // ✅ CORREÇÃO CS8618: Inicializado para string.Empty
        public string Category { get; private set; } = string.Empty;
        
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Construtor privado para Entity Framework e para forçar o uso do método Create
        private Product() { }

        public static Product Create(string name, string description, decimal price, int stockQuantity, string category)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                Category = category,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            product.Validate();
            return product;
        }

        public void Update(string name, string description, decimal price, int stockQuantity, string category)
        {
            // O uso de 'private set' garante que o mapeador do EF (ou a atribuição manual)
            // possa definir os valores, mas o resto do código só pode usar este método.
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
            UpdatedAt = DateTime.UtcNow;
            Validate();
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("A quantidade em estoque não pode ser negativa");
            
            StockQuantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("O nome do produto não pode ser vazio");
            
            if (Price <= 0)
                throw new InvalidOperationException("O preço deve ser maior que zero");
            
            if (StockQuantity < 0)
                throw new InvalidOperationException("A quantidade em estoque não pode ser negativa");
            
            if (string.IsNullOrWhiteSpace(Category))
                throw new InvalidOperationException("A categoria não pode ser vazia");
        }
    }
}