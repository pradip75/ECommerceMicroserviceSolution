using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public interface IOrderService
    {
        Task<IEnumerable<Domain.Entities.Order>> GetAllOrdersAsync();
        Task<Domain.Entities.Order> GetOrder(int Id);
        Task<Domain.Entities.Order> GetOrderByProductId(int productId);
        Task<Domain.Entities.Order> CreateOrder(Domain.Entities.Order order);
        Task<Domain.Entities.Order> UpdateOrder(Domain.Entities.Order order);
        Task<int> DeleteOrder(int Id);
    }
}
