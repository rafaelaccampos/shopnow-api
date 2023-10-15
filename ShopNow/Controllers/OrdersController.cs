using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;

        public OrdersController(
            IItemRepository itemRepository,
            IOrderRepository orderRepository, 
            ICouponRepository couponRepository)
        {
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _couponRepository = couponRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaceOrderInput placeOrderInput)
        {
            var createOrder = new PlaceOrder(_itemRepository, _orderRepository, _couponRepository);
            var order = await createOrder.Execute(placeOrderInput);

            return Ok(order);
        }
    }
}
