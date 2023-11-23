using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Repositories;

namespace ShopNow.Infra.Stock.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ShopContext _shopContext;

        public StockRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task Save(StockEntry stockEntry)
        {
            _shopContext.Add(stockEntry);
            await _shopContext.SaveChangesAsync();
        }
    }
}
