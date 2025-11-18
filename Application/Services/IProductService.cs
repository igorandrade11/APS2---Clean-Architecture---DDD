using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ProductDto dto);
        Task UpdateAsync(Guid id, ProductDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> SearchAsync(string? query);
    }
}
