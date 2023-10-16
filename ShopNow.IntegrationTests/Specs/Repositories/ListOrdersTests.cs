using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class ListOrdersTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToReturnAllOrders()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 20);

            var orders = new List<Order>
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2)
            };

            foreach(var order in orders)
            {
                order.AddItem(item, 1);
            }

            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderRepository = GetService<IOrderRepository>();

            var listOrders = new ListOrders(orderRepository);
            var ordersInDatabase = await listOrders.Execute();

            using(new AssertionScope())
            {
                ordersInDatabase.Should().HaveCount(2);
                orders.Should().BeEquivalentTo(ordersInDatabase, options
                    => options.Excluding(o => o.Cpf)
                    .For(o => o.OrderItems)
                    .Exclude(o => o.Order));
            }
        }
    }
}
