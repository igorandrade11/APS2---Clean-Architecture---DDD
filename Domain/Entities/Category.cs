using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Name { get; private set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string? Description { get; private set; }

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        protected Category() { } 

        public Category(string name, string? description = null)
        {
            Id = Guid.NewGuid();
            SetName(name);
            Description = description;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Nome inválido para categoria.");
            Name = name;
        }

        public void SetDescription(string? description)
        {
            if (description != null && description.Length > 500)
                throw new InvalidOperationException("Descrição muito longa.");
            Description = description;
        }
    }
}
