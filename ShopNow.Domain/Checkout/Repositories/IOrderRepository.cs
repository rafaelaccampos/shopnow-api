using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Domain.Checkout.Repositories
{
    public interface IOrderRepository
    {
        Task<int> Count();

        Task<Order?> Get(string code);

        Task Save(Order order);
    }
}
