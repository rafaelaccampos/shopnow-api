
using ShopNow.Domain.Repositories;

namespace ShopNow.UseCases
{
    public class ValidateCoupon
    {
        private readonly ICouponRepository _couponRepository;

        public ValidateCoupon(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<bool> Execute(string code)
        {
            var coupon = await _couponRepository.FindByCode(code);
            return coupon != null
                ? coupon.IsValid() 
                : false;
        }
    }
}
