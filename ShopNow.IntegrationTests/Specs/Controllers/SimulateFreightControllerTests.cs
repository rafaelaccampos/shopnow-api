using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Dtos;
using ShopNow.IntegrationTests.Setup;
using ShopNow.Tests.Shared.Extensions;
using System.Net;

namespace ShopNow.IntegrationTests.Specs.Controllers
{
    public class SimulateFreightControllerTests : ApiBase
    {
        private const string URL_BASE = "/freights";

        [Test]
        public async Task GetFreightShouldBeAbleToReturnFreight()
        {
            var items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 1),
            };
            _context.AddRange(items);
            await _context.SaveChangesAsync();

            var simulateFreightInput = new SimulateFreightInput
            {
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput()
                    {
                        IdItem = 1,
                        Count = 1,
                    },
                    new OrderItemInput()
                    {
                        IdItem = 2,
                        Count =  1,
                    },
                    new OrderItemInput()
                    {
                        IdItem = 3,
                        Count = 3,
                    }
                }
            };

            var response = await _httpClient.PostAsync(URL_BASE, simulateFreightInput.ToJsonContent());
            var responseAsJson = await response.Content.ReadAsStringAsync();
            var expectedFreight = 280M.Serialize();

            using(new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseAsJson.ShouldBeAnEquivalentJson(expectedFreight);
            }
        }
    }
}
