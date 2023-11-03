﻿using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Dtos;
using ShopNow.Infra.Data.Queries;
using ShopNow.IntegrationTests.Setup;
using ShopNow.Tests.Shared.Extensions;
using System.Net;

namespace ShopNow.IntegrationTests.Specs.Controllers
{
    public class OrdersControllerTests : ApiBase
    {
        private const string URL_BASE = "/orders";

        [Test]
        public async Task CreateShouldBeAbleToPlaceOrderWithCoupon()
        {
            var items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 1),
            };

            _context.AddRange(items);
            await _context.SaveChangesAsync();

            var coupon = new Coupon("VALE20", 20);
            _context.Add(coupon);
            await _context.SaveChangesAsync();

            var placeOrderInput = new PlaceOrderInput()
            {
                Cpf = Faker.Person.Cpf(false),
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput
                    {
                        IdItem = 1,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 2,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 3,
                        Count = 3,
                    }
                },
                IssueDate = new DateTime(2023, 09, 28),
                Coupon = coupon.Code
            };

            var response = await _httpClient.PostAsync(URL_BASE, placeOrderInput.ToJsonContent());
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var expectedResponseContent = new PlaceOrderOutput
            { 
                OrderCode = "202300000001",
                Total = 4872M,
                Freight = 280M,
            }.Serialize();

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseContent.ShouldBeAnEquivalentJson(expectedResponseContent);
            }
        }

        [Test]
        public async Task GetShouldBeAbleToGetOrders()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 28);
            var orders = new List<Order>
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2)
            };       
            foreach(var order in orders)
            {
                order.AddItem(item, 2);
            }
            _context.AddRange(orders);
            await _context.SaveChangesAsync();

            var response = await _httpClient.GetAsync(URL_BASE);
            var responseOrderAsJson = await response.Content.ReadAsStringAsync();
            var expectedOrdersAsJson = new List<OrderDTO>
            {
                new OrderDTO
                {
                    Id = orders.First().Id,
                    Code = orders.First().Code,
                    Cpf = orders.First().CpfNumber,
                    Freight = orders.First().Freight,
                    OrderItems = orders.First().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Total = orders.First().GetTotal()
                },
                new OrderDTO
                {
                    Id = orders.Last().Id,
                    Code = orders.Last().Code,
                    Cpf = orders.Last().CpfNumber,
                    Freight = orders.Last().Freight,
                    OrderItems = orders.Last().OrderItems.Select(oi => new OrderItemDTO{ Description = oi.Item.Description, Price= oi.Item.Price, Count = oi.Count}).ToList(),
                    Total = orders.Last().GetTotal()
                }
            }.Serialize();

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseOrderAsJson.ShouldBeAnEquivalentJson(expectedOrdersAsJson);
            }        
        }

        [Test]
        public async Task GetOrderShouldBeAbleToGetAnOrderByCode()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3);
            _context.Add(item);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = new DateTime(2023, 09, 28);
            var orders = new List<Order>
            {
                new Order(cpf, issueDate, 1),
                new Order(cpf, issueDate, 2)
            };
            foreach(var order in orders)
            {
                order.AddItem(item, 2);
            }
            _context.AddRange(orders);
            await _context.SaveChangesAsync();

            var response = await _httpClient.GetAsync($"{URL_BASE}/{orders.First().Code}");
            var responseOrderAsJson = await response.Content.ReadAsStringAsync();
            var expectedOrderAsJson = new OrderDTO
            {
                Id = orders.First().Id,
                Code = orders.First().Code,
                Cpf = orders.First().CpfNumber,
                Freight = orders.First().Freight,
                OrderItems = orders.First().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                Total = orders.First().GetTotal()
            }.Serialize();

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseOrderAsJson.ShouldBeAnEquivalentJson(expectedOrderAsJson);
            }
        }
    }
}
