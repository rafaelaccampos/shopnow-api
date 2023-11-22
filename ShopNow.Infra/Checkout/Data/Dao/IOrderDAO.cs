using ShopNow.Infra.Checkout.Data.Queries;

namespace ShopNow.Infra.Checkout.Data.Dao
{
    public interface IOrderDAO
    {
        Task<IEnumerable<OrderDTO?>> GetOrders();

        Task<OrderDTO?> GetOrder(string code);
    }
}
