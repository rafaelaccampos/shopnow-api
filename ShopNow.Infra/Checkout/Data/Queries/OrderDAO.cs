using Microsoft.EntityFrameworkCore;
using ShopNow.Infra.Checkout.Data.Dao;

namespace ShopNow.Infra.Checkout.Data.Queries
{
    public class OrderDAO : IOrderDAO
    {
        private readonly ShopContext _shopContext;

        public OrderDAO(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<OrderDTO?> GetOrder(string code)
        {
            return await _shopContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Item)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    Code = o.Code,
                    Cpf = o.CpfNumber,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Freight = o.Freight,
                    Total = o.GetTotal()
                })
                .FirstOrDefaultAsync(o => o.Code == code);
        }

        public async Task<IEnumerable<OrderDTO?>> GetOrders()
        {
            return await _shopContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Item)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    Code = o.Code,
                    Cpf = o.CpfNumber,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Freight = o.Freight,
                    Status = o.Status,
                    Total = o.GetTotal()
                })
                .ToListAsync();
        }
    }
}
