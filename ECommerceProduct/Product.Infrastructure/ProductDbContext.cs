using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Product> Product { get; set; }
    }
}
