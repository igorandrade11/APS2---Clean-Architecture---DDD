using ProductManagement.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProductManagement.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id); 

        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}