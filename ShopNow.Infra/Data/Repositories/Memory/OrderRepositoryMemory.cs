using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.Infra.Data.Repositories.Memory
{
    public class OrderRepositoryMemory : IOrderRepository
    {
        private readonly IList<Order> _orders = new List<Order>();

        public Task<IEnumerable<Order?>> FindAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> FindByCode(string code)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Order order)
        {
            _orders.Add(order);
        }

        public async Task<int> Count()
        {
            return  _orders.Count();
        }
    }
}
