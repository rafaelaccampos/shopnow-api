using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.Infra.Checkout.Data.Repositories.Database
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ShopContext _shopContext;

        public CouponRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<Coupon?> FindByCode(string code)
        {
            return await _shopContext.Coupons.FindAsync(code);
        }
    }
}
