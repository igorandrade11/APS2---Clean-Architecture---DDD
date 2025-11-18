using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CategoryCreateUpdateDto dto);
        Task UpdateAsync(Guid id, CategoryCreateUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
