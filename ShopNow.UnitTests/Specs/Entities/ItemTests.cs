using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Tests.Shared.Builders;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class ItemTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnItem()
        {
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Guitarra")
                .WithCategory("Eletrônicos")
                .WithPrice(1000)
                .Generate();

            item.Id.Should().Be(1);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheVolumeOfAnItem()
        {
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Guitarra")
                .WithCategory("Eletrônicos")
                .WithPrice(1000)
                .WithWidth(100)
                .WithHeight(30)
                .WithLength(10)
                .Generate();

            var volume = item.GetVolume();

            volume.Should().Be(0.03M);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheDensityOfAnItem()
        {
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Guitarra")
                .WithCategory("Eletrônicos")
                .WithPrice(1000)
                .WithWidth(100)
                .WithHeight(30)
                .WithLength(10)
                .WithWeight(3)
                .Generate();

            var density = item.GetDensity();

            density.Should().Be(100);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheMinimumFreightOfAnItem()
        {
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Cabo")
                .WithCategory("Eletrônicos")
                .WithPrice(30)
                .WithWidth(10)
                .WithHeight(10)
                .WithLength(10)
                .WithWeight(0.9M)
                .Generate();

            var freight = item.GetFreight();

            freight.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToCalculateTheFreightOfAnItem()
        {
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Guitarra")
                .WithCategory("Eletrônicos")
                .WithPrice(1000)
                .WithWidth(100)
                .WithHeight(30)
                .WithLength(10)
                .WithWeight(3)
                .Generate();

            var freight = item.GetFreight();

            freight.Should().Be(30);
        }

    }
}
