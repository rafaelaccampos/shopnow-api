using ShopNow.Infra.Data.Queries;

namespace ShopNow.Infra.Data.Dao
{
    public interface IOrderDAO
    {
        Task<IList<OrderDTO>> GetOrders();
        Task<OrderDTO> GetOrder(string code);
        Task<IList<OrderItemDTO>> GetOrderItems(int idOrder);
    }
}
