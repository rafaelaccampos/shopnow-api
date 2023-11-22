using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Tests.Shared.Builders
{
    public class OrderItemBuilder : BaseBuilder<OrderItem>
    {
        public OrderItemBuilder()
        {
            RuleFor(o => o.IdItem, f => f.Random.Int(1));
            RuleFor(o => o.Price, f => f.Random.Decimal(18, 2));
            RuleFor(o => o.Count, f => f.Random.Int(1));
        }

        public OrderItem WithIdItem(int idItem)
        {
            RuleFor(o => o.IdItem, idItem);
            return this;
        }

        public OrderItem WithPrice(decimal price)
        {
            RuleFor(o => o.Price, price);
            return this;
        }

        public OrderItem WithCount(decimal count)
        {
            RuleFor(o => o.Count, count);
            return this;
        }
    }
}
