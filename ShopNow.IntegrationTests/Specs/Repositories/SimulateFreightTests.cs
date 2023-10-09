using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class SimulateFreightTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToSimulateFreight()
        {
            var items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 1),
            };

            _context.Items.AddRange(items);
            _context.SaveChanges();

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

            var itemRepository = GetService<IItemRepository>();
            
            var simulateFreight = new SimulateFreight(itemRepository);
            var output = await simulateFreight.Execute(simulateFreightInput);

            output.Should().Be(280);
        }
    }
}
