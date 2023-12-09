﻿using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;
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
            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 28);
            
            var order = new Order(cpf, issueDate, 1);
            order.AddItem(item, 2);
            order.AddCoupon(coupon);

            var orderRepository = GetService<IOrderRepository>();
            await orderRepository.Save(order);

            var orderInDatabase = await _context
                .Orders
                .Include(o => o.Coupon)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Item)
                .FirstOrDefaultAsync();

            using (new AssertionScope())
            {
                orderInDatabase.Should().BeEquivalentTo(order, 
                    options => options
                    .For(o => o.OrderItems)
                    .Exclude(o => o.Order)
                    .For(o => o.OrderItems)
                    .Exclude(o => o.Item));
            }
        }

        [Test]
        public async Task ShouldBeAbleToGetTheCountOfOrdersThatHasInDatabase()
        {
            var cpf = Faker.Person.Cpf(false);
            var issueDate = DateTime.Now;

            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var orders = new List<Order>
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2)
            };

            orders.ForEach(order => order.AddItem(item, 1));
            _context.AddRange(orders);
            await _context.SaveChangesAsync();

            var orderRepository = GetService<IOrderRepository>();
            var count = await orderRepository.Count();

            count.Should().Be(2);
        }
    }
}
