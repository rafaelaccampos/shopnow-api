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

        public async Task<Order?> FindByCode(string code)
        {
            return await _shopContext.Orders
                .Include(c => c.OrderItems)
                .ThenInclude(c => c.Item)
                .Include(c => c.Coupon)
                .SingleAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Order?>> FindAllOrders()
        {
            return await _shopContext.Orders
                .Include(c => c.OrderItems)
                .ThenInclude(c => c.Item)
                .Include(c => c.Coupon)
                .ToListAsync();
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
