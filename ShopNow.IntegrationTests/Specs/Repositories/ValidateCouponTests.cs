using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class ValidateCouponTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToVerifyAValidCoupon()
        {
            var actualDate = new DateTime(2023, 09, 27);
            var coupon = new Coupon(
                "VALE20", 
                20, 
                new DateTime(2023, 09, 28), 
                actualDate);

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var validateCoupon = GetService<ValidateCoupon>();
            var isValid = await validateCoupon.Execute("VALE20", actualDate);

            isValid.Should().BeTrue();
        }

        [Test]
        public async Task ShouldBeAbleToVerifyAnInvalidCoupon()
        {
            var actualDate = new DateTime(2023, 09, 29);
            var coupon = new Coupon(
                "VALE20",
                20,
                new DateTime(2023, 09, 28),
                actualDate);

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var validateCoupon = GetService<ValidateCoupon>();
            var isValid = await validateCoupon.Execute("VALE20", actualDate);

            isValid.Should().BeFalse();
        }

        [Test]
        public async Task ShouldBeAbleToInvalidateAnCouponThatNotExists()
        {
            var validateCoupon = GetService<ValidateCoupon>();
            var isInvalid = await validateCoupon.Execute("VALE20");

            isInvalid.Should().BeFalse();
        }
    }
}
