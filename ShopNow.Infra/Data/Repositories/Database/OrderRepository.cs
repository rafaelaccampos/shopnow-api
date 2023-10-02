using Microsoft.EntityFrameworkCore;
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
            await _shopContext.Orders.AddAsync(order);
            _shopContext.Entry(order.OrderItems).State = EntityState.Detached;
            await _shopContext.SaveChangesAsync();
        }
    }
}
