using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Dtos;
using ShopNow.Infra.Checkout.Data.Dao;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrder _placeOrder;
        private readonly CancelOrder _cancelOrder;
        private readonly IOrderDAO _orderDao;

        public OrdersController(
            PlaceOrder placeOrder,
            CancelOrder cancelOrder,
            IOrderDAO orderDao)
        {
            _placeOrder = placeOrder;
            _cancelOrder = cancelOrder;
            _orderDao = orderDao;
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
            var orders = await _orderDao.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{orderCode}")]
        public async Task<IActionResult> GetByCode(string orderCode)
        {
            var order = await _orderDao.GetOrder(orderCode);

            if(order == null)
            {
                return NotFound(order);
            }

            return Ok(order);
        }

        [HttpPut]
        public async Task Cancel([FromBody] string orderCode)
        {
            await _cancelOrder.Execute(orderCode);
        }
    }
}
