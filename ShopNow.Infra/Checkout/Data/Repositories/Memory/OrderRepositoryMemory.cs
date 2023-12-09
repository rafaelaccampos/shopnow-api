using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.Infra.Checkout.Data.Repositories.Memory
{
    public class OrderRepositoryMemory : IOrderRepository
    {
        private readonly IList<Order> _orders = new List<Order>();

        public async Task<int> Count()
        {
            return _orders.Count();
        }

        public Task<Order?> Get(string code)
        {
            var order = _orders.FirstOrDefault(o => o.Code == code);
            return Task.FromResult(order);
        }

        public Task Update(Order? order)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Order order)
        {
            _orders.Add(order);
        }
    }
}
