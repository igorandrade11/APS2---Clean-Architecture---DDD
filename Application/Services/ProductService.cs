using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado");
            
            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {
            var products = await _productRepository.GetByCategoryAsync(category);
            return products.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
        {
            var products = await _productRepository.GetActiveProductsAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createDto)
        {
            var product = Product.Create(
                createDto.Name,
                createDto.Description ?? string.Empty,
                createDto.Price,
                createDto.StockQuantity,
                createDto.CategoryId
            );

            await _productRepository.AddAsync(product);
            return MapToDto(product);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto updateDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado");

            product.Update(
                updateDto.Name,
                updateDto.Description ?? string.Empty,
                updateDto.Price,
                updateDto.StockQuantity,
                updateDto.CategoryId
            );

            await _productRepository.UpdateAsync(product);
            return MapToDto(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var exists = await _productRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado");

            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> UpdateStockAsync(Guid id, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado");

            product.UpdateStock(quantity);
            await _productRepository.UpdateAsync(product);
            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string? query)
        {
            var products = await _productRepository.SearchAsync(query);
            return products.Select(MapToDto);
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : null,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
