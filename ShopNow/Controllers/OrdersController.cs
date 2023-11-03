using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Entities;
using ShopNow.Dtos;
using ShopNow.Infra.Data.Dao;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrder _placeOrder;
        private readonly IOrderDAO _orderDao;

        public OrdersController(
            PlaceOrder placeOrder,
            IOrderDAO orderDao)
        {
            _placeOrder = placeOrder;
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
                return NotFound();
            }

            return Ok(order);
        }
    }
}
