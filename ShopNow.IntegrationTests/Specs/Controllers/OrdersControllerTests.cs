using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Handlers;
using ShopNow.Dtos;
using ShopNow.Infra.Checkout.Data.Queries;
using ShopNow.Infra.Shared.Event;
using ShopNow.Infra.Stock.Repositories;
using ShopNow.IntegrationTests.Setup;
using ShopNow.Tests.Shared.Extensions;
using System.Net;

namespace ShopNow.IntegrationTests.Specs.Controllers
{
    public class OrdersControllerTests : ApiBase
    {
        private const string URL_BASE = "/orders";
        private EventBus _eventBus;

        [SetUp]
        public void SetUp()
        {
            _eventBus = GetService<EventBus>();
        }

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
                Coupon = coupon.Code
            };

            var consumer = new Consumer
            {
                EventName = "OrderPlaced",
                Handler = new OrderPlacedStockHandler(new StockRepository(_context))
            };
            _eventBus.Subscribe(consumer);

            var response = await _httpClient.PostAsync(URL_BASE, placeOrderInput.ToJsonContent());
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedResponseContent = new
            { 
                OrderCode = $"{DateTime.Now.Year}00000001",
                Total = 4872M,
                Freight = 280M,
            }.Serialize();

            var stocks = _context.Stocks.ToList();
            var expectedStocks = new List<StockEntry>
            {
                new StockEntry(1, "out", 1),
                new StockEntry(2, "out", 1),
                new StockEntry(3, "out", 3)
            };

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseContent.ShouldBeAnEquivalentJson(expectedResponseContent);
                stocks.Should().BeEquivalentTo(expectedStocks, options 
                    => options.ExcludingMissingMembers()
                    .Excluding(x => x.Id)
                    .Excluding(x => x.Item));
            }
        }

        [Test]
        public async Task CancelShouldBeAbleToCancelOrder()
        {
            var items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
            };
            _context.AddRange(items);
            await _context.SaveChangesAsync();

            var cpf = Faker.Person.Cpf(false);
            var issueDate = DateTime.Now;
            var order = new Order(cpf, issueDate);
            var orderCode = order.Code;
            order.AddItem(items.First(), 1);
            order.AddItem(items.Last(), 2);
            _context.Add(order);
            await _context.SaveChangesAsync();

            var consumer = new Consumer
            {
                EventName = "OrderCancelled",
                Handler = new OrderCancelledStockHandler(new StockRepository(_context))
            };
            _eventBus.Subscribe(consumer);

            await _httpClient.PutAsync(URL_BASE, orderCode!.ToJsonContent());

            var stocks = _context.Stocks.ToList();
            var expectedStocks = new List<StockEntry>()
            {
                new StockEntry(items.First().Id, "in", 1),
                new StockEntry(items.Last().Id, "in", 2)
            };

            stocks.Should().BeEquivalentTo(expectedStocks, 
                options => options.ExcludingMissingMembers()
            .Excluding(o => o.Id)
            .Excluding(o => o.Item));
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
                    Code = orders.First().Code!,
                    Cpf = orders.First().CpfNumber!,
                    Freight = orders.First().Freight,
                    Status = orders.First().Status,
                    OrderItems = orders.First().OrderItems.Select(oi => new OrderItemDTO { Description = oi.Item.Description, Price = oi.Item.Price, Count = oi.Count }).ToList(),
                    Total = orders.First().GetTotal()
                },
                new OrderDTO
                {
                    Id = orders.Last().Id,
                    Code = orders.Last().Code!,
                    Cpf = orders.Last().CpfNumber!,
                    Freight = orders.Last().Freight,
                    Status = orders.Last().Status,
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
                Code = orders.First().Code!,
                Cpf = orders.First().CpfNumber!,
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

        [Test]
        public async Task GetShouldBeAbleToReturn200AndAnEmptyCollectionWhenGetOrdersDoesNotHaveElements()
        {
            var response = await _httpClient.GetAsync(URL_BASE);
            var responseOrdersAsJson = await response.Content.ReadAsStringAsync();
            var expectedOrders = new List<OrderDTO>().Serialize();

            using(new AssertionScope())
            {
                response.Should().HaveStatusCode(HttpStatusCode.OK);
                responseOrdersAsJson.Should().BeEquivalentTo(expectedOrders);
            }
        }

        [Test]
        public async Task GetShouldBeAbleToReturn404AndNullWhenGetOrderByCodeIsNull()
        {
            const string CODE = "200300001";
            var response = await _httpClient.GetAsync($"{URL_BASE}/{CODE}");
            var responseOrderAsJson = await response.Content.ReadAsStringAsync();
            
            using (new AssertionScope())
            {
                response.Should().HaveStatusCode(HttpStatusCode.NotFound);
                responseOrderAsJson.Should().BeNullOrEmpty();
            }
        }

        [Test]
        public async Task CreateShouldBeAbleToReturn400WhenPlaceOrderIsInvalid()
        {
            //Arrange
            var placeOrderInput = new PlaceOrderInput { };

            //Act
            var response = await _httpClient.PostAsync(URL_BASE, placeOrderInput.ToJsonContent());
            var responseOrderAsJson = await response.Content.ReadAsStringAsync();

            var expectedMessage = $@"{{
                ""errors"": {{
                    ""Cpf"": [""O cpf é obrigatório!""],
                    ""Cpf"": [""O pedido precisa ter pelo menos um pedido!""],
                    ""OrderItems"": [""O pedido precisa ter pelo menos um pedido!""],
                }}
            }}";

            //Assert
            using (new AssertionScope())
            {
                response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
                responseOrderAsJson.ShouldContainJsonSubtree(expectedMessage);
            }
        }
    }
}
