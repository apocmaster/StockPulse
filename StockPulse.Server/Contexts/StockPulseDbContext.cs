using Microsoft.EntityFrameworkCore;
using StockPulse.Server.Entities;

namespace StockPulse.Server.Contexts
{
    public class StockPulseDbContext : DbContext
    {
        public StockPulseDbContext(DbContextOptions<StockPulseDbContext> options) : base(options)
        {
        }

        // Define your DbSets here
        public DbSet<Product> Products { get; set; }
    }
}
