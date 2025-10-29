using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
        Task<ProductDto> CreateAsync(CreateProductDto createDto);
        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto updateDto);
        Task DeleteAsync(Guid id);
        Task<ProductDto> UpdateStockAsync(Guid id, int quantity);
    }
}