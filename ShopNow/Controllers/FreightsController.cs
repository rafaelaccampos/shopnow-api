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

        public FreightsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SimulateFreightInput simulateFreightInput)
        {
            var simulateFreight = new SimulateFreight(_itemRepository);
            var freight = await simulateFreight.Execute(simulateFreightInput);

            return Ok(freight);
        }
    }
}
