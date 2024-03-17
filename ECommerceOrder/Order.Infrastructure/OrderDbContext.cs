using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Order> Order { get; set; }
    }
}
