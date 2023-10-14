using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> FindByCode(string code);

        Task<List<Order?>> FindAllOrders();

        Task Save(Order order);
    }
}
