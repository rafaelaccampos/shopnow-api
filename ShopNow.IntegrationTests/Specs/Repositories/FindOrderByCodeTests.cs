using Bogus.Extensions.Brazil;
using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class FindOrderByCodeTests : DatabaseBase
    {

        [Test]
        public async Task ShouldBeAbleToFindOrderByOrderCode()
        {
            var cpf = Faker.Person.Cpf(false);
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var orders = new List<Order>()
            {
                new Order(cpf, new DateTime(2023, 09, 30), 1),
                new Order(cpf, new DateTime(2023, 09, 29), 2)
            };

            foreach(var order in orders)
            {
                order.AddItem(item, 1);
            }

            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderRepository = GetService<IOrderRepository>();
            var findOrderByCode = new FindOrderByCode(orderRepository);

            var orderCodeInput = new OrderCodeInput
            {
                OrderCode = "202300000001"
            };

            var orderInDatabase = await findOrderByCode.Execute(orderCodeInput);

            orders.First().Should().BeEquivalentTo(orderInDatabase);
        }
    }
}
