using Microsoft.AspNetCore.Mvc;
using ShopNow.Dtos;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreightsController : ControllerBase
    {
        private readonly SimulateFreight _simulateFreight;

        public FreightsController(
            SimulateFreight simulateFreight)
        {
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
