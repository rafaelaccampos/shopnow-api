using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task Save(Order order);
    }
}
