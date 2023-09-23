using ShopNow.Domain.Entities;
using ShopNow.IntegrationTests.Setup;

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
        }
    }
}
