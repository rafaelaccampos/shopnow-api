using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.Infra.Data.Repositories.Database
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task Save(Order order)
        {
            _shopContext.Orders.Add(order);
            await _shopContext.SaveChangesAsync();
        }
    }
}
