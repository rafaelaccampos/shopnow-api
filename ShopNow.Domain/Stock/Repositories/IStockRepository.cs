using ShopNow.Domain.Stock.Entities;

namespace ShopNow.Domain.Stock.Repositories
{
    public interface IStockRepository
    {
        public Task Save(StockEntry stockEntry);
    }
}
