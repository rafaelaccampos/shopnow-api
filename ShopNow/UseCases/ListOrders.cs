using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.UseCases
{
    public class ListOrders
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository; 
        }

        public async Task<IEnumerable<Order?>> Execute()
        {
            return await _orderRepository.FindAllOrders();
        }
    }
}
