using Mapster;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<Guid> CreateAsync(CategoryCreateUpdateDto dto)
        {
            var category = dto.Adapt<Category>();
            await _repo.AddAsync(category);
            return category.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(c => c.Adapt<CategoryDto>());
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var cat = await _repo.GetByIdAsync(id);
            return cat?.Adapt<CategoryDto>();
        }

        public async Task UpdateAsync(Guid id, CategoryCreateUpdateDto dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new InvalidOperationException("Categoria não encontrada");
            category.SetName(dto.Name);
            category.SetDescription(dto.Description);
            await _repo.UpdateAsync(category);
        }
    }
}
