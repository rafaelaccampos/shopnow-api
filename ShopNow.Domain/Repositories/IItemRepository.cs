using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<Item?> FindByIdAsync(int id);
    }
}
