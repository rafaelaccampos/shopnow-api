using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Domain.Checkout.Repositories
{
    public interface IItemRepository
    {
        Task<Item?> FindByIdAsync(int id);
    }
}
