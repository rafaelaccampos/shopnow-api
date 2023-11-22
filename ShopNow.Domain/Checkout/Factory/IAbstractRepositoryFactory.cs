using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.Domain.Checkout.Factory
{
    public interface IAbstractRepositoryFactory
    {
        IItemRepository CreateItemRepository();

        ICouponRepository CreateCouponRepository();

        IOrderRepository CreateOrderRepository();
    }
}
