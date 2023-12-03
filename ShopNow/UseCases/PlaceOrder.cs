using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Domain.Shared.Event;
using ShopNow.Dtos;
using ShopNow.Infra.Shared.Event;

namespace ShopNow.UseCases
{
    public class PlaceOrder
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly EventBus _eventBus;

        public PlaceOrder(IAbstractRepositoryFactory abstractRepositoryFactory, EventBus eventBus)
        {
            _itemRepository = abstractRepositoryFactory.CreateItemRepository();
            _orderRepository = abstractRepositoryFactory.CreateOrderRepository();
            _couponRepository = abstractRepositoryFactory.CreateCouponRepository();
            _eventBus = eventBus;
        }

        public async Task<PlaceOrderOutput> Execute(PlaceOrderInput placeOrderInput)
        {
            var sequence = await _orderRepository.Count(); 
            var order = new Order(placeOrderInput.Cpf, placeOrderInput.IssueDate, ++sequence);
            decimal freight = 0;

            foreach (var orderItem in placeOrderInput.OrderItems)
            {
                var item = await _itemRepository.FindByIdAsync(orderItem.IdItem);
                freight += item.GetFreight() * orderItem.Count;
                order.AddItem(item!, orderItem.Count);
            }

            order.AddFreight(freight);

            if (placeOrderInput.Coupon != null)
            {
                var coupon = await _couponRepository.FindByCode(placeOrderInput.Coupon);
                order.AddCoupon(coupon!);
            }

            await _orderRepository.Save(order);

            var items = order.OrderItems.Select(i =>
                new ItemDto
                {
                    Id = i.IdItem,
                    Count = i.Count
                }).ToList();

            await _eventBus.Publish(new OrderPlaced(order.Code, items));
            return new PlaceOrderOutput(order);
        }
    }
}
