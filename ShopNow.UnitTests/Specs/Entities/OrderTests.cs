﻿using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Tests.Shared.Builders;
using ShopNow.UnitTests.Setup;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class OrderTests : BaseTest
    {
        private string _cpf;

        [SetUp]
        public void SetUp() 
        {
            _cpf = Faker.Person.Cpf(false);
        }

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
            var act = () => new Order(_cpf);

            act.Should()
               .NotThrow<InvalidOperationException>();
        }

        [Test]
        public void ShouldBeAbleToMakeAnOrderWithDescriptionPriceAndCount()
        {
            var order = new Order(_cpf);

            order.AddItem(new Item(1, "Smartphone", "Eletrônicos", 1000.00M, 10, 10, 10, 0.9M), 2);
            order.AddItem(new Item(2,"Table", "Móveis", 400.00M, 10, 10, 10, 0.9M), 2);

            order.GetTotal()
                 .Should()
                 .Be(2800.00M);
        }

        [Test]
        public void ShouldBeAbleToApplyDiscountInAnOrder()
        {
            var coupon = new CouponBuilder()
                .WithCode("VALE20")
                .WithPercentual(20)
                .Generate();

            var order = new Order(_cpf);

            order.AddItem(new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3), 1);
            order.AddItem(new Item(2,"Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20), 1);
            order.AddItem(new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M), 3);
            order.AddCoupon(coupon);

            order.GetTotal()
                 .Should()
                 .Be(4872M);
        }

        [Test]
        public void ShouldBeAbleToGenerateCodeOfOrder()
        {
            var order = new Order(_cpf, new DateTime(2023, 04, 08), 1);

            order.AddItem(new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3), 1);
            order.AddItem(new Item(2, "Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20), 1);
            order.AddItem(new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M), 3);

            var orderCode = order.Code;

            orderCode
                .Should()
                .Be("202300000001");
        }

        [Test]
        public void ShouldBeAbleToChangeStatusForPendingWhenOrderIsCreated()
        {
            var order = new Order(_cpf, new DateTime(2023, 04, 08), 1);

            order.Status
                .Should()
                .Be("Pending");
        }

        [Test]
        public void ShouldBeAbleToChangeOrderStatusForCancelledWhenOrderIsCancelled()
        {
            var order = new Order(_cpf, new DateTime(2023, 04, 08), 1);

            order.Cancel();

            order.Status
                .Should()
                .Be("Cancelled");
        }
    }
}
