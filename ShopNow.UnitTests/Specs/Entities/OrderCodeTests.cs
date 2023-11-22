using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class OrderCodeTests
    {
        [Test]
        public void ShouldBeAbleToGenerateOrderCode()
        {
            const int SEQUENCE = 1;

            var orderCode = new OrderCode(SEQUENCE, new DateTime(2023, 04, 09));
            var orderCodeGenerated = orderCode.GenerateCode();

            orderCodeGenerated.Should().Be("202300000001");
        }
    }
}
