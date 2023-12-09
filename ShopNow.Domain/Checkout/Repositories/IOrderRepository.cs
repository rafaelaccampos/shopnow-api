using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Domain.Checkout.Repositories
{
    public interface IOrderRepository
    {
        Task<int> Count();

        Task<Order?> Get(string code);

        Task Update(Order? order);

        Task Save(Order order);
    }
}
