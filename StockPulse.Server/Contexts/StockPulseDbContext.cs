using Microsoft.EntityFrameworkCore;

namespace StockPulse.Server.Contexts
{
    public class StockPulseDbContext : DbContext
    {
        public StockPulseDbContext(DbContextOptions<StockPulseDbContext> options) : base(options)
        {
        }
        // Define your DbSets here, for example:
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
