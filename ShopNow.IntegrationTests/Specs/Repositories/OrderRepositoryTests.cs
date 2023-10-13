using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class OrderRepositoryTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToSaveOrder()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var coupon = new Coupon("VALE20", 20);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 28);
            var order = new Order(cpf, issueDate, 1);

            order.AddItem(item, 2);
            order.AddCoupon(coupon);

            _context.Orders.AddRange(order);
            await _context.SaveChangesAsync();

            var orderInDatabase = _context.Orders.FirstOrDefault();

            using (new AssertionScope())
            {
                order.Should().BeEquivalentTo(orderInDatabase);
                order.OrderItems.Should().Contain(orderInDatabase!.OrderItems);
            }
        }
    }
}
