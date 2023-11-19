using Microsoft.AspNetCore.Mvc;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly ValidateCoupon _validateCoupon;

        public CouponsController(
            ValidateCoupon validateCoupon)
        {
            _validateCoupon = validateCoupon;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var couponIsValid = await _validateCoupon.Execute(code, DateTime.Now);
            return Ok(couponIsValid);
        }
    }
}
