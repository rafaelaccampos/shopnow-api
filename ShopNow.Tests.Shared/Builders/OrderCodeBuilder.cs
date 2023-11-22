using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Tests.Shared.Builders
{
    public class OrderCodeBuilder : BaseBuilder<OrderCode>
    {
        public OrderCodeBuilder() 
        {
            RuleFor(o => o.Sequence, f => f.Random.Int(1));
            RuleFor(o => o.Date, f => f.Date.Recent());
        }

        public OrderCodeBuilder WithSequence(int sequence)
        {
            RuleFor(o => o.Sequence, sequence); 
            return this;
        }

        public OrderCodeBuilder WithDate(DateTime date)
        {
            RuleFor(o => o.Date, date);
            return this;
        }
    }
}
