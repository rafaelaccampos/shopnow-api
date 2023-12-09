using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Domain.Shared.Event;
using ShopNow.Infra.Shared.Event;

namespace ShopNow.UseCases
{
    public class CancelOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly EventBus _eventBus;

        public CancelOrder(IAbstractRepositoryFactory abstractRepositoryFactory, EventBus eventBus)
        {
            _orderRepository = abstractRepositoryFactory.CreateOrderRepository();
            _eventBus = eventBus;
        }

        public async Task Execute(string code)
        {
            var order = await _orderRepository.Get(code);

            if(order != null)
            {
                order!.Cancel();

                await _orderRepository.Update(order);

                var items = order.OrderItems.Select(i => new ItemDto
                {
                    Id = i.IdItem,
                    Count = i.Count,
                }).ToList();

                await _eventBus.Publish(new OrderCancelled(order.Code, items));
            }
        }
    }
}
