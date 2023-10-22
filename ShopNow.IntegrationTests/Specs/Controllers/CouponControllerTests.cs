using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.IntegrationTests.Setup;
using ShopNow.Tests.Shared.Extensions;
using System.Net;

namespace ShopNow.IntegrationTests.Specs.Controllers
{
    public class CouponControllerTests : ApiBase
    {
        private readonly string URL_BASE = "/coupons";

        [Test]
        public async Task GetShouldBeAbleToGetAValidCouponByCode()
        {
            var expiredDate = DateTime.Now.AddDays(1);
            var coupon = new Coupon("VALE20", 20, expiredDate);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var response = await _httpClient.GetAsync($"{URL_BASE}/{coupon.Code}");
            var responseAsJson = await response.Content.ReadAsStringAsync();
            var expectedValidateCoupon = true.Serialize();
 
            using(new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseAsJson.ShouldBeAnEquivalentJson(expectedValidateCoupon);
            }
        }

        [Test]
        public async Task GetShouldBeAbleToGetAnInvalidCoupon()
        {
            var couponCode = "202300000001";

            var response = await _httpClient.GetAsync($"{URL_BASE}/{couponCode}");
            var responseAsJson = await response.Content.ReadAsStringAsync();
            var expectedValidateCoupon = false.Serialize();

            using(new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseAsJson.ShouldBeAnEquivalentJson(expectedValidateCoupon);
            }
        }
    }
}
