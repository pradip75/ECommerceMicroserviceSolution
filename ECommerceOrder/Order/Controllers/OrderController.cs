using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Order.Infrastructure.Interfaces;

namespace ECommerceOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("getorder/{Id:int}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            var order = await _orderService.GetOrder(Id);
            return Ok(order);
        }

        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order.Domain.Entities.Order order)
        {
            await _orderService.CreateOrder(order);
            return Ok(order);
        }

        [HttpPut("updateorder")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order.Domain.Entities.Order order)
        {
            await _orderService.UpdateOrder(order);
            return Ok(order);
        }

        [HttpDelete("deleteorder{Id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            await _orderService.DeleteOrder(Id);
            return Ok(Id);
        }
    }
}

