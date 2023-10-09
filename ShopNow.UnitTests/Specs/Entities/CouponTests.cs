using FluentAssertions;
using ShopNow.Domain.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class CouponTests
    {
        [Test]
        public void ShouldBeAbleToCalculateDiscountWithoutExpiredDate()
        {
            var coupon = new Coupon("VALE35", 35);

            coupon.AddDiscount(1000);

            coupon.Discount
                .Should()
                .Be(350);
        }

        [Test]
        public void ShouldBeAbleToApplyDiscountWithExpiredDate()
        {
            var expireDate = new DateTime(2022, 09, 28);
            var actualDate = new DateTime(2022, 09, 18);
            var coupon = new Coupon("VALE35", 35, expireDate, actualDate);

            coupon.AddDiscount(1000);

            coupon.Discount
                .Should()
                .Be(350);
        }

        [Test]
        public void ShouldNotBeAbleToApplyDiscountIfCouponHasExpired()
        {
            var expireDate = DateTime.Now.AddDays(-2);
            var actualDate = DateTime.Now;
            var coupon = new Coupon("VALE20", 20, expireDate, actualDate);

            coupon.AddDiscount(1000);

            coupon.Discount
                  .Should()
                  .Be(0);
        }

    }
}