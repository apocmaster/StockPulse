using StockPulse.Server.Entities;

namespace StockPulse.Server.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(CreateProductDto createProductDto);
        Task<Product?> GetProductByIdAsync(Guid id);
    }
}
