using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Tests.Shared.Builders
{
    public class ItemBuilder : BaseBuilder<Item>
    {
        public ItemBuilder()
        {
            RuleFor(i => i.Id, f => f.Random.Int());
            RuleFor(i => i.Price, f => f.Random.Decimal(18, 2));
            RuleFor(i => i.Width, f => f.Random.Decimal(18, 2));
            RuleFor(i => i.Weight, f => f.Random.Decimal(18, 2));
            RuleFor(i => i.Height, f => f.Random.Decimal(18, 2));
            RuleFor(i => i.Length, f => f.Random.Decimal(18, 2));
            RuleFor(i => i.Category, f => f.Random.String2(50));
            RuleFor(i => i.Description, f => f.Random.String2(50));
        }

        public ItemBuilder WithId(int id)
        {
            RuleFor(i => i.Id, id);
            return this;
        }

        public ItemBuilder WithPrice(decimal price)
        {
            RuleFor(i => i.Price, price);
            return this;
        }

        public ItemBuilder WithWidth(decimal width)
        {
            RuleFor(i => i.Width, width);
            return this;
        }

        public ItemBuilder WithWeight(decimal weight)
        {
            RuleFor(i => i.Weight, weight);
            return this;
        }

        public ItemBuilder WithHeight(decimal height)
        {
            RuleFor(i => i.Height, height);
            return this;
        }

        public ItemBuilder WithLength(decimal length)
        {
            RuleFor(i => i.Length, length);
            return this;
        }

        public ItemBuilder WithCategory(string category)
        {
            RuleFor(i => i.Category, category);
            return this;
        }

        public ItemBuilder WithDescription(string description)
        {
            RuleFor(i => i.Description, description);
            return this;
        }
    }
}
