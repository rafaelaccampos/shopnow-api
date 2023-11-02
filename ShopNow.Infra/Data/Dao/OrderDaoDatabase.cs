using ShopNow.Infra.Data.Queries;

namespace ShopNow.Infra.Data.Dao
{
    public class OrderDAODatabase : IOrderDAO
    {
        public Task<OrderDTO> GetOrder(string code)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderItemDTO>> GetOrderItems(int idOrder)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderDTO>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
