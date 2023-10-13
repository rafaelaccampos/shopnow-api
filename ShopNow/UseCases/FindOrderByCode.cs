using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;

namespace ShopNow.UseCases
{
    public class FindOrderByCode
    {
        private readonly IOrderRepository _orderRepository;

        public FindOrderByCode(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> Execute(OrderCodeInput orderCodeInput)
        {
            return await _orderRepository.FindByCode(orderCodeInput.OrderCode);
        }
    }
}
