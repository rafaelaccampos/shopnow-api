using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreightsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly SimulateFreight _simulateFreight;

        public FreightsController(
            IItemRepository itemRepository, 
            SimulateFreight simulateFreight)
        {
            _itemRepository = itemRepository;
            _simulateFreight = simulateFreight;

        }

        [HttpPost]
        public async Task<IActionResult> Post(SimulateFreightInput simulateFreightInput)
        {
            var freight = await _simulateFreight.Execute(simulateFreightInput);
            return Ok(freight);
        }
    }
}
