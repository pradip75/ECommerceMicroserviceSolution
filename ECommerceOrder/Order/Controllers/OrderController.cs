using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Interfaces;

namespace ECommerceOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("getorder/{Id:int}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            var order = await _orderRepository.GetByIdAsync(Id);
            return Ok(order);
        }

        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order.Domain.Entities.Order order)
        {
            await _orderRepository.AddAsync(order);
            return Ok(order);
        }

        [HttpPut("updateorder")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order.Domain.Entities.Order order)
        {
            await _orderRepository.UpdateAsync(order);
            return Ok(order);
        }

        [HttpDelete("deleteorder{Id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            await _orderRepository.DeleteAsync(Id);
            return Ok(Id);
        }
    }
}

