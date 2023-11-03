using ShopNow.Infra.Data.Queries;

namespace ShopNow.Infra.Data.Dao
{
    public interface IOrderDAO
    {
        Task<IEnumerable<OrderDTO?>> GetOrders();

        Task<OrderDTO?> GetOrder(string code);
    }
}
