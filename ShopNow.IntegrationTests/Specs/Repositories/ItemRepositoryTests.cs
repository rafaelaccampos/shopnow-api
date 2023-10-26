using FluentAssertions;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class ItemRepositoryTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToFindItemById()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000);

            var itemRepository = GetService<IItemRepository>();

            _context.Items.Add(item);
            _context.SaveChanges();

            var itemOfDatabase = await itemRepository.FindByIdAsync(item.Id);

            itemOfDatabase.Should().BeEquivalentTo(item);
        }
    }
}
