using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;

namespace ShopNow.Infra.Data.Repositories.Database
{
    public class ItemRepository : IItemRepository
    {
        private readonly ShopContext _shopContext;

        public ItemRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<Item?> FindByIdAsync(int id)
        {
            return await _shopContext.Items.FindAsync(id);
        }
    }
}
