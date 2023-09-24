using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.Infra.Data.Repositories.Memory;
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
                new Item("Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3),
                new Item("Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20),
                new Item("Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M),
            };

            _context.Items.AddRange(items);
            _context.SaveChanges();

            var placeOrderInput = new PlaceOrderInput
            {
                Cpf = "18731465072",
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput()
                    {
                        Id = 1,
                        Count = 1,
                    },
                    new OrderItemInput()
                    {
                        Id = 2,
                        Count =  2,
                    },
                    new OrderItemInput()
                    {
                        Id = 3,
                        Count = 3,
                    }
                }
            };

            var itemRepository = GetService<IItemRepository>();
            var placeOrder = new PlaceOrder(itemRepository, new OrderRepositoryMemory());
            
            var simulateFreight = new SimulateFreight(placeOrder);
            var output = await simulateFreight.Execute(placeOrderInput);

            output.Should().Be(260);
        }
    }
}
