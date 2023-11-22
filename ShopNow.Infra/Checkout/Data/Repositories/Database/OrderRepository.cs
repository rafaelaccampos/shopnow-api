using Microsoft.EntityFrameworkCore;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.Infra.Checkout.Data.Repositories.Database
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<int> Count()
        {
            return await _shopContext.Orders
                .CountAsync();
        }

        public async Task Save(Order order)
        {
            _shopContext.Orders.Add(order);
            await _shopContext.SaveChangesAsync();
        }
    }
}
