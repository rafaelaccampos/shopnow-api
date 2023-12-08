using FluentAssertions;
using ShopNow.Tests.Shared.Builders;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class OrderCodeTests
    {
        [Test]
        public void ShouldBeAbleToGenerateOrderCode()
        {
            const int SEQUENCE = 1;
            var createdDate = new DateTime(2023, 04, 09);

            var orderCode = new OrderCodeBuilder()
                .WithSequence(SEQUENCE)
                .WithDate(createdDate)
                .Generate();
                       
            var orderCodeGenerated = orderCode.GenerateCode();

            orderCodeGenerated.Should().Be("202300000001");
        }
    }
}
