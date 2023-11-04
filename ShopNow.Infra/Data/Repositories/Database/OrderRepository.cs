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
