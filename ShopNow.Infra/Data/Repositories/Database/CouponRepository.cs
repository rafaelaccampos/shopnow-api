using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.Infra.Data.Repositories.Database
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
