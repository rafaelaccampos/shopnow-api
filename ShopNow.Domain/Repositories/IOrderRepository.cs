using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> FindByCode(string code);

        Task<IEnumerable<Order?>> FindAllOrders();

        Task Save(Order order);
    }
}
