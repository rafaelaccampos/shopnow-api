using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Infra;
using ShopNow.Infra.Checkout.Data.Repositories.Database;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class CancelOrderTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToCancelAnOrder()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = DateTime.Now;
            var order = new Order(cpf, issueDate, 1);
            order.AddItem(item, 1);
            _context.Add(order);
            await _context.SaveChangesAsync();

            var cancelOrder = GetService<CancelOrder>();
            await cancelOrder.Execute(order.Code);

            var orderRepository = GetService<IOrderRepository>();
            var orderCanceled = await orderRepository.Get(order.Code);
            orderCanceled!.Status.Should().Be("Cancelled");
        }
    }
}
