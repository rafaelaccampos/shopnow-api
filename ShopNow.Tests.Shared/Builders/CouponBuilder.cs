using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Tests.Shared.Builders
{
    public class CouponBuilder : BaseBuilder<Coupon>
    {
        public CouponBuilder()
        {
            RuleFor(c => c.Code, f => f.Random.String2(50));
            RuleFor(c => c.Percentual, f => f.Random.Decimal(18, 2));
            RuleFor(c => c.ExpiredDate, f => f.Date.Recent());
            RuleFor(c => c.ActualDate, f => f.Date.Recent());
        }

        public CouponBuilder WithCode(string code)
        {
            RuleFor(f => f.Code, code);
            return this;
        }

        public CouponBuilder WithPercentual(decimal percentual)
        {
            RuleFor(f => f.Percentual, percentual);
            return this;
        }

        public CouponBuilder WithExpiredDate(DateTime date)
        {
            RuleFor(f => f.ExpiredDate, date);
            return this;
        }

        public CouponBuilder WithActualDate(DateTime date) 
        { 
            RuleFor(f => f.ActualDate, date);
            return this;
        }
    }
}
