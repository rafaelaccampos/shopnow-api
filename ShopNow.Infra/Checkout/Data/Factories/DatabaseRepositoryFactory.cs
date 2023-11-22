using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Infra.Checkout.Data.Repositories.Database;

namespace ShopNow.Infra.Checkout.Data.Factories
{
    public class DatabaseRepositoryFactory : IAbstractRepositoryFactory
    {
        private readonly ShopContext _shopContext;

        public DatabaseRepositoryFactory(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IItemRepository CreateItemRepository()
        {
            return new ItemRepository(_shopContext);
        }

        public ICouponRepository CreateCouponRepository()
        {
            return new CouponRepository(_shopContext);
        }

        public IOrderRepository CreateOrderRepository()
        {
            return new OrderRepository(_shopContext);
        }
    }
}
