using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetAllAsync()
        {
            return await _dbContext.Order.ToListAsync();
        }

        public async Task<Domain.Entities.Order> GetByIdAsync(int id)
        {
            return await _dbContext.Order.FindAsync(id);
        }

        public async Task<Domain.Entities.Order> GetByProductIdAsync(int productId)
        {
            return await _dbContext.Order.Where(o => o.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Domain.Entities.Order product)
        {
            await _dbContext.Order.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Order product)
        {
            _dbContext.Order.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.Order.FindAsync(id);
            if (product != null)
            {
                _dbContext.Order.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
