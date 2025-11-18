using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ProductRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Product product)
        {
            await _ctx.Products.AddAsync(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _ctx.Products.FindAsync(id);
            if (entity == null) return;
            _ctx.Products.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _ctx.Products
                        .Include(p => p.Category)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _ctx.Products
                        .Include(p => p.Category)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _ctx.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return await GetAllAsync();
            }

            query = query.Trim();

            return await _ctx.Products
                .Include(p => p.Category)
                .Where(p =>
                    EF.Functions.Like(p.Name, $"%{query}%")
                    || EF.Functions.Like(p.Description, $"%{query}%")
                    || (p.Category != null && EF.Functions.Like(p.Category.Name, $"%{query}%"))
                )
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
