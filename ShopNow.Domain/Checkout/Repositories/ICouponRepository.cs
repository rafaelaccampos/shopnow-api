using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Domain.Checkout.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon?> FindByCode(string code);
    }
}
