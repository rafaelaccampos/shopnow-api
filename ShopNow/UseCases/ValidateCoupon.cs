
using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.UseCases
{
    public class ValidateCoupon
    {
        private readonly ICouponRepository _couponRepository;

        public ValidateCoupon(IAbstractRepositoryFactory abstractRepositoryFactory)
        {
            _couponRepository = abstractRepositoryFactory.CreateCouponRepository();
        }

        public async Task<bool> Execute(string code, DateTime actualDate = new DateTime())
        {
            var coupon = await _couponRepository.FindByCode(code);
            return coupon != null && coupon.IsValid(actualDate);
        }
    }
}
