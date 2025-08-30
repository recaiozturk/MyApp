using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;

namespace MyApp.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _dbSet.Where(p => p.Category == category && p.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet.Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.IsActive).ToListAsync();
        }
    }
}

