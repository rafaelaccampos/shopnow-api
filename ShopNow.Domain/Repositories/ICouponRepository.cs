using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon?> FindByCode(string code);
    }
}
