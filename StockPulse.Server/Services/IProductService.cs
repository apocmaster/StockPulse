using Microsoft.AspNetCore.Mvc;

namespace StockPulse.Server.Services
{
    public class IProductService
    {
        IActionResult Add(CreateProductDto product);
    }
}
