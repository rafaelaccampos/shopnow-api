using Bogus.Extensions.Brazil;
using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Infra.Data.Dao;
using ShopNow.Infra.Data.Queries;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Dao
{
    public class OrderDAOTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToGetOrderByCode()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 11, 02);

            var orders = new List<Order>()
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2)
            };

            orders.ForEach(o => o.AddItem(item, 2));
            _context.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderDao = GetService<IOrderDAO>();
            const string CODE = "202300000002";
            var orderInDatabase = await orderDao.GetOrder(CODE);

            var expectedOrder = new OrderDTO
            {
                Id = orders.Last().Id,
                Cpf = cpf,
                Code = CODE,
                Freight = orders.Last().Freight,
                OrderItems = orders.Last().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                Total = orders.Last().GetTotal()
            };

            orderInDatabase.Should().BeEquivalentTo(expectedOrder);
        }

        [Test]
        public async Task ShouldReturnNullWhenOrderDoesNotExists()
        {
            const string CODE = "202300000002";
            var orderDao = GetService<IOrderDAO>();
            var order = await orderDao.GetOrder(CODE);

            order.Should().BeNull();
        }

        [Test]
        public async Task ShouldBeAbleToGetAllOrders()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 11, 03);
            var orders = new List<Order>
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2),
            };
            _context.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderDao = GetService<IOrderDAO>();
            var ordersFromDatabase = await orderDao.GetOrders();

            var expectedOrders = new List<OrderDTO>
            {
                new OrderDTO
                {
                    Id = orders.First().Id,
                    Cpf = cpf,
                    Code = orders.First().Code,
                    Freight = orders.First().Freight,
                    OrderItems = orders.First().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Total = orders.First().GetTotal()
                },
                new OrderDTO
                {
                    Id = orders.Last().Id,
                    Cpf = cpf,
                    Code = orders.Last().Code,
                    Freight = orders.Last().Freight,
                    OrderItems = orders.Last().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Total = orders.Last().GetTotal()
                },
            };

            ordersFromDatabase.Should().BeEquivalentTo(expectedOrders);
        }

        [Test]
        public async Task ShouldReturnNullWhenListOfOrdersIsEmpty()
        {
            var orderDao = GetService<IOrderDAO>();
            var orders = await orderDao.GetOrders();

            orders.Should().BeEmpty();
        }
    }
}
