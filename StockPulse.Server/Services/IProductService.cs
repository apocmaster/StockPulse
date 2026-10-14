using Microsoft.AspNetCore.Mvc;
using StockPulse.Server.Auxiliars;
using StockPulse.Server.Entities;

namespace StockPulse.Server.Services
{
    public interface IProductService
    {
        Task<Result<Product>> Add(CreateProductDto createProductDto);
        Task<Result<Product?>> GetById(Guid id);
    }
}
