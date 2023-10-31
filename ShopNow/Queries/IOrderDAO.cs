namespace ShopNow.Queries
{
    public interface IOrderDAO
    {
        Task<IList<OrderDTO>> GetOrder();
        Task<OrderDTO> GetOrder(string code);
        Task<IList<OrderItemDTO>> GetOrderItems(int idOrder);
    }
}
