using Moq;
using Order.Application;
using ECommerceOrder.Controllers;

namespace Order.Api.UnitTest
{
    public class OrderServiceXUnit
    {
        private Mock<IOrderService> _orderService;

        public OrderServiceXUnit()
        {
            _orderService = new Mock<IOrderService>();
        }
        [Fact]
        public async Task GetOrderList_OrderList()
        {
            //arrange
            var orderList = GetOrdersData();
            _orderService.Setup(x => x.GetAllOrdersAsync())
                .Returns((Task<IEnumerable<Domain.Entities.Order>>)orderList.Select(o => new Domain.Entities.Order()
                {
                    CustomeName = o.CustomeName,
                    Id = o.Id,
                    ProductId = o.ProductId
                }));
            var orderController = new OrderController(_orderService.Object);

            //act
            var productResult = orderController.GetAllOrders();

            //assert
            Assert.NotNull(orderList);
            Assert.Equal(GetOrdersData().Count(), orderList.Count());
            Assert.Equal(GetOrdersData().ToString(), orderList.ToString());
            Assert.True(orderList.Equals(orderList));
        }

        private List<Order> GetOrdersData()
        {
            List<Order> productsData = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    ProductId = 1,
                    CustomeName = "Ram"
                },
                 new Order
                {
                    Id = 2,
                    ProductId = 2,
                    CustomeName = "Shyam"
                },
                 new Order
                {
                    Id = 3,
                    ProductId = 3,
                    CustomeName = "Hari"
                },
            };
            return productsData;
        }

        public class Order
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string CustomeName { get; set; }
        }
    }
}
