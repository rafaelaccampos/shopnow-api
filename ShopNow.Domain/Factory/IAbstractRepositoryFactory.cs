using ShopNow.Domain.Repositories;

namespace ShopNow.Domain.Factory
{
    public interface IAbstractRepositoryFactory
    {
        IItemRepository CreateItemRepository();

        ICouponRepository CreateCouponRepository();

        IOrderRepository CreateOrderRepository();
    }
}
