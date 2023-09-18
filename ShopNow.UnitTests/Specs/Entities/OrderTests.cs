using FluentAssertions;
using ShopNow.Domain.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class OrderTests
    {
        [Test]
        public void ShouldNotBeAbleToMakeAnOrderWithInvalidCpf()
        {
            var act = () => new Order("18731465022");

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage("Cpf is invalid!");
        }

        [Test]
        public void ShouldBeAbleToMakeAnOrderWithValidCpf()
        {
            var act = () => new Order("18731465072");

            act.Should()
               .NotThrow<InvalidOperationException>();
        }

        [Test]
        public void ShouldBeAbleToMakeAnOrderWithDescriptionPriceAndCount()
        {
            var order = new Order("18731465072");

            order.AddItem(new Item("Smartphone", "Eletrônicos", 1000.00M, 10, 10, 10, 0.9M), 2);
            order.AddItem(new Item("Table", "Móveis", 400.00M, 10, 10, 10, 0.9M), 2);

            order.GetTotal()
                 .Should()
                 .Be(2800.00M);
        }

        [Test]
        public void ShouldBeAbleToApplyDiscountInAnOrder()
        {
            var coupon = new Coupon("VALE20", 20);
            var order = new Order("18731465072");

            order.AddItem(new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3), 1);
            order.AddItem(new Item("Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20), 1);
            order.AddItem(new Item("Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M), 3);
            order.AddCoupon(coupon);

            order.GetTotal()
                 .Should()
                 .Be(4872M);
        }

        [Test]
        public void ShouldBeAbleToCalculateAnFreightOfAnOrder()
        {
            var order = new Order("18731465072");

            order.AddItem(new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3), 1);
            order.AddItem(new Item("Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20), 1);
            order.AddItem(new Item("Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M), 3);

            var freight = order.Freight;

            freight
                .Should()
                .Be(260);
        }

        [Test]
        public void ShouldBeAbleToGenerateCodeOfOrder()
        {
            var order = new Order("18731465072", new DateTime(2023, 04, 08), 1);

            order.AddItem(new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3), 1);
            order.AddItem(new Item("Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20), 1);
            order.AddItem(new Item("Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M), 3);

            var orderCode = order.Code;

            orderCode
                .Should()
                .Be("202300000001");
        }

    }
}
