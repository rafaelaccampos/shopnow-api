using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class ValidateCouponTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToVerifyAValidCoupon()
        {
            var coupon = new Coupon(
                "VALE20", 
                20, 
                new DateTime(2023, 09, 28), 
                new DateTime(2023, 09, 27));

            _context.Coupons.Add(coupon);
            _context.SaveChanges();

            var couponRepository = GetService<ICouponRepository>();
            var validateCoupon = new ValidateCoupon(couponRepository);
            var isValid = await validateCoupon.Execute("VALE20");

            isValid.Should().BeTrue();
        }

    }
}
