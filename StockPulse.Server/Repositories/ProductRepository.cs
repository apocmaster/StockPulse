using StockPulse.Server.Contexts;
using StockPulse.Server.Entities;
using StockPulse.Server.Mappings;

namespace StockPulse.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockPulseDbContext context;

        // TODO Crear las acciones de la clase ProductRepository para interactuar con la base de datos
        public ProductRepository(StockPulseDbContext context)
        {
            this.context = context;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(CreateProductDto createProductDto)
        {
            var product = createProductDto.ToEntity();
           
            context.Products.Add(product);
            await context.SaveChangesAsync();
            
            return product;
        }
    }
}
