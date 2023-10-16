using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Dtos;
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
            var responseContent = await response.Deserialize<PlaceOrderOutput>();
            
            var expectedResponseContent = new 
            { 
                OrderCode = "202300000001",
                Total = 4872,
                Freight = 280,
            };

            using(new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseContent.Should().BeEquivalentTo(expectedResponseContent);
            }
        }
    }
}
