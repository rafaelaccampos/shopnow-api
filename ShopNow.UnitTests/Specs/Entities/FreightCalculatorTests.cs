using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Services;
using ShopNow.Tests.Shared.Builders;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class FreightCalculatorTests
    {
        [Test]
        public void ShouldBeAbleToCalculateFreightWhenFreightIsLessThanTen()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10);

            var freight = FreightCalculator.Calculate(item);

            freight.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToCalculateFreightWhenFreightIsHigherThanTen()
        {
            var item = new ItemBuilder()
                    .WithId(1)
                    .WithDescription("Amplificador")
                    .WithCategory("Eletrônicos")
                    .WithPrice(5000)
                    .WithWidth(50)
                    .WithHeight(50)
                    .WithLength(50)
                    .WithWeight(22)
                    .Generate();

            //var item = new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22);

            var freight = FreightCalculator.Calculate(item);

            freight.Should().Be(220);
        }
    }
}
