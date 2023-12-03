using ShopNow.Domain.Shared.Event;
using ShopNow.Domain.Shared.Handler;
using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Repositories;

namespace ShopNow.Domain.Stock.Handlers
{
    public class OrderPlacedStockHandler : IHandler
    {
        private readonly IStockRepository _stockRepository;

        public OrderPlacedStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Notify(IDomainEvent domainEvent)
        {
            var orderPlaced = (OrderPlaced)domainEvent;

            foreach(var orderItem in orderPlaced.Items)
            {
                var stockEntry = new StockEntry(orderItem.Id, "in", orderItem.Count);
                await _stockRepository.Save(stockEntry);
            }
        }
    }
}
