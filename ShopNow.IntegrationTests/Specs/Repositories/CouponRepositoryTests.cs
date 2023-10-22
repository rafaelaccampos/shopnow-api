using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class CouponRepositoryTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToFindCouponByCode()
        {
            var expiredDate = new DateTime(2023, 10, 09);
            var actualDate = new DateTime(2023, 10, 07);
            var coupon = new Coupon("VALE30", 30, expiredDate, actualDate);

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var couponRepository = GetService<ICouponRepository>();
            var couponInDatabase = await couponRepository.FindByCode(coupon.Code);

            coupon.Should().BeEquivalentTo(couponInDatabase, options => options
            .ExcludingMissingMembers()
            .Excluding(x => x.ActualDate)
            .Excluding(x => x.Discount));
        }

        [Test]
        public async Task ShouldBeAbleToReturnNullWhenCouponWasNotFound()
        {
            var expiredDate = new DateTime(2023, 10, 09);
            var actualDate = new DateTime(2023, 10, 07);
            var coupon = new Coupon("VALE30", 30, expiredDate, actualDate);

            var couponRepository = GetService<ICouponRepository>();
            var couponInDatabase = await couponRepository.FindByCode(coupon.Code);

            couponInDatabase.Should().BeNull();
        }
    }
}
