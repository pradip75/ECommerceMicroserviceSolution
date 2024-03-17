
using Order.Application.EventConsumer;
using Order.Infrastructure.Interfaces;

namespace Order.Application
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Domain.Entities.Order>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders;
        }

        public async Task<Domain.Entities.Order> GetOrder(int Id)
        {
            var order = await _orderRepository.GetByIdAsync(Id);
            return order;
        }

        public async Task<Domain.Entities.Order> GetOrderByProductId(int productId)
        {
            var order = await _orderRepository.GetByProductIdAsync(productId);
            return order;
        }

        public async Task<Domain.Entities.Order> CreateOrder(Domain.Entities.Order order)
        {
            await _orderRepository.AddAsync(order);
            return order;
        }

        public async Task<Domain.Entities.Order> UpdateOrder(Domain.Entities.Order order)
        {
            await _orderRepository.UpdateAsync(order);
            return order;
        }

        public async Task<int> DeleteOrder(int Id)
        {
            await _orderRepository.DeleteAsync(Id);
            return Id;
        }
    }
}
