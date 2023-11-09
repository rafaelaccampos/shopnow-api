using ShopNow.Domain.Entities;
using ShopNow.Domain.Factory;
using ShopNow.Domain.Repositories;
using ShopNow.Domain.Services;
using ShopNow.Dtos;

namespace ShopNow.UseCases
{
    public class PlaceOrder
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;

        public PlaceOrder(IAbstractRepositoryFactory abstractRepositoryFactory)
        {
            _itemRepository = abstractRepositoryFactory.CreateItemRepository();
            _orderRepository = abstractRepositoryFactory.CreateOrderRepository();
            _couponRepository = abstractRepositoryFactory.CreateCouponRepository();
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
            return new PlaceOrderOutput(order);
        }
    }
}
