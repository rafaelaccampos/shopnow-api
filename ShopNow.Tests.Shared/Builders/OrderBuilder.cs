using ShopNow.Domain.Entities;

namespace ShopNow.Tests.Shared.Builders
{
    public class OrderBuilder : BaseBuilder<Order>
    {
        public OrderBuilder()
        {
            RuleFor(o => o.Sequence, f => f.Random.Int());
            RuleFor(o => o.IssueDate, f => f.Date.Recent());
        }

        public OrderBuilder WithSequence(int sequence)
        {
            RuleFor(o => o.Sequence, sequence); 
            return this;
        }

        public OrderBuilder WithIssueDate(DateTime date)
        {
            RuleFor(o => o.IssueDate, date);
            return this;
        }
    }
}
