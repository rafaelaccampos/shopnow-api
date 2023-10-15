using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;

namespace ShopNow.UseCases
{
    public class PlaceOrder
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;

        public PlaceOrder(
            IItemRepository itemRepository, 
            IOrderRepository orderRepository,
            ICouponRepository couponRepository)
        {
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _couponRepository = couponRepository;
        }

        public async Task<PlaceOrderOutput> Execute(PlaceOrderInput placeOrderInput)
        {
            var order = new Order(placeOrderInput.Cpf, placeOrderInput.IssueDate, 1);

            foreach (var orderItem in placeOrderInput.OrderItems)
            {
                var item = await _itemRepository.FindByIdAsync(orderItem.IdItem);
                order.AddItem(item!, orderItem.Count);
            }

            if (placeOrderInput.Coupon != null)
            {
                var coupon = await _couponRepository.FindByCode(placeOrderInput.Coupon);
                order.AddCoupon(coupon!);
            }

            await _orderRepository.Save(order);
            return new PlaceOrderOutput(order);
        }
    }
}
