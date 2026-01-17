using MyApp.Data.Shared.Interfaces;

namespace MyApp.Data.Product.Interfaces
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
        Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(string category);
        Task<IEnumerable<Entities.Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
