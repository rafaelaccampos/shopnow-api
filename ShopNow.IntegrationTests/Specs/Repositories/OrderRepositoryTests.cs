using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class OrderRepositoryTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToFindOrderWithCouponByOrderCode()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var coupon = new Coupon("VALE20", 20);
            _context.Coupons.Add(coupon); 
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 30);
            var order = new Order(cpf, issueDate);

            order.AddItem(item, 2);
            order.AddCoupon(coupon);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderRepository = GetService<IOrderRepository>();
            var orderInDatabase = await orderRepository.FindByCode("202300000001");

            orderInDatabase.Should().BeEquivalentTo(order, options
                => options.Excluding(o => o.Cpf)
                .For(o => o.OrderItems)
                .Exclude(o => o.Order));
        }

        [Test]
        public async Task ShouldBeAbleToFindAllOrdersWithCoupon()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10);
            _context.Items.AddRange(item);
            await _context.SaveChangesAsync();

            var coupon = new Coupon("VALE20", 20);
            _context.Coupons.Add(coupon);

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 30);

            var orders = new List<Order>()
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2),
            };

            orders.First().AddCoupon(coupon);
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderRepository = GetService<IOrderRepository>();
            var ordersInDatabase = await orderRepository.FindAllOrders();

            ordersInDatabase.Should().BeEquivalentTo(orders, options => options.Excluding(o => o.Cpf));
        }

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
                orderInDatabase.Should().BeEquivalentTo(order);
                orderInDatabase!.OrderItems.Should().Contain(order.OrderItems);
            }
        }
    }
}
