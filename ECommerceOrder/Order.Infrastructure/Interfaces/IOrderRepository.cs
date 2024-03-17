using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Order.Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Domain.Entities.Order>> GetAllAsync();
        Task<Domain.Entities.Order> GetByIdAsync(int id);
        Task<Domain.Entities.Order> GetByProductIdAsync(int productId);
        Task AddAsync(Domain.Entities.Order product);
        Task UpdateAsync(Domain.Entities.Order product);
        Task DeleteAsync(int id);
    }
}
