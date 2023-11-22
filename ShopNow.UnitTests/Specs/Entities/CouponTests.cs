using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;

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
            var expiredDate = new DateTime(2022, 09, 28);
            var actualDate = new DateTime(2022, 09, 18);
            var coupon = new Coupon("VALE35", 35, expiredDate, actualDate);

            coupon.AddDiscount(1000);

            coupon.Discount
                .Should()
                .Be(350);
        }

        [Test]
        public void ShouldNotBeAbleToApplyDiscountIfCouponHasExpired()
        {
            var expiredDate = DateTime.Now.AddDays(-2);
            var actualDate = DateTime.Now;
            var coupon = new Coupon("VALE20", 20, expiredDate, actualDate);

            coupon.AddDiscount(1000);

            coupon.Discount
                  .Should()
                  .Be(0);
        }

        [Test]
        public void ShouldBeAbleToVerifyIfCouponIsValid()
        {
            var expiredDate = new DateTime(2023, 09, 28);
            var actualDate = new DateTime(2023, 09, 27);
            var coupon = new Coupon("VALE20", 20, expiredDate, actualDate);

            coupon.IsValid(actualDate).Should().BeTrue();
        }

        [Test]
        public void ShouldBeAbleToVerifyIfCouponIsNotValid()
        {
            var expiredDate = new DateTime(2023, 09, 28);
            var actualDate = DateTime.Now;
            var coupon = new Coupon("VALE20", 20, expiredDate, actualDate);

            coupon.IsValid(actualDate).Should().BeFalse();
        }

        [Test]
        public void ShouldBeAbleToVerifyIfCouponIsValidWithoutActualDateWhenExpiredDateHasNotExpired()
        {
            var coupon = new Coupon("VALE20", 20, DateTime.Now.AddDays(1));
            coupon.IsValid().Should().BeTrue();
        }

        [Test]
        public void ShouldBeAbleToVerifyIfCouponIsValidWithoutActualDateWhenExpiredDateIsExpired()
        {
            var coupon = new Coupon("VALE20", 20, DateTime.Now.AddDays(-1));
            coupon.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldBeAbleToVerifyIfCouponIsValidWhenActualDateAndInvalidDateAreEquals()
        {
            var expiredDate = new DateTime(2023, 09, 28, 14, 30, 00);
            var actualDate = new DateTime(2023, 09, 28, 14, 30, 00);
            var coupon = new Coupon("VALE20", 20, expiredDate, actualDate);
            coupon.IsValid(actualDate).Should().BeTrue();
        }
    }
}