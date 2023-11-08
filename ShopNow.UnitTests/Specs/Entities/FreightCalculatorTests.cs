using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Services;

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
            var item = new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22);

            var freight = FreightCalculator.Calculate(item);

            freight.Should().Be(220);
        }
    }
}
