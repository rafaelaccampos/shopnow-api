using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Repositories;

namespace ShopNow.Infra.Checkout.Data.Repositories.Memory
{
    public class ItemRepositoryMemory : IItemRepository
    {
        private readonly IList<Item> _items;

        public ItemRepositoryMemory()
        {
            _items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 30, 10, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 100, 50, 50, 20),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 0.9M)
            };
        }

        public async Task<Item?> FindByIdAsync(int id)
        {
            var item = _items.Where(item => item.Id == id).FirstOrDefault();

            if (item == null)
            {
                throw new Exception("Item not found!");
            }

            return item;
        }
    }
}
