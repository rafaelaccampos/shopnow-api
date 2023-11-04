using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<int> Count();

        Task Save(Order order);
    }
}
