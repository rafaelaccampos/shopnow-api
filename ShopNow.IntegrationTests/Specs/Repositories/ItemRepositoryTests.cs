using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class ItemRepositoryTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToFindItemById()
        {
            var item = new Item(1, "Guitarra", "Eletrônicos", 1000);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var itemRepository = GetService<IItemRepository>();
            var itemOfDatabase = await itemRepository.FindByIdAsync(item.Id);

            itemOfDatabase.Should().BeEquivalentTo(item);
        }
    }
}
