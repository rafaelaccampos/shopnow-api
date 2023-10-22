using Microsoft.AspNetCore.Mvc;
using ShopNow.Domain.Repositories;
using ShopNow.UseCases;

namespace ShopNow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponsController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var validateCoupon = new ValidateCoupon(_couponRepository);
            var couponIsValid = await validateCoupon.Execute(code, DateTime.Now);

            return Ok(couponIsValid);
        }
    }
}
