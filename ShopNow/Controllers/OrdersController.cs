using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrder _placeOrder;
        private readonly ListOrders _listOrders;
        private readonly FindOrderByCode _findOrderByCode;

        public OrdersController(
            PlaceOrder placeOrder,
            ListOrders listOrders,
            FindOrderByCode findOrderByCode)
        {
            _placeOrder = placeOrder;
            _listOrders = listOrders;
            _findOrderByCode = findOrderByCode;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaceOrderInput placeOrderInput)
        {
            var order = await _placeOrder.Execute(placeOrderInput);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await _listOrders.Execute();
            return Ok(orders);
        }

        [HttpGet("{orderCode}")]
        public async Task<IActionResult> GetByCode(string orderCode)
        {
            var orderCodeInput = new OrderCodeInput { OrderCode = orderCode };
            var order = await _findOrderByCode.Execute(orderCodeInput);
            return Ok(order);
        }
    }
}
