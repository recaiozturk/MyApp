using Microsoft.EntityFrameworkCore;
using MyApp.Data.Product.Interfaces;
using MyApp.Data.Shared.Repositories;

namespace MyApp.Data.Product.Repositories
{
    public class ProductRepository : Repository<Entities.Product>, IProductRepository
    {
        public ProductRepository(MyAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(string category)
        {
            return await _dbSet.Where(p => p.Category == category && p.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Entities.Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet.Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.IsActive).ToListAsync();
        }
    }
}
