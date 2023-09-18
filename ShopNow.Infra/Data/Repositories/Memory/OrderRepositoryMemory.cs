using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.Infra.Data.Repositories.Memory
{
    public class OrderRepositoryMemory : IOrderRepository
    {
        private readonly IList<Order> _orders = new List<Order>();

        public async Task Save(Order order)
        {
            _orders.Add(order);
        }
    }
}
