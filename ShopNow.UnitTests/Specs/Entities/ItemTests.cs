using FluentAssertions;
using ShopNow.Domain.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class ItemTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnItem()
        {
            var item = new Item("Guitarra", "Eletrônicos", 1000);
            item.Id.Should().Be(0);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheVolumeOfAnItem()
        {
            var item = new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10);
            var volume = item.GetVolume();

            volume.Should().Be(0.03M);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheDensityOfAnItem()
        {
            var item = new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3);
            var density = item.GetDensity();

            density.Should().Be(100);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheMinimumFreightOfAnItem()
        {
            var item = new Item("Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M);
            var freight = item.GetFreight();

            freight.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheFreightOfAnItem()
        {
            var item = new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3);
            var freight = item.GetFreight();

            freight.Should().Be(30);
        }

    }
}
